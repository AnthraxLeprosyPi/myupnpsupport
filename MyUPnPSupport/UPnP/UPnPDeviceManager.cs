using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;
using MyUPnPSupport.UPnP.Services;
using MyUPnPSupport.UPnP.Devices;
using MyUPnPSupport.Plugin.Devices;
using MyUPnPSupport.Plugin.Config;

namespace MyUPnPSupport.UPnP {
   public class UPnPDeviceManager  : IDisposable{

        private List<UPnPDevice> UPnPDevices { get; set; }

        public UPnPDeviceManager(bool enableDMR, bool enableDMS, bool enableDMC) {
            UPnPDevices = new List<UPnPDevice>();

            if (enableDMR) {
                MediaPortalDMR dmr = new MediaPortalDMR(Settings.DMR_NAME, Settings.DMR_GUID);
                AddDevice(dmr);
            }
            if (enableDMS) {
                DigitalMediaServer server = new DigitalMediaServer(
                    "MediaPortal (DMS)",
                    "Anthrax",
                    "MyUPnPSupport MediaPortal-Plugin",
                    "Custom root-device for the MyUPnPSupport MediaPortal-Plugin",
                    new Guid("6D40169B-BC4E-4D1D-BBB4-C26A67CC9BCF"),
                    new Uri("http://www.team-mediaportal.com"),
                    Properties.Resources.upnp_dms_l,
                    Properties.Resources.upnp_dms_s);
                this.AddDevice(server);
            }
            if (enableDMC) { 
                
            }
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
