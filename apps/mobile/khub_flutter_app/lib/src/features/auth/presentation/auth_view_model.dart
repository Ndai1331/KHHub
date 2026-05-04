import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../../../core/network/api_client.dart';
import '../../../core/network/token_storage.dart';
import '../../account/data/identity_api_service.dart';
import '../../app/data/application_configuration_api_service.dart';
import '../data/account_api_service.dart';
import '../data/token_model.dart';

class AuthViewModel extends ChangeNotifier {
  final TokenStorage _tokenStorage = TokenStorage();

  late final ApiClient _apiClient = ApiClient(
    readToken: () => _token,
    saveToken: saveToken,
    refreshTokenCallback: refreshToken,
    onUnauthorized: logout,
    languageCode: _languageCode,
  );
  late final AccountApiService accountApi = AccountApiService(_apiClient);
  late final IdentityApiService identityApi = IdentityApiService(_apiClient);
  late final ApplicationConfigurationApiService appConfigApi =
      ApplicationConfigurationApiService(_apiClient);

  TokenModel _token = TokenModel.empty();
  Map<String, dynamic> _appConfig = <String, dynamic>{};
  String _languageCode = 'en';
  ThemeMode _themeMode = ThemeMode.system;
  bool _isLoading = false;
  Map<String, dynamic> _translations = <String, dynamic>{};

  TokenModel get token => _token;
  bool get isAuthenticated => _token.isAuthenticated && !_token.isExpired;
  bool get isLoading => _isLoading;
  String get languageCode => _languageCode;
  ThemeMode get themeMode => _themeMode;
  Map<String, dynamic> get appConfig => _appConfig;
  Map<String, dynamic> get currentUser =>
      (_appConfig['currentUser'] as Map<String, dynamic>?) ?? <String, dynamic>{};

  String get profilePictureUrl =>
      (currentUser['id'] ?? '').toString().isEmpty ? '' : _profilePictureUrl;

  String _profilePictureUrl = '';

  Future<void> bootstrap() async {
    _token = await _tokenStorage.loadToken();
    _languageCode = await _tokenStorage.loadLanguage();
    await _loadTranslations();
    if (isAuthenticated) {
      await fetchAppConfig();
    }
  }

  Future<void> saveToken(TokenModel token) async {
    _token = token;
    await _tokenStorage.saveToken(token);
    notifyListeners();
  }

  Future<TokenModel?> refreshToken(String refreshToken) async {
    try {
      final response = await accountApi.refreshToken(refreshToken);
      final expiresInMs = ((response['expires_in'] ?? 0) as int) * 1000;
      return TokenModel(
        tokenType: (response['token_type'] ?? 'Bearer') as String,
        accessToken: (response['access_token'] ?? '') as String,
        refreshToken: (response['refresh_token'] ?? refreshToken) as String,
        expireTime: DateTime.now().millisecondsSinceEpoch + expiresInMs,
        scope: (response['scope'] ?? '') as String,
      );
    } catch (_) {
      return null;
    }
  }

  Future<void> login(String username, String password) async {
    _setLoading(true);
    try {
      final response = await accountApi.login(username: username, password: password);
      final expiresInMs = ((response['expires_in'] ?? 0) as int) * 1000;
      await saveToken(
        TokenModel(
          tokenType: (response['token_type'] ?? 'Bearer') as String,
          accessToken: (response['access_token'] ?? '') as String,
          refreshToken: (response['refresh_token'] ?? '') as String,
          expireTime: DateTime.now().millisecondsSinceEpoch + expiresInMs,
          scope: (response['scope'] ?? '') as String,
        ),
      );
      await fetchAppConfig();
    } finally {
      _setLoading(false);
    }
  }

  Future<void> register(String userName, String emailAddress, String password) async {
    _setLoading(true);
    try {
      await accountApi.register(
        userName: userName,
        emailAddress: emailAddress,
        password: password,
      );
      await login(userName, password);
    } finally {
      _setLoading(false);
    }
  }

  Future<void> logout() async {
    _token = TokenModel.empty();
    _appConfig = <String, dynamic>{};
    _profilePictureUrl = '';
    await _tokenStorage.clearToken();
    notifyListeners();
  }

  Future<void> fetchAppConfig() async {
    _appConfig = await appConfigApi.getApplicationConfiguration();
    final userId = (currentUser['id'] ?? '').toString();
    if (userId.isNotEmpty) {
      _profilePictureUrl = await accountApi.profilePictureFileUrl(userId);
    }
    notifyListeners();
  }

  Future<void> setLanguage(String languageCode) async {
    _languageCode = languageCode;
    await _tokenStorage.saveLanguage(languageCode);
    await _loadTranslations();
    notifyListeners();
  }

  String t(String key) {
    if (key.isEmpty) {
      return '';
    }

    String normalizedKey = key;
    if (normalizedKey.startsWith('::')) {
      normalizedKey = '${_defaultResourceName()}$normalizedKey';
    }

    final parts = normalizedKey.split('::');
    if (parts.length < 2) {
      return key;
    }

    final resourceName = parts.first.isEmpty ? _defaultResourceName() : parts.first;
    final nestedKey = parts[1];
    final source = _translations[resourceName];
    if (source is! Map<String, dynamic>) {
      return nestedKey;
    }

    dynamic value = source;
    for (final segment in nestedKey.split(':')) {
      if (value is Map<String, dynamic> && value.containsKey(segment)) {
        value = value[segment];
      } else {
        return nestedKey;
      }
    }

    return value is String ? value : nestedKey;
  }

  Future<void> _loadTranslations() async {
    final locale = _languageCode == 'tr' ? 'tr' : 'en';
    final jsonString = await rootBundle.loadString('assets/locales/$locale.json');
    _translations = jsonDecode(jsonString) as Map<String, dynamic>;
  }

  String _defaultResourceName() => 'KHHub';

  void setThemeMode(ThemeMode mode) {
    _themeMode = mode;
    notifyListeners();
  }

  void _setLoading(bool value) {
    _isLoading = value;
    notifyListeners();
  }
}
