using Microsoft.EntityFrameworkCore;

namespace ABP_7_2.EntityFrameworkCore;

public class ABP_7_2TenantDbContextFactory :
    ABP_7_2DbContextFactoryBase<ABP_7_2TenantDbContext>
{
    protected override ABP_7_2TenantDbContext CreateDbContext(
        DbContextOptions<ABP_7_2TenantDbContext> dbContextOptions)
    {
        return new ABP_7_2TenantDbContext(dbContextOptions);
    }
}
