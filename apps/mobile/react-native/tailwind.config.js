/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    './App.tsx',
    './src/**/*.{js,jsx,ts,tsx}',
  ],
  presets: [require('nativewind/preset')],
  theme: {
    extend: {
      colors: {
        // shadcn/ui semantic colors - Light mode defaults, dark: overrides in className
        background: { DEFAULT: '#ffffff', dark: '#09090b' },
        foreground: { DEFAULT: '#09090b', dark: '#fafafa' },
        card: { DEFAULT: '#ffffff', dark: '#18181b' },
        'card-border': { DEFAULT: '#e4e4e7', dark: '#27272a' },
        primary: { DEFAULT: '#18181b', dark: '#fafafa', foreground: '#fafafa', 'dark-foreground': '#18181b' },
        secondary: { DEFAULT: '#f4f4f5', dark: '#27272a', foreground: '#18181b', 'dark-foreground': '#fafafa' },
        muted: { DEFAULT: '#f4f4f5', dark: '#27272a', foreground: '#71717a', 'dark-foreground': '#a1a1aa' },
        accent: { DEFAULT: '#18181b', dark: '#fafafa', foreground: '#fafafa', 'dark-foreground': '#18181b' },
        destructive: { DEFAULT: '#ef4444', dark: '#dc2626', foreground: '#ffffff' },
        border: { DEFAULT: '#e4e4e7', dark: '#27272a' },
        input: { DEFAULT: '#d4d4d8', dark: '#3f3f46' },
        ring: { DEFAULT: '#18181b', dark: '#fafafa' },
      },
      spacing: {
        xs: '4px',
        sm: '8px',
        md: '16px',
        lg: '24px',
        xl: '32px',
      },
      borderRadius: {
        sm: '4px',
        md: '8px',
        lg: '12px',
        xl: '16px',
        '2xl': '20px',
      },
    },
  },
  plugins: [],
};
