using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace ABP_7_2.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ABP_7_2DbContext : ABP_7_2DbContextBase<ABP_7_2DbContext>
{
    public ABP_7_2DbContext(DbContextOptions<ABP_7_2DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SetMultiTenancySide(MultiTenancySides.Both);

        base.OnModelCreating(builder);
    }
}
