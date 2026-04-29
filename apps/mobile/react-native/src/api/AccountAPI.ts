import api from './API';
import { getEnvVars } from '../../Environment';

const { oAuthConfig, appName } = getEnvVars();
const tokenUrl = `${oAuthConfig.issuer}/connect/token`;

export const login = ({ username, password }: { username: string; password: string }) => {
  const params = new URLSearchParams({
    grant_type: 'password',
    scope: oAuthConfig.scope,
    username,
    password,
    client_id: oAuthConfig.clientId,
  });
  if (oAuthConfig.clientSecret) params.append('client_secret', oAuthConfig.clientSecret);

  const data = params.toString();
  return api
    .post(tokenUrl, data, {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    })
    .then(({ data }) => data);
};

export const Logout = () =>
  api({
    method: 'GET',
    url: '/api/account/logout',
  }).then(({ data }) => data);

export const profilePictureById = (id: string) =>
  api.get(`/api/account/profile-picture/${id}`).then(({ data }) => data);

export const postProfilePicture = (body: FormData) =>
  api
    .post('/api/account/profile-picture', body, {
      headers: { 'Content-Type': 'multipart/form-data' },
    })
    .then(({ data }) => data);

export const refreshToken = (refreshToken: string) => {
  const params = new URLSearchParams({
    grant_type: 'refresh_token',
    refresh_token: refreshToken,
    client_id: oAuthConfig.clientId,
  });
  if (oAuthConfig.clientSecret) params.append('client_secret', oAuthConfig.clientSecret);

  const data = params.toString();
  return api
    .post(tokenUrl, data, {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    })
    .then(({ data }) => data);
};

export const register = (body: {
  userName: string;
  emailAddress: string;
  password: string;
  appName?: string;
}) => api.post('/api/account/register', { appName, ...body }).then(({ data }) => data);

export const sendPasswordResetCode = (body: {
  email: string;
  returnUrl?: string;
  returnUrlHash?: string;
  appName?: string;
}) =>
  api
    .post('/api/account/send-password-reset-code', { appName, ...body })
    .then(({ data }) => data);

export const resetPassword = (body: {
  userId: string;
  resetToken: string;
  password: string;
  returnUrl?: string;
  returnUrlHash?: string;
}) => api.post('/api/account/reset-password', body).then(({ data }) => data);
