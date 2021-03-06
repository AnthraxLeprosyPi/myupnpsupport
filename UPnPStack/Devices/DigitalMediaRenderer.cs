// UPnP .NET Framework Device Stack, Device Module
// Device Builder Build#1.0.4144.25068

using System;
using OpenSource.UPnP;
using UPnPStack.Services;
using System.Drawing;

namespace UPnPStack.Devices {
    /// <summary>
    /// Summary description for SampleDevice.
    /// </summary>
    public class DigitalMediaRenderer : IUPnPDevice {
        private UPnPDevice device;
        protected AVTransport _aVTransport;
        protected ConnectionManagerRenderer _connectionManager;
        protected RenderingControl _renderingControl;

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
            device.AddCustomFieldInDescription("dlna:X_DLNADOC", "DMS-1.0", "urn:schemas-dlna-org:device-1-0");

            device.FriendlyName = "Intel's Embedded UPnP Renderer";
            device.Manufacturer = "Intel's Connected and Extended PC Lab";
            device.ManufacturerURL = "http://www.intel.com";
            device.ModelName = "Sample Auto-Generated Device";
            device.ModelDescription = "Intel's UPnP/AV Media Renderer Device";
            device.ModelNumber = "X1";
            device.HasPresentation = false;
            device.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1";
            _aVTransport = new UPnPStack.Services.AVTransport();
            _aVTransport.External_GetCurrentTransportActions = new UPnPStack.Services.AVTransport.Delegate_GetCurrentTransportActions(AVTransport_GetCurrentTransportActions);
            _aVTransport.External_GetDeviceCapabilities = new UPnPStack.Services.AVTransport.Delegate_GetDeviceCapabilities(AVTransport_GetDeviceCapabilities);
            _aVTransport.External_GetMediaInfo = new UPnPStack.Services.AVTransport.Delegate_GetMediaInfo(AVTransport_GetMediaInfo);
            _aVTransport.External_GetPositionInfo = new UPnPStack.Services.AVTransport.Delegate_GetPositionInfo(AVTransport_GetPositionInfo);
            _aVTransport.External_GetTransportInfo = new UPnPStack.Services.AVTransport.Delegate_GetTransportInfo(AVTransport_GetTransportInfo);
            _aVTransport.External_GetTransportSettings = new UPnPStack.Services.AVTransport.Delegate_GetTransportSettings(AVTransport_GetTransportSettings);
            _aVTransport.External_Next = new UPnPStack.Services.AVTransport.Delegate_Next(AVTransport_Next);
            _aVTransport.External_Pause = new UPnPStack.Services.AVTransport.Delegate_Pause(AVTransport_Pause);
            _aVTransport.External_Play = new UPnPStack.Services.AVTransport.Delegate_Play(AVTransport_Play);
            _aVTransport.External_Previous = new UPnPStack.Services.AVTransport.Delegate_Previous(AVTransport_Previous);
            _aVTransport.External_Seek = new UPnPStack.Services.AVTransport.Delegate_Seek(AVTransport_Seek);
            _aVTransport.External_SetAVTransportURI = new UPnPStack.Services.AVTransport.Delegate_SetAVTransportURI(AVTransport_SetAVTransportURI);
            _aVTransport.External_SetPlayMode = new UPnPStack.Services.AVTransport.Delegate_SetPlayMode(AVTransport_SetPlayMode);
            _aVTransport.External_Stop = new UPnPStack.Services.AVTransport.Delegate_Stop(AVTransport_Stop);
            device.AddService(_aVTransport);
            _connectionManager = new UPnPStack.Services.ConnectionManagerRenderer();
            _connectionManager.External_GetCurrentConnectionIDs = new UPnPStack.Services.ConnectionManagerRenderer.Delegate_GetCurrentConnectionIDs(ConnectionManager_GetCurrentConnectionIDs);
            _connectionManager.External_GetCurrentConnectionInfo = new UPnPStack.Services.ConnectionManagerRenderer.Delegate_GetCurrentConnectionInfo(ConnectionManager_GetCurrentConnectionInfo);
            _connectionManager.External_GetProtocolInfo = new UPnPStack.Services.ConnectionManagerRenderer.Delegate_GetProtocolInfo(ConnectionManager_GetProtocolInfo);            
            device.AddService(_connectionManager);
            _renderingControl = new UPnPStack.Services.RenderingControl();
            _renderingControl.External_GetBlueVideoBlackLevel = new UPnPStack.Services.RenderingControl.Delegate_GetBlueVideoBlackLevel(RenderingControl_GetBlueVideoBlackLevel);
            _renderingControl.External_GetBlueVideoGain = new UPnPStack.Services.RenderingControl.Delegate_GetBlueVideoGain(RenderingControl_GetBlueVideoGain);
            _renderingControl.External_GetBrightness = new UPnPStack.Services.RenderingControl.Delegate_GetBrightness(RenderingControl_GetBrightness);
            _renderingControl.External_GetColorTemperature = new UPnPStack.Services.RenderingControl.Delegate_GetColorTemperature(RenderingControl_GetColorTemperature);
            _renderingControl.External_GetContrast = new UPnPStack.Services.RenderingControl.Delegate_GetContrast(RenderingControl_GetContrast);
            _renderingControl.External_GetGreenVideoBlackLevel = new UPnPStack.Services.RenderingControl.Delegate_GetGreenVideoBlackLevel(RenderingControl_GetGreenVideoBlackLevel);
            _renderingControl.External_GetGreenVideoGain = new UPnPStack.Services.RenderingControl.Delegate_GetGreenVideoGain(RenderingControl_GetGreenVideoGain);
            _renderingControl.External_GetHorizontalKeystone = new UPnPStack.Services.RenderingControl.Delegate_GetHorizontalKeystone(RenderingControl_GetHorizontalKeystone);
            _renderingControl.External_GetLoudness = new UPnPStack.Services.RenderingControl.Delegate_GetLoudness(RenderingControl_GetLoudness);
            _renderingControl.External_GetMute = new UPnPStack.Services.RenderingControl.Delegate_GetMute(RenderingControl_GetMute);
            _renderingControl.External_GetRedVideoBlackLevel = new UPnPStack.Services.RenderingControl.Delegate_GetRedVideoBlackLevel(RenderingControl_GetRedVideoBlackLevel);
            _renderingControl.External_GetRedVideoGain = new UPnPStack.Services.RenderingControl.Delegate_GetRedVideoGain(RenderingControl_GetRedVideoGain);
            _renderingControl.External_GetSharpness = new UPnPStack.Services.RenderingControl.Delegate_GetSharpness(RenderingControl_GetSharpness);
            _renderingControl.External_GetVerticalKeystone = new UPnPStack.Services.RenderingControl.Delegate_GetVerticalKeystone(RenderingControl_GetVerticalKeystone);
            _renderingControl.External_GetVolume = new UPnPStack.Services.RenderingControl.Delegate_GetVolume(RenderingControl_GetVolume);
            _renderingControl.External_GetVolumeDB = new UPnPStack.Services.RenderingControl.Delegate_GetVolumeDB(RenderingControl_GetVolumeDB);
            _renderingControl.External_GetVolumeDBRange = new UPnPStack.Services.RenderingControl.Delegate_GetVolumeDBRange(RenderingControl_GetVolumeDBRange);
            _renderingControl.External_ListPresets = new UPnPStack.Services.RenderingControl.Delegate_ListPresets(RenderingControl_ListPresets);
            _renderingControl.External_SelectPreset = new UPnPStack.Services.RenderingControl.Delegate_SelectPreset(RenderingControl_SelectPreset);
            _renderingControl.External_SetBlueVideoBlackLevel = new UPnPStack.Services.RenderingControl.Delegate_SetBlueVideoBlackLevel(RenderingControl_SetBlueVideoBlackLevel);
            _renderingControl.External_SetBlueVideoGain = new UPnPStack.Services.RenderingControl.Delegate_SetBlueVideoGain(RenderingControl_SetBlueVideoGain);
            _renderingControl.External_SetBrightness = new UPnPStack.Services.RenderingControl.Delegate_SetBrightness(RenderingControl_SetBrightness);
            _renderingControl.External_SetColorTemperature = new UPnPStack.Services.RenderingControl.Delegate_SetColorTemperature(RenderingControl_SetColorTemperature);
            _renderingControl.External_SetContrast = new UPnPStack.Services.RenderingControl.Delegate_SetContrast(RenderingControl_SetContrast);
            _renderingControl.External_SetGreenVideoBlackLevel = new UPnPStack.Services.RenderingControl.Delegate_SetGreenVideoBlackLevel(RenderingControl_SetGreenVideoBlackLevel);
            _renderingControl.External_SetGreenVideoGain = new UPnPStack.Services.RenderingControl.Delegate_SetGreenVideoGain(RenderingControl_SetGreenVideoGain);
            _renderingControl.External_SetHorizontalKeystone = new UPnPStack.Services.RenderingControl.Delegate_SetHorizontalKeystone(RenderingControl_SetHorizontalKeystone);
            _renderingControl.External_SetLoudness = new UPnPStack.Services.RenderingControl.Delegate_SetLoudness(RenderingControl_SetLoudness);
            _renderingControl.External_SetMute = new UPnPStack.Services.RenderingControl.Delegate_SetMute(RenderingControl_SetMute);
            _renderingControl.External_SetRedVideoBlackLevel = new UPnPStack.Services.RenderingControl.Delegate_SetRedVideoBlackLevel(RenderingControl_SetRedVideoBlackLevel);
            _renderingControl.External_SetRedVideoGain = new UPnPStack.Services.RenderingControl.Delegate_SetRedVideoGain(RenderingControl_SetRedVideoGain);
            _renderingControl.External_SetSharpness = new UPnPStack.Services.RenderingControl.Delegate_SetSharpness(RenderingControl_SetSharpness);
            _renderingControl.External_SetVerticalKeystone = new UPnPStack.Services.RenderingControl.Delegate_SetVerticalKeystone(RenderingControl_SetVerticalKeystone);
            _renderingControl.External_SetVolume = new UPnPStack.Services.RenderingControl.Delegate_SetVolume(RenderingControl_SetVolume);
            _renderingControl.External_SetVolumeDB = new UPnPStack.Services.RenderingControl.Delegate_SetVolumeDB(RenderingControl_SetVolumeDB);            
            device.AddService(_renderingControl);

            // Setting the initial value of evented variables
            _aVTransport.Evented_LastChange = "";
            _connectionManager.Evented_SinkProtocolInfo = "http-get:*:audio/mpegurl:*,http-get:*:audio/mp3:*,http-get:*:audio/mpeg:*,http-get:*:audio/x-ms-wma:*,http-get:*:audio/wma:*,http-get:*:audio/mpeg3:*,http-get:*:video/x-ms-wmv:*,http-get:*:video/x-ms-avi:*,http-get:*:video/mpeg:*,http-get:*:audio/x-matroska:*,http-get:*:video/x-matroska:*";
            _connectionManager.Evented_SourceProtocolInfo = "Sample String";
            _connectionManager.Evented_CurrentConnectionIDs = "Sample String";
            _renderingControl.Evented_LastChange = "";
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
            Sink = "http-get:*:audio/mpegurl:*,http-get:*:audio/mp3:*,http-get:*:audio/mpeg:*,http-get:*:audio/x-ms-wma:*,http-get:*:audio/wma:*,http-get:*:audio/mpeg3:*,http-get:*:video/x-ms-wmv:*,http-get:*:video/x-ms-avi:*,http-get:*:video/mpeg:*";
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
            CurrentPresetNameList = "FactoryDefaults,InstallationDefaults";		
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

