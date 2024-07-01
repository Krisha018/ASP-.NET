namespace Online_Food.Areas.Orders.Models
{
    public class OrdersModel
    {
        public int OrderItemID { get; set; }

         public string TotalAmount { get; set; }
        public int? OrderID { get; set; }

        public decimal? Subtotal { get; set; }


        public int? Qty { get; set; }
        public int? MenuItemID { get; set; }

        public string MenuItemName { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class OrdersSearchModel
       
    {
        //public string TotalAmount { get; set; }
        public string MenuItemName { get; set; }

        //public int? Qty { get; set; }
    }

    //public class OrdersDropdownModel
    //{
    //    public int MenuItemID { get; set; }

    //    public string MenuItemName { get; set; }
    //}

}
