using HotelListing.Api.Data.DTO.User;
using HotelListing.Api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Api.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDTO apiuserDTO);
        Task<AuthResponseDTO> Login(LoginDTO loginUserDTO);

        Task<string> GenerateToken();

        Task<string> CreateRefreshToken();

        Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO response);
    }
}
