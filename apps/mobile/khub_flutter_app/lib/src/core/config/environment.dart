class OAuthConfig {
  const OAuthConfig({
    required this.issuer,
    required this.clientId,
    required this.scope,
    this.clientSecret,
  });

  final String issuer;
  final String clientId;
  final String scope;
  final String? clientSecret;
}

class Environment {
  const Environment({
    required this.apiUrl,
    required this.appUrl,
    required this.appName,
    required this.oAuthConfig,
    required this.defaultResourceName,
  });

  final String apiUrl;
  final String appUrl;
  final String appName;
  final OAuthConfig oAuthConfig;
  final String defaultResourceName;
}

const Environment currentEnv = Environment(
  apiUrl: 'https://api.khub.id.vn',
  appUrl: 'https://khub.id.vn',
  appName: 'ReactNative',
  oAuthConfig: OAuthConfig(
    issuer: 'https://auth.khub.id.vn',
    clientId: 'ReactNative',
    scope: 'offline_access AuthServer IdentityService AdministrationService',
  ),
  defaultResourceName: 'KHHub',
);
