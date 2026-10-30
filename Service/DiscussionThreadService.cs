using Arbeidskrav2_campus_chat.Data;
using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class DiscussionThreadService
{
    private readonly List<DiscussionThread> _threads = SeedData.CreateThreads();
    private readonly List<ThreadMembership> _threadMemberships = new();
    private readonly UserService _userService;
    private int _nextId;
    public DiscussionThreadService(UserService userService)
    {
        _userService = userService;
        _nextId = _threads.Max(p => p.Id) + 1;
    }

    /// <summary>
    /// Creates a new discussion thread.
    /// </summary>
    /// <param name="title">The title of the thread.</param>
    /// <param name="topic">The topic of the thread.</param>
    /// <param name="description">The description of the thread.</param>
    /// <param name="isPrivate">Whether the thread is private.</param>
    /// <param name="accessCode">The access code for the thread (required if private).</param>
    /// <returns>The created <see cref="DiscussionThread"/>.</returns>
    public DiscussionThread CreateThread(string title, string topic, string description, bool isPrivate, string accessCode)
    {
        var thread = DiscussionThread.Create(_nextId, title, topic, description, isPrivate, accessCode);
        _nextId++;
        _threads.Add(thread);
        return thread;
    }

    /// <summary>
    /// Checks if a profile can access a specific thread.
    /// </summary>
    /// <param name="threadId">The ID of the thread.</param>
    /// <param name="profileId">The ID of the profile (optional for public threads).</param>
    /// <param name="accessCode">The access code provided by the user (optional).</param>
    /// <returns>True if access is granted; otherwise, false.</returns>
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

    /// <summary>
    /// Checks if a profile is a member of a thread.
    /// </summary>
    /// <param name="profileId">The ID of the profile.</param>
    /// <param name="threadId">The ID of the thread.</param>
    /// <returns>True if the profile is a member; otherwise, false.</returns>
    public bool IsMember(int profileId, int threadId)
    {
        return _threadMemberships.Any(m => m.ProfileId == profileId && m.ThreadId == threadId);
    }

    /// <summary>
    /// Retrieves all discussion threads.
    /// </summary>
    /// <returns>A list of all threads.</returns>
    public IReadOnlyList<DiscussionThread> GetAllThreads()
    {
        return new List<DiscussionThread>(_threads);
    }

    /// <summary>
    /// Retrieves a thread by its ID.
    /// </summary>
    /// <param name="id">The thread ID.</param>
    /// <returns>The found <see cref="DiscussionThread"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the thread is not found.</exception>
    public DiscussionThread GetThreadById(int id)
    {
        var thread = _threads.FirstOrDefault(p => p.Id == id);
        if (thread == null)
        {
            throw new KeyNotFoundException("Thread not found");
        }
        return thread;
    }

    /// <summary>
    /// Updates the details of an existing thread.
    /// </summary>
    /// <param name="id">The thread ID.</param>
    /// <param name="title">The new title.</param>
    /// <param name="topic">The new topic.</param>
    /// <param name="description">The new description.</param>
    public void UpdateThread(int id, string title, string topic, string description)
    {
        var thread = GetThreadById(id);
        thread.Update(title, topic, description);
    }

    /// <summary>
    /// Deletes a thread by its ID.
    /// </summary>
    /// <param name="id">The thread ID.</param>
    public void DeleteThread(int id)
    {
        var thread = GetThreadById(id);
        _threads.Remove(thread);
    }

    /// <summary>
    /// Joins a profile to a thread, validating access if necessary.
    /// </summary>
    /// <param name="profileId">The ID of the profile joining.</param>
    /// <param name="threadId">The ID of the thread to join.</param>
    /// <param name="accessCode">The access code (required for private threads).</param>
    /// <returns>The created <see cref="ThreadMembership"/>.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if the access code is incorrect.</exception>
    /// <exception cref="ArgumentException">Thrown if the profile is already a member.</exception>
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

    /// <summary>
    /// Removes a profile from a thread's membership.
    /// </summary>
    /// <param name="profileId">The ID of the profile leaving.</param>
    /// <param name="threadId">The ID of the thread to leave.</param>
    /// <exception cref="ArgumentException">Thrown if the profile is not a member of the thread.</exception>
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

    /// <summary>
    /// Retrieves all threads that a specific profile has joined.
    /// </summary>
    /// <param name="profileId">The ID of the profile.</param>
    /// <returns>A list of threads joined by the profile.</returns>
    public IReadOnlyList<DiscussionThread> GetJoinedThreads(int profileId)
    {
        var user = _userService.GetProfileById(profileId);
        var joinedThreads = _threadMemberships
            .Where(m => m.ProfileId == user.Id)
            .Select(t => GetThreadById(t.ThreadId));

        return joinedThreads.ToList();
    }

    /// <summary>
    /// Searches for threads whose titles contain the given search term.
    /// </summary>
    /// <param name="searchTerm">The term to search for.</param>
    /// <returns>A list of matching threads.</returns>
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