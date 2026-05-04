import 'dart:convert';

class TokenModel {
  const TokenModel({
    required this.tokenType,
    required this.accessToken,
    required this.refreshToken,
    required this.expireTime,
    required this.scope,
  });

  final String tokenType;
  final String accessToken;
  final String refreshToken;
  final int expireTime;
  final String scope;

  bool get isAuthenticated => accessToken.isNotEmpty;

  bool get isExpired => DateTime.now().millisecondsSinceEpoch >= expireTime;

  Map<String, dynamic> toJson() => <String, dynamic>{
        'token_type': tokenType,
        'access_token': accessToken,
        'refresh_token': refreshToken,
        'expire_time': expireTime,
        'scope': scope,
      };

  String toRawJson() => jsonEncode(toJson());

  factory TokenModel.empty() => const TokenModel(
        tokenType: '',
        accessToken: '',
        refreshToken: '',
        expireTime: 0,
        scope: '',
      );

  factory TokenModel.fromJson(Map<String, dynamic> json) => TokenModel(
        tokenType: (json['token_type'] ?? 'Bearer') as String,
        accessToken: (json['access_token'] ?? '') as String,
        refreshToken: (json['refresh_token'] ?? '') as String,
        expireTime: (json['expire_time'] ?? 0) as int,
        scope: (json['scope'] ?? '') as String,
      );

  factory TokenModel.fromRawJson(String rawJson) =>
      TokenModel.fromJson(jsonDecode(rawJson) as Map<String, dynamic>);
}
