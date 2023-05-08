using ABP_7_2.Maui.ViewModels;

namespace ABP_7_2.Maui.Extensions;

[ContentProperty(nameof(Text))]
public class TranslateExtension : IMarkupExtension<BindingBase>
{
    public string Text { get; set; } = string.Empty;

    public string? StringFormat { get; set; }


    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        var localizationResourceManager = serviceProvider.GetRequiredService<IRootObjectProvider>()
            .RootObject.As<BindableObject>()
            .BindingContext.As<ABP_7_2ViewModelBase>()
            .L; ;        
        
        var binding = new Binding
        {
            Mode = BindingMode.OneWay,
            Path = $"[{Text}]",
            Source = localizationResourceManager,
            StringFormat = StringFormat
        };
        return binding;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
}
