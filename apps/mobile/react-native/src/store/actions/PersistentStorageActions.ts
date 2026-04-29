import { createAction } from '@reduxjs/toolkit';
import { Token } from '../../types/auth';

const setToken = createAction<Token>('persistentStorage/setToken');

const setLanguage = createAction<string>('persistentStorage/setLanguage');

export default {
  setToken,
  setLanguage,
};
