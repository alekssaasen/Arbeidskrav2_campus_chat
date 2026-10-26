namespace Arbeidskrav2_campus_chat.Models;

public class Thread
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Topic { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public Thread() { }
    public static Thread Create(
        int id,
        string title,
        string topic,
        string description)
    {
        return new Thread
        {
            Id = id,
            Title = title.Trim(),
            Topic = topic.Trim(),
            Description = description.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }
}