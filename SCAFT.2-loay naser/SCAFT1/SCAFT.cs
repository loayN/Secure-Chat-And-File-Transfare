using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;


namespace SCAFT1
{
    public partial class SCAFT : Form
    {

        static User user = new User();
        static List<User> listUsers;
        public ArrayList neighbors;
        public BackgroundWorker bwMessagesIn, bwMessageTick;
        public TcpListener server;
        String userToRecieve = "";
        DateTime localDate = DateTime.Now;



        public SCAFT()
        {
            InitializeComponent();

            neighbors = new ArrayList();
            listUsers = new List<User>();
            listening = false;
            bwMessagesIn = new BackgroundWorker();
            bwMessagesIn.WorkerSupportsCancellation = true;
            bwMessagesIn.WorkerReportsProgress = true;
            bwMessagesIn.ProgressChanged += ReceivedMessage;

            bwMessagesIn.DoWork += new DoWorkEventHandler(TCPListen.ListenForMessages);
            bwMessageTick = new BackgroundWorker();
            bwMessageTick.DoWork += new DoWorkEventHandler(bwMessageTick_DoWork);
            bwMessageTick.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwMessageTick_RunWorkerCompleted);
            chatTxtBx1.Multiline = true;
            chatTxtBx1.ScrollBars = ScrollBars.Both;

        }
        public bool listening
        {
            get { return connectbtn.Text == "disconnect"; }
            set
            {
                if (value) connectbtn.Text = "disconnect";

                else connectbtn.Text = "connect";
            }
        }
        private void bwMessageTick_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String newMsg = "HELLO" + "-" + userNameTB.Text + "-" + tbMyIp.Text + "-" + nubPort.Value;
                send(newMsg, neighbors);
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }
        private void bwMessageTick_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { }
        //choosing file to send >>> Attach button
        private void button1_Click(object sender, EventArgs e)
        {

            if (userToRecieve == "")
            {
                MessageBox.Show("click on the user you want to send him the file !!!");
                return;
            }
            openFileDialog2.ShowDialog();
            foreach (User u in listUsers)
            {
                if (u.userName == userToRecieve)
                {
                    user.userName = userToRecieve;
                    user.ip = u.ip;
                    user.port = u.port;
                }
            }
            ArrayList n = new ArrayList();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(user.ip), Int32.Parse(user.port));
            n.Add(ipep);
            string hmacfile = TheHelperFiles.fileHmac(openFileDialog2.FileName);
            userToRecieve = "SENDFILE-" + tbMyIp.Text + "-" + nubPort.Value + "-" + userNameTB.Text + "-" + openFileDialog2.FileName + "-" + hmacfile;
            send(userToRecieve, n);
        }
        private void bAutoIp_Click(object sender, EventArgs e)
        {
            IPAddress[] adds = Dns.GetHostAddresses("");
            for (int i = 0; i < adds.Length; i++)
            {
                IPAddress add = adds[i];
                if (add.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(add))
                {
                    tbMyIp.Text = add.ToString();
                    return;
                }
            }
            this.tbMyIp.Text = "127.0.0.1";
        }
        private void loadbtn_Click(object sender, EventArgs e)
        {
            this.neighbors.Clear();
            passwordTB.Text = "will be loadded from file";
            try
            {
                if (this.openFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                StreamReader nighborsAndPasswordFile = new StreamReader(openFileDialog1.FileName);
                String[] name = (openFileDialog1.FileName).Split('\\');
                AutoClosingMessageBox.Show("loading users from: " + name[name.Length - 1] + " is done ", "loading users", 2000);
                String line = nighborsAndPasswordFile.ReadLine();

                String[] ips = line.Split(',');
                if (ips == null)
                {
                    MessageBox.Show("ip's not found");

                }
                foreach (String ipp in ips)
                {
                    String[] ippa = ipp.Split(':');
                    if (ippa.Length == 1) { MessageBox.Show("missing port in ip: " + ipp[0]); }
                    if (ipp[1] != nubPort.Value) neighbors.Add(ipp);
                }
                String sharedPass = nighborsAndPasswordFile.ReadLine();
                string sharedHmacKey = nighborsAndPasswordFile.ReadLine();
                hmacPassTxt.Text = sharedHmacKey;
                passwordTB.Text = sharedPass;
                TheHelper.password = sharedPass;
                TheHelperFiles.password = sharedPass;
                TheHelper.hmacSharedkey = sharedHmacKey;
                TheHelperFiles.hmacSharedKey = sharedHmacKey;
                nighborsAndPasswordFile.Close();
            }
            catch (Exception eLoadBtn)
            {
                AutoClosingMessageBox.Show(eLoadBtn.Message, "users config error", 1500);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                bwMessageTick.RunWorkerAsync();
            }
            catch { }

        }

        private void connectbtn_Click(object sender, EventArgs e)
        {
            {

                try
                {
                    // if we should start listening
                    if (!listening && hmacPassTxt.Text != null)
                    {
                        // put a warning if there are no neighbors
                        if (neighbors.Count == 0)
                        {
                            if (MessageBox.Show("there is no loaded users, r u okay! ", "no users", MessageBoxButtons.YesNo) == DialogResult.No) return;

                        }
                        AutoClosingMessageBox.Show("you are connecting now", "connecting", 1000);
                        listening = true;
                        server = new TcpListener(IPAddress.Parse(tbMyIp.Text), Convert.ToInt32(nubPort.Value));
                        server.Start();
                        bwMessagesIn.RunWorkerAsync(server);

                        //keep sending so any new users can see my HELLO
                        timer1.Interval = 3000;
                        timer1.Start();

                    }
                    else
                    {
                        AutoClosingMessageBox.Show("you are disconnecting now", "disconnecting", 1000);

                        //if the user are stop the scaft are send bye
                        onlineUsersList.Items.Clear();
                        listening = false;
                        chatTxtBx1.Text = "";
                        String newMsg = "BYE" + "-" + userNameTB.Text;
                        send(newMsg, neighbors);


                        //the only working solution i foun
                        //used to delay when disconnect so the program will be able to send BYE msg
                        int milliseconds = 1000;
                        Thread.Sleep(milliseconds);

                        neighbors.Clear();
                        this.timer1.Stop();
                        bwMessagesIn.CancelAsync();
                        server.Stop();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    listening = false;
                    this.timer1.Stop();
                    bwMessagesIn.CancelAsync();
                    server.Stop();
                }
            }
        }

        private void ReceivedMessage(object sender, ProgressChangedEventArgs e)
        {
            string msgIn = "";
            if (e.UserState != null)
            {
                msgIn = e.UserState.ToString();
            }
            MsgType(msgIn);

        }

        private void MsgType(string msgIn)
        {

            String[] splitMsgIn = msgIn.Split('-');
            switch (splitMsgIn[0])
            {

                case "Message"://normal message, just add it to the chat box
                    {
                        if (!splitMsgIn[1].Equals(userNameTB.Text))
                        {
                            System.DateTime today = System.DateTime.Now;
                            String time = today.ToString();
                            ;
                            chatTxtBx1.AppendText(time + " -> " + msgIn + Environment.NewLine);
                        }
                        break;
                    }
                case "HELLO": // check if new user entered the chat > found in online user box <" HELLO - myName - myIP - myPort "> 
                    {
                        if (!onlineUsersList.Items.Contains(splitMsgIn[1]) && userNameTB.Text != splitMsgIn[1])
                        {
                            onlineUsersList.Items.Add(splitMsgIn[1]);
                            ///user constructorneed to be  rebuild 
                            User newUser = new User();
                            newUser.userName = splitMsgIn[1]; newUser.ip = splitMsgIn[2]; newUser.port = splitMsgIn[3];
                            listUsers.Add(newUser);

                        }
                        break;
                    }
                case "BYE": // remove a user from the list - recieved when someone leaves the chat 
                    {
                        chatTxtBx1.AppendText(msgIn + Environment.NewLine);
                        if (onlineUsersList.Items.Contains(splitMsgIn[1]))
                        {
                            onlineUsersList.Items.Remove(splitMsgIn[1]);
                            break;
                        }
                        break;
                    }
                case "SENDFILE": //in case of user want to send other user a file - asking to send
                    {
                        string[] filename = splitMsgIn[4].Split('\\');
                        string thename = filename[filename.Length - 1];
                        var result = MessageBox.Show(splitMsgIn[3] + " sent this file to you:  " + thename + "do you approve", "Received file", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.OK:
                                {
                                    string hmacfile = splitMsgIn[splitMsgIn.Length - 1];
                                    if (msgIn.Contains(" - HMAC doesn't match"))
                                    {
                                        hmacfile = splitMsgIn[splitMsgIn.Length - 2];
                                        msgIn = msgIn.Remove(msgIn.IndexOf(" - HMAC doesn't match"), " - HMAC doesn't match".Length);
                                    }
                                    SaveFileDialog sf = new SaveFileDialog();
                                    sf.FileName = thename;
                                    if (sf.ShowDialog() == DialogResult.OK)
                                    {
                                        string savePath = Path.GetDirectoryName(sf.FileName);

                                        ArrayList n = new ArrayList();
                                        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(splitMsgIn[1]), Int32.Parse(splitMsgIn[2]));
                                        n.Add(ipep);

                                        Random rnd = new Random();
                                        int rndPort = rnd.Next(5500, 6000);
                                        IPEndPoint listenTo = new IPEndPoint(IPAddress.Parse(tbMyIp.Text), rndPort);
                                        var listener = new TcpListener(listenTo);
                                        listener.Start();

                                        new Thread(() =>
                                        {
                                            FileRecieve.recieve(thename, listener, savePath, hmacfile);
                                        }).Start();

                                        userToRecieve = "OK-" + tbMyIp.Text + "-" + nubPort.Value + "-" + userNameTB.Text + "-" + splitMsgIn[4] + "-" + rndPort;
                                        send(userToRecieve, n);
                                    }
                                    break;
                                }
                            case DialogResult.Cancel:
                                {

                                    ArrayList n = new ArrayList();
                                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(splitMsgIn[1]), Int32.Parse(splitMsgIn[2]));
                                    n.Add(ipep);
                                    userToRecieve = "NO-" + tbMyIp.Text + "-" + nubPort.Value + "-" + userNameTB.Text + "-" + splitMsgIn[4];
                                    send(userToRecieve, n);
                                    break;
                                }
                            default:
                                {
                                    ArrayList n = new ArrayList();
                                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(splitMsgIn[1]), Int32.Parse(splitMsgIn[2]));
                                    n.Add(ipep);
                                    userToRecieve = "NO-" + tbMyIp.Text + "-" + nubPort.Value + "-" + userNameTB.Text + "-" + openFileDialog2.FileName;
                                    send(userToRecieve, n);
                                    break;
                                }
                        }
                        break;
                    }
                case "OK":
                    {
                        try
                        {

                            AutoClosingMessageBox.Show(splitMsgIn[3] + " approved to recieve the file  " + splitMsgIn[4], "Received file", 2000);
                            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(splitMsgIn[1]), Int32.Parse(splitMsgIn[5]));
                            new Thread(() =>
                            {
                                FileSend.send(ipEnd, splitMsgIn[4], splitMsgIn[5]);
                            }).Start();
                        }
                        catch (Exception ex) { AutoClosingMessageBox.Show(ex.Message, "ERROR", 150000); }

                        break;
                    }
                case "NO":
                    {
                        AutoClosingMessageBox.Show("your friend refused the file transfare ", "do not Received file", 2000);
                        break;
                    }
            }
        }

        private void sendmsgBtn_Click(object sender, EventArgs e)
        {
            try
            {
                System.DateTime today = System.DateTime.Now;
                String time = today.ToString();

                if (neighbors.Count == 0) return;
                string newMsg = "Message" + "-" + userNameTB.Text + "-" + msgTxbx.Text;
                send(newMsg, neighbors);


                chatTxtBx1.AppendText(time + "-> ME" + " - " + msgTxbx.Text + Environment.NewLine);
                msgTxbx.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Error -> " + ex.Message); }
        }

        private void onlineUsersList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void onlineUsersList_DoubleClick(object sender, EventArgs e)
        {
            if (onlineUsersList.SelectedItem != null) userToRecieve = onlineUsersList.SelectedItem.ToString();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void changeHmacKeyBtn_Click(object sender, EventArgs e)
        {
            TheHelper.hmacSharedkey = hmacPassTxt.Text.ToString();
            TheHelperFiles.hmacSharedKey = hmacPassTxt.Text.ToString();
        }

        public void send(string newMsg, ArrayList neig)
        {
            ArrayList parameters = new ArrayList();
            parameters.Add(newMsg);
            parameters.Add(neig);
            BackgroundWorker bwSender = new BackgroundWorker();
            bwSender.DoWork += new DoWorkEventHandler(TCPSend.EncAndSend);
            bwSender.WorkerReportsProgress = true;
            bwSender.RunWorkerAsync(parameters);
        }
    }
}
