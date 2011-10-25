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
using System.IO;
using MediaPortal.GUI.Library;
using MyUPnPSupport.Plugin.Window.Dialogs;

namespace MyUPnPSupport.Plugin.Config {
    public static class Settings {
        #region SectionType enum

        public enum SectionType {
            Music,
            Video,
            Photo
        }

        #endregion

        public const string PLUGIN_NAME = "MyUPnPSupport";
        public const string PLUGIN_AUTHOR = "Anthrax";
        public const string PLUGIN_VERSION = "0.1.0";
        public const string PLUGIN_DESCRIPTION = "A MediaPortal plugin to enable UPnP/DLNA support...";

        public const int PLUGIN_WINDOW_ID = 20111020;

        public static string SKIN_FOLDER_MEDIA = Path.Combine(GUIGraphicsContext.Skin, @"Media\" + PLUGIN_NAME);
        public static string PLUGIN_MEDIA_HOVER = @"hover_MyUPnPSupport.png";

        public static string SKINFILE_MAIN_WINDOW = GUIGraphicsContext.Skin + @"\MyUPnPSupport.xml";

        static Settings() {
            //Set defaults           
            //DMR_ENABLE = true;
            //DMS_ENABLE = false;
            //DMC_ENABLE = false;
        }

        public static bool DMR_ENABLE { get; set; }
        public static string DMR_GUID { get; set; }
        public static string DMR_NAME { get; set; }
        public static bool DMS_ENABLE { get; set; }
        public static bool DMC_ENABLE { get; set; }


        /// <summary>
        ///   Load the settings from the mediaportal config
        /// </summary>
        public static void Load() {
            using (
                MediaPortal.Profile.Settings reader =
                    new MediaPortal.Profile.Settings(MediaPortal.Configuration.Config.GetFile(MediaPortal.Configuration.Config.Dir.Config, "MediaPortal.xml"))) {
                DMR_ENABLE = reader.GetValueAsBool(PLUGIN_NAME, "DMR_ENABLE", true);
                DMR_GUID = reader.GetValueAsString(PLUGIN_NAME, "DMR_GUID", Guid.NewGuid().ToString());
                DMR_NAME = reader.GetValueAsString(PLUGIN_NAME, "DMR_NAME", "MediaPortal (DMR)");
                DMS_ENABLE = reader.GetValueAsBool(PLUGIN_NAME, "DMS_ENABLE", false);
                DMC_ENABLE = reader.GetValueAsBool(PLUGIN_NAME, "DMC_ENABLE", false);
            }
        }

        /// <summary>
        ///   Save the settings to the MP config
        /// </summary>
        public static void Save() {
            using (
                MediaPortal.Profile.Settings xmlwriter =
                    new MediaPortal.Profile.Settings(MediaPortal.Configuration.Config.GetFile(MediaPortal.Configuration.Config.Dir.Config, "MediaPortal.xml"))) {
                xmlwriter.SetValueAsBool(PLUGIN_NAME, "DMR_ENABLE", DMR_ENABLE);
                xmlwriter.SetValue(PLUGIN_NAME, "DMR_GUID", DMR_GUID);
                xmlwriter.SetValue(PLUGIN_NAME, "DMR_NAME", DMR_NAME);
                xmlwriter.SetValueAsBool(PLUGIN_NAME, "DMS_ENABLE", DMS_ENABLE);
                xmlwriter.SetValueAsBool(PLUGIN_NAME, "DMC_ENABLE", DMC_ENABLE);

            }
        }




    }
}