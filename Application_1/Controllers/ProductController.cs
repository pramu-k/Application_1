using Application_1.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application_1.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDto> getAllProdcts()
        {
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1,Name="Soap"},
                new ProductDto { Id = 2,Name="Toothpaste"}
            };
            return products;
        }
    }
}
