using Test.Dtos;

namespace Test.Services
{
    public interface IProductService
    {
        public Task<bool> CreateProductAsync(List<CreateRequestDto> models);
        public Task<List<CreateRequestDto>> CopyProductFromWebsiteAsync(string url);
    }
}
