import { useEffect, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useColorScheme as useRNColorScheme } from 'react-native';
import { useColorScheme } from 'nativewind';
import { PaperProvider } from 'react-native-paper';

import BottomTabNavigator from './navigators/BottomTabNavigator';
import PersistentStorageActions from './store/actions/PersistentStorageActions';
import { themeSelector } from './store/selectors/AppSelectors';
import { tokenSelector } from './store/selectors/PersistentStorageSelectors';
import { lightTheme, darkTheme } from './theme';
import { isTokenValid } from './utils/TokenUtils';

export default function AppContainer() {
  const token = useSelector(tokenSelector);
  const theme = useSelector(themeSelector);
  const dispatch = useDispatch();
  const systemColorScheme = useRNColorScheme();
  const { setColorScheme } = useColorScheme();

  const isValid = useMemo(() => isTokenValid(token), [token]);

  // Sync Redux theme state with NativeWind
  useEffect(() => {
    if (theme === 'System') {
      setColorScheme('system');
    } else {
      setColorScheme(theme === 'Dark' ? 'dark' : 'light');
    }
  }, [theme, setColorScheme]);

  // Paper theme (only for TextInput)
  const paperTheme = useMemo(() => {
    const resolvedTheme =
      theme === 'System' ? (systemColorScheme === 'dark' ? 'Dark' : 'Light') : theme;
    return resolvedTheme === 'Dark' ? darkTheme : lightTheme;
  }, [theme, systemColorScheme]);

  useEffect(() => {
    if (!isValid && token && token.access_token) {
      dispatch(
        PersistentStorageActions.setToken({
          token_type: '',
          access_token: '',
          refresh_token: '',
          expire_time: 0,
          scope: '',
        }),
      );
    }
  }, [isValid, token, dispatch]);

  return (
    <PaperProvider theme={paperTheme}>
      <BottomTabNavigator />
    </PaperProvider>
  );
}
