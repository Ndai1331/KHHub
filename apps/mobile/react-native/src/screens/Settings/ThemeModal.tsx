import { useEffect, useState, useContext } from 'react';
import { View, Text, Pressable, Modal } from 'react-native';

import { useThemeColors } from '../../hooks';
import { LocalizationContext } from '../../contexts/LocalizationContext';

function ThemeModal({ visible, hideModal, inputTheme, outSetTheme }) {
  const [theme, setTheme] = useState('System');
  const { accentColor } = useThemeColors();
  const { t } = useContext(LocalizationContext);

  const _setTheme = (newValue: string) => {
    setTheme(newValue);
    outSetTheme(newValue);
  };

  useEffect(() => setTheme(inputTheme), [inputTheme]);

  const options = [
    { value: 'System', label: t('AbpUi::System') },
    { value: 'Light', label: t('AbpUi::Light') },
    { value: 'Dark', label: t('AbpUi::Dark') },
  ];

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
            {t('AbpUi::Theme')}
          </Text>

          <View className="h-px bg-border dark:bg-border-dark my-4" />

          {options.map(option => (
            <Pressable
              key={option.value}
              onPress={() => _setTheme(option.value)}
              className="flex-row items-center py-3 active:bg-muted dark:active:bg-muted-dark rounded-lg px-1">
              <View
                className={`w-[18px] h-[18px] rounded-full border-2 items-center justify-center ${
                  theme === option.value
                    ? 'border-accent dark:border-accent-dark'
                    : 'border-input dark:border-input-dark'
                }`}>
                {theme === option.value && (
                  <View className="w-[10px] h-[10px] rounded-full bg-accent dark:bg-accent-dark" />
                )}
              </View>
              <Text className="ml-3 text-[15px] text-foreground dark:text-foreground-dark">
                {option.label}
              </Text>
            </Pressable>
          ))}
        </Pressable>
      </Pressable>
    </Modal>
  );
}

export default ThemeModal;
