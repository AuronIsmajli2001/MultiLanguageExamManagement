using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Dtos;

public class LanguageRequest
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string LanguageCode { get; set; }
    public int CountryId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsSelected { get; set; }
}