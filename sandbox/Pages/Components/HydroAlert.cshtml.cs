using CodeMechanic.Types;
using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orchard.Sandbox.Pages.Components;

[HtmlTargetElement("alert")]
public class HydroAlert : HydroView
{
    private readonly AlertType alert;

    public HydroAlert(AlertType alert_type)
    {
        this.alert = alert_type;
    }

    public string Message { get; set; } = string.Empty;
    public Maybe<Exception> Error { get; set; } = Maybe<Exception>.None;

    public string Kind => Error
        .Case(some: (_) => AlertType.Error.ToString()
            , none: () => AlertType.Success.ToString());
}

public class AlertType : Enumeration
{
    public static AlertType Error = new AlertType(1, nameof(Error), "alert-error");
    public static AlertType Success = new AlertType(2, nameof(Error), "alert-success");
    public static AlertType Warning = new AlertType(3, nameof(Error), "alert-warn");
    public static AlertType Info = new AlertType(4, nameof(Error), "alert-info");

    public AlertType(int id, string name, string value) : base(id, name)
    {
        Value = value;
    }

    public string Value { get; set; }

    public override string ToString()
    {
        return Value;
    }
}