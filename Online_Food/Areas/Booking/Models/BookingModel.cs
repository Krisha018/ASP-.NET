namespace Online_Food.Areas.Booking.Models
{
    public class BookingModel
    {
        public int BookingID { get; set; }
        public int? UserID { get; set; }

        public string FirstName { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public int? MenuItemID { get; set; }

        public string MenuItemName { get; set; }

        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }
       

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
    public class BookingSearchModel
    {
        public string FirstName { get; set; }

        public string MenuItemName { get; set; }
    }
}

