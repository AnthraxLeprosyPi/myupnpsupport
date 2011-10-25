// UPnP .NET Framework Device Stack, Device Module
// Device Builder Build#1.0.4144.25068

using System;
using OpenSource.UPnP;
using MyUPnPSupport.UPnP.Services;
using MediaPortal.Playlists;
using MediaPortal.GUI.Library;
using MyUPnPSupport.Properties;
using System.Drawing;

namespace MyUPnPSupport.UPnP.Devices {
    /// <summary>
    /// Summary description for SampleDevice.
    /// </summary>
    public class DigitalMediaRenderer : IUPnPDevice {
        private UPnPDevice device;
        protected AVTransport AVTransport;
        protected ConnectionManagerRenderer ConnectionManager;
        protected RenderingControl RenderingControl;

        public delegate void Delegate_ConnectionComplete(System.Int32 ConnectionID);
        public delegate void Delegate_GetCurrentConnectionIDs(out System.String ConnectionIDs);
        public delegate void Delegate_GetCurrentConnectionInfo(System.Int32 ConnectionID, out System.Int32 RcsID, out System.Int32 AVTransportID, out System.String ProtocolInfo, out System.String PeerConnectionManager, out System.Int32 PeerConnectionID, out ConnectionManagerServer.Enum_A_ARG_TYPE_Direction Direction, out ConnectionManagerServer.Enum_A_ARG_TYPE_ConnectionStatus Status);
        public delegate void Delegate_GetProtocolInfo(out System.String Source, out System.String Sink);
        public delegate void Delegate_PrepareForConnection(System.String RemoteProtocolInfo, System.String PeerConnectionManager, System.Int32 PeerConnectionID, ConnectionManagerServer.Enum_A_ARG_TYPE_Direction Direction, out System.Int32 ConnectionID, out System.Int32 AVTransportID, out System.Int32 RcsID);

        public Delegate_ConnectionComplete External_ConnectionComplete = null;
        public Delegate_GetCurrentConnectionIDs External_GetCurrentConnectionIDs = null;
        public Delegate_GetCurrentConnectionInfo External_GetCurrentConnectionInfo = null;
        public Delegate_GetProtocolInfo External_GetProtocolInfo = null;
        public Delegate_PrepareForConnection External_PrepareForConnection = null;



        /// <summary>
        /// Initializes a new custom instance of the <see cref="DigitalMediaRenderer"/> class.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="modelDescription">The model description.</param>
        /// <param name="deviceId">The device id.</param>
        /// <param name="deviceUri">The device URI.</param>
        public DigitalMediaRenderer(string friendlyName, string manufacturer, string modelName, string modelDescription, Guid deviceId, Uri deviceUri, Uri manufacturerUri, Image iconLarge, Image iconSmall)
            : this() {
            device.FriendlyName = friendlyName;
            device.UniqueDeviceName = deviceId.ToString();
            device.SerialNumber = deviceId.ToString();
            device.Manufacturer = manufacturer;
            device.ManufacturerURL = manufacturerUri.ToString();
            device.ModelName = modelName;
            device.ModelDescription = modelDescription;
            device.ModelURL = deviceUri;
            device.Icon = iconLarge;
            device.Icon2 = iconSmall;
        }


        public DigitalMediaRenderer() {
            device = UPnPDevice.CreateRootDevice(1800, 1.0, "\\");

            device.FriendlyName = "Intel's Embedded UPnP Renderer";
            device.Manufacturer = "Intel's Connected and Extended PC Lab";
            device.ManufacturerURL = "http://www.intel.com";
            device.ModelName = "Sample Auto-Generated Device";
            device.ModelDescription = "Intel's UPnP/AV Media Renderer Device";
            device.ModelNumber = "X1";
            device.HasPresentation = false;
            device.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1";
            AVTransport = new MyUPnPSupport.UPnP.Services.AVTransport();
            AVTransport.External_GetCurrentTransportActions = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_GetCurrentTransportActions(AVTransport_GetCurrentTransportActions);
            AVTransport.External_GetDeviceCapabilities = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_GetDeviceCapabilities(AVTransport_GetDeviceCapabilities);
            AVTransport.External_GetMediaInfo = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_GetMediaInfo(AVTransport_GetMediaInfo);
            AVTransport.External_GetPositionInfo = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_GetPositionInfo(AVTransport_GetPositionInfo);
            AVTransport.External_GetTransportInfo = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_GetTransportInfo(AVTransport_GetTransportInfo);
            AVTransport.External_GetTransportSettings = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_GetTransportSettings(AVTransport_GetTransportSettings);
            AVTransport.External_Next = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_Next(AVTransport_Next);
            AVTransport.External_Pause = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_Pause(AVTransport_Pause);
            AVTransport.External_Play = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_Play(AVTransport_Play);
            AVTransport.External_Previous = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_Previous(AVTransport_Previous);
            AVTransport.External_Seek = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_Seek(AVTransport_Seek);
            AVTransport.External_SetAVTransportURI = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_SetAVTransportURI(AVTransport_SetAVTransportURI);
            AVTransport.External_SetPlayMode = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_SetPlayMode(AVTransport_SetPlayMode);
            AVTransport.External_Stop = new MyUPnPSupport.UPnP.Services.AVTransport.Delegate_Stop(AVTransport_Stop);
            device.AddService(AVTransport);
            ConnectionManager = new MyUPnPSupport.UPnP.Services.ConnectionManagerRenderer();
            ConnectionManager.External_GetCurrentConnectionIDs = new MyUPnPSupport.UPnP.Services.ConnectionManagerRenderer.Delegate_GetCurrentConnectionIDs(ConnectionManager_GetCurrentConnectionIDs);
            ConnectionManager.External_GetCurrentConnectionInfo = new MyUPnPSupport.UPnP.Services.ConnectionManagerRenderer.Delegate_GetCurrentConnectionInfo(ConnectionManager_GetCurrentConnectionInfo);
            ConnectionManager.External_GetProtocolInfo = new MyUPnPSupport.UPnP.Services.ConnectionManagerRenderer.Delegate_GetProtocolInfo(ConnectionManager_GetProtocolInfo);
            device.AddService(ConnectionManager);
            RenderingControl = new MyUPnPSupport.UPnP.Services.RenderingControl();
            RenderingControl.External_GetBlueVideoBlackLevel = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetBlueVideoBlackLevel(RenderingControl_GetBlueVideoBlackLevel);
            RenderingControl.External_GetBlueVideoGain = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetBlueVideoGain(RenderingControl_GetBlueVideoGain);
            RenderingControl.External_GetBrightness = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetBrightness(RenderingControl_GetBrightness);
            RenderingControl.External_GetColorTemperature = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetColorTemperature(RenderingControl_GetColorTemperature);
            RenderingControl.External_GetContrast = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetContrast(RenderingControl_GetContrast);
            RenderingControl.External_GetGreenVideoBlackLevel = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetGreenVideoBlackLevel(RenderingControl_GetGreenVideoBlackLevel);
            RenderingControl.External_GetGreenVideoGain = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetGreenVideoGain(RenderingControl_GetGreenVideoGain);
            RenderingControl.External_GetHorizontalKeystone = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetHorizontalKeystone(RenderingControl_GetHorizontalKeystone);
            RenderingControl.External_GetLoudness = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetLoudness(RenderingControl_GetLoudness);
            RenderingControl.External_GetMute = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetMute(RenderingControl_GetMute);
            RenderingControl.External_GetRedVideoBlackLevel = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetRedVideoBlackLevel(RenderingControl_GetRedVideoBlackLevel);
            RenderingControl.External_GetRedVideoGain = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetRedVideoGain(RenderingControl_GetRedVideoGain);
            RenderingControl.External_GetSharpness = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetSharpness(RenderingControl_GetSharpness);
            RenderingControl.External_GetVerticalKeystone = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetVerticalKeystone(RenderingControl_GetVerticalKeystone);
            RenderingControl.External_GetVolume = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetVolume(RenderingControl_GetVolume);
            RenderingControl.External_GetVolumeDB = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetVolumeDB(RenderingControl_GetVolumeDB);
            RenderingControl.External_GetVolumeDBRange = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_GetVolumeDBRange(RenderingControl_GetVolumeDBRange);
            RenderingControl.External_ListPresets = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_ListPresets(RenderingControl_ListPresets);
            RenderingControl.External_SelectPreset = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SelectPreset(RenderingControl_SelectPreset);
            RenderingControl.External_SetBlueVideoBlackLevel = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetBlueVideoBlackLevel(RenderingControl_SetBlueVideoBlackLevel);
            RenderingControl.External_SetBlueVideoGain = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetBlueVideoGain(RenderingControl_SetBlueVideoGain);
            RenderingControl.External_SetBrightness = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetBrightness(RenderingControl_SetBrightness);
            RenderingControl.External_SetColorTemperature = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetColorTemperature(RenderingControl_SetColorTemperature);
            RenderingControl.External_SetContrast = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetContrast(RenderingControl_SetContrast);
            RenderingControl.External_SetGreenVideoBlackLevel = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetGreenVideoBlackLevel(RenderingControl_SetGreenVideoBlackLevel);
            RenderingControl.External_SetGreenVideoGain = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetGreenVideoGain(RenderingControl_SetGreenVideoGain);
            RenderingControl.External_SetHorizontalKeystone = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetHorizontalKeystone(RenderingControl_SetHorizontalKeystone);
            RenderingControl.External_SetLoudness = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetLoudness(RenderingControl_SetLoudness);
            RenderingControl.External_SetMute = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetMute(RenderingControl_SetMute);
            RenderingControl.External_SetRedVideoBlackLevel = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetRedVideoBlackLevel(RenderingControl_SetRedVideoBlackLevel);
            RenderingControl.External_SetRedVideoGain = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetRedVideoGain(RenderingControl_SetRedVideoGain);
            RenderingControl.External_SetSharpness = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetSharpness(RenderingControl_SetSharpness);
            RenderingControl.External_SetVerticalKeystone = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetVerticalKeystone(RenderingControl_SetVerticalKeystone);
            RenderingControl.External_SetVolume = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetVolume(RenderingControl_SetVolume);
            RenderingControl.External_SetVolumeDB = new MyUPnPSupport.UPnP.Services.RenderingControl.Delegate_SetVolumeDB(RenderingControl_SetVolumeDB);
            device.AddService(RenderingControl);

            // Setting the initial value of evented variables
            AVTransport.Evented_LastChange = "Sample String";
            ConnectionManager.Evented_SinkProtocolInfo = "http-get:*:audio/mpegurl:*,http-get:*:audio/mp3:*,http-get:*:audio/mpeg:*,http-get:*:audio/x-ms-wma:*,http-get:*:audio/wma:*,http-get:*:audio/mpeg3:*,http-get:*:video/x-ms-wmv:*,http-get:*:video/x-ms-asf:*,http-get:*:video/x-ms-avi:*,http-get:*:video/mpeg:*";
            ConnectionManager.Evented_SourceProtocolInfo = "Sample String";
            ConnectionManager.Evented_CurrentConnectionIDs = "Sample String";
            RenderingControl.Evented_LastChange = "Sample String";
        }

        public void Start() {
            device.StartDevice();
        }

        public void Stop() {
            device.StopDevice();
        }

        public virtual void AVTransport_GetCurrentTransportActions(System.UInt32 InstanceID, out System.String Actions) {
            Actions = "";
            Console.WriteLine("AVTransport_GetCurrentTransportActions(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_GetDeviceCapabilities(System.UInt32 InstanceID, out System.String PlayMedia, out System.String RecMedia, out System.String RecQualityModes) {
            PlayMedia = "";
            RecMedia = "";
            RecQualityModes = "";
            Console.WriteLine("AVTransport_GetDeviceCapabilities(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_GetMediaInfo(System.UInt32 InstanceID, out System.UInt32 NrTracks, out System.String MediaDuration, out System.String CurrentURI, out System.String CurrentURIMetaData, out System.String NextURI, out System.String NextURIMetaData, out AVTransport.Enum_PlaybackStorageMedium PlayMedium, out AVTransport.Enum_RecordStorageMedium RecordMedium, out AVTransport.Enum_RecordMediumWriteStatus WriteStatus) {
            NrTracks = 0;
            MediaDuration = "";
            CurrentURI = "";
            CurrentURIMetaData = "";
            NextURI = "";
            NextURIMetaData = "";
            PlayMedium = AVTransport.Enum_PlaybackStorageMedium.UNKNOWN;
            RecordMedium = AVTransport.Enum_RecordStorageMedium.NOT_IMPLEMENTED;
            WriteStatus = AVTransport.Enum_RecordMediumWriteStatus.NOT_IMPLEMENTED;
            Console.WriteLine("AVTransport_GetMediaInfo(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_GetPositionInfo(System.UInt32 InstanceID, out System.UInt32 Track, out System.String TrackDuration, out System.String TrackMetaData, out System.String TrackURI, out System.String RelTime, out System.String AbsTime, out System.Int32 RelCount, out System.Int32 AbsCount) {
            Track = 0;
            TrackDuration = "";
            TrackMetaData = "";
            TrackURI = "";
            RelTime = "";
            AbsTime = "";
            RelCount = 0;
            AbsCount = 0;
            Console.WriteLine("AVTransport_GetPositionInfo(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_GetTransportInfo(System.UInt32 InstanceID, out AVTransport.Enum_TransportState CurrentTransportState, out AVTransport.Enum_TransportStatus CurrentTransportStatus, out AVTransport.Enum_TransportPlaySpeed CurrentSpeed) {
            CurrentTransportState = AVTransport.Enum_TransportState.STOPPED;
            CurrentTransportStatus = AVTransport.Enum_TransportStatus.OK;
            CurrentSpeed = AVTransport.Enum_TransportPlaySpeed._1;
            Console.WriteLine("AVTransport_GetTransportInfo(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_GetTransportSettings(System.UInt32 InstanceID, out AVTransport.Enum_CurrentPlayMode PlayMode, out AVTransport.Enum_CurrentRecordQualityMode RecQualityMode) {
            PlayMode = AVTransport.Enum_CurrentPlayMode.NORMAL;
            RecQualityMode = AVTransport.Enum_CurrentRecordQualityMode.NOT_IMPLEMENTED;
            Console.WriteLine("AVTransport_GetTransportSettings(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_Next(System.UInt32 InstanceID) {
            Console.WriteLine("AVTransport_Next(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_Pause(System.UInt32 InstanceID) {
            Console.WriteLine("AVTransport_Pause(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_Play(System.UInt32 InstanceID, AVTransport.Enum_TransportPlaySpeed Speed) {
            Console.WriteLine("AVTransport_Play(" + InstanceID.ToString() + Speed.ToString() + ")");
        }

        public virtual void AVTransport_Previous(System.UInt32 InstanceID) {
            Console.WriteLine("AVTransport_Previous(" + InstanceID.ToString() + ")");
        }

        public virtual void AVTransport_Seek(System.UInt32 InstanceID, AVTransport.Enum_A_ARG_TYPE_SeekMode Unit, System.String Target) {
            Console.WriteLine("AVTransport_Seek(" + InstanceID.ToString() + Unit.ToString() + Target.ToString() + ")");
        }

        public virtual void AVTransport_SetAVTransportURI(System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData) {
            Console.WriteLine("AVTransport_SetAVTransportURI(" + InstanceID.ToString() + CurrentURI.ToString() + CurrentURIMetaData.ToString() + ")");
        }

        public virtual void AVTransport_SetPlayMode(System.UInt32 InstanceID, AVTransport.Enum_CurrentPlayMode NewPlayMode) {
            Console.WriteLine("AVTransport_SetPlayMode(" + InstanceID.ToString() + NewPlayMode.ToString() + ")");
        }

        public virtual void AVTransport_Stop(System.UInt32 InstanceID) {
            Console.WriteLine("AVTransport_Stop(" + InstanceID.ToString() + ")");
        }

        public virtual void ConnectionManager_GetCurrentConnectionIDs(out System.String ConnectionIDs) {
            ConnectionIDs = "";
            Console.WriteLine("ConnectionManager_GetCurrentConnectionIDs(" + ")");
        }

        public virtual void ConnectionManager_GetCurrentConnectionInfo(System.Int32 ConnectionID, out System.Int32 RcsID, out System.Int32 AVTransportID, out System.String ProtocolInfo, out System.String PeerConnectionManager, out System.Int32 PeerConnectionID, out ConnectionManagerRenderer.Enum_A_ARG_TYPE_Direction Direction, out ConnectionManagerRenderer.Enum_A_ARG_TYPE_ConnectionStatus Status) {
            RcsID = 0;
            AVTransportID = 0;
            ProtocolInfo = "Sample String";
            PeerConnectionManager = "Sample String";
            PeerConnectionID = 0;
            Direction = ConnectionManagerRenderer.Enum_A_ARG_TYPE_Direction.INPUT;
            Status = ConnectionManagerRenderer.Enum_A_ARG_TYPE_ConnectionStatus.OK;
            Console.WriteLine("ConnectionManager_GetCurrentConnectionInfo(" + ConnectionID.ToString() + ")");
        }

        public virtual void ConnectionManager_GetProtocolInfo(out System.String Source, out System.String Sink) {
            Source = "";
            Sink = "http-get:*:audio/mpegurl:*,http-get:*:audio/mp3:*,http-get:*:audio/mpeg:*,http-get:*:audio/x-ms-wma:*,http-get:*:audio/wma:*,http-get:*:audio/mpeg3:*,http-get:*:video/x-ms-wmv:*,http-get:*:video/x-ms-asf:*,http-get:*:video/x-ms-avi:*,http-get:*:video/mpeg:*";
            Console.WriteLine("ConnectionManager_GetProtocolInfo(" + ")");
        }

        public virtual void RenderingControl_GetBlueVideoBlackLevel(System.UInt32 InstanceID, out System.UInt16 CurrentBlueVideoBlackLevel) {
            CurrentBlueVideoBlackLevel = 0;
            Console.WriteLine("RenderingControl_GetBlueVideoBlackLevel(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetBlueVideoGain(System.UInt32 InstanceID, out System.UInt16 CurrentBlueVideoGain) {
            CurrentBlueVideoGain = 0;
            Console.WriteLine("RenderingControl_GetBlueVideoGain(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetBrightness(System.UInt32 InstanceID, out System.UInt16 CurrentBrightness) {
            CurrentBrightness = 0;
            Console.WriteLine("RenderingControl_GetBrightness(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetColorTemperature(System.UInt32 InstanceID, out System.UInt16 CurrentColorTemperature) {
            CurrentColorTemperature = 0;
            Console.WriteLine("RenderingControl_GetColorTemperature(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetContrast(System.UInt32 InstanceID, out System.UInt16 CurrentContrast) {
            CurrentContrast = 0;
            Console.WriteLine("RenderingControl_GetContrast(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetGreenVideoBlackLevel(System.UInt32 InstanceID, out System.UInt16 CurrentGreenVideoBlackLevel) {
            CurrentGreenVideoBlackLevel = 0;
            Console.WriteLine("RenderingControl_GetGreenVideoBlackLevel(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetGreenVideoGain(System.UInt32 InstanceID, out System.UInt16 CurrentGreenVideoGain) {
            CurrentGreenVideoGain = 0;
            Console.WriteLine("RenderingControl_GetGreenVideoGain(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetHorizontalKeystone(System.UInt32 InstanceID, out System.Int16 CurrentHorizontalKeystone) {
            CurrentHorizontalKeystone = 0;
            Console.WriteLine("RenderingControl_GetHorizontalKeystone(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetLoudness(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out System.Boolean CurrentLoudness) {
            CurrentLoudness = false;
            Console.WriteLine("RenderingControl_GetLoudness(" + InstanceID.ToString() + Channel.ToString() + ")");
        }

        public virtual void RenderingControl_GetMute(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out System.Boolean CurrentMute) {
            CurrentMute = false;
            Console.WriteLine("RenderingControl_GetMute(" + InstanceID.ToString() + Channel.ToString() + ")");
        }

        public virtual void RenderingControl_GetRedVideoBlackLevel(System.UInt32 InstanceID, out System.UInt16 CurrentRedVideoBlackLevel) {
            CurrentRedVideoBlackLevel = 0;
            Console.WriteLine("RenderingControl_GetRedVideoBlackLevel(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetRedVideoGain(System.UInt32 InstanceID, out System.UInt16 CurrentRedVideoGain) {
            CurrentRedVideoGain = 0;
            Console.WriteLine("RenderingControl_GetRedVideoGain(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetSharpness(System.UInt32 InstanceID, out System.UInt16 CurrentSharpness) {
            CurrentSharpness = 0;
            Console.WriteLine("RenderingControl_GetSharpness(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetVerticalKeystone(System.UInt32 InstanceID, out System.Int16 CurrentVerticalKeystone) {
            CurrentVerticalKeystone = 0;
            Console.WriteLine("RenderingControl_GetVerticalKeystone(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_GetVolume(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out System.UInt16 CurrentVolume) {
            CurrentVolume = 50;
            Console.WriteLine("RenderingControl_GetVolume(" + InstanceID.ToString() + Channel.ToString() + ")");
        }

        public virtual void RenderingControl_GetVolumeDB(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out System.Int16 CurrentVolume) {
            CurrentVolume = 50;
            Console.WriteLine("RenderingControl_GetVolumeDB(" + InstanceID.ToString() + Channel.ToString() + ")");
        }

        public virtual void RenderingControl_GetVolumeDBRange(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out System.Int16 MinValue, out System.Int16 MaxValue) {
            MinValue = 0;
            MaxValue = 100;
            Console.WriteLine("RenderingControl_GetVolumeDBRange(" + InstanceID.ToString() + Channel.ToString() + ")");
        }

        public virtual void RenderingControl_ListPresets(System.UInt32 InstanceID, out System.String CurrentPresetNameList) {
            CurrentPresetNameList = "Sample String";
            Console.WriteLine("RenderingControl_ListPresets(" + InstanceID.ToString() + ")");
        }

        public virtual void RenderingControl_SelectPreset(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_PresetName PresetName) {
            Console.WriteLine("RenderingControl_SelectPreset(" + InstanceID.ToString() + PresetName.ToString() + ")");
        }

        public virtual void RenderingControl_SetBlueVideoBlackLevel(System.UInt32 InstanceID, System.UInt16 DesiredBlueVideoBlackLevel) {
            Console.WriteLine("RenderingControl_SetBlueVideoBlackLevel(" + InstanceID.ToString() + DesiredBlueVideoBlackLevel.ToString() + ")");
        }

        public virtual void RenderingControl_SetBlueVideoGain(System.UInt32 InstanceID, System.UInt16 DesiredBlueVideoGain) {
            Console.WriteLine("RenderingControl_SetBlueVideoGain(" + InstanceID.ToString() + DesiredBlueVideoGain.ToString() + ")");
        }

        public virtual void RenderingControl_SetBrightness(System.UInt32 InstanceID, System.UInt16 DesiredBrightness) {
            Console.WriteLine("RenderingControl_SetBrightness(" + InstanceID.ToString() + DesiredBrightness.ToString() + ")");
        }

        public virtual void RenderingControl_SetColorTemperature(System.UInt32 InstanceID, System.UInt16 DesiredColorTemperature) {
            Console.WriteLine("RenderingControl_SetColorTemperature(" + InstanceID.ToString() + DesiredColorTemperature.ToString() + ")");
        }

        public virtual void RenderingControl_SetContrast(System.UInt32 InstanceID, System.UInt16 DesiredContrast) {
            Console.WriteLine("RenderingControl_SetContrast(" + InstanceID.ToString() + DesiredContrast.ToString() + ")");
        }

        public virtual void RenderingControl_SetGreenVideoBlackLevel(System.UInt32 InstanceID, System.UInt16 DesiredGreenVideoBlackLevel) {
            Console.WriteLine("RenderingControl_SetGreenVideoBlackLevel(" + InstanceID.ToString() + DesiredGreenVideoBlackLevel.ToString() + ")");
        }

        public virtual void RenderingControl_SetGreenVideoGain(System.UInt32 InstanceID, System.UInt16 DesiredGreenVideoGain) {
            Console.WriteLine("RenderingControl_SetGreenVideoGain(" + InstanceID.ToString() + DesiredGreenVideoGain.ToString() + ")");
        }

        public virtual void RenderingControl_SetHorizontalKeystone(System.UInt32 InstanceID, System.Int16 DesiredHorizontalKeystone) {
            Console.WriteLine("RenderingControl_SetHorizontalKeystone(" + InstanceID.ToString() + DesiredHorizontalKeystone.ToString() + ")");
        }

        public virtual void RenderingControl_SetLoudness(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, System.Boolean DesiredLoudness) {
            Console.WriteLine("RenderingControl_SetLoudness(" + InstanceID.ToString() + Channel.ToString() + DesiredLoudness.ToString() + ")");
        }

        public virtual void RenderingControl_SetMute(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, System.Boolean DesiredMute) {
            Console.WriteLine("RenderingControl_SetMute(" + InstanceID.ToString() + Channel.ToString() + DesiredMute.ToString() + ")");
        }

        public virtual void RenderingControl_SetRedVideoBlackLevel(System.UInt32 InstanceID, System.UInt16 DesiredRedVideoBlackLevel) {
            Console.WriteLine("RenderingControl_SetRedVideoBlackLevel(" + InstanceID.ToString() + DesiredRedVideoBlackLevel.ToString() + ")");
        }

        public virtual void RenderingControl_SetRedVideoGain(System.UInt32 InstanceID, System.UInt16 DesiredRedVideoGain) {
            Console.WriteLine("RenderingControl_SetRedVideoGain(" + InstanceID.ToString() + DesiredRedVideoGain.ToString() + ")");
        }

        public virtual void RenderingControl_SetSharpness(System.UInt32 InstanceID, System.UInt16 DesiredSharpness) {
            Console.WriteLine("RenderingControl_SetSharpness(" + InstanceID.ToString() + DesiredSharpness.ToString() + ")");
        }

        public virtual void RenderingControl_SetVerticalKeystone(System.UInt32 InstanceID, System.Int16 DesiredVerticalKeystone) {
            Console.WriteLine("RenderingControl_SetVerticalKeystone(" + InstanceID.ToString() + DesiredVerticalKeystone.ToString() + ")");
        }

        public virtual void RenderingControl_SetVolume(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, System.UInt16 DesiredVolume) {
            Console.WriteLine("RenderingControl_SetVolume(" + InstanceID.ToString() + Channel.ToString() + DesiredVolume.ToString() + ")");
        }

        public virtual void RenderingControl_SetVolumeDB(System.UInt32 InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, System.Int16 DesiredVolume) {
            Console.WriteLine("RenderingControl_SetVolumeDB(" + InstanceID.ToString() + Channel.ToString() + DesiredVolume.ToString() + ")");
        }



        #region IUPnPDevice Members

        public UPnPDevice GetUPnPDevice() {
            return device;
        }

        #endregion
    }
}

