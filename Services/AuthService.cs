using System.IdentityModel.Tokens.Jwt;
using System.Text;
using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;

namespace kuro_desserts.Services;

public class AuthService
{
    private readonly Context _db;
    public User? LoggedUser;
    public bool ShowingLogoutModal { get; private set; }

    public AuthService(Context db)
    {
        _db = db;
    }

    public void SetUser(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "sub").Value);

            var user = _db.Users.Find(userId);
            if (user!.IsDeleted) return;

            LoggedUser = user;
        }
        catch
        {
            // do nothing if jwt validation fails
        }
    }

    public static async void ClearToken(JSRuntime jsRuntime, NavigationManager navigationManager)
    {
        await jsRuntime.InvokeVoidAsync("WriteCookie.ClearToken");
        navigationManager.NavigateTo("/", forceLoad: true);
    }

    public void ShowLogoutModal()
    {
        ShowingLogoutModal = true;
    }

    public void HideLogoutModal()
    {
        ShowingLogoutModal = false;
    }
}