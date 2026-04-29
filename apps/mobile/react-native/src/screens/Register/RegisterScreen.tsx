import { useContext, useState } from 'react';
import { View, Text, Pressable, ScrollView } from 'react-native';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { TextInput } from 'react-native-paper';
import { useDispatch } from 'react-redux';

import { LocalizationContext } from '../../contexts/LocalizationContext';
import { useThemeColors } from '../../hooks';
import { ValidationMessage } from '../../components';
import { register, login } from '../../api/AccountAPI';
import PersistentStorageActions from '../../store/actions/PersistentStorageActions';
import AppActions from '../../store/actions/AppActions';
import type { RegisterScreenProps } from '../../navigators/types';


const validationSchema = Yup.object().shape({
  userName: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  emailAddress: Yup.string()
    .required('AbpAccount::ThisFieldIsRequired')
    .email('AbpAccount::ThisFieldIsNotAValidEmailAddress'),
  password: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  passwordConfirm: Yup.string()
    .required('AbpAccount::ThisFieldIsRequired')
    .oneOf([Yup.ref('password'), null], 'AbpIdentity::PasswordConfirmationFailed'),
});

function RegisterScreen({ navigation }: RegisterScreenProps) {
  const { t } = useContext(LocalizationContext);
  const { primaryContainer } = useThemeColors();
  const dispatch = useDispatch();

  const [hidePassword, setHidePassword] = useState(true);
  const [hidePasswordConfirm, setHidePasswordConfirm] = useState(true);

  const formik = useFormik({
    initialValues: { userName: '', emailAddress: '', password: '', passwordConfirm: '' },
    validationSchema,
    validateOnChange: false,
    validateOnBlur: true,
    onSubmit: async values => {
      try {
        await register({
          userName: values.userName,
          emailAddress: values.emailAddress,
          password: values.password,
        });

        const response = await login({
          username: values.userName,
          password: values.password,
        });

        const expiresInMs = (response.expires_in ?? 0) * 1000;
        dispatch(AppActions.setProfilePicture(''));
        dispatch(
          PersistentStorageActions.setToken({
            token_type: response.token_type ?? 'Bearer',
            access_token: response.access_token ?? '',
            refresh_token: response.refresh_token ?? '',
            expire_time: Date.now() + expiresInMs,
            scope: response.scope ?? '',
          }),
        );
        dispatch(AppActions.fetchAppConfigAsync({ showLoading: false }));
      } catch (error) {
        formik.setFieldError('password', 'AbpAccount::DefaultErrorMessage');
      }
    },
  });

  const shouldShowError = (field: keyof typeof formik.values) =>
    !formik.isSubmitting && (!!formik.touched[field] || formik.submitCount > 0) && !!formik.errors[field];

  return (
    <ScrollView
      className="flex-1 bg-background dark:bg-background-dark"
      contentContainerStyle={{ paddingBottom: 32 }}
      keyboardShouldPersistTaps="handled">
      <View className="px-6 pt-6">
        {/* Form Card */}
        <View className="bg-card dark:bg-card-dark rounded-2xl border border-card-border dark:border-card-border-dark shadow-sm p-5">
          <TextInput
            mode="outlined"
            label={t('AbpIdentity::UserName')}
            value={formik.values.userName}
            onChangeText={formik.handleChange('userName')}
            onBlur={formik.handleBlur('userName')}
            error={shouldShowError('userName')}
            autoCapitalize="none"
            autoComplete="username"
            style={{ backgroundColor: primaryContainer }}
          />
          {shouldShowError('userName') ? (
            <ValidationMessage>{formik.errors.userName}</ValidationMessage>
          ) : null}

          <TextInput
            mode="outlined"
            label={t('AbpIdentity::EmailAddress')}
            value={formik.values.emailAddress}
            onChangeText={formik.handleChange('emailAddress')}
            onBlur={formik.handleBlur('emailAddress')}
            error={shouldShowError('emailAddress')}
            autoCapitalize="none"
            autoComplete="email"
            keyboardType="email-address"
            style={{ marginTop: 12, backgroundColor: primaryContainer }}
          />
          {shouldShowError('emailAddress') ? (
            <ValidationMessage>{formik.errors.emailAddress}</ValidationMessage>
          ) : null}

          <TextInput
            mode="outlined"
            label={t('AbpAccount::Password')}
            value={formik.values.password}
            onChangeText={formik.handleChange('password')}
            onBlur={formik.handleBlur('password')}
            error={shouldShowError('password')}
            secureTextEntry={hidePassword}
            autoCapitalize="none"
            autoComplete="new-password"
            style={{ marginTop: 12, backgroundColor: primaryContainer }}
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

        {/* Register Button */}
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
            }`}>
              {t('AbpUi::Register')}
          </Text>
        </Pressable>
      </View>
    </ScrollView>
  );
}

export default RegisterScreen;
