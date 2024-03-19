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
    private readonly IGunService gunService;
    public List<Part> Parts { get; set; } = new();
    public string Term { get; set; }

    public Part EditPart { get; set; } = new();

    public Sandbox(ILogger<Sandbox> logger
        , IGunService gunService
    )
    {
        _logger = logger;
        this.gunService = gunService;
    }

    public async Task OnGet()
    {
        Parts = (await gunService.GetAll()).ToList();
        EditPart = Parts.FirstOrDefault();
    }

    public async Task<IActionResult> OnPostUpdatePart()
    {
        Console.WriteLine("hello from " + nameof(OnPostUpdatePart));
        // var updates =
        await gunService.Update(1, EditPart);
        return Content("hello from " + nameof(OnPostUpdatePart));
    }


    public async Task<IActionResult> OnGetSearch()
    {
        Console.WriteLine($"Searching for '{Term}'");
        // int _id = Term.ToInt();
        var found = await gunService.Search(new Part()
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
        // found.Dump(nameof(found));
        return Partial("HydroPartsTable", new HydroPartsTable() { Parts = found });
    }


    public async Task<IActionResult> OnGetCreatePart()
    {
        Console.WriteLine(nameof(OnGetCreatePart));

        var fakeparts = CreateFakeParts();

        fakeparts.Dump();
        // Parts.Dump("all parts");
        int count = await gunService.Create(fakeparts.Dump("fp"));
        return Content($"<b>Created '{count}' parts</b>");
    }

    private static readonly string[] part_names = new string[]
    {
        "DD 300 BLK PDW",
        "DD 556 NATO MK18",
        "MCMR-15 BCM 300 BLK Upper", "IWI Tavor X-95", "P90", "MCX Spear", "Honey Badger", "PPQ", "Sig Rattler 300 BLK",
        "JP Rifles .224 Valkyrie", "AWP"
    }.Shuffle();

    private static readonly string[] part_kinds = new string[] { "ar-15", "ar-10" }.Shuffle();

    private static readonly string[] part_manufacturers = new string[]
    {
        "Bravo Company", "Proof Research", "Aero Precision", "Faxon Firearms", "Smith & Wesson", "Dan Wesson",
        "Kahr Arms", "IWI", "Sig Sauer", "Walther", "Q LLC", "Remington", "Glock"
    }.Shuffle();

    private static readonly double[] part_costs = Enumerable.Range(5, 20).Select(x => x * 1000.00).ToArray().Shuffle();

    private Part[] CreateFakeParts(int count = 1)
    {
        var fakepart = Enumerable.Range(1, 5).Select(index => new Part
            {
                id = index,
                name = part_names.TakeFirstRandom(),
                kind = part_kinds.TakeFirstRandom(),
                manufacturer = part_manufacturers.TakeFirstRandom(),
                cost = part_costs.TakeFirstRandom(),
            })
            .ToArray();
        return fakepart;
    }
}