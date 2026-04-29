import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { useContext } from 'react';
import { useThemeColors } from '../hooks';
import { SettingsScreen } from '../screens';
import { LocalizationContext } from '../contexts/LocalizationContext';

const Stack = createNativeStackNavigator();

export default function SettingsStackNavigator() {
  const { headerBg, headerText } = useThemeColors();
  const { t } = useContext(LocalizationContext);

  return (
    <Stack.Navigator id="SettingsStack" initialRouteName="Settings">
      <Stack.Screen
        name="Settings"
        component={SettingsScreen}
        options={{
          title: t('AbpSettingManagement::Settings'),
          headerStyle: { backgroundColor: headerBg },
          headerTintColor: headerText,
          headerShadowVisible: false,
        }}
      />
    </Stack.Navigator>
  );
}
