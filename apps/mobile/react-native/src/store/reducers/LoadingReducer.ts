import { createReducer } from '@reduxjs/toolkit';
import LoadingActions from '../actions/LoadingActions';

const initialState: {
  activeLoadings: Record<string, unknown>;
  loading: boolean;
  opacity?: number;
} = { activeLoadings: {}, loading: false };

export default createReducer(initialState, builder =>
  builder
    .addCase(LoadingActions.start, (state, action) => {
      const { key, opacity } = (action.payload as { key: string; opacity: number }) || {};

      if (!key) return;

      state.activeLoadings[key] = action.payload;
      state.loading = true;
      if (typeof opacity === 'number') state.opacity = opacity;
    })
    .addCase(LoadingActions.stop, (state, action) => {
      const { key } = (action.payload as { key: string }) || {};

      if (key) {
        delete state.activeLoadings[key];
      }

      state.loading = Object.keys(state.activeLoadings).length > 0;
    })
    .addCase(LoadingActions.clear, state => {
      state.activeLoadings = {};
      state.loading = false;
      state.opacity = undefined;
    }),
);
