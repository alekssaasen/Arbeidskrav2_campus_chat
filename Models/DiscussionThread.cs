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
    public List<ThreadPost> ThreadPosts { get; set; } = new List<ThreadPost>();

    public DiscussionThread() { }
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
    public void Update(string title, string topic, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(topic);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        Title = title.Trim();
        Topic = topic.Trim();
        Description = description.Trim();
    }

    public bool IsCorrectCode(string? code)
    {
        if (AccessCode == code)
        {
            return true;
        }
        return false;
    }
}