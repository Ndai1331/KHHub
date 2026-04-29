import { createAction } from '@reduxjs/toolkit';

const fetchAppConfigAsync = createAction(
  'app/fetchAppConfigAsync',
  ({ showLoading = true } = {}) => ({ payload: { showLoading } }),
);

const setAppConfig = createAction('app/setAppConfig');

const setLanguageAsync = createAction('app/setLanguageAsync', payload => ({ payload }));

const setThemeAsync = createAction<string>('app/setThemeAsync');

const setProfilePicture = createAction<string>('app/setProfilePicture');

const logoutAsync = createAction('app/logoutAsync');

export default {
  fetchAppConfigAsync,
  setAppConfig,
  setLanguageAsync,
  setThemeAsync,
  setProfilePicture,
  logoutAsync,
};
