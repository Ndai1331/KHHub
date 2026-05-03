// This file is part of MediaFilesClientProxy, you can customize it here
// ReSharper disable once CheckNamespace

using System.Threading.Tasks;
using Volo.Abp.Http.Client.ClientProxying;

namespace KHHub.MasterDataService.Services.MediaFiles;

public partial class MediaFilesClientProxy
{
    // public virtual async Task<string?> GetPresignedReadUrlByPublicPathAsync(string publicPath)
    // {
    //     return await RequestAsync<string?>(nameof(GetPresignedReadUrlByPublicPathAsync), new ClientProxyRequestTypeValue
    //     {
    //         { typeof(string), publicPath }
    //     });
    // }
}
