import { createReducer } from '@reduxjs/toolkit';
import PersistentStorageActions from '../actions/PersistentStorageActions';

const initialState = {
  token: {
    token_type: '',
    access_token: '',
    refresh_token: '',
    expire_time: 0,
    scope: '',
  },
  language: null,
  tenant: {},
};

export default createReducer(initialState, builder =>
  builder
    .addCase(PersistentStorageActions.setToken, (state, action) => {
      state.token = action.payload;
    })
    .addCase(PersistentStorageActions.setLanguage, (state, action) => {
      state.language = action.payload;
    }),
);
