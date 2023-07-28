using Application_1.Data;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> getAllProdcts()
        {
            return Ok(ProductDb.ProductList);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> getProductById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var product = ProductDb.ProductList.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
