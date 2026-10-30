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

    /// <summary>
    /// Creates a new instance of <see cref="Profile"/>.
    /// </summary>
    /// <param name="id">The unique ID for the profile.</param>
    /// <param name="userName">The username.</param>
    /// <param name="bio">The user's bio (optional).</param>
    /// <param name="isAudit">Whether this is an audit profile.</param>
    /// <param name="avatarColor">The hex color code for the avatar.</param>
    /// <param name="schoolYear">The starting date of the school year.</param>
    /// <returns>A new <see cref="Profile"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    public static Profile Create(
        int id,
        string userName,
        string? bio,
        bool isAudit,
        string avatarColor = "#808080",
        DateOnly schoolYear = default)
    {
        Validate(userName, avatarColor, schoolYear);
        return new Profile
        {
            Id = id,
            UserName = userName.Trim(),
            Bio = bio?.Trim(),
            AvatarColor = avatarColor.Trim(),
            SchoolYear = schoolYear,
            IsAudit = isAudit
        };
    }

    /// <summary>
    /// Updates the profile's information.
    /// </summary>
    /// <param name="userName">The new username.</param>
    /// <param name="bio">The new bio.</param>
    /// <param name="avatarColor">The new avatar color.</param>
    /// <param name="schoolYear">The new school year.</param>
    /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
    public void Update(
        string userName,
        string? bio,
        string avatarColor,
        DateOnly schoolYear)
    {
        Validate(userName, avatarColor, schoolYear);
        UserName = userName.Trim();
        Bio = bio?.Trim();
        AvatarColor = avatarColor.Trim();
        SchoolYear = schoolYear;
    }

    private static void Validate(string name, string avatarColor, DateOnly schoolYear)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Username is required");
        }
        if (schoolYear == default)
        {
            throw new ArgumentException("School year is required");
        }
        if (string.IsNullOrWhiteSpace(avatarColor))
        {
            throw new ArgumentException("Avatar color is required");
        }
    }
}
