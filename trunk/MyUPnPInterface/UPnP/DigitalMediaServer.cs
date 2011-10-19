using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP.AV.MediaServer.DV;
using OpenSource.UPnP;
using UPnPMediaServerCore;
using System.IO;

namespace MyUPnPSupport.UPnP {
    class DigitalMediaServer : IUPnPDevice {

        private MediaServerCore MediaServerCore { get; set; }
        private UPnPMediaServer MediaServer { get; set; }

        public DigitalMediaServer(string friendlName) {
            MediaServerCore = new MediaServerCore(friendlName);
            this.MediaServerCore.OnDirectoriesChanged += new UPnPMediaServerCore.MediaServerCore.MediaServerCoreEventHandler(MediaServerCore_OnDirectoriesChanged);
            MediaServer = new UPnPMediaServer();
        }

        void MediaServerCore_OnDirectoriesChanged(MediaServerCore sender) {
            throw new NotImplementedException();
        }

        public void AddDirectory(string dirPath){
            MediaServer.AddDirectory(new DirectoryInfo(dirPath), false, false);
        }

        public UPnPDevice GetUPnPDevice() {
            return MediaServer.GetUPnPDevice();
        }
    }
}
