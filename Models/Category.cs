namespace MagyarGravir.Shop.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    // true = egyedi termék, false = készleten lévő
    public bool IsCustom { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
