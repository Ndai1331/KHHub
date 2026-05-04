import 'dart:io';

import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:provider/provider.dart';

import '../../auth/presentation/auth_view_model.dart';

class ProfilePicturePage extends StatefulWidget {
  const ProfilePicturePage({super.key});

  static const String routeName = '/profile-picture';

  @override
  State<ProfilePicturePage> createState() => _ProfilePicturePageState();
}

class _ProfilePicturePageState extends State<ProfilePicturePage> {
  final ImagePicker _picker = ImagePicker();
  int _profileOption = 0;
  XFile? _selectedImage;
  String? _previewUrl;
  bool _isLoading = false;

  @override
  void initState() {
    super.initState();
    _loadPicture();
  }

  Future<void> _loadPicture() async {
    final vm = context.read<AuthViewModel>();
    final id = (vm.currentUser['id'] ?? '').toString();
    if (id.isEmpty) return;
    setState(() => _isLoading = true);
    try {
      final data = await vm.accountApi.profilePictureById(id);
      _profileOption = (data['type'] ?? 0) as int;
      _previewUrl = (data['source'] ?? vm.profilePictureUrl) as String?;
    } finally {
      if (mounted) setState(() => _isLoading = false);
    }
  }

  Future<void> _pick(ImageSource source) async {
    final image = await _picker.pickImage(source: source, imageQuality: 90);
    if (image == null) return;
    setState(() {
      _selectedImage = image;
      _previewUrl = image.path;
      _profileOption = 2;
    });
  }

  Future<void> _save() async {
    final vm = context.read<AuthViewModel>();
    setState(() => _isLoading = true);
    try {
      await vm.accountApi.postProfilePicture(
        type: _profileOption,
        filePath: _profileOption == 2 ? _selectedImage?.path : null,
      );
      await vm.fetchAppConfig();
      if (!mounted) return;
      ScaffoldMessenger.of(context)
          .showSnackBar(const SnackBar(content: Text('Updated profile picture')));
    } catch (error) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(error.toString())));
    } finally {
      if (mounted) setState(() => _isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    final vm = context.watch<AuthViewModel>();
    final fallbackAvatar = vm.profilePictureUrl;
    final displayUrl = _previewUrl ?? fallbackAvatar;
    final bool hasLocalFile = displayUrl.startsWith('/');
    final ImageProvider<Object>? avatarImage = displayUrl.isEmpty
        ? null
        : hasLocalFile
            ? FileImage(File(displayUrl))
            : NetworkImage(displayUrl);

    return Scaffold(
      appBar: AppBar(title: Text(vm.t('AbpUi::ProfilePicture'))),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: <Widget>[
            CircleAvatar(
              radius: 52,
              backgroundImage: avatarImage,
              child: displayUrl.isEmpty ? const Icon(Icons.person, size: 48) : null,
            ),
            const SizedBox(height: 20),
            SegmentedButton<int>(
              segments: <ButtonSegment<int>>[
                ButtonSegment<int>(value: 0, label: Text(vm.t('AbpAccount::UseDefault'))),
                ButtonSegment<int>(value: 1, label: Text(vm.t('AbpAccount::DisplayName:UseGravatar'))),
                ButtonSegment<int>(value: 2, label: Text(vm.t('AbpAccount::SelectNewImage'))),
              ],
              selected: <int>{_profileOption},
              onSelectionChanged: (Set<int> values) {
                setState(() => _profileOption = values.first);
              },
            ),
            if (_profileOption == 2)
              Row(
                children: <Widget>[
                  Expanded(
                    child: OutlinedButton(
                      onPressed: () => _pick(ImageSource.camera),
                      child: Text(vm.t('AbpAccount::TakePhoto')),
                    ),
                  ),
                  const SizedBox(width: 8),
                  Expanded(
                    child: OutlinedButton(
                      onPressed: () => _pick(ImageSource.gallery),
                      child: Text(vm.t('AbpAccount::ChoosePhoto')),
                    ),
                  ),
                ],
              ),
            const Spacer(),
            SizedBox(
              width: double.infinity,
              child: FilledButton(
                onPressed: _isLoading ? null : _save,
                child: _isLoading
                    ? const SizedBox(
                        height: 16,
                        width: 16,
                        child: CircularProgressIndicator(strokeWidth: 2),
                      )
                    : Text(vm.t('AbpAccount::Save')),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
