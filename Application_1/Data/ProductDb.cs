using Application_1.Models.Dto;

namespace Application_1.Data
{
    public static class ProductDb
    {
        public static List<ProductDto> ProductList = new List<ProductDto>
            {
                new ProductDto { Id = 1,Name="Soap"},
                new ProductDto { Id = 2,Name="Toothpaste"}
            };
    }
}
