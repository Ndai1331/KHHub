import i18n from 'i18n-js';
import { getEnvVars } from '../../Environment';

// Language configuration - single source of truth
// To add a new language, just add an entry here
// To add a new language: add a JSON file in src/locales/ and an entry here
const LANGUAGE_CONFIG: Record<
  string,
  {
    displayName: string;
    getTranslations: () => Record<string, unknown>;
  }
> = {
  en: {
    displayName: 'English',
    getTranslations: () => require('../locales/en.json'),
  },
  tr: {
    displayName: 'Türkçe',
    getTranslations: () => require('../locales/tr.json'),
  },
};

// Generate AVAILABLE_LANGUAGES array from configuration
export const AVAILABLE_LANGUAGES = Object.entries(LANGUAGE_CONFIG).map(([cultureName, config]) => ({
  cultureName,
  displayName: config.displayName,
  uiCultureName: cultureName,
}));

// Default language
const DEFAULT_RESOURCE_NAME = getEnvVars().localization.defaultResourceName;

// Load translations for a specific locale
export function loadTranslations(locale: string): void {
  // Get language config or fallback to English
  const languageConfig = LANGUAGE_CONFIG[locale] || LANGUAGE_CONFIG.en;
  const translations = languageConfig.getTranslations();

  // Set locale and translations (i18n-js handles nested objects with :: separator)
  i18n.locale = locale;
  i18n.translations[locale] = translations;

  // Ensure default resource is available
  if (translations[DEFAULT_RESOURCE_NAME]) {
    i18n.translations[locale][DEFAULT_RESOURCE_NAME] = translations[DEFAULT_RESOURCE_NAME];
  }
}

// Initialize i18n configuration and custom translation function
export function initializeI18n(): void {
  i18n.defaultSeparator = '::';

  // Add missing translation callback to prevent errors
  i18n.missingTranslation = key => {
    console.warn(`Missing translation key: ${key}`);

    const currentLocale = i18n.locale;
    const hasTranslations =
      currentLocale &&
      i18n.translations[currentLocale] &&
      Object.keys(i18n.translations[currentLocale]).length > 0;

    if (!hasTranslations) {
      return key;
    }

    if (key.includes('::')) {
      const fallback = key.split('::').pop();
      return fallback || key;
    }

    return key;
  };

  // Custom translation function following Angular ABP pattern
  const cloneT = i18n.t;
  i18n.t = (key, ...args) => {
    const currentLocale = i18n.locale;
    const translations = i18n.translations[currentLocale];

    if (!translations) {
      return cloneT(key, ...args);
    }

    if (!key || typeof key !== 'string') {
      return cloneT(key, ...args);
    }

    // Handle :: prefix (default resource name) - similar to Angular ABP
    if (key.slice(0, 2) === '::') {
      const sourceKey = key.slice(2);
      key = getEnvVars().localization.defaultResourceName + '::' + sourceKey;
    }

    // Split by :: to get resource name and key (following Angular ABP pattern)
    const keys = key.split('::');

    if (keys.length < 2) {
      // No :: separator found, fallback to original behavior
      return cloneT(key, ...args);
    }

    // Get resource name (first part) or use default
    const sourceName = keys[0] || getEnvVars().localization.defaultResourceName;

    // Get the source key (second part) - this may contain : for nested paths
    const sourceKey = keys[1];

    if (!sourceName) {
      return cloneT(key, ...args);
    }

    // Get the resource object
    const source = translations[sourceName];
    if (!source) {
      return cloneT(key, ...args);
    }

    // Navigate nested path using : separator (for keys like DisplayName:IsActive)
    const pathParts = sourceKey.split(':').filter(part => part.length > 0);
    let localization = source;

    // Navigate through nested structure
    for (const part of pathParts) {
      if (localization && typeof localization === 'object' && part in localization) {
        localization = localization[part];
      } else {
        // Key not found, return the last part of the key as fallback
        return sourceKey;
      }
    }

    // Handle interpolation if args provided (similar to Angular ABP)
    if (typeof localization === 'string') {
      if (args.length > 0 && args[0]) {
        const params = args[0];
        localization = localization.replace(/\{(\d+)\}/g, (match, index) => {
          return params[index] !== undefined ? params[index] : match;
        });
      }
      return localization;
    }

    // Fallback to sourceKey if localization is not a string
    return sourceKey;
  };
}
