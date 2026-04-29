import { forwardRef, useContext } from 'react';
import { View, Text, Pressable } from 'react-native';
import { LocalizationContext } from '../../contexts/LocalizationContext';

interface FormButtonsProps {
  submit: () => void;
  cancel?: () => void;
  style?: any;
  isSubmitDisabled?: boolean;
  isShowSubmit?: boolean;
  forwardedRef?: any;
}

function FormButtons({
  submit,
  cancel,
  isSubmitDisabled = false,
  isShowSubmit = true,
}: FormButtonsProps) {
  const { t } = useContext(LocalizationContext);

  return (
    <View className="mb-4">
      {isShowSubmit ? (
        <Pressable
          onPress={submit}
          disabled={isSubmitDisabled}
          className={`rounded-lg py-3.5 items-center shadow-sm ${
            isSubmitDisabled
              ? 'bg-muted dark:bg-muted-dark'
              : 'bg-accent dark:bg-accent-dark active:opacity-90'
          }`}>
          <Text
            className={`font-semibold text-[15px] ${
              isSubmitDisabled
                ? 'text-muted-foreground dark:text-muted-dark-foreground'
                : 'text-accent-foreground dark:text-accent-dark-foreground'
            }`}>
            {t('AbpIdentity::Save')}
          </Text>
        </Pressable>
      ) : null}
      {cancel ? (
        <Pressable
          onPress={cancel}
          className="mt-3 rounded-lg py-3.5 items-center border border-border dark:border-border-dark active:bg-muted dark:active:bg-muted-dark">
          <Text className="text-foreground dark:text-foreground-dark font-medium text-[15px]">
            {t('AbpUi::Cancel')}
          </Text>
        </Pressable>
      ) : null}
    </View>
  );
}

const Forwarded = forwardRef<any, FormButtonsProps>((props, ref) => (
  <FormButtons {...props} forwardedRef={ref} />
));

export default Forwarded;
