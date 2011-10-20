using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;
using MyUPnPSupport.UPnP.Services;
using MyUPnPSupport.UPnP.Devices;

namespace MyUPnPSupport.UPnP {
   public class UPnPDeviceManager  : IDisposable{

        private List<UPnPDevice> UPnPDevices { get; set; }

        public UPnPDeviceManager() {
            UPnPDevices = new List<UPnPDevice>();
            Uri myURI = new Uri("http://www.team-mediaportal.com");           
            MediaPortalDMR renderer = new MediaPortalDMR(
                "MediaPortal (DMR)",
                "Anthrax",
                "MyUPnPSupport MediaPortal-Plugin",
                "Custom root-device for the MyUPnPSupport MediaPortal-Plugin",
                new Guid("3F4EDD00-F8DD-4B91-AA76-BCABF3DCF193"),
                myURI);

            MediaPortalDMS server = new MediaPortalDMS(
                "MediaPortal (DMS)",
                "Anthrax",
                "MyUPnPSupport MediaPortal-Plugin",
                "Custom root-device for the MyUPnPSupport MediaPortal-Plugin",
                new Guid("6D40169B-BC4E-4D1D-BBB4-C26A67CC9BCF"),
                myURI);

            this.AddDevice(renderer);
            this.AddDevice(server);            
        }

        public void AddDevice(IUPnPDevice device) {
            UPnPDevices.Add(device.GetUPnPDevice());
        }

        public void StartDevices() {
            UPnPDevices.ForEach(dev => dev.StartDevice());
        }

        public void StopDevices() {
            UPnPDevices.ForEach(dev => dev.StopDevice());
        }

        #region IDisposable Members

        public void Dispose() {            
            StopDevices();
        }

        #endregion
    }
}
