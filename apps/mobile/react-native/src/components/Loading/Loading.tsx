import { forwardRef } from 'react';
import { View, ActivityIndicator } from 'react-native';
import { useSelector } from 'react-redux';
import { loadingSelector, opacitySelector } from '../../store/selectors/LoadingSelectors';
import { useThemeColors } from '../../hooks';

interface LoadingProps {
  forwardedRef?: React.Ref<unknown>;
}

function Loading(_props: LoadingProps) {
  const loading = useSelector(loadingSelector);
  const opacity = useSelector(opacitySelector);
  const { accentColor } = useThemeColors();

  return loading ? (
    <View className="absolute inset-0 items-center justify-center z-50">
      <View
        className="absolute inset-0 bg-background dark:bg-background-dark"
        style={{ opacity: opacity || 0.7 }}
      />
      <ActivityIndicator size="large" color={accentColor} />
    </View>
  ) : null;
}

const Forwarded = forwardRef<any, LoadingProps>((props, ref) => (
  <Loading {...props} forwardedRef={ref} />
));

export default Forwarded;
