using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class UserService
{
    private readonly List<Profile> _profiles = new();
    private int _nextId = 1;
    
    public Profile CreateProfile(string name, string? bio, string avatarColor, DateOnly schoolYear)
    {
        var user = Profile.Create(_nextId, name, bio, avatarColor, schoolYear);
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
}