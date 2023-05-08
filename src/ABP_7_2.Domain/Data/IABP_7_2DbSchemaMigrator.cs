using System.Threading.Tasks;

namespace ABP_7_2.Data;

public interface IABP_7_2DbSchemaMigrator
{
    Task MigrateAsync();
}
