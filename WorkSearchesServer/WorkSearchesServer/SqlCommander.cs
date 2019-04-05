using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;

namespace WorkSearchesServer
{
    static class SqlCommander
    {
        static private SqlConnection sqlConnection;

        static public void ConnectToDatabase()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\dbogdano\source\repos\WorkSearchesServer\WorkSearchesServer\Database.mdf; Integrated Security = True";
            sqlConnection.Open();
        }

        static public DataTable SelectCompany(string companySearch)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (companySearch == "@")
            {

                Console.WriteLine("CompanySearch: " + companySearch);
                sqlCommand.CommandText = "SELECT * FROM [Company]";
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Company] WHERE(CompanyName='" + companySearch + "') OR (Vacancy='" + companySearch + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Company");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("Database: Select Company");
            return dataTable;
        }

        static public string AddCompany(string id, string companyName, string vacancy, string salary)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Company](Id, CompanyName, Vacancy, Salary)values(@Id, @CompanyName, @Vacancy, @Salary)";
    
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CompanyName", companyName);
            sqlCommand.Parameters.AddWithValue("@Vacancy", vacancy);
            sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToDecimal(salary));
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Database: Add Company";
                Console.WriteLine(response + " (Id: " + id + ") (CompanyName: " + companyName + ") (Vacancy:  " + vacancy + ") (Salary: " + salary + ")");
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "Database: Company don't added, duplicate Id";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string DelCompany(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Company] WHERE Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Company don't deleted ";
                Console.WriteLine(response + " " + id);
                return response;
            }
            else
            {
                string response = "Company deleted ";
                Console.WriteLine(response + " " + id);
                return response;
            }
        }

        static public string ChangeCompany(string id, string companyName, string vacancy, string salary)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Company] SET CompanyName = @CompanyName, Vacancy = @Vacancy, Salary = @Salary Where Id = @Id";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CompanyName", companyName);
            sqlCommand.Parameters.AddWithValue("@Vacancy", vacancy);
            sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToDecimal(salary));

            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Company don't changed";
                Console.WriteLine(response + " (Id: " + id + ") (CompanyName: " + companyName + ") (Vacancy:  " + vacancy + ") (Salary: " + salary + ")");
                return response;
            }
            else
            {
                string response = "Company changed";
                Console.WriteLine(response + " (Id: " + id + ") (CompanyName: " + companyName + ") (Vacancy:  " + vacancy + ") (Salary: " + salary + ")");
                return response;
            }
        }
    }
}
