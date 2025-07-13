using HotelListing.Api.Contracts;
using HotelListing.Api.Data.DTO.User;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AccountController(IAuthManager authManager)
        {
            this._authManager = authManager;
        }

        [HttpPost]
        //When a request is made to something like https://example.com/register, it will trigger this method.
        [Route("register")]
        //tells client what kind of response it can expect
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //[FromBody] binds the DTO (apiUserDTO) or model to the JSON payload sent by the client and received by the server.
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Register([FromBody] ApiUserDTO apiUserDTO)
        {
            //This line checks whether the incoming JSON body successfully
            //mapped to the ApiUserDTO object(i.e., model binding worked)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var errors = await _authManager.Register(apiUserDTO);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("Account saved successfully");
        }

        [HttpPost]
        //When a request is made to something like https://example.com/register, it will trigger this method.
        [Route("login")]
        //tells client what kind of response it can expect
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //[FromBody] binds the DTO (apiUserDTO) or model to the JSON payload sent by the client and received by the server.
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var authResponse = await _authManager.Login(loginDTO);
            if (authResponse == null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);
        }



        [HttpPost]
        //When a request is made to something like https://example.com/api/Account/refreshtoken, it will trigger this method.
        [Route("refreshtoken")]
        //tells client what kind of response it can expect
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //[FromBody] binds the DTO (apiUserDTO) or model to the JSON payload sent by the client and received by the server.
        public async Task<IActionResult> RefreshToken([FromBody] AuthResponseDTO request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);
            if (authResponse == null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);
        }
    }
}
