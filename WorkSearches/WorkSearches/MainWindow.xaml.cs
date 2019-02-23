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
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Company] WHERE(CompanyName='" + CompanySearchTextBox.Text + "') AND (Vacancy='" + CompanySearchTextBox.Text + "')" ;
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
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", AddCompanyId.Text);
            sqlCommand.Parameters.AddWithValue("@CompanyName", AddCompanyNameTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Vacancy", AddCompanyVacancyTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToDecimal(AddCompanySalaryTextBox.Text));
            sqlCommand.ExecuteNonQuery();
        }

        //Del Company
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Company] WHERE CompanyName = @CompanyName";
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@CompanyName", DelCompanyNameTextBox.Text);
            sqlCommand.ExecuteNonQuery();
        }

        //Change Company
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Company] SET CompanyName = @CompanyName, Vacancy = @Vacancy, Salary = @Salary Where Id = @Id";
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@CompanyName", ChangeCompanyNameTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Vacancy", ChangeCompanyVacancyTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Salary", ChangeCompanySalaryTextBox.Text);
            sqlCommand.Parameters.AddWithValue("@Id", ChangeCompanyId.Text);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
