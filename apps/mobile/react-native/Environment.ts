import { Environment } from '@khhub/models';

const gatewayPort = 44369;
const authServerPort = 44315;

const apiUrl = `http://localhost:${gatewayPort}`;
const appUrl = 'http://localhost';

const dev: Environment = {
  apiUrl,
  appUrl,
  appName: 'ReactNative',
  oAuthConfig: {
    issuer: `http://localhost:${authServerPort}`,
    clientId: 'ReactNative',
    scope:
      'offline_access AuthServer IdentityService AdministrationService',
  },
  localization: {
    defaultResourceName: 'KHHub',
  },
};

const prod: Environment = { ...dev };

export const getEnvVars = () => (__DEV__ ? dev : prod);
