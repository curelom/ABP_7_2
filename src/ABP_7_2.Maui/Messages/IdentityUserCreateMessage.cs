using CommunityToolkit.Mvvm.Messaging.Messages;
using Volo.Abp.Identity;

namespace ABP_7_2.Maui.Messages;
public class IdentityUserCreateMessage : ValueChangedMessage<IdentityUserCreateDto>
{
    public IdentityUserCreateMessage(IdentityUserCreateDto value) : base(value)
    {
    }
}
