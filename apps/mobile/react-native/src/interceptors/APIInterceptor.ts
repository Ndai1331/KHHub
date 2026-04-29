import i18n from 'i18n-js';
import Toast from 'react-native-root-toast';

import api from '../api/API';
import { refreshToken } from '../api/AccountAPI';
import LoadingActions from '../store/actions/LoadingActions';
import AppActions from '../store/actions/AppActions';
import PersistentStorageActions from '../store/actions/PersistentStorageActions';
import { isTokenExpiringSoon } from '../utils/TokenUtils';
import { Token } from '../types/auth';

type QueuedRequest = {
  resolve: (token: Token | null) => void;
  reject: (err: unknown) => void;
};

// Flag to prevent concurrent refresh requests
let isRefreshing = false;
let failedQueue: QueuedRequest[] = [];

const processQueue = (error: unknown, token: Token | null = null) => {
  failedQueue.forEach(prom => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });

  failedQueue = [];
};

const waitForRefresh = () =>
  new Promise<Token | null>((resolve, reject) => {
    failedQueue.push({ resolve, reject });
  });

function isAbpErrorResponse(errorRes: any): boolean {
  const headers = (errorRes?.headers ?? {}) as Record<string, unknown>;
  const abpHeader =
    (headers as any)._abperrorformat ??
    headers.abperrorformat ??
    headers['x-abp-error-format'] ??
    headers['x-abp-errorformat'];

  if (abpHeader) return true;

  const data = errorRes?.data;
  if (!data || typeof data !== 'object') return false;

  const err = (data as any).error;
  if (!err || typeof err !== 'object') return false;

  return (
    typeof err.message === 'string' ||
    typeof err.details === 'string' ||
    typeof err.code === 'string' ||
    (data as any).unAuthorizedRequest === true
  );
}

export function initAPIInterceptor(store) {
  api.interceptors.request.use(
    async request => {
      const {
        persistentStorage: { token, language, tenant },
      } = store.getState();

      // Skip token refresh for token endpoint requests to avoid circular calls
      const isTokenRequest = request.url?.includes('/connect/token');

      // Proactive token refresh: Check if token expires soon before making request
      if (!isTokenRequest && token && token.access_token && token.refresh_token) {
        if (isTokenExpiringSoon(token)) {
          // Token expires soon, refresh it before making the request
          if (!isRefreshing) {
            isRefreshing = true;
            try {
              const tokenData = await refreshToken(token.refresh_token);
              const expiresInMs = tokenData.expires_in ? tokenData.expires_in * 1000 : 0;
              const newToken = {
                token_type: tokenData.token_type || token.token_type,
                access_token: tokenData.access_token,
                refresh_token: tokenData.refresh_token || token.refresh_token,
                expire_time: new Date().valueOf() + expiresInMs,
                scope: tokenData.scope || token.scope,
              };
              store.dispatch(PersistentStorageActions.setToken(newToken));
              processQueue(null, newToken);
              isRefreshing = false;

              // Update the request with new token
              request.headers.Authorization = `${newToken.token_type} ${newToken.access_token}`;
            } catch (refreshError) {
              processQueue(refreshError, null);
              isRefreshing = false;
              // If refresh fails, clear token and let the request proceed (will fail with 401)
              store.dispatch(PersistentStorageActions.setToken());
            }
          } else {
            // Already refreshing, wait for it to complete
            return waitForRefresh()
              .then(newToken => {
                if (newToken) {
                  request.headers.Authorization = `${newToken.token_type} ${newToken.access_token}`;
                }
                return request;
              })
              .catch(() => request);
          }
        } else if (token && token.access_token) {
          // Token is still valid, use it
          request.headers.Authorization = `${token.token_type} ${token.access_token}`;
        }
      } else if (!request.headers.Authorization && token && token.access_token) {
        request.headers.Authorization = `${token.token_type} ${token.access_token}`;
      }

      if (!request.headers['Content-Type']) {
        request.headers['Content-Type'] = 'application/json';
      }

      if (!request.headers['Accept-Language'] && language) {
        request.headers['Accept-Language'] = language;
      }

      if (!request.headers.__tenant && tenant && tenant.tenantId) {
        request.headers.__tenant = tenant.tenantId;
      }

      return request;
    },
    error => console.error(error),
  );

  api.interceptors.response.use(
    response => response,
    async error => {
      store.dispatch(LoadingActions.clear());
      const errorRes = error.response;
      const originalRequest = error.config;

      if (errorRes) {
        // Skip token refresh logic for token endpoint requests to avoid circular calls
        const isTokenRequest = originalRequest?.url?.includes('/connect/token');

        // Handle 401 Unauthorized errors
        if (errorRes.status === 401 && !isTokenRequest) {
          const {
            persistentStorage: { token },
          } = store.getState();

          // Check if we have a refresh token and haven't already retried this request
          // Only attempt refresh if we have refresh_token and this looks like an ABP error response
          const isAbpError = isAbpErrorResponse(errorRes);

          if (isAbpError && token && token.refresh_token && !originalRequest._retry) {
            originalRequest._retry = true;

            // If already refreshing, wait for it to complete
            if (isRefreshing) {
              return waitForRefresh()
                .then(newToken => {
                  if (newToken) {
                    originalRequest.headers.Authorization = `${newToken.token_type} ${newToken.access_token}`;
                    return api(originalRequest);
                  }
                  // Refresh failed, trigger logout
                  store.dispatch(AppActions.logoutAsync());
                  return Promise.reject(error);
                })
                .catch(() => {
                  store.dispatch(AppActions.logoutAsync());
                  return Promise.reject(error);
                });
            }

            // Attempt to refresh the token
            isRefreshing = true;
            try {
              const tokenData = await refreshToken(token.refresh_token);
              const expiresInMs = tokenData.expires_in ? tokenData.expires_in * 1000 : 0;
              const newToken = {
                token_type: tokenData.token_type || token.token_type,
                access_token: tokenData.access_token,
                refresh_token: tokenData.refresh_token || token.refresh_token,
                expire_time: new Date().valueOf() + expiresInMs,
                scope: tokenData.scope || token.scope,
              };
              store.dispatch(PersistentStorageActions.setToken(newToken));
              processQueue(null, newToken);
              isRefreshing = false;

              // Retry the original request with the new token
              originalRequest.headers.Authorization = `${newToken.token_type} ${newToken.access_token}`;
              return api(originalRequest);
            } catch (refreshError) {
              processQueue(refreshError, null);
              isRefreshing = false;
              // Refresh failed, trigger logout
              console.log('Token refresh failed, logging out:', refreshError);
              store.dispatch(AppActions.logoutAsync());
              return Promise.reject(error);
            }
          } else {
            // No refresh token, already retried, or not an ABP error - trigger logout
            console.log('401 error - no refresh possible, logging out');
            store.dispatch(AppActions.logoutAsync());
          }
        } else {
          // Non-401 error, show error message
          const error: HttpErr = errorRes.data?.error || {};
          const status: number = errorRes.status;
          const err = { error, status };
          showError(err);
        }
      } else {
        // Network error or no response
        Toast.show('An unexpected error has occurred', { duration: 10000 });
      }

      return Promise.reject(error);
    },
  );
}

type HttpErr = { message: string; details: string };
function showError({ error, status }) {
  let message = '';
  let title = i18n.t('AbpAccount::DefaultErrorMessage');

  if (typeof error === 'string') {
    message = error;
  } else if (error.details) {
    message = error.details;
    title = error.message;
  } else if (error.message) {
    message = error.message;
  } else {
    switch (status) {
      case 401:
        title = i18n.t('AbpAccount::DefaultErrorMessage401');
        message = i18n.t('AbpAccount::DefaultErrorMessage401Detail');
        break;
      case 403:
        title = i18n.t('AbpAccount::DefaultErrorMessage403');
        message = i18n.t('AbpAccount::DefaultErrorMessage403Detail');
        break;
      case 404:
        title = i18n.t('AbpAccount::DefaultErrorMessage404');
        message = i18n.t('AbpAccount::DefaultErrorMessage404Detail');
        break;
      case 500:
        title = i18n.t('AbpAccount::500Message');
        message = i18n.t('AbpAccount::InternalServerErrorMessage');
        break;
      default:
        break;
    }
  }
  Toast.show(`${title}\n${message}`, { duration: 10000 });
}
