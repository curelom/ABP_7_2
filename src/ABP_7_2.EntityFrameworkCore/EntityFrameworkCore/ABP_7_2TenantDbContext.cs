using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace ABP_7_2.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ABP_7_2TenantDbContext : ABP_7_2DbContextBase<ABP_7_2TenantDbContext>
{
    public ABP_7_2TenantDbContext(DbContextOptions<ABP_7_2TenantDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SetMultiTenancySide(MultiTenancySides.Tenant);

        base.OnModelCreating(builder);
    }
}
