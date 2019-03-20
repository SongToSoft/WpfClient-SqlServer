using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System;

namespace WorkSearches
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\dbogdano\source\repos\WorkSearches\WorkSearches\Database.mdf; Integrated Security = True";
            sqlConnection.Open();
        }
 
        //Company group
        //Search Company
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (CompanySearchTextBox.Text == "")
            {
                sqlCommand.CommandText = "SELECT * FROM [Company]";
                CompanySearchLabel.Content = "Search entire table";
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Company] WHERE(CompanyName='" + CompanySearchTextBox.Text + "') OR (Vacancy='" + CompanySearchTextBox.Text + "')" ;
                CompanySearchLabel.Content = "Search: " + CompanySearchTextBox.Text;
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Company");
            sqlDataAdapter.Fill(dataTable);

            CompanyDataGrid.ItemsSource = dataTable.DefaultView;
        }

        //Add Company
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Company](Id, CompanyName, Vacancy, Salary)values(@Id, @CompanyName, @Vacancy, @Salary)";
            if ((AddCompanyId.Text == "") || (AddCompanyNameTextBox.Text == "") || (AddCompanyVacancyTextBox.Text == "") || (AddCompanySalaryTextBox.Text == ""))
            {
                AddCompanyLabel.Content = "One of the textbox is empty";
            }
            else
            {
                if (!Int32.TryParse(AddCompanySalaryTextBox.Text, out int res1) || !Int32.TryParse(AddCompanyId.Text, out int res2))
                {
                    AddCompanyLabel.Content = "Id and Salary must be number";
                }
                else
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Parameters.AddWithValue("@Id", AddCompanyId.Text);
                    sqlCommand.Parameters.AddWithValue("@CompanyName", AddCompanyNameTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("@Vacancy", AddCompanyVacancyTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToDecimal(AddCompanySalaryTextBox.Text));
                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                        AddCompanyLabel.Content = "Company added";
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        AddCompanyLabel.Content = "Company don't added, duplicate Id";
                    }
                }
            }
        }

        //Del Company
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Company] WHERE Id = @Id";
            if (DelCompanyIdTextBox.Text == "")
            {
                DelCompanyLabel.Content = "Empty id textbox";
            }
            else
            {
                if (!Int32.TryParse(DelCompanyIdTextBox.Text, out int res))
                {
                    DelCompanyLabel.Content = "Id must be number";
                }
                else
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Parameters.AddWithValue("@Id", DelCompanyIdTextBox.Text);
                    if (sqlCommand.ExecuteNonQuery() == 0)
                    {
                        DelCompanyLabel.Content = "Company don't deleted";
                    }
                    else
                    {
                        DelCompanyLabel.Content = "Company deleted";
                    }
                }
            }
        }

        //Change Company
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Company] SET CompanyName = @CompanyName, Vacancy = @Vacancy, Salary = @Salary Where Id = @Id";

            if ((ChangeCompanyId.Text == "") || (ChangeCompanyNameTextBox.Text == "") || (ChangeCompanyVacancyTextBox.Text == "") || (ChangeCompanySalaryTextBox.Text == ""))
            {
                ChangeCompanyLabel.Content = "One of the textbox is empty";
            }
            else
            {
                if (!Int32.TryParse(ChangeCompanySalaryTextBox.Text, out int res1) || !Int32.TryParse(ChangeCompanyId.Text, out int res2))
                {
                    ChangeCompanyLabel.Content = "Id and Salary must be number";
                }
                else
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Parameters.AddWithValue("@CompanyName", ChangeCompanyNameTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("@Vacancy", ChangeCompanyVacancyTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("@Salary", ChangeCompanySalaryTextBox.Text);
                    sqlCommand.Parameters.AddWithValue("@Id", ChangeCompanyId.Text);
                    if (sqlCommand.ExecuteNonQuery() == 0)
                    {
                        ChangeCompanyLabel.Content = "Company don't changed";
                    }
                    else
                    {
                        ChangeCompanyLabel.Content = "Company changed";
                    }
                }
            }
        }
    }
}
