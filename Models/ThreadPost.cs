namespace Arbeidskrav2_campus_chat.Models;

public class ThreadPost
{
    public int Id { get; private set; }
    public int AuthorId { get; private set; }
    public int ThreadId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public int? ParentPostId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ThreadPost() { }
    public static ThreadPost Create(
        int id, 
        int authorId, 
        int threadId,
        string content,
        int? parentPostId)
    
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("A post can't be empty");
        }
        return new ThreadPost
        {
            Id = id,
            AuthorId = authorId,
            ThreadId = threadId,
            Content = content.Trim(),
            ParentPostId = parentPostId,
            CreatedAt = DateTime.UtcNow
        };
    }
    public void Update(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        Content = content.Trim();
    }
}