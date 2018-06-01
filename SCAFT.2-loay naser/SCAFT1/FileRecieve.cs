using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCAFT1
{
    class FileRecieve
    {
        public static String recieve(String file, TcpListener listener, string savePath, string sourceHmac)
        {

            byte[] RecData = new byte[1024];
            int RecBytes;
            string[] filetosendarray = savePath.Split('\\');
            string theFile = filetosendarray[filetosendarray.Length - 1];
            while (true)
            {
                try
                {
                    using (var client = listener.AcceptTcpClient())
                    {
                        if (client == null)
                            continue;
                        using (NetworkStream stream = client.GetStream())
                        using (FileStream fileStream = File.Create(savePath + "\\" + file + ".encrypted"))
                        {
                            int totalrecbytes = 0;
                            while ((RecBytes = stream.Read(RecData, 0, RecData.Length)) > 0)
                            {
                                fileStream.Write(RecData, 0, RecBytes);
                                totalrecbytes += RecBytes;
                            }
                            fileStream.Flush();
                            fileStream.Close();
                            TheHelperFiles.decryptFile(savePath + "\\" + file + ".encrypted");
                            AutoClosingMessageBox.Show("the file: " + theFile + " finished downloading ", "downloading file", 2000);

                            stream.Close();
                            var destHmac = TheHelperFiles.fileHmac(savePath + "\\" + file);
                            bool result = TheHelperFiles.fileCheckHmac(destHmac, sourceHmac);
                            if (!result)
                            {
                                new Thread(() =>
                                {
                                    LogClass.Log("not valid file HMAC \r\n " +
                                                                       "filename: " + file +
                                                                       "\r\n source HMAC: " + sourceHmac +
                                                                       "\r\n recieved file HMAC: " + destHmac +
                                                                       "\r\n from: " + IPAddress.Parse(((IPEndPoint)listener.LocalEndpoint).Address.ToString()) +
                                                                        "\r\n on port: " + ((IPEndPoint)listener.LocalEndpoint).Port.ToString());
                                }).Start();
                                MessageBox.Show("the sent file HMAC didnt match with the recieved file HMAC ");
                            }

                        }
                        client.Close();
                        File.Delete(savePath + "\\" + file + ".encrypted");
                    }
                }
                catch (Exception ex) { Console.WriteLine("error in FileRecieve -> " + ex.Message); }
            }
            return "done";
        }

    }
}
