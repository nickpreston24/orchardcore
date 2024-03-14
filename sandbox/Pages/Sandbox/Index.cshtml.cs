using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages.Sandbox;

[BindProperties(SupportsGet = true)]
public class Sandbox : PageModel
{
    private readonly ILogger<Sandbox> _logger;
    private readonly INugsService nugsService;
    public List<Part> Parts { get; set; } = new();
    public string Term { get; set; }


    // [BindProperty]
    public Part EditPart { get; set; } = new();

    public Sandbox(ILogger<Sandbox> logger
        , INugsService nugsService
    )
    {
        _logger = logger;
        this.nugsService = nugsService;
    }

    public async Task OnGet()
    {
        Parts = (await nugsService.GetAll()).ToList();
        EditPart = Parts.FirstOrDefault();
    }

    public async Task<IActionResult> OnPostUpdatePart()
    {
        Console.WriteLine("hello from " + nameof(OnPostUpdatePart));
        // var updates =
        await nugsService.Update(1, EditPart);
        return Content("hello from " + nameof(OnPostUpdatePart));
    }


    public async Task<IActionResult> OnGetSearch()
    {
        Console.WriteLine($"Searching for '{Term}'");
        // int _id = Term.ToInt();
        var found = await nugsService.Search(new Part()
        {
            name = Term,
            id = Term.ToInt(),
            manufacturer = Term,
            kind = Term,
            cost = Term.ToDouble(),
            url = Term
        });
        // return Content($"<b>{found.FirstOrDefault()?.name}</b>");
        // return Content(@"<hydro name="HydroPartsTable"></hydro>");
        found.Dump(nameof(found));
        return Partial("HydroPartsTable", new HydroPartsTable() { Parts = found });
    }


    public async Task<IActionResult> OnGetCreatePart()
    {
        Console.WriteLine(nameof(OnGetCreatePart));

        var part_names = new string[]
        {
            "DD 300 BLK PDW",
            "DD 556 NATO MK18",
            "MCMR-15 BCM 300 BLK Upper", "IWI Tavor X-95", "P90", "MCX Spear", "Honey Badger", "PPQ"
        }.Shuffle();
        var part_kinds = new string[] { "ar-15", "ar-10" }.Shuffle();
        var part_manufacturers = new string[]
        {
            "Bravo Company", "Proof Research", "Aero Precision", "Faxon Firearms", "Smith & Wesson", "Dan Wesson",
            "Kahr Arms", "IWI", "Sig Sauer", "Walther", "Q LLC"
        }.Shuffle();
        var part_costs = Enumerable.Range(5, 20).Select(x => x * 1000.00).ToArray().Shuffle();
        var fakepart = new Part()
        {
            name = part_names.FirstOrDefault(),
            cost = part_costs.FirstOrDefault(),
            kind = part_kinds.FirstOrDefault(),
            manufacturer = part_manufacturers.TakeFirstRandom()
        };

        // Parts.Dump("all parts");
        await nugsService.Create(fakepart.Dump("fp"));
        return Content($"<b>Created part '{fakepart.name}'</b>");
    }
}