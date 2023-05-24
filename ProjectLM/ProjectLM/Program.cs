using ProjectLM;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server= US-5HSQ8S3; database=ProjectLM; Integrated Security=true");
            Console.WriteLine("Enter Login Credentials");
            bool Login = Management.LoginUser(con);
            Console.WriteLine();
            while (Login)
            {
                Console.WriteLine("Welcome to the Library Management System");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Add Book Details");
                Console.WriteLine("2. Edit Book Details");
                Console.WriteLine("3. Delete Book Details");
                Console.WriteLine("4. Add Student Details");
                Console.WriteLine("5. Edit Student Details");
                Console.WriteLine("6. Delete Student Details");
                Console.WriteLine("7. Issue Book to Student");
                Console.WriteLine("8. Return Book from Student");
                Console.WriteLine("9. Search Books");
                Console.WriteLine("10. Search Student");
                Console.WriteLine("11. View Number of Students with Books");
                Console.WriteLine("----------------------------------------");
                Console.Write("Please enter your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Books.AddBookDetails(con);
                        Console.WriteLine();
                        break;
                    case "2":
                        Books.EditBookDetails(con);
                        Console.WriteLine();
                        break;
                    case "3":
                        Books.DeleteBookDetails(con);
                        Console.WriteLine();
                        break;
                    case "4":
                        Students.AddStudentDetails(con);
                        Console.WriteLine();
                        break;
                    case "5":
                        Students.EditStudentDetails(con);
                        Console.WriteLine();
                        break;
                    case "6":
                        Students.DeleteStudentDetails(con);
                        Console.WriteLine();
                        break;
                    case "7":
                        Management.IssueBookToStudent(con);
                        Console.WriteLine();
                        break;
                    case "8":
                        Management.ReturnBookFromStudent(con);
                        Console.WriteLine();
                        break;
                    case "9":
                        Management.SearchBooksByAuthorOrPublication(con);
                        Console.WriteLine();
                        break;
                    case "10":
                        Management.SearchStudentByRollNo(con);
                        Console.WriteLine();
                        break;
                    case "11":
                        Management.GetIssuedBooksCount(con);
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("Only selected numbers are allowed");
                        Console.WriteLine();
                        break;
                }
            }
        } 
    }
}

