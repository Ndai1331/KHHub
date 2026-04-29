import { useContext, useState } from 'react';
import { View, Text, Pressable, ScrollView } from 'react-native';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { TextInput } from 'react-native-paper';

import { LocalizationContext } from '../../contexts/LocalizationContext';
import { useThemeColors } from '../../hooks';
import { ValidationMessage } from '../../components';
import { sendPasswordResetCode } from '../../api/AccountAPI';
import type { ForgotPasswordScreenProps } from '../../navigators/types';


const validationSchema = Yup.object().shape({
  email: Yup.string()
    .required('AbpAccount::ThisFieldIsRequired')
    .email('AbpAccount::ThisFieldIsNotAValidEmailAddress'),
});

function ForgotPasswordScreen({ navigation }: ForgotPasswordScreenProps) {
  const { t } = useContext(LocalizationContext);
  const { primaryContainer } = useThemeColors();
  const [isSent, setIsSent] = useState(false);

  const formik = useFormik({
    initialValues: { email: '' },
    validationSchema,
    validateOnChange: false,
    validateOnBlur: true,
    onSubmit: async values => {
      try {
        await sendPasswordResetCode({ email: values.email });
        setIsSent(true);
      } catch (error) {
        formik.setFieldError('email', 'AbpAccount::DefaultErrorMessage');
      }
    },
  });

  const shouldShowError = (field: 'email') =>
    !formik.isSubmitting && (!!formik.touched[field] || formik.submitCount > 0) && !!formik.errors[field];

  return (
    <ScrollView
      className="flex-1 bg-background dark:bg-background-dark"
      contentContainerStyle={{ paddingBottom: 32 }}
      keyboardShouldPersistTaps="handled">
      <View className="px-6 pt-6">
        <Text className="text-2xl font-bold text-foreground dark:text-foreground-dark tracking-tight">
          {t('KHHub::ForgotPasswordTitle')}
        </Text>
        <Text className="text-sm text-muted-foreground dark:text-muted-dark-foreground mt-1.5 mb-5">
          {isSent ? t('KHHub::CheckYourEmailSubtitle') : t('KHHub::ForgotPasswordSubtitle')}
        </Text>

        {!isSent ? (
          <>
            {/* Form Card */}
            <View className="bg-card dark:bg-card-dark rounded-2xl border border-card-border dark:border-card-border-dark shadow-sm p-5">
              <TextInput
                mode="outlined"
                label={t('AbpIdentity::EmailAddress')}
                value={formik.values.email}
                onChangeText={formik.handleChange('email')}
                onBlur={formik.handleBlur('email')}
                error={shouldShowError('email')}
                autoCapitalize="none"
                autoComplete="email"
                keyboardType="email-address"
                style={{ backgroundColor: primaryContainer }}
              />
              {shouldShowError('email') ? (
                <ValidationMessage>{formik.errors.email}</ValidationMessage>
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
              <Text
                className={`font-semibold text-[15px] ${
                  formik.isSubmitting
                    ? 'text-muted-foreground dark:text-muted-dark-foreground'
                    : 'text-accent-foreground dark:text-accent-dark-foreground'
                }`}
              >
                {t('KHHub::SendResetLink')}
              </Text>
            </Pressable>
          </>
        ) : (
          <Pressable
            onPress={() => navigation.navigate('Login')}
            className="bg-accent dark:bg-accent-dark active:opacity-90 rounded-lg py-3.5 items-center shadow-sm">
            <Text className="text-accent-foreground dark:text-accent-dark-foreground font-semibold text-[15px]">
              {t('KHHub::BackToLogin')}
            </Text>
          </Pressable>
        )}
      </View>
    </ScrollView>
  );
}

export default ForgotPasswordScreen;
