import { useContext } from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import { useThemeColors } from '../hooks';
import { LocalizationContext } from '../contexts/LocalizationContext';
import { HomeScreen } from '../screens';

const Stack = createNativeStackNavigator();

export default function HomeStackNavigator() {
  const { headerBg, headerText } = useThemeColors();
  const { t } = useContext(LocalizationContext);

  return (
    <Stack.Navigator id="HomeStack" initialRouteName="Home">
      <Stack.Screen
        name="Home"
        component={HomeScreen}
        options={{
          title: t('::Menu:Home'),
          headerStyle: { backgroundColor: headerBg },
          headerTintColor: headerText,
          headerShadowVisible: false,
        }}
      />
    </Stack.Navigator>
  );
}
