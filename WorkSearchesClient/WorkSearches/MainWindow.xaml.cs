using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System;
using WorkSearches;

namespace WorkSearchesClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timeout = 100;

        public MainWindow()
        {
            InitializeComponent();
            Client.SendRequestToServer("Клиент подключён");
        }
 
        //Company group
        //Search Company
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (CompanySearchTextBox.Text == "")
            {
                CompanySearchLabel.Content = "Поиск по всей таблице \"Компании\"";
            }
            else
            {
                CompanySearchLabel.Content = "Поиск: " + CompanySearchTextBox.Text;
            }

            Client.SendRequestToServer("SELECT COMPANY");
            System.Threading.Thread.Sleep(timeout);
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
            CompanyDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
        }

        //Add Company
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((AddCompanyNameTextBox.Text == "") || (AddCompanyVacancyTextBox.Text == "") || (AddCompanySalaryTextBox.Text == ""))
            {
                AddCompanyLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddCompanySalaryTextBox.Text, out int res1))
                {
                    AddCompanyLabel.Content = "Зарплата должна быть числом";
                }
                else
                {
                    Client.SendRequestToServer("ADD COMPANY");
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddCompanyNameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddCompanyVacancyTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddCompanySalaryTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddCompanyEmploymentTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddCompanyRequirementsTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    AddCompanyLabel.Content = Client.SendRequestToServer(AddCompanyDescriptionTextBox.Text);
                }
            }
        }

        //Del Company
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DelCompanyIdTextBox.Text == "")
            {
                DelCompanyLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelCompanyIdTextBox.Text, out int res))
                {
                    DelCompanyLabel.Content = "Id должно быть числом";
                }
                else
                {
                    Client.SendRequestToServer("DELETE COMPANY");
                    System.Threading.Thread.Sleep(timeout);
                    DelCompanyLabel.Content = Client.SendRequestToServer(DelCompanyIdTextBox.Text);
                }
            }
        }

        //Change Company
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if ((ChangeCompanyId.Text == "") || (ChangeCompanyNameTextBox.Text == "") || (ChangeCompanyVacancyTextBox.Text == "") || (ChangeCompanySalaryTextBox.Text == ""))
            {
                ChangeCompanyLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(ChangeCompanySalaryTextBox.Text, out int res1) || !Int32.TryParse(ChangeCompanyId.Text, out int res2))
                {
                    ChangeCompanyLabel.Content = "Поля Id и Зарплата должны быть числами";
                }
                else
                {
                    Client.SendRequestToServer("UPDATE COMPANY");
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeCompanyId.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeCompanyNameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeCompanyVacancyTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeCompanySalaryTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeCompanyEmploymentTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeCompanyRequirementsTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ChangeCompanyLabel.Content = Client.SendRequestToServer(ChangeCompanyDescriptionTextBox.Text);
                }
            }
        }

        //Seeker group
        //Search Seeker
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (SeekerSearchTextBox.Text == "")
            {
                CompanySearchLabel.Content = "Поиск по всей таблице \"Соискатели\"";
            }
            else
            {
                SeekerSearchLabel.Content = "Поиск: " + SeekerSearchTextBox.Text;
            }

            Client.SendRequestToServer("SELECT SEEKER");
            System.Threading.Thread.Sleep(timeout);
            if (SeekerSearchTextBox.Text != "")
            {
                DataTable dataTable = Client.SendSelectRequestToServer(SeekerSearchTextBox.Text);
                SeekerDataGrid.ItemsSource = dataTable.DefaultView;
                SeekerDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                DataTable dataTable = Client.SendSelectRequestToServer("@");
                SeekerDataGrid.ItemsSource = dataTable.DefaultView;
                SeekerDataGrid.Columns[6].Visibility = System.Windows.Visibility.Hidden;
            }
        }

        //Add Seeker
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if ((AddSeekerNameTextBox.Text == "") || (AddSeekerVacancyTextBox.Text == "") || (AddSeekerSalaryTextBox.Text == ""))
            {
                AddSeekerLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddSeekerSalaryTextBox.Text, out int res1))
                {
                    AddSeekerLabel.Content = "Зарплата должна быть числом";
                }
                else
                {
                    Client.SendRequestToServer("ADD SEEKER");
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddSeekerNameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddSeekerVacancyTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddSeekerSalaryTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddSeekerEducationTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(AddSeekerMobileNumberTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    AddSeekerLabel.Content = Client.SendRequestToServer(AddSeekerDescriptionTextBox.Text);
                }
            }
        }

        //Del Seeker
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (DelSeekerIdTextBox.Text == "")
            {
                DelSeekerLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelSeekerIdTextBox.Text, out int res))
                {
                    DelSeekerLabel.Content = "Id должно быть числом";
                }
                else
                {
                    Client.SendRequestToServer("DELETE SEEKER");
                    System.Threading.Thread.Sleep(timeout);
                    DelSeekerLabel.Content = Client.SendRequestToServer(DelSeekerIdTextBox.Text);
                }
            }
        }


        //Change Seeker
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if ((ChangeSeekerIdTextBox.Text == "") || (ChangeSeekerNameTextBox.Text == "") || (ChangeSeekerVacancyTextBox.Text == "") || (ChangeSeekerSalaryTextBox.Text == ""))
            {
                ChangeSeekerLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(ChangeSeekerSalaryTextBox.Text, out int res1) || !Int32.TryParse(ChangeSeekerIdTextBox.Text, out int res2))
                {
                    ChangeSeekerLabel.Content = "Поля Id и Зарплата должны быть числами";
                }
                else
                {
                    Client.SendRequestToServer("UPDATE SEEKER");
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeSeekerIdTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeSeekerNameTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeSeekerVacancyTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeSeekerSalaryTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeSeekerEducationTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    Client.SendRequestToServer(ChangeSeekerMobileNumberTextBox.Text);
                    System.Threading.Thread.Sleep(timeout);
                    ChangeSeekerLabel.Content = Client.SendRequestToServer(ChangeSeekerDescriptionTextBox.Text);
                }
            }
        }
    }
}
