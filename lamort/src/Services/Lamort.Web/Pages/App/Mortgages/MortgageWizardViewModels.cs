using System.ComponentModel.DataAnnotations;

namespace Lamort.Web.Pages.App.Mortgages;

public static class MortgageAmountPattern
{
    // Optional $, up to 15 digits, optional 1-2 decimal places.
    public const string Value = @"^\$?[0-9]{1,15}(\.[0-9]{1,2})?$";
}

public sealed class MortgageAssetInput
{
    [Display(Name = "Asset name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Amount")]
    [StringLength(100)]
    [RegularExpression(MortgageAmountPattern.Value, ErrorMessage = "Enter a valid amount, such as 1500 or 1500.50.")]
    public string Amount { get; set; } = string.Empty;
}

public sealed class MortgageIncomeInput
{
    [Display(Name = "Income source")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Income type")]
    public string IncomeType { get; set; } = string.Empty;

    [Display(Name = "Amount")]
    [StringLength(100)]
    [RegularExpression(MortgageAmountPattern.Value, ErrorMessage = "Enter a valid amount, such as 1500 or 1500.50.")]
    public string Amount { get; set; } = string.Empty;
}

public sealed class MortgageDebtInput
{
    [Display(Name = "Debt name")]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "999999999999999.99")]
    [Display(Name = "Amount")]
    public decimal? Amount { get; set; }
}
