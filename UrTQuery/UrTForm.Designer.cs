namespace UrTQuery
{
    partial class MainForm
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
            this.TabPage = new System.Windows.Forms.TabControl();
            this.QueryPage = new System.Windows.Forms.TabPage();
            this.ID = new System.Windows.Forms.TextBox();
            this.CheckPassword = new System.Windows.Forms.Button();
            this.rconStatus = new System.Windows.Forms.Button();
            this.GetInfo = new System.Windows.Forms.Button();
            this.GetStatus = new System.Windows.Forms.Button();
            this.ShowPassword = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RCON = new System.Windows.Forms.TextBox();
            this.Type = new System.Windows.Forms.ComboBox();
            this.Clear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.Address = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.TextBox();
            this.Input = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.ServerList = new System.Windows.Forms.TabPage();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.GetNewListButton = new System.Windows.Forms.Button();
            this.BackGroundWorkersCheck = new System.Windows.Forms.Timer(this.components);
            this.TabPage.SuspendLayout();
            this.QueryPage.SuspendLayout();
            this.ServerList.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPage
            // 
            this.TabPage.Controls.Add(this.QueryPage);
            this.TabPage.Controls.Add(this.ServerList);
            this.TabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabPage.Location = new System.Drawing.Point(0, 0);
            this.TabPage.Name = "TabPage";
            this.TabPage.SelectedIndex = 0;
            this.TabPage.Size = new System.Drawing.Size(784, 562);
            this.TabPage.TabIndex = 17;
            // 
            // QueryPage
            // 
            this.QueryPage.Controls.Add(this.ID);
            this.QueryPage.Controls.Add(this.CheckPassword);
            this.QueryPage.Controls.Add(this.rconStatus);
            this.QueryPage.Controls.Add(this.GetInfo);
            this.QueryPage.Controls.Add(this.GetStatus);
            this.QueryPage.Controls.Add(this.ShowPassword);
            this.QueryPage.Controls.Add(this.label3);
            this.QueryPage.Controls.Add(this.RCON);
            this.QueryPage.Controls.Add(this.Type);
            this.QueryPage.Controls.Add(this.Clear);
            this.QueryPage.Controls.Add(this.label2);
            this.QueryPage.Controls.Add(this.label1);
            this.QueryPage.Controls.Add(this.Port);
            this.QueryPage.Controls.Add(this.Address);
            this.QueryPage.Controls.Add(this.Output);
            this.QueryPage.Controls.Add(this.Input);
            this.QueryPage.Controls.Add(this.Send);
            this.QueryPage.Location = new System.Drawing.Point(4, 22);
            this.QueryPage.Name = "QueryPage";
            this.QueryPage.Padding = new System.Windows.Forms.Padding(3);
            this.QueryPage.Size = new System.Drawing.Size(776, 536);
            this.QueryPage.TabIndex = 0;
            this.QueryPage.Text = "QueryPage";
            this.QueryPage.UseVisualStyleBackColor = true;
            // 
            // ID
            // 
            this.ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ID.Location = new System.Drawing.Point(463, 29);
            this.ID.MaxLength = 2;
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(22, 20);
            this.ID.TabIndex = 16;
            this.ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ID.Visible = false;
            this.ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumbersOnly_KeyPress);
            // 
            // CheckPassword
            // 
            this.CheckPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckPassword.Location = new System.Drawing.Point(678, 6);
            this.CheckPassword.Name = "CheckPassword";
            this.CheckPassword.Size = new System.Drawing.Size(90, 20);
            this.CheckPassword.TabIndex = 15;
            this.CheckPassword.Text = "Test Password";
            this.CheckPassword.UseVisualStyleBackColor = true;
            this.CheckPassword.Click += new System.EventHandler(this.CheckPassword_Click);
            // 
            // rconStatus
            // 
            this.rconStatus.Location = new System.Drawing.Point(183, 28);
            this.rconStatus.Name = "rconStatus";
            this.rconStatus.Size = new System.Drawing.Size(75, 20);
            this.rconStatus.TabIndex = 14;
            this.rconStatus.Text = "rcon Status";
            this.rconStatus.UseVisualStyleBackColor = true;
            this.rconStatus.Click += new System.EventHandler(this.rconStatus_Click);
            // 
            // GetInfo
            // 
            this.GetInfo.Location = new System.Drawing.Point(97, 28);
            this.GetInfo.Name = "GetInfo";
            this.GetInfo.Size = new System.Drawing.Size(75, 20);
            this.GetInfo.TabIndex = 13;
            this.GetInfo.Text = "Get Info";
            this.GetInfo.UseVisualStyleBackColor = true;
            this.GetInfo.Click += new System.EventHandler(this.GetInfo_Click);
            // 
            // GetStatus
            // 
            this.GetStatus.Location = new System.Drawing.Point(11, 28);
            this.GetStatus.Name = "GetStatus";
            this.GetStatus.Size = new System.Drawing.Size(75, 20);
            this.GetStatus.TabIndex = 12;
            this.GetStatus.Text = "Get Status";
            this.GetStatus.UseVisualStyleBackColor = true;
            this.GetStatus.Click += new System.EventHandler(this.GetStatus_Click);
            // 
            // ShowPassword
            // 
            this.ShowPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowPassword.AutoSize = true;
            this.ShowPassword.Location = new System.Drawing.Point(570, 6);
            this.ShowPassword.Name = "ShowPassword";
            this.ShowPassword.Size = new System.Drawing.Size(102, 17);
            this.ShowPassword.TabIndex = 11;
            this.ShowPassword.Text = "Show Password";
            this.ShowPassword.UseVisualStyleBackColor = true;
            this.ShowPassword.CheckStateChanged += new System.EventHandler(this.ShowPassword_CheckStateChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "RCON password:";
            // 
            // RCON
            // 
            this.RCON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RCON.Location = new System.Drawing.Point(463, 6);
            this.RCON.Name = "RCON";
            this.RCON.PasswordChar = '*';
            this.RCON.Size = new System.Drawing.Size(100, 20);
            this.RCON.TabIndex = 9;
            this.RCON.TextChanged += new System.EventHandler(this.NoSpaces_TextChanged);
            this.RCON.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NoSpaces_KeyPress);
            // 
            // Type
            // 
            this.Type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Type.BackColor = System.Drawing.SystemColors.Info;
            this.Type.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Type.FormattingEnabled = true;
            this.Type.Items.AddRange(new object[] {
            "RCON Command",
            "Console Message",
            "Say Message",
            "Send raw Data",
            "Big Text Message",
            "Private Message",
            "Get Cvar Value"});
            this.Type.Location = new System.Drawing.Point(343, 29);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(114, 21);
            this.Type.TabIndex = 8;
            this.Type.SelectedIndexChanged += new System.EventHandler(this.Type_SelectedIndexChanged);
            // 
            // Clear
            // 
            this.Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear.Location = new System.Drawing.Point(729, 29);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(39, 20);
            this.Clear.TabIndex = 7;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP Adress:";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(218, 2);
            this.Port.MaxLength = 5;
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(40, 20);
            this.Port.TabIndex = 4;
            this.Port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumbersOnly_KeyPress);
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(69, 3);
            this.Address.MaxLength = 64;
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(108, 20);
            this.Address.TabIndex = 3;
            this.Address.TextChanged += new System.EventHandler(this.NoSpaces_TextChanged);
            this.Address.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NoSpaces_KeyPress);
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.BackColor = System.Drawing.SystemColors.Info;
            this.Output.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Output.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Output.Location = new System.Drawing.Point(8, 54);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Output.Size = new System.Drawing.Size(760, 474);
            this.Output.TabIndex = 2;
            // 
            // Input
            // 
            this.Input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Input.Location = new System.Drawing.Point(463, 29);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(213, 20);
            this.Input.TabIndex = 1;
            this.Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Input_KeyPress);
            // 
            // Send
            // 
            this.Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Send.Location = new System.Drawing.Point(682, 29);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(41, 20);
            this.Send.TabIndex = 0;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // ServerList
            // 
            this.ServerList.Controls.Add(this.RefreshButton);
            this.ServerList.Controls.Add(this.GetNewListButton);
            this.ServerList.Location = new System.Drawing.Point(4, 22);
            this.ServerList.Name = "ServerList";
            this.ServerList.Padding = new System.Windows.Forms.Padding(3);
            this.ServerList.Size = new System.Drawing.Size(776, 536);
            this.ServerList.TabIndex = 1;
            this.ServerList.Text = "ServerList";
            this.ServerList.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(110, 6);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(96, 23);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // GetNewListButton
            // 
            this.GetNewListButton.Location = new System.Drawing.Point(8, 6);
            this.GetNewListButton.Name = "GetNewListButton";
            this.GetNewListButton.Size = new System.Drawing.Size(96, 23);
            this.GetNewListButton.TabIndex = 0;
            this.GetNewListButton.Text = "Get New List";
            this.GetNewListButton.UseVisualStyleBackColor = true;
            this.GetNewListButton.Click += new System.EventHandler(this.GetNewListButton_Click);
            // 
            // BackGroundWorkersCheck
            // 
            this.BackGroundWorkersCheck.Enabled = true;
            this.BackGroundWorkersCheck.Interval = 1000;
            this.BackGroundWorkersCheck.Tick += new System.EventHandler(this.BackGroundWorkersCheck_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.TabPage);
            this.Icon = global::UrTQuery.Properties.Resources.Azuresol_Sketchy_Quake_3;
            this.MinimumSize = new System.Drawing.Size(725, 300);
            this.Name = "MainForm";
            this.Text = "UrTQuery by Tundy";
            this.TabPage.ResumeLayout(false);
            this.QueryPage.ResumeLayout(false);
            this.QueryPage.PerformLayout();
            this.ServerList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.ComboBox Type;
        private System.Windows.Forms.TextBox RCON;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ShowPassword;
        private System.Windows.Forms.Button GetStatus;
        private System.Windows.Forms.Button GetInfo;
        private System.Windows.Forms.Button rconStatus;
        private System.Windows.Forms.Button CheckPassword;
        private System.Windows.Forms.TextBox ID;
        private System.Windows.Forms.TabControl TabPage;
        private System.Windows.Forms.TabPage QueryPage;
        private System.Windows.Forms.TabPage ServerList;
        private System.Windows.Forms.Button GetNewListButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Timer BackGroundWorkersCheck;
    }
}