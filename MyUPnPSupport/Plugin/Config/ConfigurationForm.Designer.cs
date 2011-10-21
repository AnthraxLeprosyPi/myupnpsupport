namespace MyUPnPSupport.Plugin.Config {
    partial class ConfigurationForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.label16 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxDeleteOnExit = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxCheezRootFolder = new System.Windows.Forms.TextBox();
            this.labelCheezRootFolder = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonRefreshBonjourServers = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FriendlyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plexServerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxSelectQualityPriorToPlayback = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelQualityLAN = new System.Windows.Forms.Label();
            this.comboBoxQualityWAN = new System.Windows.Forms.ComboBox();
            this.comboBoxQualityLAN = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.isOnlineDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.uriPlexSectionsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uriPlexBaseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverCapabilitiesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plexServerBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Trebuchet MS", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Tomato;
            this.label16.Location = new System.Drawing.Point(12, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(269, 43);
            this.label16.TabIndex = 6;
            this.label16.Text = "MyUPnPSupport";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(688, 214);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxDeleteOnExit);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.textBoxCheezRootFolder);
            this.tabPage1.Controls.Add(this.labelCheezRootFolder);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(680, 188);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeleteOnExit
            // 
            this.checkBoxDeleteOnExit.AutoSize = true;
            this.checkBoxDeleteOnExit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDeleteOnExit.Location = new System.Drawing.Point(9, 56);
            this.checkBoxDeleteOnExit.Name = "checkBoxDeleteOnExit";
            this.checkBoxDeleteOnExit.Size = new System.Drawing.Size(119, 17);
            this.checkBoxDeleteOnExit.TabIndex = 11;
            this.checkBoxDeleteOnExit.Text = "Clear Cache on Exit";
            this.checkBoxDeleteOnExit.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxCheezRootFolder
            // 
            this.textBoxCheezRootFolder.Location = new System.Drawing.Point(139, 26);
            this.textBoxCheezRootFolder.Name = "textBoxCheezRootFolder";
            this.textBoxCheezRootFolder.Size = new System.Drawing.Size(448, 20);
            this.textBoxCheezRootFolder.TabIndex = 7;
            // 
            // labelCheezRootFolder
            // 
            this.labelCheezRootFolder.AutoSize = true;
            this.labelCheezRootFolder.Location = new System.Drawing.Point(6, 29);
            this.labelCheezRootFolder.Name = "labelCheezRootFolder";
            this.labelCheezRootFolder.Size = new System.Drawing.Size(125, 13);
            this.labelCheezRootFolder.TabIndex = 6;
            this.labelCheezRootFolder.Text = "Thumb && Artwork Cache:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonRefreshBonjourServers);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(680, 188);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plex Servers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonRefreshBonjourServers
            // 
            this.buttonRefreshBonjourServers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonRefreshBonjourServers.Location = new System.Drawing.Point(3, 162);
            this.buttonRefreshBonjourServers.Name = "buttonRefreshBonjourServers";
            this.buttonRefreshBonjourServers.Size = new System.Drawing.Size(674, 23);
            this.buttonRefreshBonjourServers.TabIndex = 1;
            this.buttonRefreshBonjourServers.Text = "Update Online Status && Discover Plex Servers (Bonjour Discovery)";
            this.buttonRefreshBonjourServers.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FriendlyName});
            this.dataGridView1.DataSource = this.plexServerBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.Size = new System.Drawing.Size(674, 153);
            this.dataGridView1.TabIndex = 0;
            // 
            // FriendlyName
            // 
            this.FriendlyName.DataPropertyName = "FriendlyName";
            this.FriendlyName.FillWeight = 75.85722F;
            this.FriendlyName.HeaderText = "Friendly Name";
            this.FriendlyName.Name = "FriendlyName";
            this.FriendlyName.ReadOnly = true;
            this.FriendlyName.Width = 99;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxSelectQualityPriorToPlayback);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.labelQualityLAN);
            this.tabPage3.Controls.Add(this.comboBoxQualityWAN);
            this.tabPage3.Controls.Add(this.comboBoxQualityLAN);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(680, 188);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Connection Speed && Quality";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectQualityPriorToPlayback
            // 
            this.checkBoxSelectQualityPriorToPlayback.AutoSize = true;
            this.checkBoxSelectQualityPriorToPlayback.Location = new System.Drawing.Point(252, 94);
            this.checkBoxSelectQualityPriorToPlayback.Name = "checkBoxSelectQualityPriorToPlayback";
            this.checkBoxSelectQualityPriorToPlayback.Size = new System.Drawing.Size(134, 17);
            this.checkBoxSelectQualityPriorToPlayback.TabIndex = 5;
            this.checkBoxSelectQualityPriorToPlayback.Text = "select before Playback";
            this.checkBoxSelectQualityPriorToPlayback.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Transcoding Quality (WAN/Internet):";
            // 
            // labelQualityLAN
            // 
            this.labelQualityLAN.AutoSize = true;
            this.labelQualityLAN.Location = new System.Drawing.Point(71, 30);
            this.labelQualityLAN.Name = "labelQualityLAN";
            this.labelQualityLAN.Size = new System.Drawing.Size(175, 13);
            this.labelQualityLAN.TabIndex = 3;
            this.labelQualityLAN.Text = "Transcoding Quality (LAN/Bonjour):";
            // 
            // comboBoxQualityWAN
            // 
            this.comboBoxQualityWAN.FormattingEnabled = true;
            this.comboBoxQualityWAN.Location = new System.Drawing.Point(252, 66);
            this.comboBoxQualityWAN.Name = "comboBoxQualityWAN";
            this.comboBoxQualityWAN.Size = new System.Drawing.Size(136, 21);
            this.comboBoxQualityWAN.TabIndex = 2;
            // 
            // comboBoxQualityLAN
            // 
            this.comboBoxQualityLAN.FormattingEnabled = true;
            this.comboBoxQualityLAN.Location = new System.Drawing.Point(252, 27);
            this.comboBoxQualityLAN.Name = "comboBoxQualityLAN";
            this.comboBoxQualityLAN.Size = new System.Drawing.Size(136, 21);
            this.comboBoxQualityLAN.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::MyUPnPSupport.Properties.Resources.MyUPnPSupport_enabled;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::MyUPnPSupport.Properties.Resources.MyUPnPSupport_enabled;
            this.pictureBox1.Location = new System.Drawing.Point(630, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // isOnlineDataGridViewCheckBoxColumn
            // 
            this.isOnlineDataGridViewCheckBoxColumn.DataPropertyName = "IsOnline";
            this.isOnlineDataGridViewCheckBoxColumn.HeaderText = "IsOnline";
            this.isOnlineDataGridViewCheckBoxColumn.Name = "isOnlineDataGridViewCheckBoxColumn";
            this.isOnlineDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isOnlineDataGridViewCheckBoxColumn.Width = 59;
            // 
            // uriPlexSectionsDataGridViewTextBoxColumn
            // 
            this.uriPlexSectionsDataGridViewTextBoxColumn.DataPropertyName = "UriPlexSections";
            this.uriPlexSectionsDataGridViewTextBoxColumn.HeaderText = "UriPlexSections";
            this.uriPlexSectionsDataGridViewTextBoxColumn.Name = "uriPlexSectionsDataGridViewTextBoxColumn";
            this.uriPlexSectionsDataGridViewTextBoxColumn.ReadOnly = true;
            this.uriPlexSectionsDataGridViewTextBoxColumn.Width = 58;
            // 
            // uriPlexBaseDataGridViewTextBoxColumn
            // 
            this.uriPlexBaseDataGridViewTextBoxColumn.DataPropertyName = "UriPlexBase";
            this.uriPlexBaseDataGridViewTextBoxColumn.HeaderText = "UriPlexBase";
            this.uriPlexBaseDataGridViewTextBoxColumn.Name = "uriPlexBaseDataGridViewTextBoxColumn";
            this.uriPlexBaseDataGridViewTextBoxColumn.ReadOnly = true;
            this.uriPlexBaseDataGridViewTextBoxColumn.Width = 59;
            // 
            // serverCapabilitiesDataGridViewTextBoxColumn
            // 
            this.serverCapabilitiesDataGridViewTextBoxColumn.DataPropertyName = "ServerCapabilities";
            this.serverCapabilitiesDataGridViewTextBoxColumn.HeaderText = "ServerCapabilities";
            this.serverCapabilitiesDataGridViewTextBoxColumn.Name = "serverCapabilitiesDataGridViewTextBoxColumn";
            this.serverCapabilitiesDataGridViewTextBoxColumn.Width = 58;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MyUPnPSupport.Properties.Resources.config_ctripes;
            this.ClientSize = new System.Drawing.Size(712, 292);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plexServerBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxDeleteOnExit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxCheezRootFolder;
        private System.Windows.Forms.Label labelCheezRootFolder;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonRefreshBonjourServers;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource plexServerBindingSource;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverCapabilitiesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isOnlineDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uriPlexSectionsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uriPlexBaseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hostNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hostAdressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userPassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendlyName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isBonjourDataGridViewCheckBoxColumn;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox comboBoxQualityWAN;
        private System.Windows.Forms.ComboBox comboBoxQualityLAN;
        private System.Windows.Forms.CheckBox checkBoxSelectQualityPriorToPlayback;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelQualityLAN;        


    }
}