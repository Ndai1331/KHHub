import { createSelector } from '@reduxjs/toolkit';

const getLoading = state => state.loading;

export const loadingSelector = createSelector([getLoading], loading => loading.loading);

export const opacitySelector = createSelector([getLoading], loading => loading.opacity);
