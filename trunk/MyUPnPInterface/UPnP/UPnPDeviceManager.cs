using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;

namespace MyUPnPSupport.UPnP {
   public class UPnPDeviceManager  : IDisposable{

        private UPnPDevice RootDevice { get; set; }

        public UPnPDeviceManager(string friendlyName, string manufacturer, string modelName, string modelDescription, Guid deviceId, Uri deviceUri) {            
            RootDevice = UPnPDevice.CreateRootDevice(900, 1, "");
            RootDevice.FriendlyName = friendlyName;
            RootDevice.UniqueDeviceName = deviceId.ToString();            
            RootDevice.StandardDeviceType = "MediaRenderer";  
            RootDevice.Manufacturer = manufacturer;
            RootDevice.ManufacturerURL = deviceUri.ToString();
            RootDevice.ModelName = modelName;
            RootDevice.ModelDescription = modelDescription;
            RootDevice.ModelURL = deviceUri;
            RootDevice.HasPresentation = false;
        }

        public void AddDevice(IUPnPDevice childDevice) {
            RootDevice.AddDevice(childDevice);
        }

        public void StartRootDevice() {
            RootDevice.StartDevice();
        }

        public void StopRootDevice() {
            RootDevice.StopDevice();
        }

        #region IDisposable Members

        public void Dispose() {            
            StopRootDevice();
        }

        #endregion
    }
}
