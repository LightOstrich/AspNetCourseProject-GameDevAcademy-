using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HomeWorkEleven.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HomeWorkEleven.Controllers;

[Route("[controller]")]
public class AuthorizationController : Controller
{
    private List<User> _users = new()
    {
        new User("Abdullo", 25, new Role("Admin")),
        new User("Fayzullo", 16, new Role("User")),
        new User("Amirxon", 26, new Role("User")),
        new User("Azizxon", 30, new Role("User")),
    };

    private static string _login = "admin";
    private static string _password = "qwerty";

    [HttpPost("Authorize")]
    public IActionResult Authorize([FromForm] string login, [FromForm] string password)
    {
        // Проверка логина и пароля
        if (IsValidUser(login, password))
        {
            var user = _users.FirstOrDefault(i => i.Name == login);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, login),
                new(ClaimTypes.Role, user.Role.ToString())
                // Здесь вы можете добавить дополнительные утверждения (claims), например, роль пользователя
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1"));
            var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // создаем JWT-токен
            var token = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creeds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Сохранение токена в куке
            Response.Cookies.Append("token", tokenString, new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
            if (user?.Role.RoleName == "Admin")
            {
                return Redirect("/Table/Index");
            }

            if (user.Age < 18)
            {
                
            }
            return Redirect("/Home");
           
        }

        return NotFound();
    }

    private bool IsValidUser(string login, string password)
    {
        return true;
    }
}