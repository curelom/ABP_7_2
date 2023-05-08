using Microsoft.EntityFrameworkCore;

namespace ABP_7_2.EntityFrameworkCore;

public class ABP_7_2DbContextFactory :
    ABP_7_2DbContextFactoryBase<ABP_7_2DbContext>
{
    protected override ABP_7_2DbContext CreateDbContext(
        DbContextOptions<ABP_7_2DbContext> dbContextOptions)
    {
        return new ABP_7_2DbContext(dbContextOptions);
    }
}
