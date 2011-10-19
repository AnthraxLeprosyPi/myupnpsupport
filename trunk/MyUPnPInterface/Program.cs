using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUPnPSupport.UPnP;

namespace MyUPnPSupport {
    class Program {

        [STAThread]
        public static void Main(string[] args) {           
            Uri myURI = new Uri("http://www.team-mediaportal.com");

            UPnPDeviceManager manager = new UPnPDeviceManager();
            DigitalMediaRenderer renderer = new DigitalMediaRenderer("MediaPortal (DMR)", "Anthrax", "MyUPnPSupport MediaPortal-Plugin", "Custom root-device for the MyUPnPSupport MediaPortal-Plugin", new Guid("B85901F4-D5F5-4003-9C8A-7E2B0F5AFE7F"), myURI);
            renderer.OnPlay += new DigitalMediaRenderer.OnPlayHandler(renderer_OnPlay);

            DigitalMediaServer server = new DigitalMediaServer("MediaPortal (DMS)", "Anthrax", "MyUPnPSupport MediaPortal-Plugin", "Custom root-device for the MyUPnPSupport MediaPortal-Plugin", new Guid("6D40169B-BC4E-4D1D-BBB4-C26A67CC9BCF"), myURI);
            server.AddDirectory(@"D:\Anthrax-Leprosy-Pi\Music\Rainald Grebe");
            manager.AddDevice(renderer);
            manager.AddDevice(server);

            manager.StartDevices();
            Console.ReadLine();
            manager.Dispose();
        }

        static void renderer_OnPlay(Uri mediaURI, string mediaTitle) {
            Console.Out.WriteLine(mediaURI.ToString() + " : " + mediaTitle);
        }       
    }
}
