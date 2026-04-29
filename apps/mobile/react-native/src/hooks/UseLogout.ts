import axios from 'axios';
import { useCallback, useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigation } from '@react-navigation/native';

import { getEnvVars } from '../../Environment';
import { store } from '../store';
import AppActions from '../store/actions/AppActions';
import PersistentStorageActions from '../store/actions/PersistentStorageActions';
import { tokenSelector } from '../store/selectors/PersistentStorageSelectors';
import type { BottomTabParamList } from '../navigators/types';
import { CrossPlatformAlert } from '../utils/AlertPolyfill';

const { oAuthConfig } = getEnvVars();
const revocationEndpoint = `${oAuthConfig.issuer}/connect/revocat`;


const revokeToken = async (token: string, tokenTypeHint: 'access_token' | 'refresh_token') => {
  try {
    const data = new URLSearchParams({
      token,
      client_id: oAuthConfig.clientId,
      token_type_hint: tokenTypeHint,
    }).toString();

    await axios.post(revocationEndpoint, data, {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    });
    return true;
  } catch {
    return false;
  }
};

const fetchAndRevokeTokens = async (token: { access_token?: string; refresh_token?: string }) => {
  const revokeAccess = token.access_token
    ? revokeToken(token.access_token, 'access_token')
    : Promise.resolve(true);
  const revokeRefresh = token.refresh_token
    ? revokeToken(token.refresh_token, 'refresh_token')
    : Promise.resolve(true);

  const [isSuccess, isSuccessRefresh] = await Promise.all([revokeAccess, revokeRefresh]);
  return { isSuccess, isSuccessRefresh };
};


function handleLogoutError(error: unknown) {
  console.error('Logout failed:', error);
  CrossPlatformAlert.alert('Logout Failed', 'Logout failed. Please try again.', [{ text: 'OK' }]);
}

export function useLogout() {
  const dispatch = useDispatch();
  const navigation = useNavigation();
  const [isLoggingOut, setIsLoggingOut] = useState(false);

  const logout = useCallback(async () => {
    const token = tokenSelector(store.getState());
    if (!token) {
      return;
    }

    setIsLoggingOut(true);
    try {
      const { isSuccess, isSuccessRefresh } = await fetchAndRevokeTokens(token);

      if (isSuccess && isSuccessRefresh) {
        dispatch(AppActions.setProfilePicture(''));
        dispatch(
          PersistentStorageActions.setToken({
            token_type: '',
            access_token: '',
            refresh_token: '',
            expire_time: 0,
            scope: '',
          }),
        );
        dispatch(AppActions.fetchAppConfigAsync({ showLoading: false }));
        (navigation as { navigate: (name: keyof BottomTabParamList) => void }).navigate(
          'HomeTab',
        );
      } else {
        handleLogoutError(new Error('Token revocation failed'));
      }
    } catch (error) {
      handleLogoutError(error);
    } finally {
      setIsLoggingOut(false);
    }
  }, [dispatch, navigation]);

  return { logout, isLoggingOut };
}