# Upgrade Guide

## Upgrading Expo SDK

1. Run `npx expo install expo@latest` to upgrade Expo
2. Run `npx expo install --fix` to align all Expo packages with the SDK version
3. Run `npx expo-doctor` to check for compatibility issues
4. Review [Expo changelog](https://github.com/expo/expo/blob/main/CHANGELOG.md) for breaking changes

## Upgrading React Native

Expo manages the React Native version. When upgrading Expo SDK, React Native is typically upgraded as well. Check the [Expo SDK compatibility](https://docs.expo.dev/versions/latest/) for the React Native version that ships with each SDK.

## Local Development with HTTPS

The template uses `https://localhost:44300` for the API. To run the backend and app:

1. Start the backend (HttpApi.Host) - it typically runs on port 44300 with HTTPS
2. Generate SSL certs for the local proxy: `mkcert localhost` (creates `localhost.pem` and `localhost-key.pem`)
3. Add certs to `.gitignore` - they should not be committed
4. Run `yarn create:local-proxy` to proxy port 443 to 8081 for the Metro bundler
5. Run `yarn start` in another terminal

## Tested Versions

- Expo SDK 54
- React Native 0.81.5
- React 19.1.0
