using BugBunty_Api.DTO;
using BugBunty_Api.Infrastucture.Authorisations;
using BugBunty_Api.Infrastucture.Models.Domaine;
using BugBunty_Api.Services.BLL.IServices;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BugBunty_Api.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _services;

        public UserController(IUserService services)
        {
            _services = services;
        }

        [HttpGet("GetTokenInfo")]
        [AuthorizeScope("full")]
        public IActionResult GetTokenInfo()
        {
            User.FindFirst(c => c.Type == ClaimTypes.Name);
            return Ok();/*new
            {
                Message = "Token reçu et validé",
                TokenInfo = new
                {
                    jwtToken.Issuer,
                    jwtToken.Audiences,
                    jwtToken.Subject,
                    jwtToken.ValidTo
                }
            });*/
        }


        [HttpPost("AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Adduser(CreateUser cu)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                User u = cu.Adapt<User>();
                _services.AddUser(u);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
