import { forwardRef, ReactNode } from 'react';
import { Text } from 'react-native';
import i18n from 'i18n-js';

interface ValidationMessageProps {
  children?: ReactNode;
  translationParams?: any;
  forwardedRef?: any;
}

const ValidationMessage = ({
  children,
  translationParams = {},
}: ValidationMessageProps) =>
  children ? (
    <Text className="text-destructive dark:text-destructive-dark text-xs mt-1.5 ml-0.5">
      {typeof children === 'string' ? i18n.t(children, translationParams) : children}
    </Text>
  ) : null;

const Forwarded = forwardRef<any, ValidationMessageProps>((props, ref) => (
  <ValidationMessage {...props} forwardedRef={ref} />
));

export default Forwarded;
