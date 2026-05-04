typedef Translate = String Function(String key);

class FormValidators {
  FormValidators._();

  static String? required(String? value, Translate t) {
    if (value == null || value.trim().isEmpty) {
      return t('AbpAccount::ThisFieldIsRequired');
    }
    return null;
  }

  static String? email(String? value, Translate t) {
    final requiredError = required(value, t);
    if (requiredError != null) {
      return requiredError;
    }
    if (!(value!.contains('@'))) {
      return t('AbpAccount::ThisFieldIsNotAValidEmailAddress');
    }
    return null;
  }

  static String? confirmPassword({
    required String? value,
    required String password,
    required Translate t,
  }) {
    final requiredError = required(value, t);
    if (requiredError != null) {
      return requiredError;
    }
    if (value != password) {
      return t('AbpIdentity::PasswordConfirmationFailed');
    }
    return null;
  }
}
