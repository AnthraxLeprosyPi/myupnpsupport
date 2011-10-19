using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSource.UPnP;
using OpenSource.UPnP.AV.RENDERER.Device;
using OpenSource.UPnP.AV.CdsMetadata;

namespace MyUPnPSupport.UPnP {
    public class DigitalMediaRenderer : AVRenderer , IDisposable {

        
        public delegate void OnPlayHandler(Uri mediaURI, string mediaTitle);
        public event OnPlayHandler OnPlay; 

        private static List<ProtocolInfoString> Capabilities = new List<ProtocolInfoString>() { 
            new ProtocolInfoString("http-get:*:audio/mpegurl:*"),
            new ProtocolInfoString("http-get:*:audio/mp3:*"),
            new ProtocolInfoString("http-get:*:audio/mpeg:*"),
            new ProtocolInfoString("http-get:*:audio/x-ms-wma:*"),
            new ProtocolInfoString("http-get:*:audio/wma:*"),
            new ProtocolInfoString("http-get:*:video/x-ms-wmv:*"),
            new ProtocolInfoString("http-get:*:video/x-ms-asf:*"),
            new ProtocolInfoString("http-get:*:video/x-ms-avi:*"),
            new ProtocolInfoString(" http-get:*:video/mpeg:*")
        };

        public DigitalMediaRenderer(string deviceName)
            : base(1, Capabilities.ToArray(), null) {
            base.device.FriendlyName = deviceName;
            
            this.OnNewConnection += new ConnectionHandler(DigitalMediaRenderer_OnNewConnection);
            this.OnClosedConnection += new ConnectionHandler(DigitalMediaRenderer_OnClosedConnection);
        }

        void DigitalMediaRenderer_OnClosedConnection(AVRenderer sender, AVConnection c) {
            
        }

        void DigitalMediaRenderer_OnNewConnection(AVRenderer sender, AVConnection c) {
            c.OnPlay += new AVConnection.PlayHandler(c_OnPlay);
        }


        void c_OnPlay(AVConnection sender, OpenSource.UPnP.AV.DvAVTransport.Enum_TransportPlaySpeed Speed) {
            OnPlay(sender.CurrentURI, sender.CurrentUriMetaData);
        }

        

        #region IDisposable Members

        public void Dispose() {
            this.device.StopDevice();
        }

        #endregion
    }
}


