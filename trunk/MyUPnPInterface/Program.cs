using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUPnPSupport.UPnP;

namespace MyUPnPSupport {
    class Program {

        [STAThread]
        public static void Main(string[] args) {

            Guid myID = new Guid("B85901F4-D5F5-4003-9C8A-7E2B0F5AFE7F");
            Uri myURI = new Uri("http://www.team-mediaportal.com");
            UPnPDeviceManager manager = new UPnPDeviceManager("MyUPnPSupport", "Anthrax", "MyUPnPSupport MediaPortal-Plugin", "Custom root-device for the MyUPnPSupport MediaPortal-Plugin", myID, myURI);
            DigitalMediaRenderer renderer = new DigitalMediaRenderer("MediaPortal (DMR)");
            renderer.OnPlay += new DigitalMediaRenderer.OnPlayHandler(renderer_OnPlay);

            DigitalMediaServer server = new DigitalMediaServer("MediaPortal (DMS)");
            //server.AddDirectory(@"D:\Anthrax-Leprosy-Pi\Music\Rainald Grebe");
            manager.AddDevice(renderer);
            manager.AddDevice(server);

            manager.StartRootDevice();
            Console.ReadLine();
            manager.Dispose();
        }

        static void renderer_OnPlay(Uri mediaURI, string mediaTitle) {
            Console.Out.WriteLine(mediaURI.ToString() + " : " + mediaTitle);
        }       
    }
}
