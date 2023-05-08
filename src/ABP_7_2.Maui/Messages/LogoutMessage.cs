using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ABP_7_2.Maui.Messages;
public class LogoutMessage : ValueChangedMessage<bool?>
{
    public LogoutMessage(bool? value = null) : base(value)
    {
    }
}