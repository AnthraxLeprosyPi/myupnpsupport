#region #region Copyright (C) 2005-2011 Team MediaPortal

// 
// Copyright (C) 2005-2011 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MediaPortal is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MediaPortal is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.
// 

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.UserInterface.Controls;
using MyUPnPSupport.Plugin.Window.Dialogs;

namespace MyUPnPSupport.Plugin.Config {
    public partial class ConfigurationForm : MPConfigForm {
        public ConfigurationForm() {
            try {
                InitializeComponent();
                Load += ConfigurationForm_Load;
                FormClosing += ConfigurationForm_FormClosing;                
            } catch (Exception ex) {
                Log.Error(ex);
            }
        }
        
        private void ConfigurationForm_Load(object sender, EventArgs e) {            
            Text = String.Format("{0} - {1} - Configuration", Settings.PLUGIN_NAME, Settings.PLUGIN_VERSION);
            Settings.Load();
            checkBoxDMR.Checked = Settings.DMR_ENABLE;
            textBoxDMRName.Text = Settings.DMR_NAME;
            textBoxDMRGuid.Text = Settings.DMR_GUID;
            checkBoxDMS.Checked = Settings.DMS_ENABLE;
            checkBoxDMC.Checked = Settings.DMC_ENABLE;
        }

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e) {
            Settings.DMR_ENABLE = checkBoxDMR.Checked;
            Settings.DMR_NAME = textBoxDMRName.Text;
            Settings.DMR_GUID = textBoxDMRGuid.Text;
            Settings.DMS_ENABLE = checkBoxDMS.Checked;
            Settings.DMC_ENABLE = checkBoxDMC.Checked;
            Settings.Save();
        }

        private void buttonDMRNewGuid_Click(object sender, EventArgs e)
        {
            textBoxDMRGuid.Text = Guid.NewGuid().ToString();
        }
    }
}