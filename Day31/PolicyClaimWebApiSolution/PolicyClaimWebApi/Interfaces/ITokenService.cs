using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
