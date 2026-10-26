using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class ThreadPostService
{
    private readonly List<ThreadPost> _threadPosts = new();
    private int _nextId = 1;
    private readonly ThreadService _threadService;
    private readonly UserService _userService;
    public ThreadPostService(ThreadService threadService, UserService userService)
    {
        _threadService = threadService;
        _userService = userService;
    }
    public ThreadPost CreatePost(int authorId, int threadId, string content)
    {
        var discussionThread = _threadService.GetThreadById(threadId);
        var profile = _userService.GetProfileById(authorId);
        var threadPost = ThreadPost.Create(_nextId, profile.Id, discussionThread.Id, content);
        
        _nextId++;
        _threadPosts.Add(threadPost);
        return threadPost;
    }
    public IEnumerable<ThreadPost> GetPostsByThreadId(int id)
    {
        var discussionThread = _threadService.GetThreadById(id);

        return _threadPosts.Where(t => t.ThreadId == discussionThread.Id);
    }
    
    public ThreadPost GetPostById(int id)
    {
        var threadPost = _threadPosts.FirstOrDefault(p => p.Id == id);
        if (threadPost == null)
        {
            throw new ArgumentException("Post not found.");
        }
        return threadPost;
    }
    public void DeletePost(int id, int authorId)
    {
        var post = GetPostById(id);
        var user = _userService.GetProfileById(authorId);

        if (post.AuthorId != user.Id)
        {
            throw new ArgumentException("This post does not belong to this user.");
        }
        _threadPosts.Remove(post);

    }
}