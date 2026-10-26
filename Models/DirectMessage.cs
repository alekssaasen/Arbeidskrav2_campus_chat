namespace Arbeidskrav2_campus_chat.Models;

public class DirectMessage
{
    public int Id { get; private set; }
    public int SenderId { get; private set; }
    public int ReceiverId { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public DateTime SentAt { get; private set; }

    private DirectMessage() {}
    
    public static DirectMessage Create(
        int id,
        int senderId, 
        int receiverId, 
        string message)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(message);
        return new DirectMessage
        {
            Id = id,
            SenderId = senderId,
            ReceiverId = receiverId,
            Message = message,
            SentAt = DateTime.UtcNow,
        };
    }
}