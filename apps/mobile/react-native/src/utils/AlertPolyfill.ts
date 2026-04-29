import { Platform, Alert } from 'react-native';

const alertPolyfill = (title: string, description?: string, options?: any) => {
  if (typeof window === 'undefined' || !window.confirm) {
    // Fallback for environments without window.confirm
    return;
  }

  const message = [title, description].filter(Boolean).join('\n');
  const result = window.confirm(message);

  if (result) {
    const confirmOption = options?.find(({ style }) => style !== 'cancel');
    confirmOption && confirmOption.onPress?.();
  } else {
    const cancelOption = options?.find(({ style }) => style === 'cancel');
    cancelOption && cancelOption.onPress?.();
  }
};

/**
 * Cross-platform Alert utility that uses native Alert on mobile
 * and a polyfill (window.confirm) on web
 */
export const CrossPlatformAlert = {
  alert: (title: string, message?: string, buttons?: any[], options?: any) => {
    if (Platform.OS === 'web') {
      alertPolyfill(title, message, buttons);
    } else {
      Alert.alert(title, message, buttons, options);
    }
  },
};
