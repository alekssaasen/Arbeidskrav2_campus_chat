namespace Arbeidskrav2_campus_chat.Models;

public class Profile
{
    public int Id { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string? Bio { get; private set; } 
    public string AvatarColor { get; private set; } = string.Empty;
    public DateOnly SchoolYear { get; private set; }
    public bool IsAudit { get; private set; }
    
    private Profile() { }

    public static Profile Create(
        int id,
        string userName,
        string? bio,
        string avatarColor = "#808080",
        DateOnly schoolYear = default)
    {
        Validate(userName, schoolYear);
        return new Profile
        {
            Id = id,
            UserName = userName.Trim(),
            Bio = bio,
            AvatarColor = avatarColor,
            SchoolYear = schoolYear,
            IsAudit = false
        };
    }

    private static void Validate(string name, DateOnly schoolYear)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Username is required");
        }
        if (schoolYear == default)
        {
            throw new ArgumentException("School year is required");
        }
    }
}
