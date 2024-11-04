using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using System.Security.Claims;

namespace MovieRentWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDTO> CreateUser(UserCreateDTO userCreate);

        Task<LoginResponseDTO> LoginUser(LoginRequestDTO loginUser);
        Task<LoginResponseDTO> ChangePassword(ChangePasswordRequestDTO entity, ClaimsPrincipal userClaims);
        Task<IEnumerable<User>> GetUsers();
    }
}
