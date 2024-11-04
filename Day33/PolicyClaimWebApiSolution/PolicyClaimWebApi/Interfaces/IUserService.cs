using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(UserDTO registerUser);
    }
}
