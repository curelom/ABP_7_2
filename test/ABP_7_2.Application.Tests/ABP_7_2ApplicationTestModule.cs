using Volo.Abp.Modularity;

namespace ABP_7_2;

[DependsOn(
    typeof(ABP_7_2ApplicationModule),
    typeof(ABP_7_2DomainTestModule)
    )]
public class ABP_7_2ApplicationTestModule : AbpModule
{

}
