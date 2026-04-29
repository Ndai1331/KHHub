import api from './API';

export const getApplicationConfiguration = () =>
  api
    .get('/api/abp/application-configuration', {
      params: {
        IncludeLocalizationResources: false,
      },
    })
    .then(({ data }) => data);
