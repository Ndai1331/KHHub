import api from './API';

export const changePassword = body =>
  api.post('/api/account/my-profile/change-password', body).then(({ data }) => data);
