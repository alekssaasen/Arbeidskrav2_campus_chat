using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class ThreadPostService
{
    private readonly List<ThreadPost> _threadPosts = new();
    private int _nextId = 1;
    private readonly DiscussionThreadService _discussionThreadService;
    private readonly UserService _userService;
    public event Action<int>? PostsChanged;
    public ThreadPostService(DiscussionThreadService discussionThreadService, UserService userService)
    {
        _discussionThreadService = discussionThreadService;
        _userService = userService;
    }
    /// <summary>
    /// Creates a new post within a discussion thread.
    /// </summary>
    /// <param name="authorId">The ID of the profile creating the post.</param>
    /// <param name="threadId">The ID of the thread where the post is created.</param>
    /// <param name="content">The content of the post.</param>
    /// <param name="parentId">The ID of the parent post, if this is a reply.</param>
    /// <param name="accessCode">The access code for the thread, if required.</param>
    /// <returns>The created <see cref="ThreadPost"/>.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if the author does not have access to the thread.</exception>
    /// <exception cref="ArgumentException">Thrown if the reply does not belong to the same thread as the parent post.</exception>
    public ThreadPost CreatePost(int authorId, int threadId, string content, int? parentId, string? accessCode)
    {
        var discussionThread = _discussionThreadService.GetThreadById(threadId);
        var profile = _userService.GetProfileById(authorId);
        var access = _discussionThreadService.CanAccessThread(discussionThread.Id, authorId, accessCode);
        if (!access)
        {
            throw new UnauthorizedAccessException("You cant post here without access.");
        }
        if (parentId != null)
        {
            var parentPost = GetPostById(parentId.Value);
            if (parentPost.ThreadId != discussionThread.Id)
            {
                throw new ArgumentException("Reply must belong to the same thread.");
            }
        }
        var threadPost = ThreadPost.Create(_nextId, profile.Id, discussionThread.Id ,content, parentId);
        _nextId++;
        _threadPosts.Add(threadPost);
        PostsChanged?.Invoke(threadPost.ThreadId);
        return threadPost;
    }

    /// <summary>
    /// Retrieves all posts in a specific thread, validating access.
    /// </summary>
    /// <param name="id">The ID of the thread.</param>
    /// <param name="profileId">The ID of the profile requesting the posts.</param>
    /// <param name="accessCode">The access code for the thread.</param>
    /// <returns>A list of posts in the thread.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if access is denied.</exception>
    public IReadOnlyList<ThreadPost> GetPostsByThreadId(int id, int? profileId, string? accessCode)
    {
        var access = _discussionThreadService.CanAccessThread(id, profileId, accessCode);
        if (!access)
        {
            throw new UnauthorizedAccessException("Unauthorized.");
        }
        return _threadPosts.Where(t => t.ThreadId == id).ToList();
    }

    /// <summary>
    /// Retrieves a single post by its ID.
    /// </summary>
    /// <param name="id">The post ID.</param>
    /// <returns>The found <see cref="ThreadPost"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the post is not found.</exception>
    public ThreadPost GetPostById(int id)
    {
        var threadPost = _threadPosts.FirstOrDefault(p => p.Id == id);
        if (threadPost == null)
        {
            throw new KeyNotFoundException("Post not found.");
        }
        return threadPost;
    }

    /// <summary>
    /// Marks a post as deleted if the author matches.
    /// </summary>
    /// <param name="id">The post ID.</param>
    /// <param name="authorId">The ID of the profile requesting deletion.</param>
    /// <exception cref="UnauthorizedAccessException">Thrown if the profile is not the author of the post.</exception>
    public void DeletePost(int id, int authorId)
    {
        var post = GetPostById(id);
        var user = _userService.GetProfileById(authorId);

        if (post.AuthorId != user.Id)
        {
            throw new UnauthorizedAccessException("This post does not belong to you.");
        }
        post.MarkDeleted();
        PostsChanged?.Invoke(post.ThreadId);

    }
}