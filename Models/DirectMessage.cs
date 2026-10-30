namespace Arbeidskrav2_campus_chat.Models;

public class DirectMessage
{
    public int Id { get; private set; }
    public int SenderId { get; private set; }
    public int ReceiverId { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public DateTime SentAt { get; private set; }

    private DirectMessage() {}
    
    /// <summary>
    /// Creates a new instance of <see cref="DirectMessage"/>.
    /// </summary>
    /// <param name="id">The unique ID for the message.</param>
    /// <param name="senderId">The ID of the sender's profile.</param>
    /// <param name="receiverId">The ID of the receiver's profile.</param>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new <see cref="DirectMessage"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if the message content is null or whitespace.</exception>
    public static DirectMessage Create(
        int id,
        int senderId, 
        int receiverId, 
        string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
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