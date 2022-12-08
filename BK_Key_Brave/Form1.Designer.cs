namespace BK_Key_Brave
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtProfile = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txt_pathBrave = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.UserData1 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.UserData2 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.UserData3 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btn_backup = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lnk_update = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.link_About = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.btn_Delete_Skip_File = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkBackupClaimed = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txt_delay_bk_key = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btn_check_Claim = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // txtProfile
            // 
            this.txtProfile.Location = new System.Drawing.Point(92, 17);
            this.txtProfile.Name = "txtProfile";
            this.txtProfile.Size = new System.Drawing.Size(394, 23);
            this.txtProfile.TabIndex = 0;
            this.txtProfile.Text = "C:\\Users\\Pikazai\\AppData\\Local\\BraveSoftware\\Brave-Browser\\User Data";
            // 
            // txt_pathBrave
            // 
            this.txt_pathBrave.Location = new System.Drawing.Point(92, 61);
            this.txt_pathBrave.Name = "txt_pathBrave";
            this.txt_pathBrave.Size = new System.Drawing.Size(394, 23);
            this.txt_pathBrave.TabIndex = 1;
            this.txt_pathBrave.Text = "D:\\Claim And Tip\\Application Brave 1.44\\Application\\brave.exe";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 20);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Values.Text = "Path Profile";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(4, 64);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(68, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Path Brave";
            // 
            // UserData1
            // 
            this.UserData1.Location = new System.Drawing.Point(92, 103);
            this.UserData1.Name = "UserData1";
            this.UserData1.Size = new System.Drawing.Size(84, 20);
            this.UserData1.TabIndex = 4;
            this.UserData1.Values.Text = "xxx\\Default";
            // 
            // UserData2
            // 
            this.UserData2.Location = new System.Drawing.Point(198, 103);
            this.UserData2.Name = "UserData2";
            this.UserData2.Size = new System.Drawing.Size(225, 20);
            this.UserData2.TabIndex = 5;
            this.UserData2.Values.Text = "xxx\\Brave-Browser\\User Data\\Default";
            // 
            // UserData3
            // 
            this.UserData3.Location = new System.Drawing.Point(92, 129);
            this.UserData3.Name = "UserData3";
            this.UserData3.Size = new System.Drawing.Size(94, 20);
            this.UserData3.TabIndex = 6;
            this.UserData3.Values.Text = "Profile 1,2,3...";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(4, 103);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(83, 20);
            this.kryptonLabel3.TabIndex = 7;
            this.kryptonLabel3.Values.Text = "User-data-dir";
            // 
            // btn_backup
            // 
            this.btn_backup.Location = new System.Drawing.Point(92, 209);
            this.btn_backup.Name = "btn_backup";
            this.btn_backup.Size = new System.Drawing.Size(90, 25);
            this.btn_backup.TabIndex = 8;
            this.btn_backup.Values.Text = "Backup Key";
            this.btn_backup.Click += new System.EventHandler(this.btn_backup_Click);
            // 
            // lnk_update
            // 
            this.lnk_update.Location = new System.Drawing.Point(220, 255);
            this.lnk_update.Name = "lnk_update";
            this.lnk_update.Size = new System.Drawing.Size(130, 20);
            this.lnk_update.TabIndex = 9;
            this.lnk_update.Values.Text = "Brave Get Key Version";
            // 
            // link_About
            // 
            this.link_About.Location = new System.Drawing.Point(2, 255);
            this.link_About.Name = "link_About";
            this.link_About.Size = new System.Drawing.Size(60, 20);
            this.link_About.TabIndex = 10;
            this.link_About.Values.Text = "@Pikazai";
            this.link_About.LinkClicked += new System.EventHandler(this.link_About_LinkClicked);
            // 
            // btn_Delete_Skip_File
            // 
            this.btn_Delete_Skip_File.Location = new System.Drawing.Point(363, 209);
            this.btn_Delete_Skip_File.Name = "btn_Delete_Skip_File";
            this.btn_Delete_Skip_File.Size = new System.Drawing.Size(116, 25);
            this.btn_Delete_Skip_File.TabIndex = 11;
            this.btn_Delete_Skip_File.Values.Text = "Delete Skip file";
            this.btn_Delete_Skip_File.Click += new System.EventHandler(this.btn_Delete_Skip_File_Click);
            // 
            // chkBackupClaimed
            // 
            this.chkBackupClaimed.Checked = true;
            this.chkBackupClaimed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackupClaimed.Location = new System.Drawing.Point(198, 163);
            this.chkBackupClaimed.Name = "chkBackupClaimed";
            this.chkBackupClaimed.Size = new System.Drawing.Size(178, 20);
            this.chkBackupClaimed.TabIndex = 12;
            this.chkBackupClaimed.Values.Text = "Only Backup Profile Claimed";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(4, 163);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel4.TabIndex = 13;
            this.kryptonLabel4.Values.Text = "Delay time";
            // 
            // txt_delay_bk_key
            // 
            this.txt_delay_bk_key.Location = new System.Drawing.Point(92, 160);
            this.txt_delay_bk_key.Name = "txt_delay_bk_key";
            this.txt_delay_bk_key.Size = new System.Drawing.Size(84, 23);
            this.txt_delay_bk_key.TabIndex = 14;
            this.txt_delay_bk_key.Text = "3500";
            // 
            // btn_check_Claim
            // 
            this.btn_check_Claim.Location = new System.Drawing.Point(220, 209);
            this.btn_check_Claim.Name = "btn_check_Claim";
            this.btn_check_Claim.Size = new System.Drawing.Size(90, 25);
            this.btn_check_Claim.TabIndex = 15;
            this.btn_check_Claim.Values.Text = "Check Claim";
            this.btn_check_Claim.Click += new System.EventHandler(this.btn_check_Claim_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 276);
            this.Controls.Add(this.btn_check_Claim);
            this.Controls.Add(this.txt_delay_bk_key);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.chkBackupClaimed);
            this.Controls.Add(this.btn_Delete_Skip_File);
            this.Controls.Add(this.link_About);
            this.Controls.Add(this.lnk_update);
            this.Controls.Add(this.btn_backup);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.UserData3);
            this.Controls.Add(this.UserData2);
            this.Controls.Add(this.UserData1);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.txt_pathBrave);
            this.Controls.Add(this.txtProfile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup Key Brave";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_pathBrave;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton UserData1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton UserData2;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton UserData3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_backup;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lnk_update;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel link_About;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Delete_Skip_File;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkBackupClaimed;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txt_delay_bk_key;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_check_Claim;
    }
}

