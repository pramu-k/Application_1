using Application_1.Data;
using Application_1.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

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
        [HttpGet("{id:int}",Name = "getProductById")]
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
            return Ok(product); //return200 status code
        }
        [HttpPost]
        public ActionResult <ProductDto> CreateProduct ([FromBody]ProductDto product)
        {
            if (product == null)
            {
                return BadRequest(product);
            }
            if(product.Id > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);//Custom status codes
            }
            int MaxProductId = ProductDb.ProductList.OrderByDescending(p => p.Id).FirstOrDefault().Id;
            product.Id = MaxProductId + 1;
            ProductDb.ProductList.Add(product);
            return CreatedAtRoute("getProductById", new { id =product.Id},product);
        }
    }
}
