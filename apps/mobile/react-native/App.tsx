import './global.css';

import * as Linking from 'expo-linking';
import { enableScreens } from 'react-native-screens';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import { NavigationContainer } from '@react-navigation/native';
import { SafeAreaProvider } from 'react-native-safe-area-context';

import '@expo/metro-runtime';

import { initAPIInterceptor } from './src/interceptors/APIInterceptor';
import { loadTranslations, initializeI18n } from './src/services/LocalizationService';
import { persistor, store } from './src/store';
import AppContent from './src/AppContent';

initializeI18n();
enableScreens();
initAPIInterceptor(store);

function initializeLocalization() {
  try {
    const state = store.getState();
    const storedLanguage = state?.persistentStorage?.language || 'en';
    loadTranslations(storedLanguage);
  } catch {
    loadTranslations('en');
  }
}

initializeLocalization();

export default function App() {
  const linking: any = {
    prefixes: [Linking.createURL('/'), 'khhub://'],
    config: {
      screens: {
        HomeStack: {
          screens: {
            Login: 'login',
            Register: 'register',
            ForgotPassword: 'forgot-password',
            ResetPassword: 'reset-password',
          },
        },
      },
    },
  };

  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <SafeAreaProvider>
          <NavigationContainer linking={linking}>
            <AppContent />
          </NavigationContainer>
        </SafeAreaProvider>
      </PersistGate>
    </Provider>
  );
}
