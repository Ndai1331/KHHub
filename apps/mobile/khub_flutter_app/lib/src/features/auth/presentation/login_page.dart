import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../../shared/forms/form_validators.dart';
import '../../../shared/layout/app_form_section.dart';
import '../../../shared/layout/app_scaffold.dart';
import '../../../shared/widgets/app_text_field.dart';
import '../../home/presentation/home_page.dart';
import 'auth_view_model.dart';
import 'forgot_password_page.dart';
import 'register_page.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  static const String routeName = '/login';

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  bool _obscurePassword = true;

  @override
  void dispose() {
    _usernameController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }
    final vm = context.read<AuthViewModel>();
    try {
      await vm.login(_usernameController.text.trim(), _passwordController.text);
      if (!mounted) return;
      Navigator.pushReplacementNamed(context, HomePage.routeName);
    } catch (error) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text(error.toString())),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();

    return AppScaffold(
      title: vm.t('AbpUi::Login'),
      child: Form(
        key: _formKey,
        child: Column(
          children: <Widget>[
            AppFormSection(
              child: Column(
                children: <Widget>[
                  AppTextField(
                    controller: _usernameController,
                    label: vm.t('AbpAccount::UserNameOrEmailAddress'),
                    validator: (String? value) => FormValidators.required(value, vm.t),
                    bottomSpacing: 16,
                  ),
                  AppTextField(
                    controller: _passwordController,
                    label: vm.t('AbpAccount::Password'),
                    obscureText: _obscurePassword,
                    suffixIcon: IconButton(
                      onPressed: () => setState(() => _obscurePassword = !_obscurePassword),
                      icon: Icon(_obscurePassword ? Icons.visibility_off : Icons.visibility),
                    ),
                    validator: (String? value) => FormValidators.required(value, vm.t),
                    bottomSpacing: 0,
                  ),
                ],
              ),
            ),
            const SizedBox(height: 24),
            SizedBox(
              width: double.infinity,
              child: FilledButton(
                onPressed: vm.isLoading ? null : _submit,
                child: vm.isLoading
                    ? const SizedBox(
                        height: 16,
                        width: 16,
                        child: CircularProgressIndicator(strokeWidth: 2),
                      )
                    : Text(vm.t('AbpUi::Login')),
              ),
            ),
            TextButton(
              onPressed: () => Navigator.pushNamed(context, ForgotPasswordPage.routeName),
              child: Text(vm.t('KHHub::ForgotPasswordLink')),
            ),
            const Spacer(),
            TextButton(
              onPressed: () => Navigator.pushNamed(context, RegisterPage.routeName),
              child: Text('${vm.t('KHHub::DontHaveAnAccount')} ${vm.t('AbpUi::Register')}'),
            ),
          ],
        ),
      ),
    );
  }
}
