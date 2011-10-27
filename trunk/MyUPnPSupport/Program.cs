using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPnPDevices;
using UPnPDevices.Services;
using MyUPnPSupport.Plugin.Devices;
using MyUPnPSupport.Plugin.Config;

namespace MyUPnPSupport {
    class Program { 

        [STAThread]
        public static void Main(string[] args) {
            
            UPnPDeviceManager manager = new UPnPDeviceManager(false, false, false);           
            manager.AddDevice(new MediaPortalDMR("DMR", Guid.NewGuid().ToString()));
            manager.StartDevices();
            
            Console.ReadLine();
            manager.Dispose();
        }

       
    }
}
