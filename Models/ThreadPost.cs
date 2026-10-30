namespace Arbeidskrav2_campus_chat.Models;

public class ThreadPost
{
    public int Id { get; private set; }
    public int AuthorId { get; private set; }
    public int ThreadId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public int? ParentPostId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    private ThreadPost() { }
    /// <summary>
    /// Creates a new instance of <see cref="ThreadPost"/>.
    /// </summary>
    /// <param name="id">The unique ID for the post.</param>
    /// <param name="authorId">The ID of the author's profile.</param>
    /// <param name="threadId">The ID of the thread this post belongs to.</param>
    /// <param name="content">The content of the post.</param>
    /// <param name="parentPostId">The ID of the parent post, if this is a reply.</param>
    /// <returns>A new <see cref="ThreadPost"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if the content is null or whitespace.</exception>
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

    /// <summary>
    /// Updates the content of the post.
    /// </summary>
    /// <param name="content">The new content.</param>
    /// <exception cref="ArgumentException">Thrown if the content is null or whitespace.</exception>
    public void Update(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        Content = content.Trim();
    }

    /// <summary>
    /// Marks the post as deleted, clearing its content.
    /// </summary>
    public void MarkDeleted()
    {
        IsDeleted = true;
        Content = string.Empty;
    }
}