import { useContext, useEffect, useMemo, useState } from 'react';
import { View, Text, Pressable, ScrollView } from 'react-native';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { TextInput } from 'react-native-paper';

import { LocalizationContext } from '../../contexts/LocalizationContext';
import { useThemeColors } from '../../hooks';
import { ValidationMessage } from '../../components';
import { resetPassword } from '../../api/AccountAPI';
import { getEnvVars } from '../../../Environment';
import type { ResetPasswordScreenProps } from '../../navigators/types';


const validationSchema = Yup.object().shape({
  userId: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  resetToken: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  password: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  passwordConfirm: Yup.string()
    .required('AbpAccount::ThisFieldIsRequired')
    .oneOf([Yup.ref('password'), null], 'AbpIdentity::PasswordConfirmationFailed'),
});

function ResetPasswordScreen({ navigation, route }: ResetPasswordScreenProps) {
  const { t } = useContext(LocalizationContext);
  const { primaryContainer } = useThemeColors();
  const { appUrl } = getEnvVars();

  const [hidePassword, setHidePassword] = useState(true);
  const [hidePasswordConfirm, setHidePasswordConfirm] = useState(true);

  const deepLinkParams = useMemo(() => {
    const params = (route?.params ?? {}) as { userId?: string; resetToken?: string };
    return { userId: params.userId ?? '', resetToken: params.resetToken ?? '' };
  }, [route?.params]);

  const formik = useFormik({
    initialValues: {
      userId: deepLinkParams.userId,
      resetToken: deepLinkParams.resetToken,
      password: '',
      passwordConfirm: '',
    },
    validationSchema,
    enableReinitialize: true,
    validateOnChange: false,
    validateOnBlur: true,
    onSubmit: async values => {
      try {
        await resetPassword({
          userId: values.userId,
          resetToken: values.resetToken,
          password: values.password,
          returnUrl: appUrl,
          returnUrlHash: '',
        });
        navigation.navigate('Login');
      } catch (error) {
        formik.setFieldError('password', 'AbpAccount::DefaultErrorMessage');
      }
    },
  });

  const shouldShowError = (field: keyof typeof formik.values) =>
    !formik.isSubmitting && (!!formik.touched[field] || formik.submitCount > 0) && !!formik.errors[field];

  const hasResetParams = !!formik.values.userId && !!formik.values.resetToken;

  useEffect(() => {
    if (!hasResetParams) navigation.replace('ForgotPassword');
  }, [hasResetParams, navigation]);

  if (!hasResetParams) return null;

  return (
    <ScrollView
      className="flex-1 bg-background dark:bg-background-dark"
      contentContainerStyle={{ paddingBottom: 32 }}
      keyboardShouldPersistTaps="handled">
      <View className="px-6 pt-6">
        <Text className="text-2xl font-bold text-foreground dark:text-foreground-dark tracking-tight">
          {t('KHHub::ResetPasswordTitle')}
        </Text>
        <Text className="text-sm text-muted-foreground dark:text-muted-dark-foreground mt-1.5 mb-5">
          {t('KHHub::ResetPasswordSubtitle')}
        </Text>

        {/* Form Card */}
        <View className="bg-card dark:bg-card-dark rounded-2xl border border-card-border dark:border-card-border-dark shadow-sm p-5">
          <TextInput
            mode="outlined"
            label={t('AbpIdentity::DisplayName:NewPassword')}
            value={formik.values.password}
            onChangeText={formik.handleChange('password')}
            onBlur={formik.handleBlur('password')}
            error={shouldShowError('password')}
            secureTextEntry={hidePassword}
            autoCapitalize="none"
            autoComplete="new-password"
            style={{ backgroundColor: primaryContainer }}
            right={
              <TextInput.Icon
                icon={hidePassword ? 'eye-off' : 'eye'}
                onPress={() => setHidePassword(current => !current)}
              />
            }
          />
          {shouldShowError('password') ? (
            <ValidationMessage>{formik.errors.password}</ValidationMessage>
          ) : null}

          <TextInput
            mode="outlined"
            label={t('KHHub::PasswordConfirm')}
            value={formik.values.passwordConfirm}
            onChangeText={formik.handleChange('passwordConfirm')}
            onBlur={formik.handleBlur('passwordConfirm')}
            error={shouldShowError('passwordConfirm')}
            secureTextEntry={hidePasswordConfirm}
            autoCapitalize="none"
            autoComplete="new-password"
            style={{ marginTop: 12, backgroundColor: primaryContainer }}
            right={
              <TextInput.Icon
                icon={hidePasswordConfirm ? 'eye-off' : 'eye'}
                onPress={() => setHidePasswordConfirm(current => !current)}
              />
            }
          />
          {shouldShowError('passwordConfirm') ? (
            <ValidationMessage>{formik.errors.passwordConfirm}</ValidationMessage>
          ) : null}
        </View>

        <Pressable
          onPress={() => formik.handleSubmit()}
          disabled={formik.isSubmitting}
          className={`mt-5 rounded-lg py-3.5 items-center shadow-sm ${
            formik.isSubmitting
              ? 'bg-muted dark:bg-muted-dark'
              : 'bg-accent dark:bg-accent-dark active:opacity-90'
          }`}>
          <Text className="text-accent-foreground dark:text-accent-dark-foreground font-semibold text-[15px]">
            {t('KHHub::ResetPasswordAction')}
          </Text>
        </Pressable>
      </View>
    </ScrollView>
  );
}

export default ResetPasswordScreen;
