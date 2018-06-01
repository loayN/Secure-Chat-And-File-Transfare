using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SCAFT1
{
    class LogClass
    {
        public static void Log(string logMessage)
        {
            if (!File.Exists("SCAFT-log.txt"))
            {
                FileStream f = new FileStream("SCAFT-log.txt", FileMode.Create);
                f.Close();
            }
            StreamWriter w = File.AppendText("SCAFT-log.txt");
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  ");
            w.WriteLine(" Error details  :{0}", logMessage);
            w.WriteLine("-------------------------------");
            w.Flush();
            w.Close();
        }
    }
}
