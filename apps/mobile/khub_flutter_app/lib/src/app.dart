import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'core/theme/app_theme.dart';
import 'features/account/presentation/change_password_page.dart';
import 'features/account/presentation/profile_picture_page.dart';
import 'features/auth/presentation/auth_view_model.dart';
import 'features/auth/presentation/forgot_password_page.dart';
import 'features/auth/presentation/login_page.dart';
import 'features/auth/presentation/register_page.dart';
import 'features/auth/presentation/reset_password_page.dart';
import 'features/home/presentation/home_page.dart';
import 'features/settings/presentation/settings_page.dart';

class KHHubApp extends StatelessWidget {
  const KHHubApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Consumer<AuthViewModel>(
      builder: (BuildContext context, AuthViewModel vm, _) {
        return MaterialApp(
          debugShowCheckedModeBanner: false,
          title: 'KHHub',
          locale: Locale(vm.languageCode),
          supportedLocales: const <Locale>[Locale('en'), Locale('tr')],
          themeMode: vm.themeMode,
          theme: AppTheme.light(),
          darkTheme: AppTheme.dark(),
          initialRoute: vm.isAuthenticated ? HomePage.routeName : LoginPage.routeName,
          routes: <String, WidgetBuilder>{
            LoginPage.routeName: (_) => const LoginPage(),
            RegisterPage.routeName: (_) => const RegisterPage(),
            ForgotPasswordPage.routeName: (_) => const ForgotPasswordPage(),
            ResetPasswordPage.routeName: (_) => const ResetPasswordPage(),
            HomePage.routeName: (_) => const HomePage(),
            SettingsPage.routeName: (_) => const SettingsPage(),
            ChangePasswordPage.routeName: (_) => const ChangePasswordPage(),
            ProfilePicturePage.routeName: (_) => const ProfilePicturePage(),
          },
        );
      },
    );
  }
}
