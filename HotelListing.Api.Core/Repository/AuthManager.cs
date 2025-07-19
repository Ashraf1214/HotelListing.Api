using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data.DTO.User;
using HotelListing.Api.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelListing.Api.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Apiuser> _userManager;
        private readonly IConfiguration _configuration;
        private Apiuser _user;

        private const string _loginProvider = "HotelListingAPI";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<Apiuser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<AuthResponseDTO> Login(LoginDTO loginUserDTO)
        {
            //Checking the DB for user with this email
            _user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            var isValid = await _userManager.CheckPasswordAsync(_user, loginUserDTO.Password);
            if (_user is null || !isValid)
            {
                return null;
            }
            //Calling GenerateToken to generate JWT
            var token = await GenerateToken();
            return new AuthResponseDTO
            {
                Token = token,
                UserId = _user.Id,
            };
        }
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO apiuserDTO)
        {
            _user = _mapper.Map<Apiuser>(apiuserDTO);
            _user.UserName = apiuserDTO.Email;

            var result = await _userManager.CreateAsync(_user, apiuserDTO.Password);
            if (!result.Succeeded)
                return result.Errors;

            var role = apiuserDTO.RoleLevel switch
            {
                'A' => "Administrator",
                'U' => "User",
                _ => null
            };

            if (role != null)
                await _userManager.AddToRoleAsync(_user, role);

            return Enumerable.Empty<IdentityError>();
        }

        //JWT Token generetion process
        public async Task<string> GenerateToken()
        {
            var securitykey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid",_user.Id.ToString()),
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var refreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, refreshToken);
            return refreshToken;
        }

        public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO response)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(response.Token);
            var username = tokenContent.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            _user = await _userManager.FindByNameAsync(username);
            if (_user == null)
            {
                return null;
            }
            var isValid = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, response.Token);
            if (isValid)
            {
                var token = await GenerateToken();
                return new AuthResponseDTO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
    }
}
