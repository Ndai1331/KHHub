import { View } from 'react-native';
import { useDispatch } from 'react-redux';

import type { ChangePasswordScreenProps } from '../../navigators/types';
import { changePassword } from '../../api/IdentityAPI';
import LoadingActions from '../../store/actions/LoadingActions';
import ChangePasswordForm from './ChangePasswordForm';

function ChangePasswordScreen({ navigation }: ChangePasswordScreenProps) {
  const dispatch = useDispatch();

  const submit = (data: { currentPassword: string; newPassword: string }) => {
    dispatch(LoadingActions.start({ key: 'changePassword' }));

    changePassword(data)
      .then(() => navigation.goBack())
      .finally(() => dispatch(LoadingActions.stop({ key: 'changePassword' })));
  };

  return (
    <View className="flex-1 bg-background dark:bg-background-dark">
      <ChangePasswordForm submit={submit} cancel={() => navigation.goBack()} />
    </View>
  );
}

export default ChangePasswordScreen;
