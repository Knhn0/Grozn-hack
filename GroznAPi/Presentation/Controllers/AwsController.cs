using CodePackage.Yandex.Storage;
using Contracts.Aws;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AwsController : BaseController
{
    private readonly IYandexObjectStorageManager _storage;
    private static string BaseUrl => "https://storage.yandexcloud.net/grozn-hack/";

    public AwsController(IYandexObjectStorageManager storage)
    {
        _storage = storage;
    }

    [HttpPost]
    public async Task<ActionResult> UploadFile(IFormFile formFile)
    {
        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);

        try
        {
            await _storage.Put(formFile.FileName, memoryStream);
            return Ok(new AwsFileUploadedResponse
            {
                IsUploaded = true,
                File = new AwsFileDto
                {
                    Url = BaseUrl + formFile.Name
                }
            });
        }
        catch
        {
            return Ok(new AwsFileUploadedResponse
            {
                IsUploaded = false,
                File = null
            });
        }
    }
}