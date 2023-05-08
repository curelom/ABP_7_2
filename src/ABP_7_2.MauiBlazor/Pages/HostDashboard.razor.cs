using Microsoft.AspNetCore.Components;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Blazor.Pages.Shared.AverageExecutionDurationPerDayWidget;
using Volo.Abp.AuditLogging.Blazor.Pages.Shared.ErrorRateWidget;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.BlazoriseUI;
using Volo.Saas.Host;
using Volo.Saas.Host.Blazor.Pages.Shared.Components.SaasEditionPercentageWidget;
using Volo.Saas.Host.Blazor.Pages.Shared.Components.SaasLatestTenantsWidget;

namespace ABP_7_2.MauiBlazor.Pages;

public partial class HostDashboard
{
    [Inject]
    public IPermissionChecker PermissionChecker { get; set; } = default!;

    protected List<BreadcrumbItem> BreadcrumbItems = new();

    protected AuditLoggingErrorRateWidgetComponent? ErrorRateWidgetComponent;

    protected AuditLoggingAverageExecutionDurationPerDayWidgetComponent? AverageExecutionDurationPerDayWidgetComponent;

    protected SaasEditionPercentageWidgetComponent? SaasEditionPercentageWidgetComponent;

    protected SaasLatestTenantsWidgetComponent? SaasLatestTenantsWidgetComponent;

    protected DateTime StartDate { get; set; }

    protected DateTime EndDate { get; set; }

    protected bool HasAuditLoggingPermission { get; set; }

    protected bool HasSaasPermission { get; set; }

    protected async override Task OnInitializedAsync()
    {
        StartDate = Clock.Now.AddMonths(-1).Date;
        EndDate = Clock.Now.Date;
        HasAuditLoggingPermission = await PermissionChecker.IsGrantedAsync(AbpAuditLoggingPermissions.AuditLogs.Default);
        HasSaasPermission = await PermissionChecker.IsGrantedAsync(SaasHostPermissions.Tenants.Default);
        await SetBreadcrumbItemsAsync();
    }

    protected virtual async Task RefreshAsync()
    {
        if (HasAuditLoggingPermission)
        {
            if (ErrorRateWidgetComponent != null)
            {
                await ErrorRateWidgetComponent.RefreshAsync();
            }

            if(AverageExecutionDurationPerDayWidgetComponent != null)
            {
                await AverageExecutionDurationPerDayWidgetComponent.RefreshAsync();
            }
        }

        if (HasSaasPermission && SaasEditionPercentageWidgetComponent != null)
        {
            await SaasEditionPercentageWidgetComponent.RefreshAsync();
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new BreadcrumbItem(L["Dashboard"]));
        return ValueTask.CompletedTask;
    }

    protected virtual Task OnDatesChangedAsync(IReadOnlyList<DateTime> dates)
    {
        StartDate = dates.Min();
        EndDate = dates.Max();

        return Task.CompletedTask;
    }
}
