import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../../shared/forms/form_validators.dart';
import '../../../shared/layout/app_form_section.dart';
import '../../../shared/layout/app_scaffold.dart';
import '../../../shared/widgets/app_text_field.dart';
import '../../auth/presentation/auth_view_model.dart';

class ChangePasswordPage extends StatefulWidget {
  const ChangePasswordPage({super.key});

  static const String routeName = '/change-password';

  @override
  State<ChangePasswordPage> createState() => _ChangePasswordPageState();
}

class _ChangePasswordPageState extends State<ChangePasswordPage> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _currentController = TextEditingController();
  final TextEditingController _newController = TextEditingController();
  final TextEditingController _confirmController = TextEditingController();

  @override
  void dispose() {
    _currentController.dispose();
    _newController.dispose();
    _confirmController.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) return;
    try {
      await context.read<AuthViewModel>().identityApi.changePassword(
            currentPassword: _currentController.text,
            newPassword: _newController.text,
          );
      if (!mounted) return;
      Navigator.pop(context);
    } catch (error) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(error.toString())));
    }
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    return AppScaffold(
      title: vm.t('AbpUi::ChangePassword'),
      child: Form(
        key: _formKey,
        child: ListView(
          children: <Widget>[
            AppFormSection(
              child: Column(
                children: <Widget>[
                  AppTextField(
                    controller: _currentController,
                    label: vm.t('AbpIdentity::DisplayName:CurrentPassword'),
                    obscureText: true,
                    validator: (String? value) => FormValidators.required(value, vm.t),
                  ),
                  AppTextField(
                    controller: _newController,
                    label: vm.t('AbpIdentity::DisplayName:NewPassword'),
                    obscureText: true,
                    validator: (String? value) => FormValidators.required(value, vm.t),
                  ),
                  AppTextField(
                    controller: _confirmController,
                    label: vm.t('AbpIdentity::DisplayName:NewPasswordConfirm'),
                    obscureText: true,
                    validator: (String? value) => FormValidators.confirmPassword(
                      value: value,
                      password: _newController.text,
                      t: vm.t,
                    ),
                    bottomSpacing: 0,
                  ),
                ],
              ),
            ),
            const SizedBox(height: 24),
            FilledButton(onPressed: _submit, child: Text(vm.t('AbpAccount::Save'))),
          ],
        ),
      ),
    );
  }
}
