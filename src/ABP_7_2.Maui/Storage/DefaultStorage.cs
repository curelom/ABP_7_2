using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Maui.Storage;

public class DefaultStorage : IStorage, ITransientDependency
{
    public async Task<string> GetAsync(string key)
    {
        return await SecureStorage.Default.GetAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await SecureStorage.Default.SetAsync(key, value);
    }

    public Task RemoveAsync(string key)
    {
        SecureStorage.Default.Remove(key);
        return Task.CompletedTask;
    }
}