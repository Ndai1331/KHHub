import 'package:shared_preferences/shared_preferences.dart';

import '../../features/auth/data/token_model.dart';

class TokenStorage {
  static const String _tokenKey = 'khhub_token';
  static const String _languageKey = 'khhub_language';

  Future<TokenModel> loadToken() async {
    final prefs = await SharedPreferences.getInstance();
    final rawToken = prefs.getString(_tokenKey);
    if (rawToken == null || rawToken.isEmpty) {
      return TokenModel.empty();
    }

    return TokenModel.fromRawJson(rawToken);
  }

  Future<void> saveToken(TokenModel token) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString(_tokenKey, token.toRawJson());
  }

  Future<void> clearToken() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove(_tokenKey);
  }

  Future<String> loadLanguage() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString(_languageKey) ?? 'en';
  }

  Future<void> saveLanguage(String languageCode) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString(_languageKey, languageCode);
  }
}
