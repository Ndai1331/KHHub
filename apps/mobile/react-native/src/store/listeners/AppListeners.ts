import { ListenerMiddlewareInstance } from '@reduxjs/toolkit';

import { getApplicationConfiguration } from '../../api/ApplicationConfigurationAPI';
import { loadTranslations } from '../../services/LocalizationService';
import AppActions from '../actions/AppActions';
import LoadingActions from '../actions/LoadingActions';
import PersistentStorageActions from '../actions/PersistentStorageActions';

export const setupAppListeners = (listenerMiddleware: ListenerMiddlewareInstance) => {
  listenerMiddleware.startListening({
    actionCreator: AppActions.fetchAppConfigAsync,
    effect: async (action, listenerApi) => {
      const { showLoading = true } = action.payload;

      if (showLoading) {
        listenerApi.dispatch(LoadingActions.start({ key: 'appConfig', opacity: 1 }));
      }

      try {
        const data = await getApplicationConfiguration();

        listenerApi.dispatch(AppActions.setAppConfig(data));

        if (showLoading) {
          listenerApi.dispatch(LoadingActions.stop({ key: 'appConfig' }));
        }
      } catch (error) {
        console.error('Failed to fetch app config:', error);
        if (showLoading) {
          listenerApi.dispatch(LoadingActions.stop({ key: 'appConfig' }));
        }
      }
    },
  });

  listenerMiddleware.startListening({
    actionCreator: AppActions.setLanguageAsync,
    effect: async (action, listenerApi) => {
      const newLocale = action.payload;

      // Update language in persistent storage
      listenerApi.dispatch(PersistentStorageActions.setLanguage(newLocale));

      // Reload translations for the new locale (client-side only)
      loadTranslations(newLocale);
    },
  });

  listenerMiddleware.startListening({
    actionCreator: AppActions.logoutAsync,
    effect: async (action, listenerApi) => {
      listenerApi.dispatch(
        PersistentStorageActions.setToken({
          token_type: '',
          access_token: '',
          refresh_token: '',
          expire_time: 0,
          scope: '',
        }),
      );

      // Trigger app config refresh without loading indicator
      listenerApi.dispatch(AppActions.fetchAppConfigAsync({ showLoading: false }));
    },
  });
};
