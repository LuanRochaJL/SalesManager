using Microsoft.AspNetCore.Mvc;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesManager.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repo;
        private readonly IConfiguration _config;
        private string erroConnection = "Connection failed!";
        public OrderController(OrderRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpGet]
        [Route("orders")]
        [Authorize]
        [SwaggerOperation(Summary = "Returns order list", Description = "Returns order list")]
        [SwaggerResponse(200, "Returns order list")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> ListAsync([FromQuery]OrderFilter model)
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
        [Route("orders")]
        [Authorize]
        [SwaggerOperation(Summary = "Insert new order", Description = "Insert new order")]
        [SwaggerResponse(201, "New item created")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> Post([FromBody]OrderInput model)
        {
            try
            {
                
                List<OrderProduct> orderProducts = new List<OrderProduct>();
                foreach (var item in model.OrderProducts)
                {
                    orderProducts.Add(new OrderProduct(item.OrderId, item.ProductId, item.Quantity));
                }
                Order order = new Order(model.UserId).SetCreationDateNow();
                order.OrderProducts = orderProducts;
                _repo.Add(order);
                if(await _repo.SaveChangesAsync())
                {
                    return Created(order.Id.ToString(), new{ Id = order.Id});
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