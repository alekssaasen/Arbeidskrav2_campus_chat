namespace Arbeidskrav2_campus_chat.Models;

public class ThreadMembership
{
    public int ProfileId { get; set; }
    public int ThreadId { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="ThreadMembership"/>.
    /// </summary>
    /// <param name="profileId">The ID of the profile.</param>
    /// <param name="threadId">The ID of the thread.</param>
    /// <returns>A new <see cref="ThreadMembership"/> instance.</returns>
    public static ThreadMembership Create(int profileId, int threadId)
    {
        return new ThreadMembership
        {
            ProfileId = profileId,
            ThreadId = threadId
        };
    }
}