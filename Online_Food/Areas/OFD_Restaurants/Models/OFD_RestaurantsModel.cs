namespace Online_Food.Areas.OFD_Restaurants.Models
{
    public class OFD_RestaurantsModel
    {
        public int RestaurantID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Rating { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class OFD_RestaurantsSearchModel
    {
       
        public string Name { get;  set; }
        public string Address { get; set; }
    }

    public class OFD_RestaurantsDropdownModel
    {

        public int RestaurantID { get; set; }

        public string Name { get; set; }
    }

}
