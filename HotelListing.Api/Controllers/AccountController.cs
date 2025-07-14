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
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
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
            _logger.LogInformation($"Registering a new user with email: {apiUserDTO.Email}");
            //This line checks whether the incoming JSON body successfully
            //mapped to the ApiUserDTO object(i.e., model binding worked)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
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
            catch (Exception)
            {
                _logger.LogError($"Something went wrong in the {nameof(Register)} method. Registration was attempted for {apiUserDTO.Email}");
                return Problem(title: "Database save failure",
                    detail: "Something went wrong while saving the account. Please try again later.", 
                    statusCode: 500);
            }

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
            _logger.LogInformation($"Attempting to log in user with email: {loginDTO.Email}");
            try
            {
                var authResponse = await _authManager.Login(loginDTO);
                if (authResponse == null)
                {
                    return Unauthorized();
                }
                return Ok(authResponse);
            }
            catch (Exception)
            {
               _logger.LogError($"Something went wrong in the {nameof(Login)} method. Login was attempted for {loginDTO.Email}.");
                return Problem(title: "Login failure",
                    detail: "Something went wrong while logging in. Please try again later.",
                    statusCode: 500);
            }
            
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
