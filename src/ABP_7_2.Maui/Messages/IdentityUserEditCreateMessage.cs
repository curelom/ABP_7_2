using CommunityToolkit.Mvvm.Messaging.Messages;
using Volo.Abp.Identity;

namespace ABP_7_2.Maui.Messages;
public class IdentityUserEditMessage : ValueChangedMessage<IdentityUserEditMessageArgs>
{
    public IdentityUserEditMessage(IdentityUserEditMessageArgs value) : base(value)
    {
    }
}

public class IdentityUserEditMessageArgs
{
    public Guid UserId { get; set; }

    public IdentityUserUpdateDto? User { get; set; }
}
