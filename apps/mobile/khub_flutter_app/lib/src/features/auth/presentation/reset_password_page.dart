import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../../shared/forms/form_validators.dart';
import '../../../shared/layout/app_form_section.dart';
import '../../../shared/layout/app_scaffold.dart';
import '../../../shared/widgets/app_text_field.dart';
import 'auth_view_model.dart';
import 'login_page.dart';

class ResetPasswordPage extends StatefulWidget {
  const ResetPasswordPage({super.key});

  static const String routeName = '/reset-password';

  @override
  State<ResetPasswordPage> createState() => _ResetPasswordPageState();
}

class _ResetPasswordPageState extends State<ResetPasswordPage> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _userIdController = TextEditingController();
  final TextEditingController _resetTokenController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmController = TextEditingController();

  @override
  void dispose() {
    _userIdController.dispose();
    _resetTokenController.dispose();
    _passwordController.dispose();
    _confirmController.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) return;
    try {
      await context.read<AuthViewModel>().accountApi.resetPassword(
            userId: _userIdController.text.trim(),
            resetToken: _resetTokenController.text.trim(),
            password: _passwordController.text,
          );
      if (!mounted) return;
      Navigator.pushNamedAndRemoveUntil(context, LoginPage.routeName, (_) => false);
    } catch (error) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(error.toString())));
    }
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    return AppScaffold(
      title: vm.t('KHHub::ResetPasswordTitle'),
      child: Form(
        key: _formKey,
        child: ListView(
          children: <Widget>[
            AppFormSection(
              child: Column(
                children: <Widget>[
                  AppTextField(
                    controller: _userIdController,
                    label: 'User ID',
                    validator: (String? value) => FormValidators.required(value, vm.t),
                  ),
                  AppTextField(
                    controller: _resetTokenController,
                    label: 'Reset token',
                    validator: (String? value) => FormValidators.required(value, vm.t),
                  ),
                  AppTextField(
                    controller: _passwordController,
                    label: vm.t('AbpIdentity::DisplayName:NewPassword'),
                    obscureText: true,
                    validator: (String? value) => FormValidators.required(value, vm.t),
                  ),
                  AppTextField(
                    controller: _confirmController,
                    label: vm.t('KHHub::PasswordConfirm'),
                    obscureText: true,
                    validator: (String? value) => FormValidators.confirmPassword(
                      value: value,
                      password: _passwordController.text,
                      t: vm.t,
                    ),
                    bottomSpacing: 0,
                  ),
                ],
              ),
            ),
            const SizedBox(height: 24),
            FilledButton(onPressed: _submit, child: Text(vm.t('KHHub::ResetPasswordAction'))),
          ],
        ),
      ),
    );
  }
}
