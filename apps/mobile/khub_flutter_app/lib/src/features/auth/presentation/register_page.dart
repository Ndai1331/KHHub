import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../../shared/forms/form_validators.dart';
import '../../../shared/layout/app_form_section.dart';
import '../../../shared/layout/app_scaffold.dart';
import '../../../shared/widgets/app_text_field.dart';
import '../../home/presentation/home_page.dart';
import 'auth_view_model.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({super.key});

  static const String routeName = '/register';

  @override
  State<RegisterPage> createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _userNameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmController = TextEditingController();

  @override
  void dispose() {
    _userNameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _confirmController.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }
    try {
      await context.read<AuthViewModel>().register(
            _userNameController.text.trim(),
            _emailController.text.trim(),
            _passwordController.text,
          );
      if (!mounted) return;
      Navigator.pushNamedAndRemoveUntil(context, HomePage.routeName, (_) => false);
    } catch (error) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(error.toString())));
    }
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    final isLoading = vm.isLoading;
    return AppScaffold(
      title: vm.t('AbpUi::Register'),
      child: Form(
        key: _formKey,
        child: ListView(
          children: <Widget>[
            AppFormSection(
              child: Column(
                children: <Widget>[
                  AppTextField(
                    controller: _userNameController,
                    label: vm.t('AbpIdentity::UserName'),
                    validator: (String? value) => FormValidators.required(value, vm.t),
                  ),
                  AppTextField(
                    controller: _emailController,
                    label: vm.t('AbpIdentity::EmailAddress'),
                    keyboardType: TextInputType.emailAddress,
                    validator: (String? value) => FormValidators.email(value, vm.t),
                  ),
                  AppTextField(
                    controller: _passwordController,
                    label: vm.t('AbpAccount::Password'),
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
            FilledButton(
              onPressed: isLoading ? null : _submit,
              child: Text(vm.t('AbpUi::Register')),
            ),
          ],
        ),
      ),
    );
  }
}
