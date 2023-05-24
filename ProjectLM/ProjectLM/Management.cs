using System.Data.SqlClient;
using System.Data;

namespace ProjectLM
{
    internal class Management
    {
        public static bool LoginUser(SqlConnection con)
        {
            Console.Write("Enter Username: ");
            var Username = Console.ReadLine();
            Console.Write("Enter Password: ");
            var Password = Console.ReadLine();
            con.Open();
            bool passed = false;

            var query = "SELECT * FROM LoginId WHERE Username = @Username and Password = @Password";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);

            SqlDataReader reader = cmd.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    passed = true;
                }
                else
                {
                    Console.WriteLine("Invalid Details");
                    passed = false;
                }
            }
            con.Close();
            return passed;
        }
        public static void IssueBookToStudent(SqlConnection con)
        {
            Console.WriteLine("Issue Book to Student");
            Console.WriteLine("---------------------");
            try
            {
            Console.Write("Enter Book ID: ");
            var bookId = int.Parse(Console.ReadLine());
            Console.Write("Enter Student ID: ");
            
                var studentId = int.Parse(Console.ReadLine());
                {
                    con.Open();
                    var checkQuery = "SELECT Stock FROM Books WHERE BookId = @BookId";
                    var checkAdapter = new SqlDataAdapter(checkQuery, con);
                    checkAdapter.SelectCommand.Parameters.AddWithValue("@BookId", bookId);

                    var checkDataSet = new DataSet();
                    checkAdapter.Fill(checkDataSet);

                    if (checkDataSet.Tables[0].Rows.Count == 0)
                    {
                        Console.WriteLine("Book not found.");
                        return;
                    }

                    var stock = (int)checkDataSet.Tables[0].Rows[0]["Stock"];

                    if (stock <= 0)
                    {
                        Console.WriteLine("Book is not available.");
                        return;
                    }
                    var studentQuery = "SELECT * FROM Students WHERE StudentId = @StudentId";
                    var studentAdapter = new SqlDataAdapter(studentQuery, con);
                    studentAdapter.SelectCommand.Parameters.AddWithValue("@StudentId", studentId);

                    var studentDataSet = new DataSet();
                    studentAdapter.Fill(studentDataSet);

                    if (studentDataSet.Tables[0].Rows.Count == 0)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }
                    var issueQuery = "INSERT INTO IssuedBooks (BookId, StudentId, IssueDate) " +
                                     "VALUES (@BookId, @StudentId, @IssueDate)";

                    using (var command = new SqlCommand(issueQuery, con))
                    {
                        command.Parameters.AddWithValue("@BookId", bookId);
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        command.Parameters.AddWithValue("@IssueDate", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                    var updateQuery = "UPDATE Books SET Stock = @Stock WHERE BookId = @BookId";

                    using (var command = new SqlCommand(updateQuery, con))
                    {
                        command.Parameters.AddWithValue("@Stock", stock - 1);
                        command.Parameters.AddWithValue("@BookId", bookId);

                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }catch (Exception ex) {Console.WriteLine(ex.Message); return; }
            Console.WriteLine("Book issued successfully.");
        }
        public static void ReturnBookFromStudent(SqlConnection con)
        {
            Console.WriteLine("Return Book from Student");
            Console.WriteLine("-----------------------");
            try
            {
            Console.Write("Enter Book ID: ");
            var bookId = int.Parse(Console.ReadLine());
            Console.Write("Enter Student ID: ");
            
                var studentId = int.Parse(Console.ReadLine());
                {
                    con.Open();
                    var checkQuery = "SELECT * FROM IssuedBooks WHERE BookId = @BookId AND StudentId = @StudentId";
                    var checkAdapter = new SqlDataAdapter(checkQuery, con);
                    checkAdapter.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    checkAdapter.SelectCommand.Parameters.AddWithValue("@StudentId", studentId);

                    var checkDataSet = new DataSet();
                    checkAdapter.Fill(checkDataSet);

                    if (checkDataSet.Tables[0].Rows.Count == 0)
                    {
                        Console.WriteLine("The book is not issued to the student.");
                        return;
                    }
                    var returnQuery = "DELETE FROM IssuedBooks WHERE BookId = @BookId AND StudentId = @StudentId";

                    using (var command = new SqlCommand(returnQuery, con))
                    {
                        command.Parameters.AddWithValue("@BookId", bookId);
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        command.ExecuteNonQuery();
                    }
                    var stockQuery = "SELECT Stock FROM Books WHERE BookId = @BookId";
                    var stockAdapter = new SqlDataAdapter(stockQuery, con);
                    stockAdapter.SelectCommand.Parameters.AddWithValue("@BookId", bookId);

                    var stockDataSet = new DataSet();
                    stockAdapter.Fill(stockDataSet);

                    var stock = (int)stockDataSet.Tables[0].Rows[0]["Stock"];

                    var updateQuery = "UPDATE Books SET Stock = @Stock WHERE BookId = @BookId";

                    using (var command = new SqlCommand(updateQuery, con))
                    {
                        command.Parameters.AddWithValue("@Stock", stock + 1);
                        command.Parameters.AddWithValue("@BookId", bookId);

                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }catch (Exception ex) {Console.WriteLine(ex.Message); return; }

            Console.WriteLine("Book returned successfully.");
        }
        public static void SearchBooksByAuthorOrPublication(SqlConnection con)
        {
            Console.WriteLine("Search Books by Author or Publication");
            Console.WriteLine("------------------------------------");
            try
            {
                Console.Write("Enter Search Term: ");
            
                var searchTerm = Console.ReadLine();
                {
                    con.Open();

                    var query = "SELECT * FROM Books WHERE Author LIKE @SearchTerm OR Publication LIKE @SearchTerm";
                    var adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        Console.WriteLine("No books found.");
                        return;
                    }

                    var table = dataSet.Tables[0];

                    Console.WriteLine("Search Results:");
                    Console.WriteLine("---------------");
                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine($"Book ID: {row["BookId"]}");
                        Console.WriteLine($"Title: {row["Title"]}");
                        Console.WriteLine($"Author: {row["Author"]}");
                        Console.WriteLine($"Publication: {row["Publication"]}");
                        Console.WriteLine($"Stock: {row["Stock"]}");
                        Console.WriteLine();
                    }
                    con.Close();
                }
            }catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }
        public static void SearchStudentByRollNo(SqlConnection con)
        {
            Console.WriteLine("Search Student by Roll No");
            Console.WriteLine("-------------------------");
            try
            {
                Console.Write("Enter Roll No: ");
            
                var rollNo = Console.ReadLine();
                {
                    con.Open();

                    var query = "SELECT * FROM Students WHERE RollNo = @RollNo";
                    var adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@RollNo", rollNo);

                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }

                    var table = dataSet.Tables[0];
                    var row = table.Rows[0];

                    Console.WriteLine("Student Details:");
                    Console.WriteLine($"Student ID: {row["StudentId"]}");
                    Console.WriteLine($"Roll No: {row["RollNo"]}");
                    Console.WriteLine($"Name: {row["Name"]}");
                    con.Close();
                }


            }catch(Exception ex) { Console.WriteLine( ex.Message); return; }
        }
        public static void GetIssuedBooksCount(SqlConnection con)
        {
            Console.WriteLine("Issued Books Count");
            Console.WriteLine("------------------");
            try
            {
                {
                    con.Open();

                    var query = "SELECT COUNT(*) FROM IssuedBooks";
                    var adapter = new SqlDataAdapter(query, con);

                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    var count = (int)dataSet.Tables[0].Rows[0][0];

                    Console.WriteLine($"Number of students with books: {count}");
                    con.Close();
                }
            }catch (Exception ex) { Console.WriteLine(ex.Message);return; }
        }
    }
}
