using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WorkSearchesServer
{
    static class Server
    {
        static private int status = 0;
        static private string command = "";
        static private string companySearch = "", companyId = "", companyName = "", companyVacancy = "", companySalary = "", companyEmployment = "", companyRequirements = "", companyDescription = "";
        static private string seekerSearch = "", seekerId = "", seekerName = "", seekerVacancy = "", seekerSalary = "", seekerEducation = "", seekerMobileNumber = "", seekerDescription = "";
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

                Console.WriteLine("Сервер запущен и ожидает подключения...");

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

                    if (status == 0 && ((builder.ToString() == "SELECT COMPANY") ||
                                        (builder.ToString() == "ADD COMPANY")    ||
                                        (builder.ToString() == "DELETE COMPANY") || 
                                        (builder.ToString() == "UPDATE COMPANY") ||
                                        (builder.ToString() == "SELECT SEEKER") ||
                                        (builder.ToString() == "ADD SEEKER") ||
                                        (builder.ToString() == "DELETE SEEKER") ||
                                        (builder.ToString() == "UPDATE SEEKER")))
                    {
                        command = builder.ToString();
                        ++status;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            if (command == "SELECT COMPANY")
                            {
                                companySearch = builder.ToString();
                                DataTable dataTable = SqlCommander.SelectCompany(companySearch);
                                byte[] responseData = GetBinaryFormatData(dataTable);
                                handler.Send(responseData);
                                Clear();
                            }
                            if (command == "ADD COMPANY")
                            {
                                if (companyName == "")
                                {
                                    companyName = builder.ToString();
                                }
                                else
                                {
                                    if (companyVacancy == "")
                                    {
                                        companyVacancy = builder.ToString();
                                    }
                                    else
                                    {
                                        if (companySalary == "")
                                        {
                                            companySalary = builder.ToString();
                                        }
                                        else
                                        {
                                            if (companyEmployment == "")
                                            {
                                                companyEmployment = builder.ToString();
                                            }
                                            else
                                            {
                                                if (companyRequirements == "")
                                                {
                                                    companyRequirements = builder.ToString();
                                                }
                                                else
                                                {
                                                    companyDescription = builder.ToString();
                                                    string response = SqlCommander.AddCompany(companyName, companyVacancy, companySalary, companyEmployment, companyRequirements, companyDescription);
                                                    data = Encoding.Unicode.GetBytes(response);
                                                    handler.Send(data);
                                                    Clear();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (command == "DELETE COMPANY")
                            {
                                companyId = builder.ToString();
                                string response = SqlCommander.DelCompany(companyId);
                                data = Encoding.Unicode.GetBytes(response);
                                handler.Send(data);
                                Clear();
                            }
                            if (command == "UPDATE COMPANY")
                            {
                                if (companyId == "")
                                {
                                    companyId = builder.ToString();
                                }
                                else
                                {
                                    if (companyName == "")
                                    {
                                        companyName = builder.ToString();
                                    }
                                    else
                                    {
                                        if (companyVacancy == "")
                                        {
                                            companyVacancy = builder.ToString();
                                        }
                                        else
                                        {
                                            if (companySalary == "")
                                            {
                                                companySalary = builder.ToString();
                                            }
                                            else
                                            {
                                                if (companyEmployment == "")
                                                {
                                                    companyEmployment = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (companyRequirements == "")
                                                    {
                                                        companyRequirements = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        companyDescription = builder.ToString();
                                                        string response = SqlCommander.ChangeCompany(companyId, companyName, companyVacancy, companySalary, companyEmployment, companyRequirements, companyDescription);
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
                            if (command == "SELECT SEEKER")
                            {
                                seekerSearch = builder.ToString();
                                DataTable dataTable = SqlCommander.SelectSeeker(seekerSearch);
                                byte[] responseData = GetBinaryFormatData(dataTable);
                                handler.Send(responseData);
                                Clear();
                            }
                            if (command == "ADD SEEKER")
                            {
                                if (seekerName == "")
                                {
                                    seekerName = builder.ToString();
                                }
                                else
                                {
                                    if (seekerVacancy == "")
                                    {
                                        seekerVacancy = builder.ToString();
                                    }
                                    else
                                    {
                                        if (seekerSalary == "")
                                        {
                                            seekerSalary = builder.ToString();
                                        }
                                        else
                                        {
                                            if (seekerEducation == "")
                                            {
                                                seekerEducation = builder.ToString();
                                            }
                                            else
                                            {
                                                if (seekerMobileNumber == "")
                                                {
                                                    seekerMobileNumber = builder.ToString();
                                                }
                                                else
                                                {
                                                    seekerDescription = builder.ToString();
                                                    string response = SqlCommander.AddSeeker(seekerName, seekerVacancy, seekerSalary, seekerEducation, seekerMobileNumber, seekerDescription);
                                                    data = Encoding.Unicode.GetBytes(response);
                                                    handler.Send(data);
                                                    Clear();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (command == "DELETE SEEKER")
                            {
                                seekerId = builder.ToString();
                                string response = SqlCommander.DelSeeker(seekerId);
                                data = Encoding.Unicode.GetBytes(response);
                                handler.Send(data);
                                Clear();
                            }
                            if (command == "UPDATE SEEKER")
                            {
                                if (seekerId == "")
                                {
                                    seekerId = builder.ToString();
                                }
                                else
                                {
                                    if (seekerName == "")
                                    {
                                        seekerName = builder.ToString();
                                    }
                                    else
                                    {
                                        if (seekerVacancy == "")
                                        {
                                            seekerVacancy = builder.ToString();
                                        }
                                        else
                                        {
                                            if (seekerSalary == "")
                                            {
                                                seekerSalary = builder.ToString();
                                            }
                                            else
                                            {
                                                if (seekerEducation == "")
                                                {
                                                    seekerEducation = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (seekerMobileNumber == "")
                                                    {
                                                        seekerMobileNumber = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        seekerDescription = builder.ToString();
                                                        string response = SqlCommander.ChangeSeeker(seekerId, seekerName, seekerVacancy, seekerSalary, seekerEducation, seekerMobileNumber, seekerDescription);
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
            command = "";

            companySearch = "";
            companyId = "";
            companyName = "";
            companyVacancy = "";
            companySalary = "";
            companyEmployment = "";
            companyRequirements = "";
            companyDescription = "";

            seekerSearch = "";
            seekerId = "";
            seekerName = "";
            seekerVacancy = "";
            seekerSalary = "";
            seekerEducation = "";
            seekerMobileNumber = "";
            seekerDescription = "";
        }
    }
}
