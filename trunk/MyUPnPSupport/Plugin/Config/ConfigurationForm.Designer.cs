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
            this.tabPageDMS = new System.Windows.Forms.TabPage();
            this.plexServerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageDMC = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.isOnlineDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.uriPlexSectionsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uriPlexBaseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverCapabilitiesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxDMR = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxDMS = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxDMC = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageDMR.SuspendLayout();
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
            this.tabPageDMR.Controls.Add(this.label3);
            this.tabPageDMR.Controls.Add(this.checkBoxDMC);
            this.tabPageDMR.Controls.Add(this.label2);
            this.tabPageDMR.Controls.Add(this.checkBoxDMS);
            this.tabPageDMR.Controls.Add(this.label1);
            this.tabPageDMR.Controls.Add(this.checkBoxDMR);
            this.tabPageDMR.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMR.Name = "tabPageDMR";
            this.tabPageDMR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMR.Size = new System.Drawing.Size(545, 171);
            this.tabPageDMR.TabIndex = 0;
            this.tabPageDMR.Text = "General Settings";
            this.tabPageDMR.UseVisualStyleBackColor = true;
            // 
            // tabPageDMS
            // 
            this.tabPageDMS.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMS.Name = "tabPageDMS";
            this.tabPageDMS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMS.Size = new System.Drawing.Size(545, 171);
            this.tabPageDMS.TabIndex = 1;
            this.tabPageDMS.Text = "Digital Media Server (DMS)";
            this.tabPageDMS.UseVisualStyleBackColor = true;
            // 
            // tabPageDMC
            // 
            this.tabPageDMC.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMC.Name = "tabPageDMC";
            this.tabPageDMC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMC.Size = new System.Drawing.Size(545, 171);
            this.tabPageDMC.TabIndex = 2;
            this.tabPageDMC.Text = "Digital Media Controller (DMC)";
            this.tabPageDMC.UseVisualStyleBackColor = true;
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
            // checkBoxDMR
            // 
            this.checkBoxDMR.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxDMR.BackgroundImage = global::MyUPnPSupport.Properties.Resources.defaultAudioBig;
            this.checkBoxDMR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBoxDMR.Checked = true;
            this.checkBoxDMR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDMR.Location = new System.Drawing.Point(69, 31);
            this.checkBoxDMR.Name = "checkBoxDMR";
            this.checkBoxDMR.Size = new System.Drawing.Size(84, 84);
            this.checkBoxDMR.TabIndex = 0;
            this.checkBoxDMR.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Digital Media Renderer\r\n(DLNA DMR)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Digital Media Server\r\n(DLNA DMS)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxDMS
            // 
            this.checkBoxDMS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxDMS.BackgroundImage = global::MyUPnPSupport.Properties.Resources.defaultHardDiskBig;
            this.checkBoxDMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBoxDMS.Location = new System.Drawing.Point(223, 31);
            this.checkBoxDMS.Name = "checkBoxDMS";
            this.checkBoxDMS.Size = new System.Drawing.Size(84, 84);
            this.checkBoxDMS.TabIndex = 2;
            this.checkBoxDMS.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Digital Media Controller\r\n(DLNA DMC)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxDMC
            // 
            this.checkBoxDMC.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxDMC.BackgroundImage = global::MyUPnPSupport.Properties.Resources.defaultNetworkBig;
            this.checkBoxDMC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBoxDMC.Enabled = false;
            this.checkBoxDMC.Location = new System.Drawing.Point(377, 31);
            this.checkBoxDMC.Name = "checkBoxDMC";
            this.checkBoxDMC.Size = new System.Drawing.Size(84, 84);
            this.checkBoxDMC.TabIndex = 4;
            this.checkBoxDMC.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxDMC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxDMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxDMR;        


    }
}