import { useContext } from 'react';
import { View, Text, Image, ScrollView } from 'react-native';
import { useSelector } from 'react-redux';
import { Ionicons } from '@expo/vector-icons';

import { useColorScheme } from 'nativewind';

import { appConfigSelector } from '../../store/selectors/AppSelectors';
import { useThemeColors } from '../../hooks';
import { LocalizationContext } from '../../contexts/LocalizationContext';
import type { HomeScreenProps } from '../../navigators/types';

const logoDark = require('../../../assets/logo.png');
const logoLight = require('../../../assets/logo-light.png');

const features = [
  { icon: 'shield-checkmark-outline' as const, titleKey: '::Feature:Secure', descKey: '::Feature:SecureDesc' },
  { icon: 'flash-outline' as const, titleKey: '::Feature:Fast', descKey: '::Feature:FastDesc' },
  { icon: 'phone-portrait-outline' as const, titleKey: '::Feature:Modern', descKey: '::Feature:ModernDesc' },
];

function HomeScreen({ navigation }: HomeScreenProps) {
  const { t } = useContext(LocalizationContext);
  const { accentColor } = useThemeColors();
  const { colorScheme } = useColorScheme();
  const currentUser = useSelector(appConfigSelector)?.currentUser;
  const logo = colorScheme === 'dark' ? logoLight : logoDark;

  const greeting = currentUser?.isAuthenticated && currentUser?.name
    ? `${t('::Welcome')}, ${currentUser.name}!`
    : `${t('::Welcome')}!`;

  return (
    <ScrollView
      className="flex-1 bg-background dark:bg-background-dark"
      contentContainerStyle={{ paddingBottom: 32 }}
      showsVerticalScrollIndicator={false}>

      {/* Hero Section */}
      <View className="items-center pt-10 px-6">
        <Image
          source={logo}
          className="h-10 mb-6"
          resizeMode="contain"
        />

        <Text className="text-3xl font-bold text-foreground dark:text-foreground-dark tracking-tight text-center">
          {greeting}
        </Text>
        <Text className="text-[15px] text-muted-foreground dark:text-muted-dark-foreground mt-2 text-center leading-relaxed px-4">
          {t('::LongWelcomeMessage')}
        </Text>
      </View>

      {/* Feature Cards */}
      <View className="px-6 mt-10">
        <View className="flex-row gap-3">
          {features.slice(0, 2).map((feature, index) => (
            <View
              key={index}
              className="flex-1 bg-card dark:bg-card-dark rounded-xl border border-card-border dark:border-card-border-dark shadow-sm p-4">
              <View className="w-10 h-10 rounded-lg bg-secondary dark:bg-secondary-dark items-center justify-center mb-3">
                <Ionicons name={feature.icon} size={20} color={accentColor} />
              </View>
              <Text className="text-sm font-semibold text-foreground dark:text-foreground-dark">
                {t(feature.titleKey)}
              </Text>
              <Text className="text-xs text-muted-foreground dark:text-muted-dark-foreground mt-1 leading-relaxed">
                {t(feature.descKey)}
              </Text>
            </View>
          ))}
        </View>

        <View className="mt-3">
          <View className="bg-card dark:bg-card-dark rounded-xl border border-card-border dark:border-card-border-dark shadow-sm p-4 flex-row items-center">
            <View className="w-10 h-10 rounded-lg bg-secondary dark:bg-secondary-dark items-center justify-center mr-4">
              <Ionicons name={features[2].icon} size={20} color={accentColor} />
            </View>
            <View className="flex-1">
              <Text className="text-sm font-semibold text-foreground dark:text-foreground-dark">
                {t(features[2].titleKey)}
              </Text>
              <Text className="text-xs text-muted-foreground dark:text-muted-dark-foreground mt-1 leading-relaxed">
                {t(features[2].descKey)}
              </Text>
            </View>
          </View>
        </View>
      </View>
    </ScrollView>
  );
}

export default HomeScreen;
