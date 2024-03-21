using CodeMechanic.Diagnostics;
using CodeMechanic.Types;

namespace Orchard.Sandbox.Pages;

/// <summary>
/// I'm using this special Enumeration to let us easily recycle our form(s),
/// rather than making a form for every little change.
///
/// Usage: Set the handler name to the function you wish to call.  Removes `On*` verbs for you.
/// </summary>
public class HxHandler : Enumeration
{
    // The name of the handler for an hx-page-handler attribute
    public string Handler { get; set; } = string.Empty;

    public HxHandler(int id, string name, string handler_name = "OnGet") : base(id, name)
    {
        // intentional, do not remove.  Let the dev set this name, and blow up if he does not.
        Handler = handler_name.NotEmpty()
            ? handler_name
            : throw new ArgumentNullException(nameof(handler_name));
    }

    // TODO: modifiy the replaces to be regex replacing the START of the method name, just to be perfectly accurate
    private static string RemoveCrud(string handlerName)
    {
        return handlerName.Replace("OnGet", "")
            .Replace("OnPatch", "")
            .Replace("OnDelete", "")
            .Replace("OnUpdate", "")
            .Replace("OnPost", "");
    }
}




// public static HxHandler Add = new HxHandler(1, nameof(Add), "OnGetAdd");
// public static HxHandler Edit = new HxHandler(2, nameof(Edit), "OnGetEdit");
// public static HxHandler Delete = new HxHandler(3, nameof(Delete), "OnGetDelete");
// public static HxHandler Search = new HxHandler(4, nameof(Search), "OnGetSearch");

// public static implicit operator HxHandler(string handler)
// {
//     Console.WriteLine("searching for hanlder : " + handler);
//     var found = GetAll<HxHandler>()
//         .Dump("all handlers")
//         .SingleOrDefault(x =>
//             x.Handler.Equals(handler, StringComparison.InvariantCultureIgnoreCase)
//         );
//
//     if (found == default)
//     {
//         throw new Exception($"No handler with name {handler} found!");
//     }
//
//     return new HxHandler(found.Id, found.Name, found.Handler);
// }
