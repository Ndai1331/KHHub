import '../../../core/network/api_client.dart';

class IdentityApiService {
  IdentityApiService(this._apiClient);

  final ApiClient _apiClient;

  Future<void> changePassword({
    required String currentPassword,
    required String newPassword,
  }) async {
    await _apiClient.post(
      '/api/account/my-profile/change-password',
      body: <String, dynamic>{
        'currentPassword': currentPassword,
        'newPassword': newPassword,
        'newPasswordConfirm': newPassword,
      },
    );
  }
}
