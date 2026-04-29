import { createAction } from '@reduxjs/toolkit';

const start = createAction('loading/start', payload => ({ payload }));

const stop = createAction('loading/stop', payload => ({ payload }));

const clear = createAction('loading/clear');

export default {
  start,
  stop,
  clear,
};
