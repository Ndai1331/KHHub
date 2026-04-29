import { useState, useContext } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { View, Text, Pressable } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

import type { SettingsScreenProps } from '../../navigators/types';
import { useThemeColors } from '../../hooks';
import { LocalizationContext } from '../../contexts/LocalizationContext';
import AppActions from '../../store/actions/AppActions';
import {
  languageSelector,
  createLanguagesSelector,
  themeSelector,
} from '../../store/selectors/AppSelectors';

import ThemeModal from './ThemeModal';
import LanguageModal from './LanguageModal';

function SettingsScreen(_props: SettingsScreenProps) {
  const dispatch = useDispatch();
  const { t } = useContext(LocalizationContext);
  const { accentColor } = useThemeColors();
  const theme = useSelector(themeSelector);
  const language = useSelector(languageSelector);
  const languages = createLanguagesSelector();

  const [themeVisible, setThemeVisible] = useState(false);
  const [languageVisible, setLanguageVisible] = useState(false);

  const setCurrTheme = (value: string) => {
    dispatch(AppActions.setThemeAsync(value));
    setThemeVisible(false);
  };

  const setLang = (selectedLang: string) => {
    dispatch(AppActions.setLanguageAsync(selectedLang));
    setLanguageVisible(!languageVisible);
  };

  return (
    <View className="flex-1 bg-background dark:bg-background-dark">
      <ThemeModal
        visible={themeVisible}
        hideModal={() => setThemeVisible(false)}
        outSetTheme={setCurrTheme}
        inputTheme={theme}
      />

      <LanguageModal
        visible={languageVisible}
        hideModal={() => setLanguageVisible(!languageVisible)}
        languages={languages}
        inputLang={language.cultureName}
        setLanguage={(selected: string) => setLang(selected)}
      />

      {/* General Settings Card */}
      <View className="mx-4 mt-5 bg-card dark:bg-card-dark rounded-xl border border-card-border dark:border-card-border-dark shadow-sm overflow-hidden">
        {/* Language */}
        <Pressable
          onPress={() => setLanguageVisible(true)}
          className="flex-row items-center px-4 py-3.5 active:bg-muted dark:active:bg-muted-dark">
          <View className="w-8 h-8 rounded-lg bg-secondary dark:bg-secondary-dark items-center justify-center">
            <Ionicons name="earth" size={17} color={accentColor} />
          </View>
          <View className="ml-3 flex-1">
            <Text className="text-[15px] text-foreground dark:text-foreground-dark">
              {t('AbpUi::Language')}
            </Text>
            <Text className="text-xs text-muted-foreground dark:text-muted-dark-foreground mt-0.5">
              {language.displayName}
            </Text>
          </View>
        </Pressable>

        <View className="h-px bg-border dark:bg-border-dark ml-[60px]" />

        {/* Theme */}
        <Pressable
          onPress={() => setThemeVisible(true)}
          className="flex-row items-center px-4 py-3.5 active:bg-muted dark:active:bg-muted-dark">
          <View className="w-8 h-8 rounded-lg bg-secondary dark:bg-secondary-dark items-center justify-center">
            <Ionicons name="contrast" size={17} color={accentColor} />
          </View>
          <View className="ml-3 flex-1">
            <Text className="text-[15px] text-foreground dark:text-foreground-dark">
              {t('AbpUi::Theme')}
            </Text>
            <Text className="text-xs text-muted-foreground dark:text-muted-dark-foreground mt-0.5">
              {t(`AbpUi::${theme}`)}
            </Text>
          </View>
        </Pressable>
      </View>

    </View>
  );
}

export default SettingsScreen;
