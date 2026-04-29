import { useColorScheme } from 'nativewind';

/**
 * Returns colors for components that can't use className (e.g., Paper TextInput).
 * For NativeWind components, use dark: variant classes directly instead.
 */
const useThemeColors = () => {
  const { colorScheme } = useColorScheme();
  const isDark = colorScheme === 'dark';

  return {
    // Paper TextInput styling
    primaryContainer: isDark ? '#18181b' : '#ffffff',
    // Navigator headers
    headerBg: isDark ? '#09090b' : '#ffffff',
    headerText: isDark ? '#fafafa' : '#09090b',
    // Icon colors
    iconColor: isDark ? '#a1a1aa' : '#71717a',
    accentColor: isDark ? '#fafafa' : '#18181b',
    // Semantic colors for non-className usage
    destructiveColor: isDark ? '#dc2626' : '#ef4444',
    inputBorderColor: isDark ? '#3f3f46' : '#d4d4d8',
  };
};

export default useThemeColors;
