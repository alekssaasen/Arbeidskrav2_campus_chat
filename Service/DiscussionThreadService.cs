using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class DiscussionThreadService
{
    private readonly List<DiscussionThread> _threads = new();
    private int _nextId = 1;

    public DiscussionThread CreateThread(string title, string topic, string description)
    {
        var thread = DiscussionThread.Create(_nextId, title, topic, description);
        _nextId++;
        _threads.Add(thread);
        return thread;
    }
    public bool CanAccessThread(int threadId, int profileId, string? accessCode)
    {
        var user = _userService.GetProfileById(profileId);
        var discussionThread = GetThreadById(threadId);
        if (user.IsAudit || !discussionThread.IsPrivate)
        {
            return true;
        }
        if (discussionThread.IsPrivate && discussionThread.AccessCode != accessCode)
        {
            return false;
        }

        return discussionThread.AccessCode == accessCode;;
    }

    public List<DiscussionThread> GetAllThreads()
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

    public ThreadMembership JoinThread(int profileId, int threadId)
    {
        var profile = _userService.GetProfileById(profileId);
        var thread = GetThreadById(threadId);
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
}