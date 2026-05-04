import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../auth/presentation/auth_view_model.dart';

class SettingsPage extends StatelessWidget {
  const SettingsPage({super.key});

  static const String routeName = '/settings';

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    return Scaffold(
      appBar: AppBar(title: Text(vm.t('AbpSettingManagement::Settings'))),
      body: ListView(
        children: <Widget>[
          ListTile(
            title: Text(vm.t('AbpUi::Language')),
            subtitle: Text(vm.languageCode),
            onTap: () async {
              final selected = await showModalBottomSheet<String>(
                context: context,
                builder: (_) => SafeArea(
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      ListTile(
                        title: const Text('English'),
                        onTap: () => Navigator.pop(context, 'en'),
                      ),
                      ListTile(
                        title: const Text('Turkish'),
                        onTap: () => Navigator.pop(context, 'tr'),
                      ),
                    ],
                  ),
                ),
              );
              if (selected != null) {
                await vm.setLanguage(selected);
              }
            },
          ),
          ListTile(
            title: Text(vm.t('AbpUi::Theme')),
            subtitle: Text(vm.t('AbpUi::${vm.themeMode.name[0].toUpperCase()}${vm.themeMode.name.substring(1)}')),
            onTap: () async {
              final selected = await showModalBottomSheet<ThemeMode>(
                context: context,
                builder: (_) => SafeArea(
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      ListTile(
                        title: const Text('System'),
                        onTap: () => Navigator.pop(context, ThemeMode.system),
                      ),
                      ListTile(
                        title: const Text('Light'),
                        onTap: () => Navigator.pop(context, ThemeMode.light),
                      ),
                      ListTile(
                        title: const Text('Dark'),
                        onTap: () => Navigator.pop(context, ThemeMode.dark),
                      ),
                    ],
                  ),
                ),
              );
              if (selected != null) {
                vm.setThemeMode(selected);
              }
            },
          ),
        ],
      ),
    );
  }
}
