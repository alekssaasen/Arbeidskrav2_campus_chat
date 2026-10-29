using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
namespace Arbeidskrav2_campus_chat.Service;

public class CurrentUserService
{
    public int? ProfileId { get; private set; }
    
    private readonly ProtectedSessionStorage _storage;
    public bool IsLoaded { get; private set; }
    public event Action? ProfileChanged;

    public CurrentUserService(ProtectedSessionStorage storage)
    {
        _storage = storage;
    }

    public async Task SetProfileAsync(int profileId)
    {
        await _storage.SetAsync("profileId", profileId);
        ProfileId = profileId;
        ProfileChanged?.Invoke();
    }

    public async Task RestoreProfileAsync()
    {
        if (IsLoaded) return;
        
        var loaded = await _storage.GetAsync<int>("profileId");
        if (loaded.Success)
        {
            ProfileId = loaded.Value;
        }

        IsLoaded = true;
    }
}