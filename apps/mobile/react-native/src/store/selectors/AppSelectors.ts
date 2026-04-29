import { createSelector } from '@reduxjs/toolkit';
import { AVAILABLE_LANGUAGES } from '../../services/LocalizationService';

const getApp = state => state.app;
const getPersistentStorage = state => state.persistentStorage;

export const appConfigSelector = createSelector([getApp], state => state.appConfig);

export const languageSelector = createSelector([getPersistentStorage], persistentStorage => {
  const storedLanguage = persistentStorage?.language;
  return (
    AVAILABLE_LANGUAGES.find(lang => lang.cultureName === storedLanguage) || AVAILABLE_LANGUAGES[0]
  );
});

export function createLanguagesSelector() {
  return AVAILABLE_LANGUAGES;
}

export const themeSelector = createSelector([getApp], state => state?.theme ?? 'System');

export const profilePictureSelector = createSelector([getApp], state => state?.profilePicture);
