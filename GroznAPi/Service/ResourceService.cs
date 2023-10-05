using Repository;
using Service.Abstactions;

namespace Service;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    
    public ResourceService(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }
}