using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ABP_7_2.Maui.Messages;
public class LoginMessage : ValueChangedMessage<bool?>
{
    public LoginMessage(bool? value = null) : base(value)
    {
    }
}
