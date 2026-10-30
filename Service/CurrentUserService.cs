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

    /// <summary>
    /// Sets the current user's profile ID and saves it to session storage.
    /// </summary>
    /// <param name="profileId">The profile ID to set.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetProfileAsync(int profileId)
    {
        await _storage.SetAsync("profileId", profileId);
        ProfileId = profileId;
        ProfileChanged?.Invoke();
    }

    /// <summary>
    /// Notifies subscribers that the current user's profile has changed.
    /// </summary>
    public void NotifyProfileChange()
    {
        ProfileChanged?.Invoke();
    }

    /// <summary>
    /// Restores the user's profile ID from session storage.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
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