using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System;

using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WorkSearches
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Client.SendRequestToServer("Client is connected");
        }
 
        //Company group
        //Search Company
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (CompanySearchTextBox.Text == "")
            {
                CompanySearchLabel.Content = "Search entire table";
            }
            else
            {
                CompanySearchLabel.Content = "Search: " + CompanySearchTextBox.Text;
            }

            Client.SendRequestToServer("SELECT");
            System.Threading.Thread.Sleep(1000);
            if (CompanySearchTextBox.Text != "")
            {
                DataTable dataTable = Client.SendSelectRequestToServer(CompanySearchTextBox.Text);
                CompanyDataGrid.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                DataTable dataTable = Client.SendSelectRequestToServer("@");
                CompanyDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        //Add Company
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
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
                    Client.SendRequestToServer("ADD");
                    System.Threading.Thread.Sleep(1000);
                    Client.SendRequestToServer(AddCompanyId.Text);
                    System.Threading.Thread.Sleep(1000);
                    Client.SendRequestToServer(AddCompanyNameTextBox.Text);
                    System.Threading.Thread.Sleep(1000);
                    Client.SendRequestToServer(AddCompanyVacancyTextBox.Text);
                    System.Threading.Thread.Sleep(1000);
                    AddCompanyLabel.Content = Client.SendRequestToServer(AddCompanySalaryTextBox.Text);
                }
            }
        }

        //Del Company
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
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
                    Client.SendRequestToServer("DELETE");
                    System.Threading.Thread.Sleep(1000);
                    DelCompanyLabel.Content = Client.SendRequestToServer(DelCompanyIdTextBox.Text);
                }
            }
        }

        //Change Company
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
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
                    Client.SendRequestToServer("UPDATE");
                    System.Threading.Thread.Sleep(1000);
                    Client.SendRequestToServer(ChangeCompanyId.Text);
                    System.Threading.Thread.Sleep(1000);
                    Client.SendRequestToServer(ChangeCompanyNameTextBox.Text);
                    System.Threading.Thread.Sleep(1000);
                    Client.SendRequestToServer(ChangeCompanyVacancyTextBox.Text);
                    System.Threading.Thread.Sleep(1000);
                    ChangeCompanyLabel.Content = Client.SendRequestToServer(ChangeCompanySalaryTextBox.Text);
                }
            }
        }
    }
}
