using Microsoft.AspNetCore.Mvc;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesManager.Api.Controllers
{
    [ApiController]
    public class UserControler : ControllerBase
    {
        private readonly UserRepository _repo;
        private readonly IConfiguration _config;
        private string erroConnection = "Connection failed!";
        public UserControler(UserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpGet]
        [Route("users")]
        [Authorize]
        [SwaggerOperation(Summary = "Returns user list", Description = "Returns user list")]
        [SwaggerResponse(200, "Returns user list")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> ListAsync([FromQuery]UserFilter model)
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

    }
}