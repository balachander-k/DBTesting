using System;
using MySql.Data.MySqlClient;

class Program
{
    // MySQL Connection String - Change these values accordingly
    //static string connectionString = "Server=localhost-address;Database=dotnetpractice;User Id=root;Password=vroot;";
    static string connectionString = "Server=localhost;Database=dotnetpractice; User ID=root;Password=vroot ;Port=3306;";


    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== CRUD Operations Menu =====");
            Console.WriteLine("1. Display All Records");
            Console.WriteLine("2. Insert New Record");
            Console.WriteLine("3. Update Existing Record");
            Console.WriteLine("4. Delete a Record");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayRecords();
                    break;
                case "2":
                    InsertRecord();
                    break;
                case "3":
                    UpdateRecord();
                    break;
                case "4":
                    DeleteRecord();
                    break;
                case "5":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please enter a number between 1 and 5.");
                    break;
            }
        }
    }

    // Display Records
    static void DisplayRecords()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM users";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n===== User Records =====");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Age: {reader["age"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Insert a New Record
    static void InsertRecord()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.Write("Invalid input. Enter a valid age: ");
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "INSERT INTO users (name, age) VALUES (@name, @age)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Record inserted successfully!" : "Insert failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Update an Existing Record
    static void UpdateRecord()
    {
        Console.Write("Enter User ID to Update: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input. Enter a valid ID: ");
        }

        Console.Write("Enter New Name: ");
        string newName = Console.ReadLine();

        Console.Write("Enter New Age: ");
        int newAge;
        while (!int.TryParse(Console.ReadLine(), out newAge))
        {
            Console.Write("Invalid input. Enter a valid age: ");
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "UPDATE users SET name = @newName, age = @newAge WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@newName", newName);
                cmd.Parameters.AddWithValue("@newAge", newAge);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Record updated successfully!" : "Update failed. ID not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Delete a Record
    static void DeleteRecord()
    {
        Console.Write("Enter User ID to Delete: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input. Enter a valid ID: ");
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM users WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Record deleted successfully!" : "Delete failed. ID not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
