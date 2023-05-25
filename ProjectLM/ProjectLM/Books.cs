using System.Data.SqlClient;
using System.Data;

namespace ProjectLM
{
    internal class Books 
    {
        public static void AddBookDetails(SqlConnection con)
        {
            Console.WriteLine("Add Book Details");
            Console.WriteLine("----------------");

            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Author: ");
            var author = Console.ReadLine();
            Console.Write("Publication: ");
            var publication = Console.ReadLine();
            Console.Write("Stock: ");
            var stock = int.Parse(Console.ReadLine());
            {
                con.Open();
                var query = "INSERT INTO Books (Title, Author, Publication, Stock) " +
                            "VALUES (@Title, @Author, @Publication, @Stock)";

                using (var command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@Publication", publication);
                    command.Parameters.AddWithValue("@Stock", stock);

                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            
            Console.WriteLine("Book added successfully.");
            
        }
        public static void EditBookDetails(SqlConnection con)
        {
            Console.WriteLine("Edit Book Details");
            Console.WriteLine("-----------------");

            Console.Write("Enter Book ID: ");
            var bookId = int.Parse(Console.ReadLine());
            {
                con.Open();

                var query = "SELECT * FROM Books WHERE BookId = @BookId";
                var adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@BookId", bookId);

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }

                var table = dataSet.Tables[0];
                var row = table.Rows[0];

                Console.WriteLine("Current Book Details:");
                Console.WriteLine($"Title: {row["Title"]}");
                Console.WriteLine($"Author: {row["Author"]}");
                Console.WriteLine($"Publication: {row["Publication"]}");
                Console.WriteLine($"Stock: {row["Stock"]}");

                Console.WriteLine("Enter Updated Book Details");
                Console.Write("Title: ");
                var title = Console.ReadLine();
                Console.Write("Author: ");
                var author = Console.ReadLine();
                Console.Write("Publication: ");
                var publication = Console.ReadLine();
                Console.Write("Stock: ");
                var stock = int.Parse(Console.ReadLine());

                row["Title"] = title;
                row["Author"] = author;
                row["Publication"] = publication;
                row["Stock"] = stock;

                var updateQuery = "UPDATE Books SET Title = @Title, Author = @Author, Publication = @Publication, Stock = @Stock " +
                                  "WHERE BookId = @BookId";

                using (var command = new SqlCommand(updateQuery, con))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@Publication", publication);
                    command.Parameters.AddWithValue("@Stock", stock);
                    command.Parameters.AddWithValue("@BookId", bookId);

                    command.ExecuteNonQuery();
                }
                con.Close();
            }

            Console.WriteLine("Book updated successfully.");
        }
        public static void DeleteBookDetails(SqlConnection con)
        {
            Console.WriteLine("Delete Book Details");
            Console.WriteLine("-------------------");

            Console.Write("Enter Book ID: ");
            var bookId = int.Parse(Console.ReadLine());
            {
                con.Open();

                var query = "SELECT * FROM Books WHERE BookId = @BookId";
                var adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@BookId", bookId);

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }

                var table = dataSet.Tables[0];
                var row = table.Rows[0];

                Console.WriteLine("Book Details:");
                Console.WriteLine($"Title: {row["Title"]}");
                Console.WriteLine($"Author: {row["Author"]}");
                Console.WriteLine($"Publication: {row["Publication"]}");
                Console.WriteLine($"Stock: {row["Stock"]}");

                Console.WriteLine("Are you sure you want to delete this book? (Y/N)");
                var confirm = Console.ReadLine();

                if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    var deleteQuery = "DELETE FROM Books WHERE BookId = @BookId";

                    using (var command = new SqlCommand(deleteQuery, con))
                    {
                        command.Parameters.AddWithValue("@BookId", bookId);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Book deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
                con.Close();
            }
        }
    }
}
