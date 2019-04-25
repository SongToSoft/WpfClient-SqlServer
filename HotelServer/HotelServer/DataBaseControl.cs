using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace HotelServer
{
    class DataBaseControl
    {
        static private SqlConnection sqlConnection;

        static public void ConnectToDatabase()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\dbogdano\source\repos\HotelServer\HotelServer\HotelDatabase.mdf; Integrated Security = True;";
            sqlConnection.Open();
        }

        static public int GetId(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT MAX(Id) FROM " + tableName;
            sqlCommand.Connection = sqlConnection;
            object obj = sqlCommand.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return id;
            }
        }

        static public DataTable SelectAccommondation(string query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (query == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Заселение]";
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Заселение] WHERE(Name=N'" + query.Normalize() + "') OR (TypeRoom=N'" + query.Normalize() + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Заселение");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("Вывод заселившихся");
            return dataTable;
        }

        static public string AddAccommodation(string name, string number, string type, string price, string date, string duration, bool clean, bool eat)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Заселение](Id, Name, NumberPeople, TypeRoom, Price, ArrivalDate, Duration, Cleaning, Breakfast) values (@Id, @Name, @Number, @Type, @Price, @Date, @Duration, @Clean, @Eat)";

            string id = (Convert.ToString(GetId("[Заселение]") + 1));

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@Number", Convert.ToDecimal(number));
            sqlCommand.Parameters.AddWithValue("@Type", type);
            sqlCommand.Parameters.AddWithValue("@Price", Convert.ToDecimal(price));
            sqlCommand.Parameters.AddWithValue("@Date", date);
            sqlCommand.Parameters.AddWithValue("@Duration", Convert.ToDecimal(duration));
            sqlCommand.Parameters.AddWithValue("@Clean", clean);
            sqlCommand.Parameters.AddWithValue("@Eat", eat);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Постояльцы заселены"; 
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "Постояльцы не заселены";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string DelAccommondation(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Заселение] WHERE Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Постояльцы не выселены ";
                Console.WriteLine(response);
                return response;
            }
            else
            {
                string response = "Постояльцы выселены";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string ChangeAccommodation(string id, string name, string number, string type, string price, string date, string duration, bool clean, bool eat)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Заселение] SET Name = @Name, NumberPeople = @NumberPeople, TypeRoom = @TypeRoom, Price = @Price, ArrivalDate = @ArrivalDate, Duration = @Duration, Cleaning = @Cleaning, Breakfast = @Breakfast Where Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@NumberPeople", Convert.ToDecimal(number));
            sqlCommand.Parameters.AddWithValue("@TypeRoom", type);
            sqlCommand.Parameters.AddWithValue("@Price", Convert.ToDecimal(price));
            sqlCommand.Parameters.AddWithValue("@ArrivalDate", date);
            sqlCommand.Parameters.AddWithValue("@Duration", Convert.ToDecimal(duration));
            sqlCommand.Parameters.AddWithValue("@Cleaning", clean);
            sqlCommand.Parameters.AddWithValue("@Breakfast", eat);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Статус постояльцев изменён";
                Console.WriteLine(response);
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "Статус постояльцев не изменён";
                Console.WriteLine(response);
                return response;
            }
        }

        static public DataTable SelectBooking(string query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (query == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Бронирование]";
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Бронирование] WHERE(Name=N'" + query.Normalize() + "') OR (TypeRoom=N'" + query.Normalize() + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Бронирование");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("Вывод забронируемых номеров");
            return dataTable;
        }

        static public string AddBooking(string name, string number, string type, string price, string date, string duration, bool clean, bool eat)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Бронирование](Id, Name, NumberPeople, TypeRoom, Price, ArrivalDate, Duration, Cleaning, Breakfast) values (@Id, @Name, @Number, @Type, @Price, @Date, @Duration, @Clean, @Eat)";

            string id = (Convert.ToString(GetId("[Бронирование]") + 1));

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@Number", Convert.ToDecimal(number));
            sqlCommand.Parameters.AddWithValue("@Type", type);
            sqlCommand.Parameters.AddWithValue("@Price", Convert.ToDecimal(price));
            sqlCommand.Parameters.AddWithValue("@Date", date);
            sqlCommand.Parameters.AddWithValue("@Duration", Convert.ToDecimal(duration));
            sqlCommand.Parameters.AddWithValue("@Clean", clean);
            sqlCommand.Parameters.AddWithValue("@Eat", eat);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Бронь добавлена";
                Console.WriteLine(response);
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "Бронь не добавлена";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string DelBooking(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Бронирование] WHERE Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Бронь не удалена";
                Console.WriteLine(response);
                return response;
            }
            else
            {
                string response = "Бронь удалена";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string ChangeBooking(string id, string name, string number, string type, string price, string date, string duration, bool clean, bool eat)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Бронирование] SET Name = @Name, NumberPeople = @NumberPeople, TypeRoom = @TypeRoom, Price = @Price, ArrivalDate = @ArrivalDate, Duration = @Duration, Cleaning = @Cleaning, Breakfast = @Breakfast Where Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@NumberPeople", Convert.ToDecimal(number));
            sqlCommand.Parameters.AddWithValue("@TypeRoom", type);
            sqlCommand.Parameters.AddWithValue("@Price", Convert.ToDecimal(price));
            sqlCommand.Parameters.AddWithValue("@ArrivalDate", date);
            sqlCommand.Parameters.AddWithValue("@Duration", Convert.ToDecimal(duration));
            sqlCommand.Parameters.AddWithValue("@Cleaning", clean);
            sqlCommand.Parameters.AddWithValue("@Breakfast", eat);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Статус бронирования изменён";
                Console.WriteLine(response);
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "Статус бронирования не изменён";
                Console.WriteLine(response);
                return response;
            }
        }
    }
}