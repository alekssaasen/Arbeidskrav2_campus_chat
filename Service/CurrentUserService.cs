namespace Arbeidskrav2_campus_chat.Service;

public class CurrentUserService
{
    public int? ProfileId { get; private set; }

    public void SetProfile(int profileId)
    {
        ProfileId = profileId;
    }
}