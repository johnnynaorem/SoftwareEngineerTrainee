using EFCoreWebAPI.Models.DTO;

namespace EFCoreWebAPI.Interfaces
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(UserCreateDTO registerUser);
    }
}
