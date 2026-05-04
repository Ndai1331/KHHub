import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../../shared/forms/form_validators.dart';
import '../../../shared/layout/app_form_section.dart';
import '../../../shared/layout/app_scaffold.dart';
import '../../../shared/widgets/app_text_field.dart';
import 'auth_view_model.dart';

class ForgotPasswordPage extends StatefulWidget {
  const ForgotPasswordPage({super.key});

  static const String routeName = '/forgot-password';

  @override
  State<ForgotPasswordPage> createState() => _ForgotPasswordPageState();
}

class _ForgotPasswordPageState extends State<ForgotPasswordPage> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  bool _sent = false;

  @override
  void dispose() {
    _emailController.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }

    try {
      await context.read<AuthViewModel>().accountApi.sendPasswordResetCode(
            email: _emailController.text.trim(),
          );
      setState(() => _sent = true);
    } catch (error) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(error.toString())));
    }
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    return AppScaffold(
      title: vm.t('KHHub::ForgotPasswordTitle'),
      child: _sent
          ? Center(
              child: FilledButton(
                onPressed: () => Navigator.pop(context),
                child: Text(vm.t('KHHub::BackToLogin')),
              ),
            )
          : Form(
              key: _formKey,
              child: Column(
                children: <Widget>[
                  AppFormSection(
                    child: AppTextField(
                      controller: _emailController,
                      label: vm.t('AbpIdentity::EmailAddress'),
                      keyboardType: TextInputType.emailAddress,
                      validator: (String? value) => FormValidators.email(value, vm.t),
                      bottomSpacing: 0,
                    ),
                  ),
                  const SizedBox(height: 20),
                  FilledButton(
                    onPressed: _submit,
                    child: Text(vm.t('KHHub::SendResetLink')),
                  ),
                ],
              ),
            ),
    );
  }
}
