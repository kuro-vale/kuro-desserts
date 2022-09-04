using System.Net.Http.Headers;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace kuro_desserts.Services;

public class AuthService
{
    public User? LoggedUser { get; private set; }
    public bool ShowingLogoutModal { get; private set; }

    public async Task SetUser(string token, HttpClient client, NavigationManager navigationManager)
    {
        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
        LoggedUser = await client.GetFromJsonAsync<User>($"{navigationManager.BaseUri}api/auth/current");
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