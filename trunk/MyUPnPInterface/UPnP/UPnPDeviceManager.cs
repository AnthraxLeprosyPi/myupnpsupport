using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;

namespace MyUPnPSupport.UPnP {
   public class UPnPDeviceManager  : IDisposable{

        private List<UPnPDevice> UPnPDevices { get; set; }

        public UPnPDeviceManager() {
            UPnPDevices = new List<UPnPDevice>();
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
