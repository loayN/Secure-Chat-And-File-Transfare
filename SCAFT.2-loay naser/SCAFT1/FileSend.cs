using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCAFT1
{
    class FileSend
    {
        public static String send(IPEndPoint ipEnd, String fileToSend, String port)
        {
            string[] filetosendarray = fileToSend.Split('\\');
            string theFile = filetosendarray[filetosendarray.Length - 1];
            byte[] SendingBuffer = null;

            TcpClient client = new TcpClient();
            client.Connect(ipEnd);
            TheHelperFiles.encryptFile(fileToSend);

            using (NetworkStream stream = client.GetStream())
            {
                using (var fileStream = new FileStream(fileToSend+ ".encrypted", FileMode.Open, FileAccess.Read))
                {

                    try
                    {
                        int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fileStream.Length) / Convert.ToDouble(1024)));
                        Console.WriteLine("send: number of packets: " + NoOfPackets);
                        int TotalLength = (int)fileStream.Length, CurrentPacketLength, counter = 0;
                        for (int i = 0; i < NoOfPackets; i++)
                        {
                            if (TotalLength > 1024)
                            {
                                CurrentPacketLength = 1024;
                                TotalLength = TotalLength - CurrentPacketLength;
                            }
                            else
                                CurrentPacketLength = TotalLength;
                            SendingBuffer = new byte[CurrentPacketLength];
                            fileStream.Read(SendingBuffer, 0, CurrentPacketLength);
                            stream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                        }
                        AutoClosingMessageBox.Show("the file: " + theFile + " has been sent ", "done sending file",2000);
                        fileStream.Close();
                        File.Delete(fileToSend + ".encrypted");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        stream.Close();
                        client.Close();
                    }
                }

            }
            return " ";
        }
    }
}
