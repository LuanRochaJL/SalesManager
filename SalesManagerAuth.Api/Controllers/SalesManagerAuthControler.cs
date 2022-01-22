using Microsoft.AspNetCore.Mvc;
using SalesManager.Domain.Entities;
using SalesManager.Repository.Repositories;
using SalesManager.Repository.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SalesManagerAuth.Api.Controllers
{
    [ApiController]
    public class SalesManagerAuthControler : ControllerBase
    {
        private readonly UserRepository _repo;
        private readonly IConfiguration _config;
        private string erroConnection = "Connection failed!";
        public SalesManagerAuthControler(UserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost]
        [Route("signin")]
        [SwaggerOperation(Summary = "Authenticate user", Description = "Authenticate user")]
        [SwaggerResponse(200, "User Authenticated")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<ActionResult<dynamic>> Authenticator([FromBody]UserAuthenticator model)
        {
            try
            {
                var user = await _repo.AuthenticatorAsync(model);

                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos!" });

                var token = TokenService.CreateToken(user.UserName, _config.GetConnectionString("secret"));

                return Ok
                (
                    new
                    {
                        user = user,
                        token = token
                    }
                );
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erroConnection);
            }
        }

        [HttpPost]
        [Route("signup")]
        [SwaggerOperation(Summary = "Insert new user", Description = "Insert new user")]
        [SwaggerResponse(201, "New user created")]
        [SwaggerResponse(500, "Connection failed")]
        public async Task<IActionResult> Post([FromBody]UserInput model)
        {
            try
            {
                User user = new User(model.UserName, model.Password, model.Name, model.Email).SetCreationDateNow();
                _repo.Add(user);
                if(await _repo.SaveChangesAsync())
                {
                    return Created(user.Id.ToString(), user);
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