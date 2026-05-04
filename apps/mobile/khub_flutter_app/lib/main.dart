import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'src/app.dart';
import 'src/features/auth/presentation/auth_view_model.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  final authViewModel = AuthViewModel();
  await authViewModel.bootstrap();

  runApp(
    ChangeNotifierProvider<AuthViewModel>.value(
      value: authViewModel,
      child: const KHHubApp(),
    ),
  );
}
