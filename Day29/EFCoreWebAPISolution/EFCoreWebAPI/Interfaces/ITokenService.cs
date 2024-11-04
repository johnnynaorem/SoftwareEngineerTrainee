using EFCoreWebAPI.Models.DTO;

namespace EFCoreWebAPI.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
