using refactor_me.Data;
using refactor_me.Interfaces;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;
        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
        }

        [HttpGet]
        public async Task<Products> GetAll()
        {
            return new Products() { Items = await _productService.GetAllAsync() };
        }

        [HttpGet]
        public async Task<Products> SearchByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                string message = string.Format("Please provide a product name");
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            return new Products() { Items = new List<Product>() { await _productService.GetByName(name) } };
        }

        [HttpGet]
        public async Task<Product> GetProduct(Guid id)
        {
            Product product = await _productService.GetByIdAsync(id);
            if(product == null)
            {
                string message = string.Format("Product with id = {0} not found", id);
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else
            {
                return product;
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.CreateAsync(product);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, [FromBody] Product product)
        {
            //INFO: Validate input parameter. Check empty Guid to avoid querying database
            if(Guid.Equals(id, default(Guid)) || !Guid.Equals(id, product.Id) || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productService.UpdateAsync(product);

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            if (Guid.Equals(id, default(Guid)))
            {
                return BadRequest();
            }

            await _productService.DeleteAsync(id);
            return Ok();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return new ProductOptions() { Items =  _productOptionService.GetOptionsByProductId(productId) };
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public async Task<ProductOption> GetOption(Guid productId, Guid id)
        {
            var productOption = await _productOptionService.GetByIdAsync(id);

            if(productOption == null)
            {
                string message = string.Format("Product option with id = {0} not found", id);
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            if (!Guid.Equals(productId, productOption.ProductId))
            {
                string message = string.Format("Product option with id = {0} does not match with product id = {1}", id, productId);
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            return productOption;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOption(Guid productId, [FromBody] ProductOption option)
        {
            if (!Guid.Equals(productId, option.ProductId) || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await _productService.GetByIdAsync(productId);

            if(product == null)
            {
                return BadRequest();
            }

            await _productOptionService.CreateAsync(option);
            return Ok();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOption(Guid productId, Guid id, [FromBody] ProductOption option)
        {
            if (Guid.Equals(id, default(Guid)) || Guid.Equals(productId, default(Guid)) || 
                !Guid.Equals(productId, option.ProductId) || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await _productService.GetByIdAsync(productId);

            if (product == null)
            {
                return BadRequest();
            }

            await _productOptionService.UpdateAsync(option);
            return Ok();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteOption(Guid productId, Guid id)
        {
            if (Guid.Equals(id, default(Guid)) || Guid.Equals(productId, default(Guid)))
            {
                return BadRequest();
            }

            var product = await _productService.GetByIdAsync(productId);

            if (product == null)
            {
                return BadRequest();
            }

            await _productOptionService.DeleteAsync(id);
            return Ok();
        }
    }
}
