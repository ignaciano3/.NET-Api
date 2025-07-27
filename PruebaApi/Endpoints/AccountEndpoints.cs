using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaApi.Data;
using PruebaApi.Dtos.Account;
using PruebaApi.Interfaces;
using PruebaApi.Mappers;
using PruebaApi.Models;

namespace PruebaApi.Endpoints
{
    public static class AccountEndpoints
    {
        public static WebApplication MapAccounts(this WebApplication app)
        {
            var group = app.MapGroup("/api/accounts")
                .WithTags("Authentication");

            group.MapPost("/register", Register);
            group.MapPost("/login", Login);
            group.MapGet("/", GetAll)
                .RequireAuthorization("AdminOnly");
            //group.MapPost("/login", Login);


            return app;
        }

        public static async Task<Results<Ok<NewUserDto>, BadRequest<IEnumerable<string>>>> Register(
            RegisterDto registerDto, UserManager<User> userManager,
            ITokenService tokenService)
        {
            var user = registerDto.ToUser();
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, "Admin");

                if (roleResult.Succeeded)
                {
                    var token = await tokenService.CreateToken(user);
                    return TypedResults.Ok(new NewUserDto
                    {
                        Token = token,
                        Username = user.UserName,
                        Email = user.Email
                    });
                }

                return TypedResults.BadRequest(roleResult.Errors.Select(e => e.Description));
            }

            return TypedResults.BadRequest(result.Errors.Select(e => e.Description));
        }

        public static async Task<Results<Ok<NewUserDto>, UnauthorizedHttpResult>> Login(LoginDto loginDto,
            UserManager<User> userManager,
            ITokenService tokenService, SignInManager<User> signInManager)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return TypedResults.Unauthorized();

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return TypedResults.Unauthorized();

            var token = await tokenService.CreateToken(user);

            return TypedResults.Ok(new NewUserDto
            {
                Email = user.Email,
                Username = user.UserName,
                Token = token
            });
        }

        public static async Task<Ok<List<User>>> GetAll(ApplicationDbContext context)
        {
            var users = await context.Users.ToListAsync();
            return TypedResults.Ok(users);
        }
    }
}