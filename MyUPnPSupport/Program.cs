using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUPnPSupport.UPnP;
using MyUPnPSupport.UPnP.Services;
using MyUPnPSupport.Plugin.Devices;

namespace MyUPnPSupport {
    class Program { 

        [STAThread]
        public static void Main(string[] args) {          

            UPnPDeviceManager manager = new UPnPDeviceManager(true, true, true);           
            manager.StartDevices();
            
            Console.ReadLine();
            manager.Dispose();
        }

       
    }
}
