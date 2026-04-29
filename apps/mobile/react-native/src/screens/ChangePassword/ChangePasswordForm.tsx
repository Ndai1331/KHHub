import * as Yup from 'yup';
import { useMemo, useRef, useState } from 'react';
import { View } from 'react-native';
import { useFormik } from 'formik';
import i18n from 'i18n-js';
import { TextInput } from 'react-native-paper';

import { FormButtons, ValidationMessage } from '../../components';
import { useThemeColors } from '../../hooks';

interface ChangePasswordFormProps {
  submit: (values: any) => void;
  cancel: () => void;
}

const ValidationSchema = Yup.object().shape({
  currentPassword: Yup.string().required('AbpAccount::ThisFieldIsRequired'),
  newPassword: Yup.string()
    .required('AbpAccount::ThisFieldIsRequired')
    .oneOf(
      [Yup.ref('repeatPassword'), null],
      'AbpIdentity::Volo.Abp.Identity:PasswordConfirmationFailed',
    ),
  repeatPassword: Yup.string()
    .required('AbpAccount::ThisFieldIsRequired')
    .oneOf(
      [Yup.ref('newPassword'), null],
      'AbpIdentity::Volo.Abp.Identity:PasswordConfirmationFailed',
    ),
});

function ChangePasswordForm({ submit, cancel }: ChangePasswordFormProps) {
  const { primaryContainer } = useThemeColors();
  const inputStyle = useMemo(() => ({ backgroundColor: primaryContainer }), [primaryContainer]);

  const [showCurrentPassword, setShowCurrentPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const [hidePassword, setHidePassword] = useState(true);

  const currentPasswordRef = useRef(null);
  const newPasswordRef = useRef(null);
  const repeatPasswordRef = useRef(null);

  const onSubmit = (values: any) => {
    submit({ ...values, newPasswordConfirm: values.newPassword });
  };

  const formik = useFormik({
    enableReinitialize: true,
    validateOnBlur: true,
    validationSchema: ValidationSchema,
    initialValues: { currentPassword: '', newPassword: '', repeatPassword: '' },
    onSubmit,
  });

  const isInvalidControl = (controlName: string | null = null) => {
    if (!controlName) return;
    return (
      ((!!formik.touched[controlName] && formik.submitCount > 0) || formik.submitCount > 0) &&
      !!formik.errors[controlName]
    );
  };

  return (
    <View className="flex-1 px-6 pt-6">
      <View className="mb-4">
        <TextInput
          mode="outlined"
          returnKeyType="next"
          returnKeyLabel="next"
          autoCapitalize="none"
          ref={currentPasswordRef}
          error={isInvalidControl('currentPassword')}
          onSubmitEditing={() => newPasswordRef?.current?.focus()}
          onChangeText={formik.handleChange('currentPassword')}
          onBlur={formik.handleBlur('currentPassword')}
          value={formik.values.currentPassword}
          secureTextEntry={!showCurrentPassword}
          label={i18n.t('AbpIdentity::DisplayName:CurrentPassword')}
          style={inputStyle}
          right={
            <TextInput.Icon
              icon={!showCurrentPassword ? 'eye-off' : 'eye'}
              onPress={() => setShowCurrentPassword(!showCurrentPassword)}
            />
          }
        />
        {isInvalidControl('currentPassword') && (
          <ValidationMessage>{formik.errors.currentPassword as string}</ValidationMessage>
        )}
      </View>

      <View className="mb-4">
        <TextInput
          mode="outlined"
          returnKeyType="next"
          ref={newPasswordRef}
          error={isInvalidControl('newPassword')}
          onSubmitEditing={() => repeatPasswordRef?.current?.focus()}
          onChangeText={formik.handleChange('newPassword')}
          onBlur={formik.handleBlur('newPassword')}
          value={formik.values.newPassword}
          secureTextEntry={!showNewPassword}
          autoCapitalize="none"
          label={i18n.t('AbpIdentity::DisplayName:NewPassword')}
          style={inputStyle}
          right={
            <TextInput.Icon
              icon={!showNewPassword ? 'eye-off' : 'eye'}
              onPress={() => setShowNewPassword(!showNewPassword)}
            />
          }
        />
        {isInvalidControl('newPassword') && (
          <ValidationMessage>{formik.errors.newPassword as string}</ValidationMessage>
        )}
      </View>

      <View className="mb-4">
        <TextInput
          mode="outlined"
          returnKeyType="done"
          ref={repeatPasswordRef}
          error={isInvalidControl('repeatPassword')}
          onSubmitEditing={() => formik.handleSubmit()}
          onChangeText={formik.handleChange('repeatPassword')}
          onBlur={formik.handleBlur('repeatPassword')}
          value={formik.values.repeatPassword}
          secureTextEntry={hidePassword}
          autoCapitalize="none"
          label={i18n.t('AbpIdentity::DisplayName:NewPasswordConfirm')}
          style={inputStyle}
          right={
            <TextInput.Icon
              icon={hidePassword ? 'eye-off' : 'eye'}
              onPress={() => setHidePassword(!hidePassword)}
            />
          }
        />
        {isInvalidControl('repeatPassword') && (
          <ValidationMessage>{formik.errors.repeatPassword as string}</ValidationMessage>
        )}
      </View>

      <FormButtons submit={formik.handleSubmit} cancel={cancel} />
    </View>
  );
}

export default ChangePasswordForm;
