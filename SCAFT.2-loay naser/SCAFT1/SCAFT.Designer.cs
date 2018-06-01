namespace SCAFT1
{
    partial class SCAFT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.connectbtn = new System.Windows.Forms.Button();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.userNameTB = new System.Windows.Forms.TextBox();
            this.userNameLB = new System.Windows.Forms.Label();
            this.passwordLB = new System.Windows.Forms.Label();
            this.portLB = new System.Windows.Forms.Label();
            this.bAutoIp = new System.Windows.Forms.Button();
            this.tbMyIp = new System.Windows.Forms.TextBox();
            this.ipLB = new System.Windows.Forms.Label();
            this.nubPort = new System.Windows.Forms.NumericUpDown();
            this.userCoordinateGB = new System.Windows.Forms.GroupBox();
            this.changeHmacKeyBtn = new System.Windows.Forms.Button();
            this.hmaclbl = new System.Windows.Forms.Label();
            this.hmacPassTxt = new System.Windows.Forms.TextBox();
            this.loadbtn = new System.Windows.Forms.Button();
            this.onlineUsersList = new System.Windows.Forms.ListBox();
            this.onlineUsersLB = new System.Windows.Forms.Label();
            this.chatGB = new System.Windows.Forms.GroupBox();
            this.chatTxtBx = new System.Windows.Forms.Panel();
            this.sendmsgBtn = new System.Windows.Forms.Button();
            this.attachfileBtn = new System.Windows.Forms.Button();
            this.msgLB = new System.Windows.Forms.Label();
            this.msgTxbx = new System.Windows.Forms.TextBox();
            this.chatTxtBx1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nubPort)).BeginInit();
            this.userCoordinateGB.SuspendLayout();
            this.chatGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectbtn
            // 
            this.connectbtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.connectbtn.Location = new System.Drawing.Point(27, 256);
            this.connectbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectbtn.Name = "connectbtn";
            this.connectbtn.Size = new System.Drawing.Size(122, 53);
            this.connectbtn.TabIndex = 12;
            this.connectbtn.Text = "Connect";
            this.connectbtn.UseVisualStyleBackColor = false;
            this.connectbtn.Click += new System.EventHandler(this.connectbtn_Click);
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(91, 154);
            this.passwordTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.ReadOnly = true;
            this.passwordTB.Size = new System.Drawing.Size(152, 24);
            this.passwordTB.TabIndex = 10;
            // 
            // userNameTB
            // 
            this.userNameTB.Location = new System.Drawing.Point(91, 31);
            this.userNameTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userNameTB.Name = "userNameTB";
            this.userNameTB.Size = new System.Drawing.Size(152, 24);
            this.userNameTB.TabIndex = 9;
            // 
            // userNameLB
            // 
            this.userNameLB.AutoSize = true;
            this.userNameLB.Location = new System.Drawing.Point(15, 34);
            this.userNameLB.Name = "userNameLB";
            this.userNameLB.Size = new System.Drawing.Size(79, 17);
            this.userNameLB.TabIndex = 8;
            this.userNameLB.Text = "User Name:";
            // 
            // passwordLB
            // 
            this.passwordLB.AutoSize = true;
            this.passwordLB.Location = new System.Drawing.Point(15, 162);
            this.passwordLB.Name = "passwordLB";
            this.passwordLB.Size = new System.Drawing.Size(71, 17);
            this.passwordLB.TabIndex = 7;
            this.passwordLB.Text = "Password:";
            // 
            // portLB
            // 
            this.portLB.AutoSize = true;
            this.portLB.Location = new System.Drawing.Point(15, 118);
            this.portLB.Name = "portLB";
            this.portLB.Size = new System.Drawing.Size(39, 17);
            this.portLB.TabIndex = 3;
            this.portLB.Text = "Port:";
            // 
            // bAutoIp
            // 
            this.bAutoIp.Location = new System.Drawing.Point(251, 74);
            this.bAutoIp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bAutoIp.Name = "bAutoIp";
            this.bAutoIp.Size = new System.Drawing.Size(111, 28);
            this.bAutoIp.TabIndex = 2;
            this.bAutoIp.Text = "Auto";
            this.bAutoIp.UseVisualStyleBackColor = true;
            this.bAutoIp.Click += new System.EventHandler(this.bAutoIp_Click);
            // 
            // tbMyIp
            // 
            this.tbMyIp.Location = new System.Drawing.Point(91, 78);
            this.tbMyIp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMyIp.Name = "tbMyIp";
            this.tbMyIp.Size = new System.Drawing.Size(152, 24);
            this.tbMyIp.TabIndex = 1;
            this.tbMyIp.Text = "127.0.0.1";
            // 
            // ipLB
            // 
            this.ipLB.AutoSize = true;
            this.ipLB.Location = new System.Drawing.Point(15, 86);
            this.ipLB.Name = "ipLB";
            this.ipLB.Size = new System.Drawing.Size(25, 17);
            this.ipLB.TabIndex = 0;
            this.ipLB.Text = "IP:";
            // 
            // nubPort
            // 
            this.nubPort.Location = new System.Drawing.Point(91, 110);
            this.nubPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nubPort.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nubPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nubPort.Name = "nubPort";
            this.nubPort.Size = new System.Drawing.Size(149, 24);
            this.nubPort.TabIndex = 4;
            this.nubPort.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // userCoordinateGB
            // 
            this.userCoordinateGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userCoordinateGB.Controls.Add(this.changeHmacKeyBtn);
            this.userCoordinateGB.Controls.Add(this.hmaclbl);
            this.userCoordinateGB.Controls.Add(this.hmacPassTxt);
            this.userCoordinateGB.Controls.Add(this.loadbtn);
            this.userCoordinateGB.Controls.Add(this.connectbtn);
            this.userCoordinateGB.Controls.Add(this.passwordTB);
            this.userCoordinateGB.Controls.Add(this.userNameTB);
            this.userCoordinateGB.Controls.Add(this.userNameLB);
            this.userCoordinateGB.Controls.Add(this.passwordLB);
            this.userCoordinateGB.Controls.Add(this.portLB);
            this.userCoordinateGB.Controls.Add(this.bAutoIp);
            this.userCoordinateGB.Controls.Add(this.tbMyIp);
            this.userCoordinateGB.Controls.Add(this.ipLB);
            this.userCoordinateGB.Controls.Add(this.nubPort);
            this.userCoordinateGB.Location = new System.Drawing.Point(27, 25);
            this.userCoordinateGB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userCoordinateGB.Name = "userCoordinateGB";
            this.userCoordinateGB.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userCoordinateGB.Size = new System.Drawing.Size(374, 330);
            this.userCoordinateGB.TabIndex = 7;
            this.userCoordinateGB.TabStop = false;
            this.userCoordinateGB.Text = "user coordinates";
            // 
            // changeHmacKeyBtn
            // 
            this.changeHmacKeyBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.changeHmacKeyBtn.Location = new System.Drawing.Point(175, 256);
            this.changeHmacKeyBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.changeHmacKeyBtn.Name = "changeHmacKeyBtn";
            this.changeHmacKeyBtn.Size = new System.Drawing.Size(122, 53);
            this.changeHmacKeyBtn.TabIndex = 16;
            this.changeHmacKeyBtn.Text = "change HMAC";
            this.changeHmacKeyBtn.UseVisualStyleBackColor = false;
            this.changeHmacKeyBtn.Click += new System.EventHandler(this.changeHmacKeyBtn_Click);
            // 
            // hmaclbl
            // 
            this.hmaclbl.AutoSize = true;
            this.hmaclbl.Location = new System.Drawing.Point(96, 196);
            this.hmaclbl.Name = "hmaclbl";
            this.hmaclbl.Size = new System.Drawing.Size(120, 17);
            this.hmaclbl.TabIndex = 15;
            this.hmaclbl.Text = "HMAC shared key:";
            // 
            // hmacPassTxt
            // 
            this.hmacPassTxt.Location = new System.Drawing.Point(92, 217);
            this.hmacPassTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hmacPassTxt.Name = "hmacPassTxt";
            this.hmacPassTxt.Size = new System.Drawing.Size(116, 24);
            this.hmacPassTxt.TabIndex = 14;
            // 
            // loadbtn
            // 
            this.loadbtn.Location = new System.Drawing.Point(261, 134);
            this.loadbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadbtn.Name = "loadbtn";
            this.loadbtn.Size = new System.Drawing.Size(113, 62);
            this.loadbtn.TabIndex = 13;
            this.loadbtn.Text = "Load group members  and password ";
            this.loadbtn.UseVisualStyleBackColor = true;
            this.loadbtn.Click += new System.EventHandler(this.loadbtn_Click);
            // 
            // onlineUsersList
            // 
            this.onlineUsersList.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineUsersList.FormattingEnabled = true;
            this.onlineUsersList.ItemHeight = 23;
            this.onlineUsersList.Location = new System.Drawing.Point(27, 418);
            this.onlineUsersList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.onlineUsersList.Name = "onlineUsersList";
            this.onlineUsersList.Size = new System.Drawing.Size(243, 234);
            this.onlineUsersList.TabIndex = 15;
            this.onlineUsersList.SelectedIndexChanged += new System.EventHandler(this.onlineUsersList_SelectedIndexChanged);
            this.onlineUsersList.DoubleClick += new System.EventHandler(this.onlineUsersList_DoubleClick);
            // 
            // onlineUsersLB
            // 
            this.onlineUsersLB.AutoSize = true;
            this.onlineUsersLB.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlineUsersLB.Location = new System.Drawing.Point(22, 386);
            this.onlineUsersLB.Name = "onlineUsersLB";
            this.onlineUsersLB.Size = new System.Drawing.Size(168, 29);
            this.onlineUsersLB.TabIndex = 16;
            this.onlineUsersLB.Text = "Online users : ";
            // 
            // chatGB
            // 
            this.chatGB.Controls.Add(this.sendmsgBtn);
            this.chatGB.Controls.Add(this.attachfileBtn);
            this.chatGB.Controls.Add(this.msgLB);
            this.chatGB.Controls.Add(this.msgTxbx);
            this.chatGB.Controls.Add(this.chatTxtBx1);
            this.chatGB.Location = new System.Drawing.Point(427, 25);
            this.chatGB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chatGB.Name = "chatGB";
            this.chatGB.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chatGB.Size = new System.Drawing.Size(497, 490);
            this.chatGB.TabIndex = 17;
            this.chatGB.TabStop = false;
            this.chatGB.Text = "Chat window";
            // 
            // chatTxtBx
            // 
            this.chatTxtBx.Location = new System.Drawing.Point(27, 700);
            this.chatTxtBx.Name = "chatTxtBx";
            this.chatTxtBx.Size = new System.Drawing.Size(10, 10);
            this.chatTxtBx.TabIndex = 5;
            // 
            // sendmsgBtn
            // 
            this.sendmsgBtn.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendmsgBtn.Location = new System.Drawing.Point(210, 422);
            this.sendmsgBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sendmsgBtn.Name = "sendmsgBtn";
            this.sendmsgBtn.Size = new System.Drawing.Size(121, 50);
            this.sendmsgBtn.TabIndex = 4;
            this.sendmsgBtn.Text = "Send Message";
            this.sendmsgBtn.UseVisualStyleBackColor = true;
            this.sendmsgBtn.Click += new System.EventHandler(this.sendmsgBtn_Click);
            // 
            // attachfileBtn
            // 
            this.attachfileBtn.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attachfileBtn.Location = new System.Drawing.Point(10, 422);
            this.attachfileBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.attachfileBtn.Name = "attachfileBtn";
            this.attachfileBtn.Size = new System.Drawing.Size(75, 50);
            this.attachfileBtn.TabIndex = 3;
            this.attachfileBtn.Text = "Attach file";
            this.attachfileBtn.UseVisualStyleBackColor = true;
            this.attachfileBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // msgLB
            // 
            this.msgLB.AutoSize = true;
            this.msgLB.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLB.Location = new System.Drawing.Point(6, 378);
            this.msgLB.Name = "msgLB";
            this.msgLB.Size = new System.Drawing.Size(94, 24);
            this.msgLB.TabIndex = 2;
            this.msgLB.Text = "Message:";
            // 
            // msgTxbx
            // 
            this.msgTxbx.Location = new System.Drawing.Point(106, 368);
            this.msgTxbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.msgTxbx.Multiline = true;
            this.msgTxbx.Name = "msgTxbx";
            this.msgTxbx.Size = new System.Drawing.Size(370, 43);
            this.msgTxbx.TabIndex = 1;
            // 
            // chatTxtBx1
            // 
            this.chatTxtBx1.Location = new System.Drawing.Point(24, 34);
            this.chatTxtBx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chatTxtBx1.Multiline = true;
            this.chatTxtBx1.Name = "chatTxtBx1";
            this.chatTxtBx1.Size = new System.Drawing.Size(452, 296);
            this.chatTxtBx1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // SCAFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 722);
            this.Controls.Add(this.chatTxtBx);
            this.Controls.Add(this.chatGB);
            this.Controls.Add(this.onlineUsersLB);
            this.Controls.Add(this.onlineUsersList);
            this.Controls.Add(this.userCoordinateGB);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SCAFT";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nubPort)).EndInit();
            this.userCoordinateGB.ResumeLayout(false);
            this.userCoordinateGB.PerformLayout();
            this.chatGB.ResumeLayout(false);
            this.chatGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectbtn;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox userNameTB;
        private System.Windows.Forms.Label userNameLB;
        private System.Windows.Forms.Label passwordLB;
        private System.Windows.Forms.Label portLB;
        private System.Windows.Forms.Button bAutoIp;
        private System.Windows.Forms.TextBox tbMyIp;
        private System.Windows.Forms.Label ipLB;
        private System.Windows.Forms.NumericUpDown nubPort;
        private System.Windows.Forms.GroupBox userCoordinateGB;
        private System.Windows.Forms.Label onlineUsersLB;
        private System.Windows.Forms.GroupBox chatGB;
        private System.Windows.Forms.Button attachfileBtn;
        private System.Windows.Forms.Label msgLB;
        private System.Windows.Forms.TextBox msgTxbx;
        private System.Windows.Forms.TextBox chatTxtBx1;
        private System.Windows.Forms.Button sendmsgBtn;
        private System.Windows.Forms.Button loadbtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ListBox onlineUsersList;
        private System.Windows.Forms.Button changeHmacKeyBtn;
        private System.Windows.Forms.Label hmaclbl;
        private System.Windows.Forms.TextBox hmacPassTxt;
        private System.Windows.Forms.Panel chatTxtBx;
    }
}

