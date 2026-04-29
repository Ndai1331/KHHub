import type { NativeStackScreenProps } from '@react-navigation/native-stack';

export type BottomTabParamList = {
  HomeTab: undefined;
  SettingsTab: undefined;
  AccountTab: undefined;
};

type HomeStackParamList = {
  Home: undefined;
};

type SettingsStackParamList = {
  Settings: undefined;
};

type AccountStackParamList = {
  Account: undefined;
  Login: undefined;
  Register: undefined;
  ForgotPassword: undefined;
  ResetPassword: { userId?: string; resetToken?: string } | undefined;
  ChangePassword: undefined;
  ProfilePicture: undefined;
};

export type HomeScreenProps = NativeStackScreenProps<HomeStackParamList, 'Home'>;
export type SettingsScreenProps = NativeStackScreenProps<SettingsStackParamList, 'Settings'>;
export type AccountScreenProps = NativeStackScreenProps<AccountStackParamList, 'Account'>;
export type LoginScreenProps = NativeStackScreenProps<AccountStackParamList, 'Login'>;
export type RegisterScreenProps = NativeStackScreenProps<AccountStackParamList, 'Register'>;
export type ForgotPasswordScreenProps = NativeStackScreenProps<AccountStackParamList, 'ForgotPassword'>;
export type ResetPasswordScreenProps = NativeStackScreenProps<AccountStackParamList, 'ResetPassword'>;
export type ChangePasswordScreenProps = NativeStackScreenProps<AccountStackParamList, 'ChangePassword'>;
export type ProfilePictureScreenProps = NativeStackScreenProps<AccountStackParamList, 'ProfilePicture'>;
