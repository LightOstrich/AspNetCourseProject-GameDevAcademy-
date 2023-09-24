using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HomeWorkEleven.Controllers;

public class AuthorizationController : Controller
{
    private static string _login = "admin";
    private static string _password = "qwerty";

    [HttpPost("Authorize")]
    public IActionResult Authorize([FromForm] string login, [FromForm] string password)
    {
        // Проверка логина и пароля
        if (IsValidUser(login, password))
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, login)
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
            return Ok(tokenString);
        }

        return NotFound();
    }

    private bool IsValidUser(string login, string password)
    {
        return login == _login && password == _password;
    }
}