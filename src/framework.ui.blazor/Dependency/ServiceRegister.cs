


using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Ui.Blazor.Components;
using Orion.Framework.Exceptions;
using Orion.Framework.Ui.Blazor.Reports;
using Orion.Framework.Dependency;
using Syncfusion.Licensing;
using Orion.Framework.Ui.Blazor.Icons;
using Orion.Framework.Domains.ValueObjects;
using Syncfusion.Blazor;
using Orion.Framework.Ui.Blazor.Shared;

namespace Orion.Framework.Ui.Blazor.Dependency
{
    public class ServiceRegister : IDependencyRegistrar
    {
        /// <summary>
        /// 
        /// </summary>
        public void Register(IServiceCollection services)
        {
            var licenseKey = "MTMzN0AzMTM4MmUzMTJlMzBYTml4RFZ2ZmVsRmlNbmdCcDNjVG9naS9qWEFzVXJvL0FkSmlJbnkzVHV3PQ==";
            SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            services.AddSyncfusionBlazor();
            services.AddLoadingIndicator(options => { });
            services.AddBlazorise(options => { options.ChangeTextOnKeyPress = false; }).AddMaterialProviders().AddMaterialIcons();
            services.AddSingleton<IExceptionHelper, ExceptionHelper>();
            services.AddScoped<IReportViewerService, ReportViewerService>();
            services.AddScoped<IToastService, ToastService>();
            services.AddScoped<FWorkAppState>();
            services.AddScoped<AppDataTransfer>();
            services.AddScoped<IconManager>();
            services.AddSweetAlert2(options => {
                options.Theme = SweetAlertTheme.MaterialUI;
                options.SetThemeForColorSchemePreference(ColorScheme.Light, SweetAlertTheme.MaterialUI);
            });

        }
    }
}
