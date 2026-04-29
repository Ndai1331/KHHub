import AsyncStorage from '@react-native-async-storage/async-storage';
import { configureStore, createListenerMiddleware } from '@reduxjs/toolkit';
import { persistReducer, persistStore } from 'redux-persist';
import rootReducer from './reducers';
import { setupAppListeners } from './listeners';

const listenerMiddleware = createListenerMiddleware();

const persistConfig = {
  key: 'root',
  storage: AsyncStorage,
  whitelist: ['persistentStorage'],
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: ['persist/PERSIST', 'persist/REHYDRATE'],
      },
    }).prepend(listenerMiddleware.middleware),
});

export const persistor = persistStore(store);

setupAppListeners(listenerMiddleware);
