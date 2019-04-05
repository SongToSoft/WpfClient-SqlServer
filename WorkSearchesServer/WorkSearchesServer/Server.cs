using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WorkSearchesServer
{
    static class Server
    {
        static private int status = 0;
        static private string command = "empty";
        static private string companySearch = "", id = "", companyName = "", vacancy = "", salary = "";
        static private int port = 4020;

        static public void Run()
        {
            SqlCommander.ConnectToDatabase();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine("The server is running. Waiting for connections...");

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

                    if (status == 0 && ((builder.ToString() == "SELECT") ||
                                        (builder.ToString() == "ADD")    ||
                                        (builder.ToString() == "DELETE") || 
                                        (builder.ToString() == "UPDATE")))
                    {
                        command = builder.ToString();
                        ++status;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            if (command == "SELECT")
                            {
                                companySearch = builder.ToString();
                                DataTable dataTable = SqlCommander.SelectCompany(companySearch);
                                byte[] responseData = GetBinaryFormatData(dataTable);
                                handler.Send(responseData);
                                Clear();
                            }
                            if (command == "ADD")
                            {
                                if (id == "")
                                {
                                    id = builder.ToString();
                                }
                                else
                                {
                                    if (companyName == "")
                                    {
                                        companyName = builder.ToString();
                                    }
                                    else
                                    {
                                        if (vacancy == "")
                                        {
                                            vacancy = builder.ToString();
                                        }
                                        else
                                        {
                                            if (salary == "")
                                            {
                                                salary = builder.ToString();
                                                string response = SqlCommander.AddCompany(id, companyName, vacancy, salary);
                                                data = Encoding.Unicode.GetBytes(response);
                                                handler.Send(data);
                                                Clear();
                                            }
                                        }
                                    }
                                }
                            }
                            if (command == "DELETE")
                            {
                                id = builder.ToString();
                                string response = SqlCommander.DelCompany(id);
                                data = Encoding.Unicode.GetBytes(response);
                                handler.Send(data);
                                Clear();
                            }
                            if (command == "UPDATE")
                            {
                                if (id == "")
                                {
                                    id = builder.ToString();
                                }
                                else
                                {
                                    if (companyName == "")
                                    {
                                        companyName = builder.ToString();
                                    }
                                    else
                                    {
                                        if (vacancy == "")
                                        {
                                            vacancy = builder.ToString();
                                        }
                                        else
                                        {
                                            if (salary == "")
                                            {
                                                salary = builder.ToString();
                                                string response = SqlCommander.ChangeCompany(id, companyName, vacancy, salary);
                                                data = Encoding.Unicode.GetBytes(response);
                                                handler.Send(data);
                                                Clear();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            string message = "Your message has been delivered";
                            data = Encoding.Unicode.GetBytes(message);
                            handler.Send(data);
                        }
                    }

                    //Console.WriteLine(DateTime.Now.ToShortTimeString() + " status: " + status + " command: " + command + " message: " + builder.ToString());

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Serialize DataTable for Select
        static byte[] GetBinaryFormatData(DataTable dt)
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
            command = "empty";
            companySearch = "";
            id = "";
            companyName = "";
            vacancy = "";
            salary = "";
        }
    }
}
