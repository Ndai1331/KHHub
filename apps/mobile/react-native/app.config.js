export default {
  expo: {
    name: 'KHHub',
    slug: 'KHHub',
    scheme: 'khhub',
    version: '1.0.0',
    orientation: 'portrait',
    userInterfaceStyle: 'automatic',
    icon: './assets/icon.png',
    newArchEnabled: true,
    splash: {
      image: './assets/splash.png',
      resizeMode: 'cover',
      backgroundColor: '#38003c',
    },
    updates: {
      fallbackToCacheTimeout: 0,
    },
    assetBundlePatterns: ['**/*'],
    ios: {
      supportsTablet: true,
      bundleIdentifier: 'com.KHHub',
      buildNumber: '1.0.0',
      infoPlist: {
        NSCameraUsageDescription: 'This app uses the camera to scan barcodes on event tickets.',
      },
    },
    android: {
      package: 'com.KHHub',
      versionCode: 1,
      adaptiveIcon: {
        foregroundImage: './assets/adaptive-icon.png',
        backgroundColor: '#FFFFFF',
      },
    },
    web: {
      favicon: './assets/icon.png',
    },
    plugins: [
      [
        'expo-image-picker',
        {
          photosPermission: 'The app accesses your photos to let you share them with your friends.',
        },
      ],
    ],
  },
};
