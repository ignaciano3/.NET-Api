using PruebaApi.Dtos.Account;
using PruebaApi.Models;

namespace PruebaApi.Mappers
{
    public static class AccountMapper
    {
        public static User ToUser(this RegisterDto registerDto)
        {
            return new User()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
        }
    }
}
