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
            this.tabPageDMR = new System.Windows.Forms.TabPage();
            this.buttonDMRNewGuid = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDMRGuid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDMRName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxDMR = new System.Windows.Forms.CheckBox();
            this.tabPageDMS = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxDMS = new System.Windows.Forms.CheckBox();
            this.tabPageDMC = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxDMC = new System.Windows.Forms.CheckBox();
            this.plexServerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.isOnlineDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.uriPlexSectionsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uriPlexBaseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverCapabilitiesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPageDMR.SuspendLayout();
            this.tabPageDMS.SuspendLayout();
            this.tabPageDMC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plexServerBindingSource)).BeginInit();
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
            this.tabControl1.Controls.Add(this.tabPageDMR);
            this.tabControl1.Controls.Add(this.tabPageDMS);
            this.tabControl1.Controls.Add(this.tabPageDMC);
            this.tabControl1.Location = new System.Drawing.Point(12, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(553, 197);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPageDMR
            // 
            this.tabPageDMR.Controls.Add(this.buttonDMRNewGuid);
            this.tabPageDMR.Controls.Add(this.label7);
            this.tabPageDMR.Controls.Add(this.textBoxDMRGuid);
            this.tabPageDMR.Controls.Add(this.label6);
            this.tabPageDMR.Controls.Add(this.textBoxDMRName);
            this.tabPageDMR.Controls.Add(this.label1);
            this.tabPageDMR.Controls.Add(this.checkBoxDMR);
            this.tabPageDMR.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMR.Name = "tabPageDMR";
            this.tabPageDMR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMR.Size = new System.Drawing.Size(545, 171);
            this.tabPageDMR.TabIndex = 0;
            this.tabPageDMR.Text = "Digital MediaRenderer (DMR)";
            this.tabPageDMR.UseVisualStyleBackColor = true;
            // 
            // buttonDMRNewGuid
            // 
            this.buttonDMRNewGuid.Location = new System.Drawing.Point(430, 88);
            this.buttonDMRNewGuid.Name = "buttonDMRNewGuid";
            this.buttonDMRNewGuid.Size = new System.Drawing.Size(39, 23);
            this.buttonDMRNewGuid.TabIndex = 6;
            this.buttonDMRNewGuid.Text = "new";
            this.buttonDMRNewGuid.UseVisualStyleBackColor = true;
            this.buttonDMRNewGuid.Click += new System.EventHandler(this.buttonDMRNewGuid_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(155, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "UPnP GUID:";
            // 
            // textBoxDMRGuid
            // 
            this.textBoxDMRGuid.Location = new System.Drawing.Point(230, 90);
            this.textBoxDMRGuid.Name = "textBoxDMRGuid";
            this.textBoxDMRGuid.ReadOnly = true;
            this.textBoxDMRGuid.Size = new System.Drawing.Size(194, 20);
            this.textBoxDMRGuid.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "UPnP Name:";
            // 
            // textBoxDMRName
            // 
            this.textBoxDMRName.Location = new System.Drawing.Point(230, 60);
            this.textBoxDMRName.Name = "textBoxDMRName";
            this.textBoxDMRName.Size = new System.Drawing.Size(239, 20);
            this.textBoxDMRName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Digital Media Renderer\r\n(DLNA DMR)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxDMR
            // 
            this.checkBoxDMR.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxDMR.BackgroundImage = global::MyUPnPSupport.Properties.Resources.upnp_dmr_l;
            this.checkBoxDMR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBoxDMR.Checked = true;
            this.checkBoxDMR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDMR.Location = new System.Drawing.Point(31, 29);
            this.checkBoxDMR.Name = "checkBoxDMR";
            this.checkBoxDMR.Size = new System.Drawing.Size(84, 84);
            this.checkBoxDMR.TabIndex = 0;
            this.checkBoxDMR.UseVisualStyleBackColor = true;
            // 
            // tabPageDMS
            // 
            this.tabPageDMS.Controls.Add(this.label4);
            this.tabPageDMS.Controls.Add(this.label2);
            this.tabPageDMS.Controls.Add(this.checkBoxDMS);
            this.tabPageDMS.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMS.Name = "tabPageDMS";
            this.tabPageDMS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMS.Size = new System.Drawing.Size(545, 171);
            this.tabPageDMS.TabIndex = 1;
            this.tabPageDMS.Text = "Digital Media Server (DMS)";
            this.tabPageDMS.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(233, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Coming soon...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Digital Media Server\r\n(DLNA DMS)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxDMS
            // 
            this.checkBoxDMS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxDMS.BackgroundImage = global::MyUPnPSupport.Properties.Resources.upnp_dms_l;
            this.checkBoxDMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBoxDMS.Location = new System.Drawing.Point(24, 29);
            this.checkBoxDMS.Name = "checkBoxDMS";
            this.checkBoxDMS.Size = new System.Drawing.Size(84, 84);
            this.checkBoxDMS.TabIndex = 4;
            this.checkBoxDMS.UseVisualStyleBackColor = true;
            // 
            // tabPageDMC
            // 
            this.tabPageDMC.Controls.Add(this.label5);
            this.tabPageDMC.Controls.Add(this.label3);
            this.tabPageDMC.Controls.Add(this.checkBoxDMC);
            this.tabPageDMC.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMC.Name = "tabPageDMC";
            this.tabPageDMC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMC.Size = new System.Drawing.Size(545, 171);
            this.tabPageDMC.TabIndex = 2;
            this.tabPageDMC.Text = "Digital Media Controller (DMC)";
            this.tabPageDMC.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(200, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(255, 48);
            this.label5.TabIndex = 8;
            this.label5.Text = "This might take a little while...\r\n(it\'s the actual window plugin)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Digital Media Controller\r\n(DLNA DMC)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxDMC
            // 
            this.checkBoxDMC.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxDMC.BackgroundImage = global::MyUPnPSupport.Properties.Resources.upnp_dmc_l;
            this.checkBoxDMC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBoxDMC.Enabled = false;
            this.checkBoxDMC.Location = new System.Drawing.Point(31, 21);
            this.checkBoxDMC.Name = "checkBoxDMC";
            this.checkBoxDMC.Size = new System.Drawing.Size(84, 84);
            this.checkBoxDMC.TabIndex = 6;
            this.checkBoxDMC.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::MyUPnPSupport.Properties.Resources.hover_extensions;
            this.pictureBox1.Location = new System.Drawing.Point(485, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 73);
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
            this.ClientSize = new System.Drawing.Size(577, 275);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDMR.ResumeLayout(false);
            this.tabPageDMR.PerformLayout();
            this.tabPageDMS.ResumeLayout(false);
            this.tabPageDMS.PerformLayout();
            this.tabPageDMC.ResumeLayout(false);
            this.tabPageDMC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plexServerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDMR;
        private System.Windows.Forms.TabPage tabPageDMS;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn isBonjourDataGridViewCheckBoxColumn;
        private System.Windows.Forms.TabPage tabPageDMC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxDMR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxDMS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxDMC;
        private System.Windows.Forms.Button buttonDMRNewGuid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxDMRGuid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDMRName;        


    }
}