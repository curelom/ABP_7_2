using ABP_7_2.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ABP_7_2.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ABP_7_2EntityFrameworkCoreModule),
    typeof(ABP_7_2ApplicationContractsModule)
)]
public class ABP_7_2DbMigratorModule : AbpModule
{

}
