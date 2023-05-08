namespace ABP_7_2.Maui.Storage;

public interface IStorage
{
    Task<string> GetAsync(string key);

    Task SetAsync(string key, string value);

    Task RemoveAsync(string key);
}