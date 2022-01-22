using Microsoft.AspNetCore.Mvc;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesManager.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repo;
        private readonly IConfiguration _config;
        private string erroConnection = "Connection failed!";
        public ProductController(ProductRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        
        [HttpGet]
        [Route("products")]
        [Authorize]
        [SwaggerOperation(Summary = "Returns product list", Description = "Returns product list")]
        [SwaggerResponse(200, "Returns item list")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> ListAsync([FromQuery]ProductFilter model)
        {
            try
            {
                return Ok(await _repo.ListAsync(model));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erroConnection);
            }
        }

        [HttpPost]
        [Route("products")]
        [Authorize]
        [SwaggerOperation(Summary = "Insert new product", Description = "Insert new product")]
        [SwaggerResponse(201, "New item created")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> Post([FromBody]ProductInput model)
        {
            try
            {
                Product prod = new Product(model.Name, model.Description, model.Price).SetCreationDateNow();
                _repo.Add(prod);
                if(await _repo.SaveChangesAsync())
                {
                    return Created(prod.Id.ToString(), prod);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erroConnection);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("products")]
        [Authorize]
        [SwaggerOperation(Summary = "Change product details", Description = "Change product details")]
        [SwaggerResponse(201, "Changed item")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> Put([FromBody]ProductUpdate model)
        {
            try
            {
                var prod = await _repo.GetByIdAsync(model.Id);
                if(prod == null)
                {
                    return NotFound();
                }

                _repo.Update(prod.SetProduct(model));
                if(await _repo.SaveChangesAsync())
                {
                    return Created(prod.Id.ToString(), prod);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erroConnection);
            }

            return BadRequest();
        }
    }
}