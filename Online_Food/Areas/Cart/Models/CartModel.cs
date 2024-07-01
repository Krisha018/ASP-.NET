namespace Online_Food.Areas.Cart.Models
{
    public class CartModel
    {
        public int? Qty { get; set; }
        public int? UserID { get; set; }
        public int CartID { get; set; }
        public int? MenuItemID { get; set; }
        public string? MenuImg { get; set; }
        public string MenuItemName { get; set; }

       
        public string Description { get; set; }
        public decimal? Price { get; set; }
    }
}
