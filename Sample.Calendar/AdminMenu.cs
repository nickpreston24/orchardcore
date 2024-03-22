using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System.Threading.Tasks;

namespace Sample.Calendar;

public class AdminMenu : INavigationProvider
{

    public AdminMenu(IStringLocalizer<AdminMenu> localizer)
    {
        T = localizer;
    }

    private readonly IStringLocalizer T;

    public async Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        builder
            .Add(T["Calendar"], "16", calendar => calendar
              .AddClass("calendar").Id("calendar")
              .AddClass("Active")
              .Add(T["Sample Calendar Page"], layers => layers
                    .Action("Index", "Admin", new { area = "Sample.Calendar" })
                    .Permission(Permissions.CreateCalendarContents)
                    .LocalNav()
                ));
    }
}
