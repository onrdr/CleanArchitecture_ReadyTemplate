using ApplicationCore.Interfaces.Services;
using Integration.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Base;

[Collection("Database collection")]
public class TestBase
{
    public ApplicationFixture ApplicationFixture { get; }

    public ICategoryService CategoryService
        => ApplicationFixture.Services.GetRequiredService<ICategoryService>();

    public IProductService ProductService
        => ApplicationFixture.Services.GetRequiredService<IProductService>(); 

    protected TestBase(ApplicationFixture applicationFixture)
    {
        ApplicationFixture = applicationFixture;
    }
}
