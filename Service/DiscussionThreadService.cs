using Arbeidskrav2_campus_chat.Data;
using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class DiscussionThreadService
{
    private readonly List<DiscussionThread> _threads = SeedData.CreateThreads();
    private readonly List<ThreadMembership> _threadMemberships = new();
    private readonly UserService _userService;
    private int _nextId;
    public DiscussionThreadService(UserService userService, int nextId)
    {
        _userService = userService;
        _nextId = _threads.Max(p => p.Id) + 1;
    }

    public DiscussionThread CreateThread(string title, string topic, string description, bool isPrivate, string accessCode)
    {
        var thread = DiscussionThread.Create(_nextId, title, topic, description, isPrivate, accessCode);
        _nextId++;
        _threads.Add(thread);
        return thread;
    }
    public bool CanAccessThread(int threadId, int? profileId, string? accessCode)
    {
        var discussionThread = GetThreadById(threadId);
        if (!discussionThread.IsPrivate)
        {
            return true;
        }
        if (profileId == null)
        {
            return false;
        }
        var user = _userService.GetProfileById(profileId.Value);
        if (user.IsAudit)
        {
            return true;
        }
        if (IsMember(profileId.Value, threadId))
        {
            return true;
        }
        return discussionThread.IsCorrectCode(accessCode);
    }

    public bool IsMember(int profileId, int threadId)
    {
        return _threadMemberships.Any(m => m.ProfileId == profileId && m.ThreadId == threadId);
    }
    public IReadOnlyList<DiscussionThread> GetAllThreads()
    {
        return new List<DiscussionThread>(_threads);
    }

    public DiscussionThread GetThreadById(int id)
    {
        var thread = _threads.FirstOrDefault(p => p.Id == id);
        if (thread == null)
        {
            throw new KeyNotFoundException("Thread not found");
        }
        return thread;
    }
    public void UpdateThread(int id, string title, string topic, string description)
    {
        var thread = GetThreadById(id);
        thread.Update(title, topic, description);
    }
    public void DeleteThread(int id)
    {
        var thread = GetThreadById(id);
        _threads.Remove(thread);
    }

    public ThreadMembership JoinThread(int profileId, int threadId, string? accessCode)
    {
        var profile = _userService.GetProfileById(profileId);
        var thread = GetThreadById(threadId);
        if (!CanAccessThread(threadId, profileId, accessCode))
        {
            throw new UnauthorizedAccessException("Wrong code.");
        }
        var isAlreadyMember = _threadMemberships.Any(t => t.ProfileId == profile.Id && t.ThreadId == thread.Id);
        if (!isAlreadyMember)
        {
           var membership = ThreadMembership.Create(profileId, threadId);
           _threadMemberships.Add(membership);
           return membership;
        }
        else
        {
            throw new ArgumentException("You have already joined this thread.");
        }
    }
    public void LeaveThread(int profileId, int threadId)
    {
        var profile = _userService.GetProfileById(profileId);
        var thread = GetThreadById(threadId);
        var membership = _threadMemberships
            .FirstOrDefault(m => m.ProfileId == profile.Id && m.ThreadId == thread.Id);
        if (membership == null)
        {
            throw new ArgumentException("You have not joined this thread.");
        }
        _threadMemberships.Remove(membership);
    }

    public IReadOnlyList<DiscussionThread> GetJoinedThreads(int profileId)
    {
        var user = _userService.GetProfileById(profileId);
        var joinedThreads = _threadMemberships
            .Where(m => m.ProfileId == user.Id)
            .Select(t => GetThreadById(t.ThreadId));

        return joinedThreads.ToList();
    }

    public IReadOnlyList<DiscussionThread> SearchThreads(string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return GetAllThreads();
        }
        return _threads
            .Where(t => t.Title.Contains(searchTerm.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}