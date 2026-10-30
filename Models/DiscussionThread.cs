namespace Arbeidskrav2_campus_chat.Models;

public class DiscussionThread
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Topic { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsPrivate { get; private set; }
    private string? AccessCode { get; set; } 
    public DateTime CreatedAt { get; private set; }

    private DiscussionThread() { }
    /// <summary>
    /// Creates a new instance of <see cref="DiscussionThread"/>.
    /// </summary>
    /// <param name="id">The unique ID for the thread.</param>
    /// <param name="title">The title of the thread.</param>
    /// <param name="topic">The topic of the thread.</param>
    /// <param name="description">The description of the thread.</param>
    /// <param name="isPrivate">Whether the thread is private.</param>
    /// <param name="accessCode">The access code required for private threads.</param>
    /// <returns>A new <see cref="DiscussionThread"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if any required parameter is invalid.</exception>
    public static DiscussionThread Create(
        int id,
        string title,
        string topic,
        string description,
        bool isPrivate,
        string accessCode)
    {
        if (isPrivate)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(accessCode);
            accessCode = accessCode.Trim();
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(topic);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new DiscussionThread
        {
            Id = id,
            Title = title.Trim(),
            Topic = topic.Trim(),
            Description = description.Trim(),
            IsPrivate = isPrivate,
            AccessCode = isPrivate ? accessCode : null,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates the thread's basic information.
    /// </summary>
    /// <param name="title">The new title.</param>
    /// <param name="topic">The new topic.</param>
    /// <param name="description">The new description.</param>
    /// <exception cref="ArgumentException">Thrown if any parameter is null or whitespace.</exception>
    public void Update(string title, string topic, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(topic);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        Title = title.Trim();
        Topic = topic.Trim();
        Description = description.Trim();
    }

    /// <summary>
    /// Validates if the provided code matches the thread's access code.
    /// </summary>
    /// <param name="code">The code to check.</param>
    /// <returns>True if the code is correct or if no code is required; otherwise, false.</returns>
    public bool IsCorrectCode(string? code)
    {
        if (AccessCode == code)
        {
            return true;
        }
        return false;
    }
}