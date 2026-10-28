using Arbeidskrav2_campus_chat.Data;
using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class UserService
{
    private readonly List<Profile> _profiles = SeedData.CreateProfiles();
    private int _nextId = 7;

    public Profile CreateProfile(string name, string? bio, bool isAudit, string avatarColor, DateOnly schoolYear)
    {
        var user = Profile.Create(_nextId, name, bio, isAudit, avatarColor, schoolYear);
        _nextId++;
        _profiles.Add(user);
        return user;
    }

    public List<Profile> GetAllProfiles()
    {
        return new List<Profile>(_profiles);
    }

    public Profile GetProfileById(int id)
    {
        var user = _profiles.FirstOrDefault(p => p.Id == id);
        if (user == null)
        {
            throw new KeyNotFoundException("User does not exists");
        }
        return user;
    }
    public void UpdateProfile(int id, string userName, string bio, string avatarColor, DateOnly schoolYear)
    {
        var profile = GetProfileById(id);
        profile.Update(userName, bio, avatarColor, schoolYear);
    }
    public void DeleteProfile(int id)
    {
        var profile = GetProfileById(id);
        _profiles.Remove(profile);
    }
}