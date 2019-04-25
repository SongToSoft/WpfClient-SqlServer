using System;
using System.Data;
using System.Data.SqlClient;

namespace WorkSearchesServer
{
    static class SqlCommander
    {
        static private SqlConnection sqlConnection;

        static public void ConnectToDatabase()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\dbogdano\Documents\GitHub\WPFApllication\WorkSearchesServer\WorkSearchesServer\Database.mdf; Integrated Security = True;";
            sqlConnection.Open();
        }

        static public DataTable SelectCompany(string companySearch)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (companySearch == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Company]";
            }
            else
            {
                Console.WriteLine("Поиск в таблице Компании: " + companySearch);
                sqlCommand.CommandText = "SELECT * FROM [Company] WHERE(CompanyName=N'" + companySearch.Normalize() + "') OR (Vacancy=N'" + companySearch.Normalize() + "') OR (Employment=N'" + companySearch.Normalize() + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Company");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Вакансий Компаний");
            return dataTable;
        }

        static public DataTable SelectSeeker(string seekerSearch)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (seekerSearch == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Job_Seeker]";
            }
            else
            {
                Console.WriteLine("Поиск в таблице Соискатели: " + seekerSearch);
                sqlCommand.CommandText = "SELECT * FROM [Job_Seeker] WHERE(Name=N'" + seekerSearch.Normalize() + "') OR (DesiredVacancy=N'" + seekerSearch.Normalize() + "') OR (Education=N'" + seekerSearch.Normalize() + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Job_Seeker");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Соискателей");
            return dataTable;
        }

        static public string AddCompany(string companyName, string vacancy, string salary, string employment, string requirements, string description)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Company](Id, CompanyName, Vacancy, Salary, Employment, Requirements, Description)values(@Id, @CompanyName, @Vacancy, @Salary, @Employment, @Requirements, @Description)";

            //string id = (Convert.ToString(GetCount("[Company]") + 1));
            string id = (Convert.ToString(GetId("[Company]") + 1));
    
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CompanyName", companyName);
            sqlCommand.Parameters.AddWithValue("@Vacancy", vacancy);
            sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToDecimal(salary));
            sqlCommand.Parameters.AddWithValue("@Employment", employment);
            sqlCommand.Parameters.AddWithValue("@Requirements", requirements);
            sqlCommand.Parameters.AddWithValue("@Description", description);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавление Вакансий Компаний ";
                Console.WriteLine(response + " (Id: " + id + ") (Название компании: " + companyName + ") (Вакансия:  " + vacancy + ") (Зарплата: " + salary + ")");
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "База Данных: Компания не была добавлена, описание больше 500 символов  ";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string AddSeeker(string seekerName, string seekerVacancy, string seekerSalary, string seekerEducation, string seekerMobileNumber, string seekerDescription)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Job_Seeker](Id, Name, DesiredVacancy, DesiredSalary, Education, MobileNumber, Description)values(@Id, @Name, @DesiredVacancy, @DesiredSalary, @Education, @MobileNumber, @Description)";

            //string id = (Convert.ToString(GetCount("[Job_Seeker]") + 1));
            string id = (Convert.ToString(GetId("[Job_Seeker]") + 1));

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", seekerName);
            sqlCommand.Parameters.AddWithValue("@DesiredVacancy", seekerVacancy);
            sqlCommand.Parameters.AddWithValue("@DesiredSalary", Convert.ToDecimal(seekerSalary));
            sqlCommand.Parameters.AddWithValue("@Education", seekerEducation);
            sqlCommand.Parameters.AddWithValue("@MobileNumber", seekerMobileNumber);
            sqlCommand.Parameters.AddWithValue("@Description", seekerDescription);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавлена анкета соискателя ";
                Console.WriteLine(response + " (Id: " + id + ") (Имя соискателя: " + seekerName + ") (Ожидаемая Вакансия:  " + seekerVacancy + ") (Ожидаемая Зарплата: " + seekerSalary + ")");
                return response;

            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "База Данных: Не добавлена анкета соискателя, описание больше 500 символов ";
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
                string response = "База Данных: Вакансия компании не удалена ";
                Console.WriteLine(response + " " + id);
                return response;
            }
            else
            {
                string response = "База Данных: Вакансия компании удалена ";
                Console.WriteLine(response + " " + id);
                return response;
            }
        }

        static public string DelSeeker(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Job_Seeker] WHERE Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Анкета соискателя не удалена ";
                Console.WriteLine(response + " " + id);
                return response;
            }
            else
            {
                string response = "База Данных: Анкета соискателя удалёна ";
                Console.WriteLine(response + " " + id);
                return response;
            }
        }

        static public string ChangeCompany(string id, string companyName, string vacancy, string salary, string employment, string requirements, string description)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Company] SET CompanyName = @CompanyName, Vacancy = @Vacancy, Salary = @Salary, Employment = @Employment, Requirements = @Requirements, Description = @Description Where Id = @Id";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CompanyName", companyName);
            sqlCommand.Parameters.AddWithValue("@Vacancy", vacancy);
            sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToDecimal(salary));
            sqlCommand.Parameters.AddWithValue("@Employment", employment);
            sqlCommand.Parameters.AddWithValue("@Requirements", requirements);
            sqlCommand.Parameters.AddWithValue("@Description", description);

            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Вакансия компании не изменена ";
                Console.WriteLine(response + " (Id: " + id + ") (Название компании: " + companyName + ") (Вакансия:  " + vacancy + ") (Зарплата: " + salary + ")");
                return response;
            }
            else
            {
                string response = "База Данных: Вакансия компании изменена ";
                Console.WriteLine(response + " (Id: " + id + ") (Название компании: " + companyName + ") (Вакансия:  " + vacancy + ") (Зарплата: " + salary + ")");
                return response;
            }
        }

        static public string ChangeSeeker(string seekerId, string seekerName, string seekerVacancy, string seekerSalary, string seekerEducation, string seekerMobileNumber, string seekerDescription)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Job_Seeker] SET Name = @Name, DesiredVacancy = @DesiredVacancy, DesiredSalary = @DesiredSalary, Education = @Education, MobileNumber = @MobileNumber, Description = @Description Where Id = @Id";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@Id", seekerId);
            sqlCommand.Parameters.AddWithValue("@Name", seekerName);
            sqlCommand.Parameters.AddWithValue("@DesiredVacancy", seekerVacancy);
            sqlCommand.Parameters.AddWithValue("@DesiredSalary", Convert.ToDecimal(seekerSalary));
            sqlCommand.Parameters.AddWithValue("@Education", seekerEducation);
            sqlCommand.Parameters.AddWithValue("@MobileNumber", seekerMobileNumber);
            sqlCommand.Parameters.AddWithValue("@Description", seekerDescription);

            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Анкета соискателя не изменена ";
                Console.WriteLine(response + " (Id: " + seekerId + ") (Название компании: " + seekerName + ") (Ожидаемая Вакансия:  " + seekerVacancy + ") (Ожидаемая Зарплата: " + seekerSalary + ")");
                return response;
            }
            else
            {
                string response = "База Данных: Анкета соискателя изменена ";
                Console.WriteLine(response + " (Id: " + seekerId + ") (Название компании: " + seekerName + ") (Ожидаемая Вакансия:  " + seekerVacancy + ") (Ожидаемая Зарплата: " + seekerSalary + ")");
                return response;
            }
        }

        //Get the number of rows in table
        static public int GetCount(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT COUNT(*) FROM" + tableName;
            sqlCommand.Connection = sqlConnection;
            return (int)sqlCommand.ExecuteScalar();
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
    }
}
