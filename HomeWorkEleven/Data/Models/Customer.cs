using System.ComponentModel.DataAnnotations;

namespace HomeWorkEleven.Models;

public class Customer
{
    [Key] public int CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше нуля.")]
    public int Age { get; set; }

    public string? Country { get; set; }
}