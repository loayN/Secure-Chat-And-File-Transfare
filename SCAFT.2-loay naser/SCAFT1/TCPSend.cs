using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;

namespace SCAFT1
{
    class TCPSend
    {
        public static void EncAndSend(object sender, DoWorkEventArgs doe)
        {
            TcpClient client;
            Random rand = new Random();

            ArrayList parameters = (ArrayList)doe.Argument;
            string msgToSend = parameters[0].ToString();


            ArrayList groupMembers = parameters[1] as ArrayList;

            if (msgToSend.Contains("BYE"))
            {
                Console.WriteLine("Send the msg is :" + msgToSend);
            }

            msgToSend = TheHelper.EncryptMessage(msgToSend);

            for(int i=0; i<groupMembers.Count; i++)
            {
                String[] member = groupMembers[i].ToString().Split(':');
                try
                {
                    client = new TcpClient();
                    client.Connect(member[0],Convert.ToInt32(member[1]));
                    StreamWriter swOut = new StreamWriter(client.GetStream());
                    swOut.WriteLine(msgToSend);
                    swOut.Flush(); swOut.Close(); client.Close();

                    int wait = rand.Next(100, 1500);
                    Thread.Sleep(wait);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurd in TCPSend -> " + ex.Message);
                }
            }
        }
    }
}
