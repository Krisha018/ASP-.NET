//namespace Online_Food
//{
//    public class class1
//    {
//        public int Id { get; set; }
//        public int FirstName { get; set; }
//        public int LastName { get; set; }


//    }
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            Console.WriteLine("\n----- Using Query Syntax---\n");
//            List<class1> basicQuery = (from emp in class1.Getclass1()select emp).ToList();
//            foreach(class1 emp in basicQuery)
//            {
//                Console.WriteLine($"Id:{emp.Id}Name:{emp.FirstName}{emp.LastName}");
//            }
//            //using Method Syntax
//            IEnumerable<class1>basicMethod=class1.Getclass1().ToList();
//            Console.WriteLine("\n----- Using Query Syntax---\n");
//            foreach(class1 emp in basicMethod)
//            {

//            }
//        }
//    }
//}
