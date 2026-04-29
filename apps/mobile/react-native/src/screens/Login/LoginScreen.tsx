import * as Yup from 'yup';
import { useContext, useState } from 'react';
import { View, Text, Pressable, ScrollView } from 'react-native';
import { useFormik } from 'formik';
import { TextInput } from 'react-native-paper';
import { useDispatch } from 'react-redux';

import type { LoginScreenProps } from '../../navigators/types';
import { useThemeColors } from '../../hooks';
import { ValidationMessage } from '../../components';
import { login } from '../../api/AccountAPI';
import { LocalizationContext } from '../../contexts/LocalizationContext';

import AppActions from '../../store/actions/AppActions';
import PersistentStorageActions from '../../store/actions/PersistentStorageActions';


const validationSchema = Yup.object().shape({
  username: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  password: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
});

function LoginScreen({ navigation }: LoginScreenProps) {
  const { t } = useContext(LocalizationContext);
  const { primaryContainer } = useThemeColors();
  const [hidePassword, setHidePassword] = useState(true);
  const dispatch = useDispatch();

  const formik = useFormik({
    initialValues: { username: '', password: '' },
    validationSchema,
    validateOnChange: false,
    validateOnBlur: true,
    onSubmit: async values => {
      try {
        const response = await login({
          username: values.username,
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
        formik.setFieldError('password', 'AbpAccount::InvalidUserNameOrPassword');
      }
    },
  });

  const shouldShowError = (field: 'username' | 'password') =>
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
            label={t('AbpAccount::UserNameOrEmailAddress')}
            value={formik.values.username}
            onChangeText={formik.handleChange('username')}
            onBlur={formik.handleBlur('username')}
            error={shouldShowError('username')}
            autoCapitalize="none"
            autoComplete="username"
            style={{ backgroundColor: primaryContainer }}
          />
          {shouldShowError('username') ? (
            <ValidationMessage>{formik.errors.username}</ValidationMessage>
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
            autoComplete="password"
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

          <Text
            onPress={() => navigation.navigate('ForgotPassword')}
            className="mt-3 self-end text-accent dark:text-accent-dark text-sm font-medium">
            {t('KHHub::ForgotPasswordLink')}
          </Text>
        </View>

        {/* Login Button */}
        <Pressable
          onPress={() => formik.handleSubmit()}
          disabled={formik.isSubmitting}
          className={`mt-5 rounded-lg py-3.5 items-center shadow-sm border border-border dark:border-border-dark ${
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
            {t('AbpUi::Login')}
          </Text>
        </Pressable>

        {/* Register Link */}
        <View className="mt-6 flex-row justify-center gap-x-1.5">
          <Text className="text-sm text-muted-foreground dark:text-muted-dark-foreground">
            {t('KHHub::DontHaveAnAccount')}
          </Text>
          <Text
            onPress={() => navigation.navigate('Register')}
            className="text-sm text-accent dark:text-accent-dark font-semibold underline">
            {t('AbpUi::Register')}
          </Text>
        </View>
      </View>
    </ScrollView>
  );
}

export default LoginScreen;
