using Arbeidskrav2_campus_chat.Models;

namespace Arbeidskrav2_campus_chat.Service;

public class ThreadService
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
}