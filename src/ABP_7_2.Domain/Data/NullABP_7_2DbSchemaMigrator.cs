using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Data;

/* This is used if database provider does't define
 * IABP_7_2DbSchemaMigrator implementation.
 */
public class NullABP_7_2DbSchemaMigrator : IABP_7_2DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
