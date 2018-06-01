using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCAFT1
{
    class TCPListen
    {
        public static void ListenForMessages(object sender, DoWorkEventArgs doe)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
            TcpListener listen = (TcpListener)doe.Argument;
            while (!bgw.CancellationPending)
            {

                try
                {
                    TcpClient client = listen.AcceptTcpClient();
                    if (bgw.CancellationPending || client == null)
                    {
                        client.Close();
                        return;
                    }
                    StreamReader srIn = new StreamReader(client.GetStream());
                    string newMsg = srIn.ReadLine();
                    srIn.Close();
                    client.Close();

                    newMsg = TheHelper.separateHmac(newMsg);


                    bgw.ReportProgress(0, newMsg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurd in TCPListen -> " + ex.Message);
                }
            }
            return;
        }
    }
}
