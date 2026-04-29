import * as ImagePicker from 'expo-image-picker';
import { useState, useEffect, useContext } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { View, Text, Pressable, Image, Platform, ActivityIndicator } from 'react-native';

import type { ProfilePictureScreenProps } from '../../navigators/types';
import { getEnvVars } from '../../../Environment';
import { FormButtons } from '../../components';
import { useThemeColors } from '../../hooks';
import { CrossPlatformAlert } from '../../utils/AlertPolyfill';
import { LocalizationContext } from '../../contexts/LocalizationContext';
import { profilePictureById, postProfilePicture } from '../../api/AccountAPI';
import AppActions from '../../store/actions/AppActions';
import LoadingActions from '../../store/actions/LoadingActions';
import { appConfigSelector, profilePictureSelector } from '../../store/selectors/AppSelectors';

const { apiUrl } = getEnvVars();

const imageOptions: ImagePicker.ImagePickerOptions = {
  mediaTypes: ['images'],
  allowsEditing: true,
  aspect: [4, 3],
  quality: 1,
  base64: true,
};

function ProfilePictureScreen(_props: ProfilePictureScreenProps) {
  const { accentColor } = useThemeColors();
  const dispatch = useDispatch();
  const { t } = useContext(LocalizationContext);
  const { currentUser } = useSelector(appConfigSelector) ?? {};
  const profilePicture = useSelector(profilePictureSelector);
  const isLoading = useSelector(
    (state: { loading: { activeLoadings?: Record<string, unknown> } }) =>
      !!state.loading?.activeLoadings?.['profilePicture'],
  );
  const defaultImage = `${apiUrl}/api/account/profile-picture-file/${currentUser.id}`;

  const [profileOption, setProfileOption] = useState(0);
  const [image, setImage] = useState(null);

  const startLoading = (key: { key: string }) => dispatch(LoadingActions.start(key));
  const clearLoading = (key: { key: string }) => dispatch(LoadingActions.stop(key));

  const pickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync(imageOptions);
    if (!result.canceled) {
      setImage(result.assets[0]);
      dispatch(AppActions.setProfilePicture(`data:image/png;base64,${result.assets[0].base64}`));
    }
  };

  const takePhoto = async () => {
    if (Platform.OS === 'ios') {
      const { status } = await ImagePicker.requestCameraPermissionsAsync();
      if (status !== 'granted') {
        CrossPlatformAlert.alert('Permission Denied', 'Sorry, we need camera permissions to take a photo.');
        return;
      }
    }
    const result = await ImagePicker.launchCameraAsync(imageOptions);
    if (!result.canceled) {
      setImage(result.assets[0]);
      dispatch(AppActions.setProfilePicture(`data:image/png;base64,${result.assets[0].base64}`));
    }
  };

  const submit = async () => {
    startLoading({ key: 'profilePicture' });
    if (profileOption === 2 && !image) {
      clearLoading({ key: 'profilePicture' });
      return;
    }

    const formData = new FormData();
    formData.append('type', profileOption.toString());

    if (profileOption === 2) {
      const { uri } = image;
      const uriParts = uri.split('.');
      const fileType = uriParts[uriParts.length - 1];
      const name = `photo.${fileType}`;
      const type = `image/${fileType}`;

      if (Platform.OS === 'web') {
        const res = await fetch(uri);
        const blob = await res.blob();
        formData.append('imageContent', blob, name);
      } else {
        formData.append('imageContent', { uri, name, type } as any);
      }
      dispatch(AppActions.setProfilePicture(uri));
    }

    postProfilePicture(formData)
      .then(() => {
        if (profileOption === 0) {
          dispatch(AppActions.setProfilePicture(defaultImage));
          return;
        }
        if (profileOption === 1) {
          profilePictureById(currentUser.id)
            .then(({ source }) => { if (source) dispatch(AppActions.setProfilePicture(source)); })
            .catch(error => console.warn('Failed to fetch Gravatar after submission:', error))
            .finally(() => clearLoading({ key: 'profilePicture' }));
          return;
        }
      })
      .finally(() => clearLoading({ key: 'profilePicture' }));
  };

  const changeOption = (avatarOption: number) => {
    setProfileOption(avatarOption);
    switch (avatarOption) {
      case 0:
        dispatch(AppActions.setProfilePicture(defaultImage));
        break;
      case 1:
        profilePictureById(currentUser.id)
          .then(({ source }) => { if (source) dispatch(AppActions.setProfilePicture(source)); })
          .catch(error => console.warn('Failed to fetch Gravatar:', error));
        break;
    }
  };

  useEffect(() => {
    if (!currentUser?.id) return;
    startLoading({ key: 'profilePicture' });
    profilePictureById(currentUser.id)
      .then((data = {}) => {
        const { type, source, fileContent } = data || {};
        setProfileOption(type);
        switch (type) {
          case 0: dispatch(AppActions.setProfilePicture(defaultImage)); break;
          case 1: if (source) dispatch(AppActions.setProfilePicture(source)); break;
          case 2: if (fileContent) dispatch(AppActions.setProfilePicture(`data:image/png;base64,${fileContent}`)); break;
        }
      })
      .catch(error => {
        console.warn('Failed to fetch profile picture:', error);
        dispatch(AppActions.setProfilePicture(defaultImage));
      })
      .finally(() => clearLoading({ key: 'profilePicture' }));
  }, [currentUser.id, dispatch, defaultImage]);

  const radioOptions = [
    { value: 0, label: t('AbpAccount::UseDefault') },
    { value: 1, label: t('AbpAccount::DisplayName:UseGravatar') },
    { value: 2, label: t('AbpAccount::SelectNewImage') },
  ];

  return (
    <View className="flex-1 bg-background dark:bg-background-dark px-6">
      {/* Avatar */}
      <View className="items-center mt-6">
        {isLoading ? (
          <View className="w-[100px] h-[100px] items-center justify-center">
            <ActivityIndicator size="large" color={accentColor} />
          </View>
        ) : profilePicture ? (
          <Image
            source={{ uri: profilePicture }}
            className="w-[100px] h-[100px] rounded-full border-2 border-accent/20 dark:border-accent-dark/20 shadow-sm"
          />
        ) : (
          <View className="w-[100px] h-[100px] rounded-full bg-accent dark:bg-accent-dark items-center justify-center shadow-sm">
            <Text className="text-accent-foreground dark:text-accent-dark-foreground text-3xl font-semibold">
              {currentUser?.name?.charAt(0)?.toUpperCase() || 'U'}
            </Text>
          </View>
        )}
      </View>

      {/* Radio Options */}
      <View className="mt-6 mb-4">
        {radioOptions.map(option => (
          <Pressable
            key={option.value}
            onPress={() => changeOption(option.value)}
            className="flex-row items-center py-3 active:bg-muted dark:active:bg-muted-dark rounded-lg px-1">
            <View
              className={`w-[18px] h-[18px] rounded-full border-2 items-center justify-center ${
                profileOption === option.value
                  ? 'border-accent dark:border-accent-dark'
                  : 'border-input dark:border-input-dark'
              }`}>
              {profileOption === option.value && (
                <View className="w-[10px] h-[10px] rounded-full bg-accent dark:bg-accent-dark" />
              )}
            </View>
            <Text className="ml-3 text-[15px] text-foreground dark:text-foreground-dark">
              {option.label}
            </Text>
          </Pressable>
        ))}

        {profileOption === 2 ? (
          <View className="flex-row mt-3 mb-4 gap-x-3">
            <Pressable
              onPress={takePhoto}
              className="flex-1 rounded-lg py-3 items-center border border-border dark:border-border-dark active:bg-muted dark:active:bg-muted-dark">
              <Text className="text-foreground dark:text-foreground-dark font-medium text-sm">
                {t('AbpAccount::TakePhoto')}
              </Text>
            </Pressable>
            <Pressable
              onPress={pickImage}
              className="flex-1 rounded-lg py-3 items-center border border-border dark:border-border-dark active:bg-muted dark:active:bg-muted-dark">
              <Text className="text-foreground dark:text-foreground-dark font-medium text-sm">
                {t('AbpAccount::ChoosePhoto')}
              </Text>
            </Pressable>
          </View>
        ) : null}
      </View>

      <FormButtons submit={submit} />
    </View>
  );
}

export default ProfilePictureScreen;
