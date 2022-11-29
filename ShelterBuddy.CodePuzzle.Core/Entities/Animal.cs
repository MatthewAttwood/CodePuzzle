using System.ComponentModel.DataAnnotations;

namespace ShelterBuddy.CodePuzzle.Core.Entities;

public class Animal : BaseEntity<Guid>
{
    [Required(ErrorMessage = "You must provide a name value.")]
    public string? Name { get; set; }
    public string? Colour { get; set; }
    public string? MicrochipNumber { get; set; }

    [Required(ErrorMessage = "You must provide a species value.")]
    public string? Species { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateInShelter { get; set; }
    public DateTime? DateLost { get; set; }
    public DateTime? DateFound { get; set; }
    public int? AgeYears { get; set; }
    public int? AgeMonths { get; set; }
    public int? AgeWeeks { get; set; }

    public string AgeText => ((AgeYears != null ? $"{AgeYears} years " : "") + (AgeMonths != null ? $"{AgeMonths} months " : "") + (AgeWeeks != null ? $"{AgeWeeks} weeks" : "")).Trim();
}