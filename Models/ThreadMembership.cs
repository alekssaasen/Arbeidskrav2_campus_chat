namespace Arbeidskrav2_campus_chat.Models;

public class ThreadMembership
{
    public int ProfileId { get; set; }
    public int ThreadId { get; set; }

    public static ThreadMembership Create(int profileId, int threadId)
    {
        return new ThreadMembership
        {
            ProfileId = profileId,
            ThreadId = threadId
        };
    }
}