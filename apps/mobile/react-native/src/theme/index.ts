import { MD3LightTheme, MD3DarkTheme } from 'react-native-paper';

import { lightColors, darkColors } from './colors';

// Paper themes - only used for TextInput component
export const lightTheme = {
  ...MD3LightTheme,
  roundness: 8,
  colors: {
    ...MD3LightTheme.colors,
    ...lightColors,
  },
};

export const darkTheme = {
  ...MD3DarkTheme,
  roundness: 8,
  colors: {
    ...MD3DarkTheme.colors,
    ...darkColors,
  },
};
