using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Calendar;

public class Permissions : IPermissionProvider
{
    public static readonly Permission CreateCalendarContents = new("CreateCalendarContents", "Create Calendar Contents");

    public static IEnumerable<Permission> ListPermissions => new[]
    {
        CreateCalendarContents
    };

    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(ListPermissions);

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() => new[]
    {
            new PermissionStereotype
            {
                Name = "Administrator",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
            new PermissionStereotype
            {
                Name = "Editor",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
            new PermissionStereotype
            {
                Name = "Moderator",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
            new PermissionStereotype
            {
                Name = "Author",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
            new PermissionStereotype
            {
                Name = "Contributor",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
            new PermissionStereotype
            {
                Name = "Authenticated",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
            new PermissionStereotype
            {
                Name = "Anonymous",
                Permissions = new[]
                {
                    CreateCalendarContents
                },
            },
    };
}
