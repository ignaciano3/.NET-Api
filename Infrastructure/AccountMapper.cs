using Application.Dtos.Account;

namespace Infrastructure
{
    public static class AccountMapper
    {
        public static User ToUser(this RegisterDto registerDto)
        {
            return new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
        }
    }
}
