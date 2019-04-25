using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HotelServer
{
    static class Server
    {
        static private int port = 8050;

        static private int status = 0;
        static private string command = "";
        static private string bookingSearch = "", bookingId = "", bookingName = "", bookingNumber = "", bookingPrice = "", bookingType = "", bookingDate = "", bookingDuration = "", bookingClean = "", bookingEat = "";
        static private string accommodationSearch = "", accommodationId = "", accommodationName = "", accommodationNumber = "", accommodationPrice = "",accommodationType = "", accommodationDate = "", accommodationDuration = "", accommodationClean = "", accommodationEat = "";
        static private bool accommodationCleanValue = false, accommodationEatValue = false;
        static private bool bookingCleanValue = false, bookingEatValue = false;
        static private string login = "", password = "";
        static public void Run()
        {
            DataBaseControl.ConnectToDatabase();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запустился и находится в ожидании запросов.");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    if (status == 0 && ((builder.ToString() == "SELECT BOOKING") ||
                                        (builder.ToString() == "ADD BOOKING") ||
                                        (builder.ToString() == "DELETE BOOKING") ||
                                        (builder.ToString() == "CHANGE BOOKING") ||
                                        (builder.ToString() == "SELECT ACCOMMODATION") ||
                                        (builder.ToString() == "ADD ACCOMMODATION") ||
                                        (builder.ToString() == "DELETE ACCOMMODATION") ||
                                        (builder.ToString() == "CHANGE ACCOMMODATION")) ||
                                        (builder.ToString() == "AUTHENTICATION"))
                    {
                        command = builder.ToString();
                        ++status;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            if (command == "SELECT ACCOMMODATION")
                            {
                                SelectAccommodation(builder.ToString(), handler);
                            }
                            if (command == "ADD ACCOMMODATION")
                            {
                                AddAccommodation(builder.ToString(), handler);
                            }
                            if (command == "DELETE ACCOMMODATION")
                            {
                                DelAccommodation(builder.ToString(), handler);
                            }
                            if (command == "CHANGE ACCOMMODATION")
                            {
                                ChangeAccommodation(builder.ToString(), handler);
                            }

                            if (command == "SELECT BOOKING")
                            {
                                SelectBooking(builder.ToString(), handler);
                            }
                            if (command == "ADD BOOKING")
                            {
                                AddBooking(builder.ToString(), handler);
                            }
                            if (command == "DELETE BOOKING")
                            {
                                DelBooking(builder.ToString(), handler);
                            }
                            if (command == "CHANGE BOOKING")
                            {
                                ChangeBooking(builder.ToString(), handler);
                            }
                            if (command == "AUTHENTICATION")
                            {
                                Authentication(builder.ToString(), handler);
                            }
                        }
                        else
                        {
                            Console.WriteLine(builder.ToString());
                            string message = "Ваше сообщение доставлено";
                            data = Encoding.Unicode.GetBytes(message);
                            handler.Send(data);
                        }
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void SelectAccommodation(string query, Socket handler)
        {
            accommodationSearch = query;
            DataTable dataTable = DataBaseControl.SelectAccommondation(accommodationSearch);
            byte[] responseData = ConvertTableToByte(dataTable);
            handler.Send(responseData);
            Clear();
        }

        static public void DelAccommodation(string query, Socket handler)
        {
            accommodationId = query;
            string response = DataBaseControl.DelAccommondation(accommodationId);
            byte[] data = new byte[256];
            data = Encoding.Unicode.GetBytes(response);
            handler.Send(data);
            Clear();
        }

        static public void AddAccommodation(string query, Socket handler)
        {
            if (accommodationName == "")
            {
                accommodationName = query;
            }
            else
            {
                if (accommodationNumber == "")
                {
                    accommodationNumber = query;
                }
                else
                {
                    if (accommodationType == "")
                    {
                        accommodationType = query;
                    }
                    else
                    {
                        if (accommodationPrice == "")
                        {
                            accommodationPrice = query;
                        }
                        else
                        {
                            if (accommodationDate == "")
                            {
                                accommodationDate = query;
                            }
                            else
                            {
                                if (accommodationDuration == "")
                                {
                                    accommodationDuration = query;
                                }
                                else
                                {
                                    if (accommodationClean == "")
                                    {
                                        accommodationClean = query;
                                        if (accommodationClean == "true")
                                        {
                                            accommodationCleanValue = true;
                                        }
                                        else
                                        {
                                            accommodationCleanValue = false;
                                        }
                                    }
                                    else
                                    {
                                        if (accommodationEat == "")
                                        {
                                            accommodationEat = query;
                                            if (accommodationEat == "true")
                                            {
                                                accommodationEatValue = true;
                                            }
                                            else
                                            {
                                                accommodationEatValue = false;
                                            }
                                            string response = DataBaseControl.AddAccommodation(accommodationName,
                                                                                               accommodationNumber,
                                                                                               accommodationType,
                                                                                               accommodationPrice,
                                                                                               accommodationDate,
                                                                                               accommodationDuration,
                                                                                               accommodationCleanValue,
                                                                                               accommodationEatValue);
                                            byte[] data = new byte[256];
                                            data = Encoding.Unicode.GetBytes(response);
                                            handler.Send(data);
                                            Clear();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static public void ChangeAccommodation(string query, Socket handler)
        {
            if (accommodationId == "")
            {
                accommodationId = query;
            }
            else
            {
                if (accommodationName == "")
                {
                    accommodationName = query;
                }
                else
                {
                    if (accommodationNumber == "")
                    {
                        accommodationNumber = query;
                    }
                    else
                    {
                        if (accommodationType == "")
                        {
                            accommodationType = query;
                        }
                        else
                        {
                            if (accommodationPrice == "")
                            {
                                accommodationPrice = query;
                            }
                            else
                            {
                                if (accommodationDate == "")
                                {
                                    accommodationDate = query;
                                }
                                else
                                {
                                    if (accommodationDuration == "")
                                    {
                                        accommodationDuration = query;
                                    }
                                    else
                                    {
                                        if (accommodationClean == "")
                                        {
                                            accommodationClean = query;
                                            if (accommodationClean == "true")
                                            {
                                                accommodationCleanValue = true;
                                            }
                                            else
                                            {
                                                accommodationCleanValue = false;
                                            }
                                        }
                                        else
                                        {
                                            if (accommodationEat == "")
                                            {
                                                accommodationEat = query;
                                                if (accommodationEat == "true")
                                                {
                                                    accommodationEatValue = true;
                                                }
                                                else
                                                {
                                                    accommodationEatValue = false;
                                                }
                                                string response = DataBaseControl.ChangeAccommodation(accommodationId,
                                                                                                      accommodationName,
                                                                                                      accommodationNumber,
                                                                                                      accommodationType,
                                                                                                      accommodationPrice,
                                                                                                      accommodationDate,
                                                                                                      accommodationDuration,
                                                                                                      accommodationCleanValue,
                                                                                                      accommodationEatValue);
                                                byte[] data = new byte[256];
                                                data = Encoding.Unicode.GetBytes(response);
                                                handler.Send(data);
                                                Clear();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static public void SelectBooking(string query, Socket handler)
        {
            bookingSearch = query;
            DataTable dataTable = DataBaseControl.SelectBooking(bookingSearch);
            byte[] responseData = ConvertTableToByte(dataTable);
            handler.Send(responseData);
            Clear();
        }

        static public void AddBooking(string query, Socket handler)
        {
            if (bookingName == "")
            {
                bookingName = query;
            }
            else
            {
                if (bookingNumber == "")
                {
                    bookingNumber = query;
                }
                else
                {
                    if (bookingType == "")
                    {
                        bookingType = query;
                    }
                    else
                    {
                        if (bookingPrice == "")
                        {
                            bookingPrice = query;
                        }
                        else
                        {
                            if (bookingDate == "")
                            {
                                bookingDate = query;
                            }
                            else
                            {
                                if (bookingDuration == "")
                                {
                                    bookingDuration = query;
                                }
                                else
                                {
                                    if (bookingClean == "")
                                    {
                                        bookingClean = query;
                                        if (bookingClean == "true")
                                        {
                                            bookingCleanValue = true;
                                        }
                                        else
                                        {
                                            bookingCleanValue = false;
                                        }
                                    }
                                    else
                                    {
                                        if (bookingEat == "")
                                        {
                                            bookingEat = query;
                                            if (bookingEat == "true")
                                            {
                                                bookingEatValue = true;
                                            }
                                            else
                                            {
                                                bookingEatValue = false;
                                            }
                                            string response = DataBaseControl.AddBooking(bookingName,
                                                                                         bookingNumber,
                                                                                         bookingType,
                                                                                         bookingPrice,
                                                                                         bookingDate,
                                                                                         bookingDuration,
                                                                                         bookingCleanValue,
                                                                                         bookingEatValue);
                                            byte[] data = new byte[256];
                                            data = Encoding.Unicode.GetBytes(response);
                                            handler.Send(data);
                                            Clear();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static public void DelBooking(string query, Socket handler)
        {
            bookingId = query;
            string response = DataBaseControl.DelBooking(bookingId);
            byte[] data = new byte[256];
            data = Encoding.Unicode.GetBytes(response);
            handler.Send(data);
            Clear();
        }

        static public void ChangeBooking(string query, Socket handler)
        {
            if (bookingId == "")
            {
                bookingId = query;
            }
            else
            {
                if (bookingName == "")
                {
                    bookingName = query;
                }
                else
                {
                    if (bookingNumber == "")
                    {
                        bookingNumber = query;
                    }
                    else
                    {
                        if (bookingType == "")
                        {
                            bookingType = query;
                        }
                        else
                        {
                            if (bookingPrice == "")
                            {
                                bookingPrice = query;
                            }
                            else
                            {
                                if (bookingDate == "")
                                {
                                    bookingDate = query;
                                }
                                else
                                {
                                    if (bookingDuration == "")
                                    {
                                        bookingDuration = query;
                                    }
                                    else
                                    {
                                        if (bookingClean == "")
                                        {
                                            bookingClean = query;
                                            if (bookingClean == "true")
                                            {
                                                bookingCleanValue = true;
                                            }
                                            else
                                            {
                                                bookingCleanValue = false;
                                            }
                                        }
                                        else
                                        {
                                            if (bookingEat == "")
                                            {
                                                bookingEat = query;
                                                if (bookingEat == "true")
                                                {
                                                    bookingEatValue = true;
                                                }
                                                else
                                                {
                                                    bookingEatValue = false;
                                                }
                                                string response = DataBaseControl.ChangeBooking(bookingId,
                                                                                                bookingName,
                                                                                                bookingNumber,
                                                                                                bookingType,
                                                                                                bookingPrice,
                                                                                                bookingDate,
                                                                                                bookingDuration,
                                                                                                bookingCleanValue,
                                                                                                bookingEatValue);
                                                byte[] data = new byte[256];
                                                data = Encoding.Unicode.GetBytes(response);
                                                handler.Send(data);
                                                Clear();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static public void Authentication(string query, Socket handler)
        {
            if (login == "")
            {
                login = query;
            }
            else
            {
                if (password == "")
                {
                    password = query;
                    string response;
                    if ((login == "admin") && (password == "admin"))
                    {
                        response = "true";
                        Console.WriteLine("Удачная аутентификация");
                    }
                    else
                    {
                        response = "false";
                        Console.WriteLine("Не удачная аутентификация");
                    }
                    byte[] data = new byte[256];
                    data = Encoding.Unicode.GetBytes(response);
                    handler.Send(data);
                    Clear();
                }
            }
        }
        static byte[] ConvertTableToByte(DataTable dt)
        {
            BinaryFormatter bFormat = new BinaryFormatter();
            byte[] outList = null;
            dt.RemotingFormat = SerializationFormat.Binary;
            using (MemoryStream ms = new MemoryStream())
            {
                bFormat.Serialize(ms, dt);
                outList = ms.ToArray();
            }
            return outList;
        }

        static private void Clear()
        {
            status = 0;
            command = "";

            bookingSearch = "";
            bookingId = "";
            bookingName = "";
            bookingNumber = "";
            bookingType = "";
            bookingDate = "";
            bookingDuration = "";
            bookingClean = "";
            bookingEat = "";
            bookingPrice = "";

            accommodationSearch = "";
            accommodationId = "";
            accommodationName = "";
            accommodationNumber = "";
            accommodationType = "";
            accommodationDate = "";
            accommodationDuration = "";
            accommodationClean = "";
            accommodationEat = "";
            accommodationPrice = "";

            login = "";
            password = "";
        }
    }
}