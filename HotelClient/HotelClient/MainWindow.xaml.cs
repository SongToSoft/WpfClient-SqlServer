using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Windows.Media.Imaging;
using System.IO;

namespace HotelClient
{
    public partial class MainWindow : Window
    {
        private int timeout = 100;

        public MainWindow()
        {
            InitializeComponent();
            Client.SendRequestToServer("Подключение прошло успешно");
            ImageHotel.Source = new BitmapImage(new Uri(Path.GetFullPath("hotel.jpg")));
        }
 
        private int GetPrice(string type, int duration, bool clean, bool eat)
        {
            int allPrice = 0;
            int oneDayPrice = 0;
            int extraPrice = 0;
            if (type == "Standart")
                oneDayPrice = 100;
            if (type == "Studio")
                oneDayPrice = 150;
            if (type == "Family Room")
                oneDayPrice = 200;
            if (type == "Duplex")
                oneDayPrice = 220;
            if (type == "De Luxe")
                oneDayPrice = 250;
            if (type == "Junior Suite")
                oneDayPrice = 300;
            if (type == "Suite")
                oneDayPrice = 350;
            if (type == "Residence")
                oneDayPrice = 400;
            if (type == "Apartament")
                oneDayPrice = 450;
            if (type == "Honeymoon Room")
                oneDayPrice = 475;

            if (clean)
                extraPrice += 20;
            if (eat)
                extraPrice += 10;
            allPrice = (oneDayPrice + extraPrice) * duration;
            return allPrice;
        }

        private void SearchAccommondation(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (AccommondationSearchTextBox.Text == "")
                AccommondationSearchLabel.Content = "Вывод всех постоятельце";
            else
                AccommondationSearchLabel.Content = "Вывод постоятельцев: " + AccommondationSearchTextBox.Text;

            Client.SendRequestToServer("SELECT ACCOMMODATION");
            System.Threading.Thread.Sleep(timeout);
            if (AccommondationSearchTextBox.Text != "")
            {
                DataTable dataTable = Client.SendSelectRequestToServer(AccommondationSearchTextBox.Text);
                AccommondationDataGrid.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                DataTable dataTable = Client.SendSelectRequestToServer("@");
                AccommondationDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void SearchBooking(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (BookingSearchTextBox.Text == "")
                BookingSearchLabel.Content = "Вывод броней";
            else
                BookingSearchLabel.Content = "Вывод броней: " + BookingSearchTextBox.Text;

            Client.SendRequestToServer("SELECT BOOKING");
            System.Threading.Thread.Sleep(timeout);
            if (BookingSearchTextBox.Text != "")
            {
                DataTable dataTable = Client.SendSelectRequestToServer(BookingSearchTextBox.Text);
                BookingDataGrid.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                DataTable dataTable = Client.SendSelectRequestToServer("@");
                BookingDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void AddAccommondation(object sender, RoutedEventArgs e)
        {
            if ((AddAccommondationNameTextBox.Text == "") ||
                (AddAccommondationNumberTextBox.Text == "") ||
                (AddAccommondationComboBox.Text == "") ||
                (AddAccommondationDateTextBox.Text == "") ||
                (AddAccommondationDurationTextBox.Text == ""))
            {
                AddAccommondationLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddAccommondationNumberTextBox.Text, out int res1))
                {
                    AddAccommondationLabel.Content = "Количество людей должно быть числом";
                }
                else
                {
                    if (!Int32.TryParse(AddAccommondationDurationTextBox.Text, out int res2))
                    {
                        AddAccommondationLabel.Content = "Продолжительность должна быть числом";
                    }
                    else
                    {
                        Client.SendRequestToServer("ADD ACCOMMODATION");
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddAccommondationNameTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddAccommondationNumberTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddAccommondationComboBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(Convert.ToString(GetPrice(AddAccommondationComboBox.Text,
                                                                            (int)Convert.ToDecimal(AddAccommondationDurationTextBox.Text),
                                                                            (bool)AddAccommondationCleanCheckBox.IsChecked,
                                                                            (bool)AddAccommondationEatCheckBox.IsChecked)));
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddAccommondationDateTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddAccommondationDurationTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);

                        string clean = "";
                        if ((bool)AddAccommondationCleanCheckBox.IsChecked)
                            clean = "true";
                        else
                            clean = "false";
                        Client.SendRequestToServer(clean);
                        System.Threading.Thread.Sleep(timeout);
                    
                        string eat = "";
                        if ((bool)AddAccommondationEatCheckBox.IsChecked)
                            eat = "true";
                        else
                            eat = "false";
                        AddAccommondationLabel.Content = Client.SendRequestToServer(eat);
                    }
                }
            }
        }

        private void DelAccommondation(object sender, RoutedEventArgs e)
        {
            if (DelAccommondationIdTextBox.Text == "")
            {
                DelAccommondationLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelAccommondationIdTextBox.Text, out int res))
                {
                    DelAccommondationLabel.Content = "Id должно быть числом";
                }
                else
                {
                    Client.SendRequestToServer("DELETE ACCOMMODATION");
                    System.Threading.Thread.Sleep(timeout);
                    DelAccommondationLabel.Content = Client.SendRequestToServer(DelAccommondationIdTextBox.Text);
                }
            }
        }

        private void GetPriceAccommondationButton(object sender, RoutedEventArgs e)
        {
            int allPrice = 0;
            if ((AddAccommondationNameTextBox.Text == "") ||
               (AddAccommondationNumberTextBox.Text == "") ||
               (AddAccommondationComboBox.Text == "") ||
               (AddAccommondationDateTextBox.Text == "") ||
               (AddAccommondationDurationTextBox.Text == ""))
            {
                AddAccommondationLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddAccommondationNumberTextBox.Text, out int res1))
                {
                    AddAccommondationLabel.Content = "Количество людей должно быть числом";
                }
                else
                {
                    if (!Int32.TryParse(AddAccommondationDurationTextBox.Text, out int res2))
                    {
                        AddAccommondationLabel.Content = "Продолжительность должна быть числом";
                    }
                    else
                    {
                        allPrice = GetPrice(AddAccommondationComboBox.Text, 
                                           (int)Convert.ToDecimal(AddAccommondationDurationTextBox.Text),
                                           (bool)AddAccommondationCleanCheckBox.IsChecked, 
                                           (bool)AddAccommondationEatCheckBox.IsChecked);
                        AddAccommondationLabel.Content = "Общая стоимость: " + allPrice;
                    }
                }
            }
        }

        private void ChangeAccommondation(object sender, RoutedEventArgs e)
        {
            if ((ChangeAccommondationNameTextBox.Text == "") ||
               (ChangeAccommondationNumberTextBox.Text == "") ||
               (ChangeAccommondationComboBox.Text == "") ||
               (ChangeAccommondationDateTextBox.Text == "") ||
               (ChangeAccommondationDurationTextBox.Text == ""))
            {
                ChangeAccommondationLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(ChangeAccommondationIdTextBox.Text, out int res1))
                {
                    ChangeAccommondationLabel.Content = "Id должно быть числом";
                }
                if (!Int32.TryParse(ChangeAccommondationNumberTextBox.Text, out int res2))
                {
                    ChangeAccommondationLabel.Content = "Количество людей должно быть числом";
                }
                else
                {
                    if (!Int32.TryParse(ChangeAccommondationDurationTextBox.Text, out int res3))
                    {
                        ChangeAccommondationLabel.Content = "Продолжительность должна быть числом";
                    }
                    else
                    {
                        Client.SendRequestToServer("CHANGE ACCOMMODATION");
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeAccommondationIdTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeAccommondationNameTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeAccommondationNumberTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeAccommondationComboBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(Convert.ToString(GetPrice(ChangeAccommondationComboBox.Text,
                                                  (int)Convert.ToDecimal(ChangeAccommondationDurationTextBox.Text),
                                                  (bool)ChangeAccommondationCleanCheckBox.IsChecked,
                                                  (bool)ChangeAccommondationEatCheckBox.IsChecked)));
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeAccommondationDateTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeAccommondationDurationTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);

                        string clean = "";
                        if ((bool)ChangeAccommondationCleanCheckBox.IsChecked)
                            clean = "true";
                        else
                            clean = "false";
                        Client.SendRequestToServer(clean);
                        System.Threading.Thread.Sleep(timeout);

                        string eat = "";
                        if ((bool)ChangeAccommondationEatCheckBox.IsChecked)
                            eat = "true";
                        else
                            eat = "false";
                        ChangeAccommondationLabel.Content = Client.SendRequestToServer(eat);
                    }
                }
            }
        }

        private void AddBooking(object sender, RoutedEventArgs e)
        {
            if ((AddBookingNameTextBox.Text == "") ||
                (AddBookingNumberTextBox.Text == "") ||
                (AddBookingComboBox.Text == "") ||
                (AddBookingDateTextBox.Text == "") ||
                (AddBookingDurationTextBox.Text == ""))
            {
                AddBookingLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddBookingNumberTextBox.Text, out int res1))
                {
                    AddBookingLabel.Content = "Количество людей должно быть числом";
                }
                else
                {
                    if (!Int32.TryParse(AddBookingDurationTextBox.Text, out int res2))
                    {
                        AddBookingLabel.Content = "Продолжительность должна быть числом";
                    }
                    else
                    {
                        Client.SendRequestToServer("ADD BOOKING");
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddBookingNameTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddBookingNumberTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddBookingComboBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(Convert.ToString(GetPrice(AddBookingComboBox.Text,
                                                  (int)Convert.ToDecimal(AddBookingDurationTextBox.Text),
                                                  (bool)AddBookingCleanCheckBox.IsChecked,
                                                  (bool)AddBookingEatCheckBox.IsChecked)));
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddBookingDateTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(AddBookingDurationTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);

                        string clean = "";
                        if ((bool)AddBookingCleanCheckBox.IsChecked)
                            clean = "true";
                        else
                            clean = "false";
                        Client.SendRequestToServer(clean);
                        System.Threading.Thread.Sleep(timeout);

                        string eat = "";
                        if ((bool)AddBookingEatCheckBox.IsChecked)
                            eat = "true";
                        else
                            eat = "false";
                        AddBookingLabel.Content = Client.SendRequestToServer(eat);
                    }
                }
            }
        }

        private void DelBooking(object sender, RoutedEventArgs e)
        {
            if (DelBookingIdTextBox.Text == "")
            {
                DelBookingLabel.Content = "Поле Id пустое";
            }
            else
            {
                if (!Int32.TryParse(DelBookingIdTextBox.Text, out int res))
                {
                    DelBookingLabel.Content = "Id должно быть числом";
                }
                else
                {
                    Client.SendRequestToServer("DELETE BOOKING");
                    System.Threading.Thread.Sleep(timeout);
                    DelBookingLabel.Content = Client.SendRequestToServer(DelBookingIdTextBox.Text);
                }
            }
        }

        private void ChangeBooking(object sender, RoutedEventArgs e)
        {
            if ((ChangeBookingNameTextBox.Text == "") ||
               (ChangeBookingNumberTextBox.Text == "") ||
               (ChangeBookingComboBox.Text == "") ||
               (ChangeBookingDateTextBox.Text == "") ||
               (ChangeBookingDurationTextBox.Text == ""))
            {
                ChangeBookingLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(ChangeBookingIdTextBox.Text, out int res1))
                {
                    ChangeBookingLabel.Content = "Id должно быть числом";
                }
                if (!Int32.TryParse(ChangeBookingNumberTextBox.Text, out int res2))
                {
                    ChangeBookingLabel.Content = "Количество людей должно быть числом";
                }
                else
                {
                    if (!Int32.TryParse(ChangeBookingDurationTextBox.Text, out int res3))
                    {
                        ChangeBookingLabel.Content = "Продолжительность должна быть числом";
                    }
                    else
                    {
                        Client.SendRequestToServer("CHANGE BOOKING");
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeBookingIdTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeBookingNameTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeBookingNumberTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeBookingComboBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(Convert.ToString(GetPrice(ChangeBookingComboBox.Text,
                                                  (int)Convert.ToDecimal(ChangeBookingDurationTextBox.Text),
                                                  (bool)ChangeBookingCleanCheckBox.IsChecked,
                                                  (bool)ChangeBookingEatCheckBox.IsChecked)));
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeBookingDateTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);
                        Client.SendRequestToServer(ChangeBookingDurationTextBox.Text);
                        System.Threading.Thread.Sleep(timeout);

                        string clean = "";
                        if ((bool)ChangeBookingCleanCheckBox.IsChecked)
                            clean = "true";
                        else
                            clean = "false";
                        Client.SendRequestToServer(clean);
                        System.Threading.Thread.Sleep(timeout);

                        string eat = "";
                        if ((bool)ChangeBookingEatCheckBox.IsChecked)
                            eat = "true";
                        else
                            eat = "false";
                        ChangeBookingLabel.Content = Client.SendRequestToServer(eat);
                    }
                }
            }
        }

        private void GetPriceBookingButton(object sender, RoutedEventArgs e)
        {
            int allPrice = 0;
            if ((AddBookingNameTextBox.Text == "") ||
               (AddBookingNumberTextBox.Text == "") ||
               (AddBookingComboBox.Text == "") ||
               (AddBookingDateTextBox.Text == "") ||
               (AddBookingDurationTextBox.Text == ""))
            {
                AddBookingLabel.Content = "Одно из основных полей пустое";
            }
            else
            {
                if (!Int32.TryParse(AddBookingNumberTextBox.Text, out int res1))
                {
                    AddBookingLabel.Content = "Количество людей должно быть числом";
                }
                else
                {
                    if (!Int32.TryParse(AddBookingDurationTextBox.Text, out int res2))
                    {
                        AddBookingLabel.Content = "Продолжительность должна быть числом";
                    }
                    else
                    {
                        allPrice = GetPrice(AddBookingComboBox.Text,
                                           (int)Convert.ToDecimal(AddBookingDurationTextBox.Text),
                                           (bool)AddBookingCleanCheckBox.IsChecked,
                                           (bool)AddBookingEatCheckBox.IsChecked);
                        AddBookingLabel.Content = "Общая стоимость: " + allPrice;
                    }
                }
            }
        }

        private void Authentication(object sender, RoutedEventArgs e)
        {
            if ((LoginTextBox.Password == "") || (PasswordTextBox.Password == ""))
            {
                AuthenticationLabel.Content = "Одно из полей пустое";
            }
            else
            {
                string response = "";
                Client.SendRequestToServer("AUTHENTICATION");
                System.Threading.Thread.Sleep(timeout);
                Client.SendRequestToServer(LoginTextBox.Password);
                System.Threading.Thread.Sleep(timeout);
                response = Client.SendRequestToServer(PasswordTextBox.Password);
                if (response == "true")
                {
                    AuthenticationRectangle.Visibility = System.Windows.Visibility.Hidden;
                    LoginTextBox.Visibility = System.Windows.Visibility.Hidden;
                    PasswordTextBox.Visibility = System.Windows.Visibility.Hidden;
                    AuthenticationButton.Visibility = System.Windows.Visibility.Hidden;
                    AuthenticationLabel.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    AuthenticationLabel.Content = "Неверный пароль или логин";
                }
            }
        }
    }
}
 
