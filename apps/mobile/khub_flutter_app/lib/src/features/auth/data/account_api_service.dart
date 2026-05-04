import '../../../core/config/environment.dart';
import '../../../core/network/api_client.dart';

class AccountApiService {
  AccountApiService(this._apiClient);

  final ApiClient _apiClient;

  String get _tokenUrl => '${currentEnv.oAuthConfig.issuer}/connect/token';

  Future<Map<String, dynamic>> login({
    required String username,
    required String password,
  }) async {
    final params = <String, String>{
      'grant_type': 'password',
      'scope': currentEnv.oAuthConfig.scope,
      'username': username,
      'password': password,
      'client_id': currentEnv.oAuthConfig.clientId,
    };
    if (currentEnv.oAuthConfig.clientSecret != null) {
      params['client_secret'] = currentEnv.oAuthConfig.clientSecret!;
    }

    final data = await _apiClient.post(
      _tokenUrl,
      body: Uri(queryParameters: params).query,
      headers: <String, String>{'Content-Type': 'application/x-www-form-urlencoded'},
      isAbsolute: true,
    );
    return data as Map<String, dynamic>;
  }

  Future<Map<String, dynamic>> refreshToken(String refreshToken) async {
    final params = <String, String>{
      'grant_type': 'refresh_token',
      'refresh_token': refreshToken,
      'client_id': currentEnv.oAuthConfig.clientId,
    };
    if (currentEnv.oAuthConfig.clientSecret != null) {
      params['client_secret'] = currentEnv.oAuthConfig.clientSecret!;
    }
    final data = await _apiClient.post(
      _tokenUrl,
      body: Uri(queryParameters: params).query,
      headers: <String, String>{'Content-Type': 'application/x-www-form-urlencoded'},
      isAbsolute: true,
    );
    return data as Map<String, dynamic>;
  }

  Future<Map<String, dynamic>> register({
    required String userName,
    required String emailAddress,
    required String password,
  }) async {
    final data = await _apiClient.post(
      '/api/account/register',
      body: <String, dynamic>{
        'appName': currentEnv.appName,
        'userName': userName,
        'emailAddress': emailAddress,
        'password': password,
      },
    );
    return data as Map<String, dynamic>;
  }

  Future<void> sendPasswordResetCode({required String email}) async {
    await _apiClient.post(
      '/api/account/send-password-reset-code',
      body: <String, dynamic>{'appName': currentEnv.appName, 'email': email},
    );
  }

  Future<void> resetPassword({
    required String userId,
    required String resetToken,
    required String password,
  }) async {
    await _apiClient.post(
      '/api/account/reset-password',
      body: <String, dynamic>{
        'userId': userId,
        'resetToken': resetToken,
        'password': password,
        'returnUrl': currentEnv.appUrl,
        'returnUrlHash': '',
      },
    );
  }

  Future<Map<String, dynamic>> profilePictureById(String id) async {
    final data = await _apiClient.get('/api/account/profile-picture/$id');
    return data as Map<String, dynamic>;
  }

  Future<void> postProfilePicture({
    required int type,
    String? filePath,
  }) async {
    await _apiClient.postMultipart(
      '/api/account/profile-picture',
      fields: <String, String>{'type': '$type'},
      filePath: filePath,
    );
  }

  Future<String> profilePictureFileUrl(String userId) async {
    return '${currentEnv.apiUrl}/api/account/profile-picture-file/$userId';
  }
}
