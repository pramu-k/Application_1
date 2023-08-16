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
        private readonly ILogger<ProductController> logger;

        public ProductController(ILogger<ProductController>_logger)
        {
            logger = _logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> getAllProdcts()
        {
            logger.LogInformation("We are showing getAllProdcts");
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
                logger.LogError("Error");
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
        [ProducesResponseType(StatusCodes.Status201Created)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult <ProductDto> CreateProduct ([FromBody]ProductDto product)
        {
            if (product == null)
            {
                return BadRequest(product);
            }
            if(product.Id > 0)
            {
                return StatusCode(101);//Custom status codes
            }
            int MaxProductId = ProductDb.ProductList.OrderByDescending(p => p.Id).FirstOrDefault().Id;
            product.Id = MaxProductId + 1;
            ProductDb.ProductList.Add(product);
            return CreatedAtRoute("getProductById", new { id =product.Id},product);
        }
        [HttpDelete("{id:int}",Name ="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = ProductDb.ProductList.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            ProductDb.ProductList.Remove(product);
            return NoContent();
        }
        [HttpPut("{id:int}",Name="UpdateProduct")]
        public IActionResult UpdateProduct(int id,[FromBody]ProductDto productDto)
        {
            if(productDto==null || id!=productDto.Id)
            {
                return BadRequest();
            }
            var product = ProductDb.ProductList.FirstOrDefault(p => p.Id == id);
            product.Name= productDto.Name;
            return NoContent();
         
        }
    }

}
