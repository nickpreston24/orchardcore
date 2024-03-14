using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;
using OrchardExample.Map.Models;

namespace OrchardExample.Map.Handlers
{
    public class MapPartHandler : ContentPartHandler<MapPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, MapPart part)
        {
            part.Show = true;

            return Task.CompletedTask;
        }
    }
}