namespace Online_Food.Areas.Delivery_Drivers.Models
{
    public class Delivery_DriversModel
    {
        public int DriverID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string PhoneNumber { get; set; }
        public string DriverLicenceNo { get; set; }
        public string? DriverLicencePhoto { get; set; }
       

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }

    public class Delivery_DriversSearchModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }


}
