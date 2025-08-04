using Infrastructure;

namespace PruebaApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);

    }
}
