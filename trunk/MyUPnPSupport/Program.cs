using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUPnPSupport.UPnP;
using MyUPnPSupport.UPnP.Services;

namespace MyUPnPSupport {
    class Program {

        [STAThread]
        public static void Main(string[] args) {

            Uri myURI = new Uri("http://www.team-mediaportal.com");

            UPnPDeviceManager manager = new UPnPDeviceManager();          
            manager.StartDevices();
            Console.ReadLine();
            manager.Dispose();
        }

       
    }
}
