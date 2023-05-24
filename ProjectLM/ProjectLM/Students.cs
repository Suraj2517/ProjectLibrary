using System.Data.SqlClient;
using System.Data;

namespace ProjectLM
{
    internal class Students
    {
        public static void AddStudentDetails(SqlConnection con)
        {
            Console.WriteLine("Add Student Details");
            Console.WriteLine("-------------------");

            Console.Write("Roll No: ");
            var rollNo = Console.ReadLine();
            Console.Write("Name: ");
            var name = Console.ReadLine();
            {
                con.Open();

                var query = "INSERT INTO Students (RollNo, Name) VALUES (@RollNo, @Name)";

                using (var command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);
                    command.Parameters.AddWithValue("@Name", name);

                    command.ExecuteNonQuery();
                }
                con.Close();
            }

            Console.WriteLine("Student added successfully.");
        }
        public static void EditStudentDetails(SqlConnection con)
        {
            Console.WriteLine("Edit Student Details");
            Console.WriteLine("--------------------");

            Console.Write("Enter Student ID: ");
            var studentId = int.Parse(Console.ReadLine());
            {
                con.Open();

                var query = "SELECT * FROM Students WHERE StudentId = @StudentId";
                var adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@StudentId", studentId);

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                var table = dataSet.Tables[0];
                var row = table.Rows[0];

                Console.WriteLine("Current Student Details:");
                Console.WriteLine($"Roll No: {row["RollNo"]}");
                Console.WriteLine($"Name: {row["Name"]}");

                Console.WriteLine("Enter Updated Student Details");
                Console.Write("Roll No: ");
                var rollNo = Console.ReadLine();
                Console.Write("Name: ");
                var name = Console.ReadLine();

                row["RollNo"] = rollNo;
                row["Name"] = name;

                var updateQuery = "UPDATE Students SET RollNo = @RollNo, Name = @Name WHERE StudentId = @StudentId";

                using (var command = new SqlCommand(updateQuery, con))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    command.ExecuteNonQuery();
                }
                con.Close();
            }

            Console.WriteLine("Student updated successfully.");
        }
        public static void DeleteStudentDetails(SqlConnection con)
        {
            Console.WriteLine("Delete Student Details");
            Console.WriteLine("----------------------");

            Console.Write("Enter Student ID: ");
            var studentId = int.Parse(Console.ReadLine());
            {
                con.Open();

                var query = "SELECT * FROM Students WHERE StudentId = @StudentId";
                var adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@StudentId", studentId);

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
                Console.WriteLine($"Roll No: {row["RollNo"]}");
                Console.WriteLine($"Name: {row["Name"]}");

                Console.WriteLine("Are you sure you want to delete this student? (Y/N)");
                var confirm = Console.ReadLine();

                if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    var deleteQuery = "DELETE FROM Students WHERE StudentId = @StudentId";

                    using (var command = new SqlCommand(deleteQuery, con))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Student deleted successfully.");
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
