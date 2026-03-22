namespace MagyarGravir.Shop.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    // fő kép
    public string? MainImagePath { get; set; }

    // készleten lévő darab, null = egyedi gyártás
    public int? StockQuantity { get; set; }

    public bool IsActive { get; set; } = true;
}
