namespace Online_Food.Areas.FoodOrder.Models
{
    public class FoodOrderModel
    {
        public int OrderID { get; set; }

        public int? UserID { get; set; }
        public string? FoodImg { get; set; }
        public string FirstName { get; set; }
        public DateTime OrderDate { get; set; }


        public string? TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
       



        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class FoodOrderSearchModel
    {
        //public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string TotalAmount { get; set; }
      
    }

    public class FoodOrderDropdowModel
    {
        public int OrderID { get; set; }

        public string TotalAmount { get; set; }
    }


}
