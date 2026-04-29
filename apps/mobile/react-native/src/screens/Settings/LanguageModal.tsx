import { useEffect, useState, useContext } from 'react';
import { View, Text, Pressable, Modal, ScrollView } from 'react-native';

import { useThemeColors } from '../../hooks';
import { LocalizationContext } from '../../contexts/LocalizationContext';

function LanguageModal({ visible, languages, hideModal, inputLang, setLanguage }) {
  const [value, setValue] = useState('en');
  const { accentColor } = useThemeColors();
  const { t } = useContext(LocalizationContext);

  useEffect(() => setValue(inputLang), [inputLang]);

  const _setLanguage = (selectedLang: string) => {
    setValue(selectedLang);
    setLanguage(selectedLang);
  };

  return (
    <Modal
      visible={visible}
      transparent
      animationType="fade"
      onRequestClose={hideModal}>
      <Pressable
        onPress={hideModal}
        className="flex-1 justify-center items-center bg-black/60">
        <Pressable
          onPress={() => {}}
          className="w-[85%] rounded-2xl p-6 bg-card dark:bg-card-dark border border-card-border dark:border-card-border-dark shadow-xl">
          <Text className="text-lg font-semibold text-foreground dark:text-foreground-dark">
            {t('AbpUi::Language')}
          </Text>

          <View className="h-px bg-border dark:bg-border-dark my-4" />

          <ScrollView style={{ maxHeight: 400 }} keyboardShouldPersistTaps="handled">
            {languages.map((language: any) => (
              <Pressable
                key={language.cultureName}
                onPress={() => _setLanguage(language.cultureName)}
                className="flex-row items-center py-3 active:bg-muted dark:active:bg-muted-dark rounded-lg px-1">
                <View
                  className={`w-[18px] h-[18px] rounded-full border-2 items-center justify-center ${
                    value === language.cultureName
                      ? 'border-accent dark:border-accent-dark'
                      : 'border-input dark:border-input-dark'
                  }`}>
                  {value === language.cultureName && (
                    <View className="w-[10px] h-[10px] rounded-full bg-accent dark:bg-accent-dark" />
                  )}
                </View>
                <Text className="ml-3 text-[15px] text-foreground dark:text-foreground-dark">
                  {language.displayName}
                </Text>
              </Pressable>
            ))}
          </ScrollView>
        </Pressable>
      </Pressable>
    </Modal>
  );
}

export default LanguageModal;
