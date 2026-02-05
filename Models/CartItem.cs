namespace MagyarGravir.Shop.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }

        // Dummy property-k: ideiglenes helykitöltők, hogy a projekt fusson.
        public string? CustomText { get; set; }
        public string? CustomImagePath { get; set; }
    }
}
