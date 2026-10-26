namespace Arbeidskrav2_campus_chat.Models;

public class ThreadPost
{
    public int Id { get; private set; }
    public int AuthorId { get; private set; }
    public int ThreadId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public ThreadPost() { }
    public static ThreadPost Create(
        int id, 
        int authorId, 
        int threadId, 
        string content)
        
    {
        return new ThreadPost
        {
            Id = id,
            AuthorId = authorId,
            ThreadId = threadId,
            Content = content.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }
}