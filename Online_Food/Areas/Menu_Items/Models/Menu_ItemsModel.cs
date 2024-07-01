using System.ComponentModel.DataAnnotations;

namespace Online_Food.Areas.Menu_Items.Models
{
    public class Menu_ItemsModel
    {
        //[Required(ErrorMessage = "Please Enter MenuItemID")]
        public int MenuItemID { get; set; }

        //[Required(ErrorMessage = "Please Enter RestaurantID")]

        //public IFormFile? ImageFile { get; set; } // For file upload
        public string MenuImg { get; set; }
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
    public class Menu_ItemsSearchModel
    {
        
        public string MenuItemName   { get; set; }

       
    }
    public class Menu_ItemsDropdownModel
    {
        public int MenuItemID { get; set; }
        public string MenuItemName { get; set; }

        
    }


}
