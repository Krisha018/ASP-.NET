namespace Online_Food.DAL
{
    public class DAL_Helper
    {
        public static string ConnString = new
            ConfigurationBuilder().AddJsonFile
            ("appsettings.json").Build
            ().GetConnectionString("MyConnectionString");
    }
}
