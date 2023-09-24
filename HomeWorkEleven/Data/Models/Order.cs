using System.ComponentModel.DataAnnotations;

namespace HomeWorkEleven.Models;

public class Order
{
    [Key] public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше нуля.")]
    public int Amount { get; set; }

    [Compare(nameof(DateTime), ErrorMessage = "Значение должно быть меньше текущей даты и времени.")]
    public DateTime DateTime { get; set; }
}