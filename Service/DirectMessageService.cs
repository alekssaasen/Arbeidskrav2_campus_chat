using Arbeidskrav2_campus_chat.Models;
namespace Arbeidskrav2_campus_chat.Service;

public class DirectMessageService
{
    private readonly List<DirectMessage> _directMessages = new();
    private int _nextId = 1;
    private readonly UserService _userServices;
    public event Action<int, int>? MessageSent;

    public DirectMessageService(UserService userServices)
    {
        _userServices = userServices;
    }

    /// <summary>
    /// Creates a new direct message between two users.
    /// </summary>
    /// <param name="senderId">ID of the profile sending the message.</param>
    /// <param name="receiverId">ID of the profile receiving the message.</param>
    /// <param name="message">The message content.</param>
    /// <returns>The created <see cref="DirectMessage"/>.</returns>
    public DirectMessage CreateMessage(int senderId, int receiverId, string message)
    {
        var userA = _userServices.GetProfileById(senderId);
        var userB = _userServices.GetProfileById(receiverId);
        var dM = DirectMessage.Create(_nextId, userA.Id, userB.Id, message);
        _nextId++;
        _directMessages.Add(dM);
        MessageSent?.Invoke(senderId, receiverId);
        return dM;
    }
    private bool CanReadConversation(int callerId, int userAId, int userBId)
    {
        if (callerId == userAId)
        {
            return true;
        }
        if (callerId == userBId)
        {
            return true;
        }
        var caller = _userServices.GetProfileById(callerId);
        if (caller.IsAudit)
        {
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Retrieves all messages in a conversation between two users, checking for access permissions.
    /// </summary>
    /// <param name="callerId">ID of the profile requesting the messages.</param>
    /// <param name="userAId">ID of the first user in the conversation.</param>
    /// <param name="userBId">ID of the second user in the conversation.</param>
    /// <returns>A list of messages in the conversation.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if the caller is not part of the conversation and not an auditor.</exception>
    public IReadOnlyList<DirectMessage> GetMessages(int callerId, int userAId, int userBId)
    {
        var userA = _userServices.GetProfileById(userAId);
        var userB = _userServices.GetProfileById(userBId);

        if (!CanReadConversation(callerId, userAId, userBId))
        {
            throw new UnauthorizedAccessException("You cannot read this conversation.");
        }
        
        var conversation = _directMessages.
            Where(m => m.SenderId == userA.Id && m.ReceiverId == userB.Id || m.SenderId == userB.Id && m.ReceiverId == userA.Id)
            .OrderBy(d => d.SentAt);

        return conversation.ToList();
    }

    /// <summary>
    /// Retrieves the latest message from each conversation a user is involved in.
    /// </summary>
    /// <param name="profileId">ID of the profile whose conversations are being retrieved.</param>
    /// <returns>A list containing the most recent message from each conversation.</returns>
    public IReadOnlyList<DirectMessage> GetConversations(int profileId)
    {
        var userA = _userServices.GetProfileById(profileId);
        var messages = _directMessages.Where(dm => dm.SenderId == userA.Id || dm.ReceiverId == userA.Id);
        var grouped = messages.GroupBy(m => m.SenderId == userA.Id ? m.ReceiverId : m.SenderId);

        var newestDm = grouped.Select(g => g.OrderByDescending(m => m.SentAt).First());

        return newestDm.OrderByDescending(m => m.SentAt).ToList();
    }
}