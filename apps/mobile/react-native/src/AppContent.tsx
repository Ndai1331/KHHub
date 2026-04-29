import i18n from 'i18n-js';
import { useEffect, useMemo } from 'react';
import { useSelector } from 'react-redux';
import { RootSiblingParent } from 'react-native-root-siblings';
import { ActionSheetProvider } from '@expo/react-native-action-sheet';

import { store } from './store';
import { LocalizationContext } from './contexts/LocalizationContext';
import { loadTranslations } from './services/LocalizationService';
import { languageSelector } from './store/selectors/AppSelectors';
import AppActions from './store/actions/AppActions';
import Loading from './components/Loading/Loading';

import AppContainer from './AppContainer';

export default function AppContent() {
  const language = useSelector(languageSelector);
  const currentLocale = language?.cultureName || i18n.locale || 'en';

  useEffect(() => {
    store.dispatch(AppActions.fetchAppConfigAsync({ showLoading: false }));
  }, []);

  useEffect(() => {
    if (currentLocale && i18n.locale !== currentLocale) {
      loadTranslations(currentLocale);
    }
  }, [currentLocale]);

  const localizationContextValue = useMemo(
    () => ({ t: i18n.t, locale: currentLocale }),
    [currentLocale],
  );

  return (
    <>
      <LocalizationContext.Provider value={localizationContextValue}>
        <ActionSheetProvider>
          <RootSiblingParent>
            <AppContainer />
          </RootSiblingParent>
        </ActionSheetProvider>
      </LocalizationContext.Provider>
      <Loading />
    </>
  );
}
