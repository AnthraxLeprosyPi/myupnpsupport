using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP.AV.MediaServer.DV;
using OpenSource.UPnP;
using UPnPMediaServerCore;
using System.IO;

namespace MyUPnPSupport.UPnP {
    class DigitalMediaServer : MediaServerDevice {
        
        private static DeviceInfo info = new DeviceInfo() {
            AllowRemoteContentManagement = true,
            FriendlyName = "MediaPortal (DMS)",
            Manufacturer = "OpenSource",
            ManufacturerURL = "",
            ModelName = "Media Server",
            ModelDescription = "Provides content through UPnP ContentDirectory service",
            ModelURL = "",
            ModelNumber = "0.765",
            LocalRootDirectory = "",
            SearchCapabilities = "dc:title,dc:creator,upnp:class,upnp:album,res@protocolInfo,res@size,res@bitrate",
            SortCapabilities = "dc:title,dc:creator,upnp:class,upnp:album",
            EnableSearch = false
        };

        public DigitalMediaServer(UPnPDevice parent)
            : base(info, parent, true, "http-get:*:*:*", "") {            
        }

        public void AddDirectory(string url){
            
        }

    }
}
