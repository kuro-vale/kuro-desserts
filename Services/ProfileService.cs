using System.Net.Http.Headers;
using System.Text.Json;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Components;

namespace kuro_desserts.Services;

public class ProfileService
{
    public Address[]? Addresses { get; private set; }
    public bool ShowingDeleteUserModal { get; private set; }
    public bool ShowingAddressesModal { get; private set; }

    public void ShowDeleteModal()
    {
        ShowingDeleteUserModal = true;
    }

    public void HideDeleteUserModal()
    {
        ShowingDeleteUserModal = false;
    }

    public async Task ShowAddressesModal(HttpClient client, NavigationManager navigationManager, string token)
    {
        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
        try
        {
            Addresses = await client.GetFromJsonAsync<Address[]>($"{navigationManager.BaseUri}api/addresses");
        }
        catch (JsonException)
        {
            Addresses = null;
        }

        ShowingAddressesModal = true;
    }

    public void HideAddressesModal()
    {
        ShowingAddressesModal = false;
    }
}