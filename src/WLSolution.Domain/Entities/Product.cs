using System.ComponentModel.DataAnnotations;

namespace WLSolution.Domain.Entities;

public class Product
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
