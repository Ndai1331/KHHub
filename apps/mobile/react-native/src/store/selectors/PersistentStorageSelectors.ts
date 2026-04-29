import { createSelector } from '@reduxjs/toolkit';

const getPersistentStorage = state => state.persistentStorage;

export const tokenSelector = createSelector(
  [getPersistentStorage],
  persistentStorage => persistentStorage.token,
);
