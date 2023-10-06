using CodePackage.Yandex.Storage;
using Contracts.Aws;

namespace Service;

public class AwsService
{
    private readonly IYandexObjectStorageManager _storage;
    private static string BaseUrl => "https://storage.yandexcloud.net/grozn-hack/";

    public AwsService(IYandexObjectStorageManager storage)
    {
        _storage = storage;
    }
    
    public async Task<AwsFileUploadedResponse> UploadFile(AwsFileUploadDto dto)
    {
        try
        {
            await _storage.Put(dto.FileName, dto.Stream);
            return new AwsFileUploadedResponse
            {
                IsUploaded = true,
                File = new AwsFileDto
                {
                    Url = BaseUrl + dto.FileName,
                    Type = dto.Type
                }
            };
        }
        catch
        {
            return new AwsFileUploadedResponse
            {
                IsUploaded = false,
                File = null
            };
        }
    }
}