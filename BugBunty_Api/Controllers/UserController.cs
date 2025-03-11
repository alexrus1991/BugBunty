using BugBunty_Api.DTO;
using BugBunty_Api.Infrastucture.Models.Domaine;
using BugBunty_Api.Services.BLL.IServices;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BugBunty_Api.Controllers
{
    [Authorize]
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
        public IActionResult GetTokenInfo()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
                return Unauthorized("Aucun token fourni");

            // Supprimer "Bearer " du token
            token = token.Replace("Bearer ", "");

            // Lire le token JWT
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return Ok(new
            {
                Message = "Token reçu et validé",
                TokenInfo = new
                {
                    jwtToken.Issuer,
                    jwtToken.Audiences,
                    jwtToken.Subject,
                    jwtToken.ValidTo
                }
            });
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
