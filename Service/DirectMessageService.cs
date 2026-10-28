using Arbeidskrav2_campus_chat.Models;
namespace Arbeidskrav2_campus_chat.Service;

public class DirectMessageService
{
    private readonly List<DirectMessage> _directMessages = new();
    private int _nextId = 1;
    private readonly UserService _userServices;

    public DirectMessageService(UserService userServices)
    {
        _userServices = userServices;
    }

    public DirectMessage CreateMessage(int senderId, int receiverId, string message)
    {
        var userA = _userServices.GetProfileById(senderId);
        var userB = _userServices.GetProfileById(receiverId);
        var dM = DirectMessage.Create(_nextId, userA.Id, userB.Id, message);
        _nextId++;
        _directMessages.Add(dM);
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
    public IReadOnlyList<DirectMessage> GetConversations(int profileId)
    {
        var userA = _userServices.GetProfileById(profileId);
        var messages = _directMessages.Where(dm => dm.SenderId == userA.Id || dm.ReceiverId == userA.Id);
        var grouped = messages.GroupBy(m => m.SenderId == userA.Id ? m.ReceiverId : m.SenderId);

        var newestDm = grouped.Select(g => g.OrderByDescending(m => m.SentAt).First());

        return newestDm.OrderByDescending(m => m.SentAt).ToList();
    }
}