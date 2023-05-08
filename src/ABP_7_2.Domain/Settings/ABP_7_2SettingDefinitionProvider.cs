using Volo.Abp.Settings;

namespace ABP_7_2.Settings;

public class ABP_7_2SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ABP_7_2Settings.MySetting1));
    }
}
