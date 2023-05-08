namespace ABP_7_2.MauiBlazor.Settings;

public interface IABP_7_2ApplicationSettingService
{   
   Task<string> GetAccessTokenAsync();
    
    Task SetAccessTokenAsync(string? accessToken);
}