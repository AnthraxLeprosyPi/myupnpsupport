using System;
using System.Collections.Generic;
using MyUPnPSupport.Plugin.Config;
using OpenSource.UPnP;
using UPnPStack.Devices;

namespace MyUPnPSupport.Plugin.Devices {
   public class UPnPDeviceManager  : IDisposable{

        private List<UPnPDevice> UPnPDevices { get; set; }

        public UPnPDeviceManager(bool enableDMR, bool enableDMS, bool enableDMC) {
            UPnPDevices = new List<UPnPDevice>();

            if (enableDMR) {
                MediaPortalDMR dmr = new MediaPortalDMR(Settings.DMR_NAME, Settings.DMR_GUID);
                AddDevice(dmr);
            }
            if (enableDMS) {
                MediaPortalDMS server = new MediaPortalDMS("MediaPortal (DMS)","6D40169B-BC4E-4D1D-BBB4-C26A67CC9BCF");
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
