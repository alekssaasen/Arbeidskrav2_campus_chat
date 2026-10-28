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
    
    public IEnumerable<DirectMessage> GetMessages(int senderId, int receiverId)
    {
        var userA = _userServices.GetProfileById(senderId);
        var userB = _userServices.GetProfileById(receiverId);
        var conversation = _directMessages.
            Where(u => u.SenderId == userA.Id && u.ReceiverId == userB.Id || u.SenderId == userB.Id && u.ReceiverId == userA.Id)
            .OrderBy(d => d.SentAt);

        return conversation;
    }
    public IEnumerable<DirectMessage> GetConversations(int profileId)
    {
        var userA = _userServices.GetProfileById(profileId);
        var messages = _directMessages.Where(dm => dm.SenderId == userA.Id || dm.ReceiverId == userA.Id);
        var grouped = messages.GroupBy(m => m.SenderId == userA.Id ? m.ReceiverId : m.SenderId);

        var newestDm = grouped.Select(g => g.OrderByDescending(m => m.SentAt).First());

        return newestDm.OrderByDescending(m => m.SentAt);
    }
}