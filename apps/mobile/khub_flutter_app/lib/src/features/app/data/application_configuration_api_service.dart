import '../../../core/network/api_client.dart';

class ApplicationConfigurationApiService {
  ApplicationConfigurationApiService(this._apiClient);

  final ApiClient _apiClient;

  Future<Map<String, dynamic>> getApplicationConfiguration() async {
    final data = await _apiClient.get(
      '/api/abp/application-configuration',
      queryParams: <String, dynamic>{'IncludeLocalizationResources': false},
    );
    return data as Map<String, dynamic>;
  }
}
