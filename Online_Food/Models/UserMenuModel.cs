
namespace Online_Food.Models
{
    public class UserMenuModel
    {
        public int MenuItemID { get; set; }

        //[Required(ErrorMessage = "Please Enter RestaurantID")]

        //public IFormFile? ImageFile { get; set; } // For file upload
        public string? MenuImg { get; set; }
        public int? RestaurantID { get; set; }
        public string Name { get; set; }

        public string MenuItemName { get; set; }


        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
