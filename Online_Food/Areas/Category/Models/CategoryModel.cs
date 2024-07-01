namespace Online_Food.Areas.Category.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }


        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
     

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
    public class CategorySearchModel
    {
        public string CategoryName { get; set; }

        public string CategoryType { get; set; }
    }

    public class CategoryDropdownModel
    {
        public int CategoryID { get; set; }


        public string CategoryName { get; set; }
    }
}

