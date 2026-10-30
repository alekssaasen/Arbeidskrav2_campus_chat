using Arbeidskrav2_campus_chat.Data;
using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class UserService
{
    private readonly List<Profile> _profiles = SeedData.CreateProfiles();
    private int _nextId;

    public UserService()
    {
        _nextId = _profiles.Max(p => p.Id) + 1;
    }

    /// <summary>
    /// Creates a new user profile.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <param name="bio">The bio of the user.</param>
    /// <param name="isAudit">Whether the user has audit permissions.</param>
    /// <param name="avatarColor">The hex color code for the user's avatar.</param>
    /// <param name="schoolYear">The user's school year start date.</param>
    /// <returns>The created <see cref="Profile"/>.</returns>
    public Profile CreateProfile(string name, string? bio, bool isAudit, string avatarColor, DateOnly schoolYear)
    {
        var user = Profile.Create(_nextId, name, bio, isAudit, avatarColor, schoolYear);
        _nextId++;
        _profiles.Add(user);
        return user;
    }

    /// <summary>
    /// Retrieves all user profiles.
    /// </summary>
    /// <returns>A list of all profiles.</returns>
    public IReadOnlyList<Profile> GetAllProfiles()
    {
        return new List<Profile>(_profiles);
    }

    /// <summary>
    /// Retrieves a profile by its ID.
    /// </summary>
    /// <param name="id">The profile ID.</param>
    /// <returns>The found <see cref="Profile"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the user does not exist.</exception>
    public Profile GetProfileById(int id)
    {
        var user = _profiles.FirstOrDefault(p => p.Id == id);
        if (user == null)
        {
            throw new KeyNotFoundException("User does not exists");
        }
        return user;
    }

    /// <summary>
    /// Updates an existing profile's information.
    /// </summary>
    /// <param name="callerId">The ID of the profile requesting the update.</param>
    /// <param name="id">The ID of the profile to update.</param>
    /// <param name="userName">The new username.</param>
    /// <param name="bio">The new bio.</param>
    /// <param name="avatarColor">The new avatar color.</param>
    /// <param name="schoolYear">The new school year.</param>
    /// <exception cref="UnauthorizedAccessException">Thrown if the caller tries to edit someone else's profile.</exception>
    public void UpdateProfile(int callerId, int id, string userName, string? bio, string avatarColor, DateOnly schoolYear)
    {
        if (callerId != id)
        {
            throw new UnauthorizedAccessException("You can only edit your own profile.");
        }
        var profile = GetProfileById(id);
        profile.Update(userName, bio, avatarColor, schoolYear);
    }

    /// <summary>
    /// Deletes a profile by its ID.
    /// </summary>
    /// <param name="id">The profile ID.</param>
    public void DeleteProfile(int id)
    {
        var profile = GetProfileById(id);
        _profiles.Remove(profile);
    }
}