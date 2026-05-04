import 'dart:convert';

import 'package:http/http.dart' as http;

import '../../features/auth/data/token_model.dart';
import '../config/environment.dart';

typedef RefreshTokenCallback = Future<TokenModel?> Function(String refreshToken);
typedef ReadTokenCallback = TokenModel Function();
typedef SaveTokenCallback = Future<void> Function(TokenModel token);
typedef UnauthorizedCallback = Future<void> Function();

class ApiClient {
  ApiClient({
    required this.readToken,
    required this.refreshTokenCallback,
    required this.saveToken,
    required this.onUnauthorized,
    this.languageCode = 'en',
  });

  final ReadTokenCallback readToken;
  final RefreshTokenCallback refreshTokenCallback;
  final SaveTokenCallback saveToken;
  final UnauthorizedCallback onUnauthorized;
  final String languageCode;

  final http.Client _httpClient = http.Client();

  Future<dynamic> get(String path, {Map<String, dynamic>? queryParams}) async {
    final uri = _buildUri(path, queryParams: queryParams);
    return _send('GET', uri);
  }

  Future<dynamic> post(
    String path, {
    Object? body,
    Map<String, String>? headers,
    bool isAbsolute = false,
  }) async {
    final uri = isAbsolute ? Uri.parse(path) : _buildUri(path);
    return _send('POST', uri, body: body, headers: headers);
  }

  Future<dynamic> postMultipart(
    String path, {
    required Map<String, String> fields,
    String? filePath,
    String fileFieldName = 'imageContent',
  }) async {
    final uri = _buildUri(path);
    final token = await _ensureValidToken();
    final request = http.MultipartRequest('POST', uri);
    request.fields.addAll(fields);
    request.headers['Accept-Language'] = languageCode;
    if (token.accessToken.isNotEmpty) {
      request.headers['Authorization'] = '${token.tokenType} ${token.accessToken}';
    }
    if (filePath != null && filePath.isNotEmpty) {
      request.files.add(await http.MultipartFile.fromPath(fileFieldName, filePath));
    }

    final streamedResponse = await request.send();
    final response = await http.Response.fromStream(streamedResponse);
    return _handleResponse(response);
  }

  Uri _buildUri(String path, {Map<String, dynamic>? queryParams}) {
    final normalizedPath = path.startsWith('/') ? path : '/$path';
    return Uri.parse('${currentEnv.apiUrl}$normalizedPath').replace(
      queryParameters: queryParams?.map(
        (String key, dynamic value) => MapEntry(key, value.toString()),
      ),
    );
  }

  Future<dynamic> _send(
    String method,
    Uri uri, {
    Object? body,
    Map<String, String>? headers,
    bool canRetry = true,
  }) async {
    final token = await _ensureValidToken();
    final requestHeaders = <String, String>{
      'Content-Type': 'application/json',
      'Accept-Language': languageCode,
      ...?headers,
    };

    if (token.accessToken.isNotEmpty) {
      requestHeaders['Authorization'] = '${token.tokenType} ${token.accessToken}';
    }

    late http.Response response;
    if (method == 'GET') {
      response = await _httpClient.get(uri, headers: requestHeaders);
    } else {
      response = await _httpClient.post(
        uri,
        headers: requestHeaders,
        body: body is String ? body : jsonEncode(body ?? <String, dynamic>{}),
      );
    }

    if (response.statusCode == 401 && canRetry && token.refreshToken.isNotEmpty) {
      final refreshedToken = await refreshTokenCallback(token.refreshToken);
      if (refreshedToken != null && refreshedToken.accessToken.isNotEmpty) {
        await saveToken(refreshedToken);
        return _send(method, uri, body: body, headers: headers, canRetry: false);
      }
      await onUnauthorized();
    }

    return _handleResponse(response);
  }

  Future<TokenModel> _ensureValidToken() async {
    final token = readToken();
    final isExpiringSoon = token.expireTime - DateTime.now().millisecondsSinceEpoch <= 120000;

    if (!token.isAuthenticated || token.refreshToken.isEmpty || !isExpiringSoon) {
      return token;
    }

    final refreshedToken = await refreshTokenCallback(token.refreshToken);
    if (refreshedToken == null) {
      return token;
    }
    await saveToken(refreshedToken);
    return refreshedToken;
  }

  dynamic _handleResponse(http.Response response) {
    if (response.body.isEmpty) {
      if (response.statusCode >= 200 && response.statusCode < 300) {
        return <String, dynamic>{};
      }
      throw Exception('Request failed: ${response.statusCode}');
    }

    final data = jsonDecode(response.body);
    if (response.statusCode >= 200 && response.statusCode < 300) {
      return data;
    }

    final error = data is Map<String, dynamic> ? data['error'] : null;
    final message = error is Map<String, dynamic>
        ? (error['message'] ?? error['details'] ?? 'Unknown API error')
        : 'Unknown API error';
    throw Exception(message);
  }
}
