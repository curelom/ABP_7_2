using ABP_7_2.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ABP_7_2;

[DependsOn(
    typeof(ABP_7_2EntityFrameworkCoreTestModule)
    )]
public class ABP_7_2DomainTestModule : AbpModule
{

}
