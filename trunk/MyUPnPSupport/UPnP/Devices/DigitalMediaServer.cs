// UPnP .NET Framework Device Stack, Device Module
// Device Builder Build#1.0.4144.25068

using System;
using OpenSource.UPnP;
using MyUPnPSupport.UPnP.MediaServer;
using System.Collections.Generic;

namespace MyUPnPSupport.UPnP.Devices {
    /// <summary>
    /// Summary description for SampleDevice.
    /// </summary>
   public class DigitalMediaServer : IUPnPDevice{
        private UPnPDevice device;


        /// <summary>
        /// Initializes a new customized instance of the <see cref="DigitalMediaServer"/> class.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="modelDescription">The model description.</param>
        /// <param name="deviceId">The device id.</param>
        /// <param name="deviceUri">The device URI.</param>
        public DigitalMediaServer(string friendlyName, string manufacturer, string modelName, string modelDescription, Guid deviceId, Uri deviceUri) : this() {
            device.FriendlyName = friendlyName;
            device.UniqueDeviceName = deviceId.ToString();
            device.SerialNumber = deviceId.ToString();    
            device.Manufacturer = manufacturer;
            device.ManufacturerURL = deviceUri.ToString();
            device.ModelName = modelName;
            device.ModelDescription = modelDescription;
            device.ModelURL = deviceUri;
            device.Icon = Resources.Resource.logo_mp_l;
            device.Icon2 = Resources.Resource.logo_mp_s;         
        }

        public DigitalMediaServer() {
            device = UPnPDevice.CreateRootDevice(1800, 1.0, "\\");

            device.FriendlyName = "Intel's Embedded Media Server";
            device.Manufacturer = "Intel's Connected and Extended PC Lab";
            device.ManufacturerURL = "http://www.intel.com";
            device.ModelName = "XPC Media Server";
            device.ModelDescription = "UPnP/AV 1.0 Compliant Media Server";
            device.ModelNumber = "";
            device.HasPresentation = false;
            device.DeviceURN = "urn:schemas-upnp-org:device:MediaServer:1";
            MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer ConnectionManager = new MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer();
            ConnectionManager.External_ConnectionComplete = new MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer.Delegate_ConnectionComplete(ConnectionManager_ConnectionComplete);
            ConnectionManager.External_GetCurrentConnectionIDs = new MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer.Delegate_GetCurrentConnectionIDs(ConnectionManager_GetCurrentConnectionIDs);
            ConnectionManager.External_GetCurrentConnectionInfo = new MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer.Delegate_GetCurrentConnectionInfo(ConnectionManager_GetCurrentConnectionInfo);
            ConnectionManager.External_GetProtocolInfo = new MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer.Delegate_GetProtocolInfo(ConnectionManager_GetProtocolInfo);
            ConnectionManager.External_PrepareForConnection = new MyUPnPSupport.UPnP.MediaServer.MediaPortalConnectionManagerServer.Delegate_PrepareForConnection(ConnectionManager_PrepareForConnection);
            device.AddService(ConnectionManager);
            MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory ContentDirectory = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory();
            ContentDirectory.External_Browse = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_Browse(ContentDirectory_Browse);
            ContentDirectory.External_CreateObject = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_CreateObject(ContentDirectory_CreateObject);
            ContentDirectory.External_CreateReference = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_CreateReference(ContentDirectory_CreateReference);
            ContentDirectory.External_DeleteResource = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_DeleteResource(ContentDirectory_DeleteResource);
            ContentDirectory.External_DestroyObject = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_DestroyObject(ContentDirectory_DestroyObject);
            ContentDirectory.External_ExportResource = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_ExportResource(ContentDirectory_ExportResource);
            ContentDirectory.External_GetSearchCapabilities = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_GetSearchCapabilities(ContentDirectory_GetSearchCapabilities);
            ContentDirectory.External_GetSortCapabilities = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_GetSortCapabilities(ContentDirectory_GetSortCapabilities);
            ContentDirectory.External_GetSystemUpdateID = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_GetSystemUpdateID(ContentDirectory_GetSystemUpdateID);
            ContentDirectory.External_GetTransferProgress = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_GetTransferProgress(ContentDirectory_GetTransferProgress);
            ContentDirectory.External_ImportResource = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_ImportResource(ContentDirectory_ImportResource);
            ContentDirectory.External_Search = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_Search(ContentDirectory_Search);
            ContentDirectory.External_StopTransferResource = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_StopTransferResource(ContentDirectory_StopTransferResource);
            ContentDirectory.External_UpdateObject = new MyUPnPSupport.UPnP.MediaServer.MediaPortalContentDirectory.Delegate_UpdateObject(ContentDirectory_UpdateObject);
            device.AddService(ContentDirectory);

            // Setting the initial value of evented variables
            ConnectionManager.Evented_SinkProtocolInfo = "Sample String";
            ConnectionManager.Evented_SourceProtocolInfo = "Sample String";
            ConnectionManager.Evented_CurrentConnectionIDs = "Sample String";
            ContentDirectory.Evented_SystemUpdateID = 0;
            ContentDirectory.Evented_TransferIDs = "Sample String";
            ContentDirectory.Evented_ContainerUpdateIDs = "Sample String";
        }

        //public void AddDirectory(string dirPath) {
        //    MediaServer.AddDirectory(new DirectoryInfo(dirPath), false, false);
        //}

        //public void AddDirectories(List<string> directories) {
        //    directories
        //        .ConvertAll(strdir => new DirectoryInfo(strdir))
        //        .FindAll(dirinfo => dirinfo.Exists)
        //        .ForEach(dirinfo => MediaServer.AddDirectory(dirinfo, false, false));
        //}

        public void ConnectionManager_ConnectionComplete(System.Int32 ConnectionID) {
            Console.WriteLine("ConnectionManager_ConnectionComplete(" + ConnectionID.ToString() + ")");
        }

        public void ConnectionManager_GetCurrentConnectionIDs(out System.String ConnectionIDs) {
            ConnectionIDs = "Sample String";
            Console.WriteLine("ConnectionManager_GetCurrentConnectionIDs(" + ")");
        }

        public void ConnectionManager_GetCurrentConnectionInfo(System.Int32 ConnectionID, out System.Int32 RcsID, out System.Int32 AVTransportID, out System.String ProtocolInfo, out System.String PeerConnectionManager, out System.Int32 PeerConnectionID, out MediaPortalConnectionManagerServer.Enum_A_ARG_TYPE_Direction Direction, out MediaPortalConnectionManagerServer.Enum_A_ARG_TYPE_ConnectionStatus Status) {
            RcsID = 0;
            AVTransportID = 0;
            ProtocolInfo = "Sample String";
            PeerConnectionManager = "Sample String";
            PeerConnectionID = 0;
            Direction = MediaPortalConnectionManagerServer.Enum_A_ARG_TYPE_Direction.INPUT;
            Status = MediaPortalConnectionManagerServer.Enum_A_ARG_TYPE_ConnectionStatus.OK;
            Console.WriteLine("ConnectionManager_GetCurrentConnectionInfo(" + ConnectionID.ToString() + ")");
        }

        public void ConnectionManager_GetProtocolInfo(out System.String Source, out System.String Sink) {
            Source = "Sample String";
            Sink = "Sample String";
            Console.WriteLine("ConnectionManager_GetProtocolInfo(" + ")");
        }

        public void ConnectionManager_PrepareForConnection(System.String RemoteProtocolInfo, System.String PeerConnectionManager, System.Int32 PeerConnectionID, MediaPortalConnectionManagerServer.Enum_A_ARG_TYPE_Direction Direction, out System.Int32 ConnectionID, out System.Int32 AVTransportID, out System.Int32 RcsID) {
            ConnectionID = 0;
            AVTransportID = 0;
            RcsID = 0;
            Console.WriteLine("ConnectionManager_PrepareForConnection(" + RemoteProtocolInfo.ToString() + PeerConnectionManager.ToString() + PeerConnectionID.ToString() + Direction.ToString() + ")");
        }

        public void ContentDirectory_Browse(System.String ObjectID, MediaPortalContentDirectory.Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, out System.String Result, out System.UInt32 NumberReturned, out System.UInt32 TotalMatches, out System.UInt32 UpdateID) {
            Result = "Sample String";
            NumberReturned = 0;
            TotalMatches = 0;
            UpdateID = 0;
            Console.WriteLine("ContentDirectory_Browse(" + ObjectID.ToString() + BrowseFlag.ToString() + Filter.ToString() + StartingIndex.ToString() + RequestedCount.ToString() + SortCriteria.ToString() + ")");
        }

        public void ContentDirectory_CreateObject(System.String ContainerID, System.String Elements, out System.String ObjectID, out System.String Result) {
            ObjectID = "Sample String";
            Result = "Sample String";
            Console.WriteLine("ContentDirectory_CreateObject(" + ContainerID.ToString() + Elements.ToString() + ")");
        }

        public void ContentDirectory_CreateReference(System.String ContainerID, System.String ObjectID, out System.String NewID) {
            NewID = "Sample String";
            Console.WriteLine("ContentDirectory_CreateReference(" + ContainerID.ToString() + ObjectID.ToString() + ")");
        }

        public void ContentDirectory_DeleteResource(System.Uri ResourceURI) {
            Console.WriteLine("ContentDirectory_DeleteResource(" + ResourceURI.ToString() + ")");
        }

        public void ContentDirectory_DestroyObject(System.String ObjectID) {
            Console.WriteLine("ContentDirectory_DestroyObject(" + ObjectID.ToString() + ")");
        }

        public void ContentDirectory_ExportResource(System.Uri SourceURI, System.Uri DestinationURI, out System.UInt32 TransferID) {
            TransferID = 0;
            Console.WriteLine("ContentDirectory_ExportResource(" + SourceURI.ToString() + DestinationURI.ToString() + ")");
        }

        public void ContentDirectory_GetSearchCapabilities(out System.String SearchCaps) {
            SearchCaps = "Sample String";
            Console.WriteLine("ContentDirectory_GetSearchCapabilities(" + ")");
        }

        public void ContentDirectory_GetSortCapabilities(out System.String SortCaps) {
            SortCaps = "Sample String";
            Console.WriteLine("ContentDirectory_GetSortCapabilities(" + ")");
        }

        public void ContentDirectory_GetSystemUpdateID(out System.UInt32 Id) {
            Id = 0;
            Console.WriteLine("ContentDirectory_GetSystemUpdateID(" + ")");
        }

        public void ContentDirectory_GetTransferProgress(System.UInt32 TransferID, out MediaPortalContentDirectory.Enum_A_ARG_TYPE_TransferStatus TransferStatus, out System.String TransferLength, out System.String TransferTotal) {
            TransferStatus = MediaPortalContentDirectory.Enum_A_ARG_TYPE_TransferStatus.COMPLETED;
            TransferLength = "Sample String";
            TransferTotal = "Sample String";
            Console.WriteLine("ContentDirectory_GetTransferProgress(" + TransferID.ToString() + ")");
        }

        public void ContentDirectory_ImportResource(System.Uri SourceURI, System.Uri DestinationURI, out System.UInt32 TransferID) {
            TransferID = 0;
            Console.WriteLine("ContentDirectory_ImportResource(" + SourceURI.ToString() + DestinationURI.ToString() + ")");
        }

        public void ContentDirectory_Search(System.String ContainerID, System.String SearchCriteria, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, out System.String Result, out System.UInt32 NumberReturned, out System.UInt32 TotalMatches, out System.UInt32 UpdateID) {
            Result = "Sample String";
            NumberReturned = 0;
            TotalMatches = 0;
            UpdateID = 0;
            Console.WriteLine("ContentDirectory_Search(" + ContainerID.ToString() + SearchCriteria.ToString() + Filter.ToString() + StartingIndex.ToString() + RequestedCount.ToString() + SortCriteria.ToString() + ")");
        }

        public void ContentDirectory_StopTransferResource(System.UInt32 TransferID) {
            Console.WriteLine("ContentDirectory_StopTransferResource(" + TransferID.ToString() + ")");
        }

        public void ContentDirectory_UpdateObject(System.String ObjectID, System.String CurrentTagValue, System.String NewTagValue) {
            Console.WriteLine("ContentDirectory_UpdateObject(" + ObjectID.ToString() + CurrentTagValue.ToString() + NewTagValue.ToString() + ")");
        }


        #region IUPnPDevice Members

        public UPnPDevice GetUPnPDevice() {
            return device;
        }

        #endregion
    }
}

