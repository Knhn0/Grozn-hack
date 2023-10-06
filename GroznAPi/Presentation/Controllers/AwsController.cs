using CodePackage.Yandex.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AwsController : BaseController
{
    private readonly IYandexObjectStorageManager _storage;

    public AwsController(IYandexObjectStorageManager storage)
    {
        _storage = storage;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _storage.Get("ололол (1).pdf"));
    }
}