namespace Online_Food.Areas.Payments.Models
{
    public class PaymentsModel
    {
        public int PaymentID { get; set; }

        public int? OrderID { get; set; }

        public string TotalAmount { get; set; }
        public string Amount { get; set; }


        public DateTime PaymentDate { get; set; }
      

        public string PaymentStatus { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class PaymentsSearchModel
    {
        public string TotalAmount { get; set; }

        public string PaymentStatus { get; set; }
    }
}
