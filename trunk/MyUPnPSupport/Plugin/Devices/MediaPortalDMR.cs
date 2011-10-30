using System;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.Player;
using MediaPortal.Playlists;
using UPnPDevices.Devices;
using UPnPDevices.Services;
using MediaPortal.Util;

namespace MyUPnPSupport.Plugin.Devices {
    class MediaPortalDMR : DigitalMediaRenderer {

        private static readonly PlayListPlayer Player;
        private Timer PositionTimer;

        static MediaPortalDMR() {
            Player = PlayListPlayer.SingletonPlayer;
            Player.Init();
            g_Player.Init();
        }


        public MediaPortalDMR(string name, string guid)
            : base(name,
                    "Anthrax",
                    "MyUPnPSupport MediaPortal-Plugin",
                    "Custom UPnP-Renderer device for the MyUPnPSupport MediaPortal-Plugin",
                    new Guid(guid),
                    new Uri("http://www.team-mediaportal.com"),
                    new Uri("http://www.team-mediaportal.com"),
                    Properties.Resources.upnp_dmr_l,
                    Properties.Resources.upnp_dmr_s) {
            g_Player.PlayBackStopped += new g_Player.StoppedHandler(g_Player_PlayBackStopped);
            g_Player.PlayBackStarted += new g_Player.StartedHandler(g_Player_PlayBackStarted);
            g_Player.PlayBackEnded += new g_Player.EndedHandler(g_Player_PlayBackEnded);
            PositionTimer = new Timer();
            PositionTimer.Interval = 1000;
            PositionTimer.Tick += new EventHandler(PositionTimer_Tick);
        }

        void g_Player_PlayBackEnded(g_Player.MediaType type, string filename) {
            _aVTransport.TransportState = AVTransport.Enum_TransportState.NO_MEDIA_PRESENT;
            PositionTimer.Stop();
        }

        void g_Player_PlayBackStarted(g_Player.MediaType type, string filename) {
            _aVTransport.TransportState = AVTransport.Enum_TransportState.PLAYING;
            PositionTimer.Start();
        }

        void g_Player_PlayBackStopped(g_Player.MediaType type, int stoptime, string filename) {
            _aVTransport.TransportState = AVTransport.Enum_TransportState.STOPPED;
            PositionTimer.Stop();
        }


        private string GetFormattedTime(double dTime) {
            return String.Format("{0:00}:{1:00}:{2:00}", (int)(dTime / 3600d), (int)((dTime % 3600d) / 60d), (int)(dTime % 60d));
        }

        private double LastPosition = 0;

        void PositionTimer_Tick(object sender, EventArgs e) {
            double CurrentPosition = g_Player.CurrentPosition;
            if (CurrentPosition > 0 || CurrentPosition != LastPosition) {
                LastPosition = CurrentPosition;
                if (g_Player.Duration < 0) {
                    _aVTransport.CurrentMediaDuration = "NOT_IMPLEMENTED";
                    _aVTransport.CurrentTrackDuration = _aVTransport.CurrentMediaDuration;
                } else {
                    _aVTransport.CurrentMediaDuration = GetFormattedTime(g_Player.Duration);
                    _aVTransport.CurrentTrackDuration = _aVTransport.CurrentMediaDuration;
                }
                _aVTransport.AbsoluteTimePosition = GetFormattedTime(CurrentPosition);
                _aVTransport.RelativeTimePosition = GetFormattedTime(CurrentPosition);
            }
        }

        public override void AVTransport_GetTransportInfo(uint InstanceID, out AVTransport.Enum_TransportState CurrentTransportState, out AVTransport.Enum_TransportStatus CurrentTransportStatus, out AVTransport.Enum_TransportPlaySpeed CurrentSpeed) {
            if (g_Player.Player == null || g_Player.Stopped) {
                CurrentTransportState = AVTransport.Enum_TransportState.STOPPED;
            } else if (g_Player.Paused) {
                CurrentTransportState = AVTransport.Enum_TransportState.PAUSED_PLAYBACK;
            } else if (g_Player.Playing) {
                CurrentTransportState = AVTransport.Enum_TransportState.PLAYING;
            } else {
                CurrentTransportState = AVTransport.Enum_TransportState.TRANSITIONING;
            }
            CurrentTransportStatus = AVTransport.Enum_TransportStatus.OK;
            CurrentSpeed = AVTransport.Enum_TransportPlaySpeed._1;
        }

        public override void AVTransport_GetDeviceCapabilities(uint InstanceID, out string PlayMedia, out string RecMedia, out string RecQualityModes) {
            RecMedia = "NOT_IMPLEMENTED";
            RecQualityModes = "NOT_IMPLEMENTED";
            PlayMedia = String.Join(",", Enum.GetNames(typeof(AVTransport.Enum_PlaybackStorageMedium))).Replace(",NOT_IMPLEMENTED", "");
        }

        public override void AVTransport_GetMediaInfo(uint InstanceID, out uint NrTracks, out string MediaDuration, out string CurrentURI, out string CurrentURIMetaData, out string NextURI, out string NextURIMetaData, out AVTransport.Enum_PlaybackStorageMedium PlayMedium, out AVTransport.Enum_RecordStorageMedium RecordMedium, out AVTransport.Enum_RecordMediumWriteStatus WriteStatus) {
            base.AVTransport_GetMediaInfo(InstanceID, out NrTracks, out MediaDuration, out CurrentURI, out CurrentURIMetaData, out NextURI, out NextURIMetaData, out PlayMedium, out RecordMedium, out WriteStatus);
        }

        public override void AVTransport_GetTransportSettings(uint InstanceID, out AVTransport.Enum_CurrentPlayMode PlayMode, out AVTransport.Enum_CurrentRecordQualityMode RecQualityMode) {
            PlayMode = _aVTransport.CurrentPlayMode;
            RecQualityMode = UPnPDevices.Services.AVTransport.Enum_CurrentRecordQualityMode.NOT_IMPLEMENTED;
        }

        public override void AVTransport_GetCurrentTransportActions(uint InstanceID, out string Actions) {
            Actions = "Play,Stop,Pause,Next,Previous";
        }

        public override void AVTransport_SetPlayMode(uint InstanceID, AVTransport.Enum_CurrentPlayMode NewPlayMode) {
            _aVTransport.CurrentPlayMode = NewPlayMode;
        }

        public override void AVTransport_GetPositionInfo(uint InstanceID, out uint Track, out string TrackDuration, out string TrackMetaData, out string TrackURI, out string RelTime, out string AbsTime, out int RelCount, out int AbsCount) {
            Track = 1;
            TrackDuration = GetFormattedTime(g_Player.Duration);
            TrackMetaData = "<DIDL-Lite xmlns=\"urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\"><item id=\"X\" parentID=\"Y\" restricted=\"1\"><dc:title>Test-Title</dc:title><dc:creator>Anthrax</dc:creator><upnp:class>object.item.audioItem</upnp:class></item></DIDL-Lite>";
            TrackURI = g_Player.CurrentFile;
            RelTime = GetFormattedTime(g_Player.CurrentPosition);
            AbsTime = GetFormattedTime(g_Player.CurrentPosition);
            RelCount = int.MaxValue;
            AbsCount = int.MaxValue;
        }

        public override void AVTransport_SetAVTransportURI(uint InstanceID, string CurrentURI, string CurrentURIMetaData) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_SetAVTransportURI(InstanceID, CurrentURI, CurrentURIMetaData)));
                return;
            }
           
            if (String.IsNullOrEmpty(CurrentURI)) {
                g_Player.Stop();
                _aVTransport.TransportState = AVTransport.Enum_TransportState.STOPPED;
            } else {
                _aVTransport.TransportState = AVTransport.Enum_TransportState.TRANSITIONING;
                g_Player.Play(CurrentURI);
                //g_Player.PlayVideoStream(CurrentURI);
                //_aVTransport.AVTransportURI = CurrentURI;
                //_aVTransport.AVTransportURIMetaData = CurrentURIMetaData;
            }
            Log.Debug("AVTransport_SetAVTransportURI(" + InstanceID.ToString() + CurrentURI.ToString() + CurrentURIMetaData.ToString() + ")");
        }
        

        public override void AVTransport_Next(uint InstanceID) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_Next(InstanceID)));
                return;
            }
            Player.PlayNext();
        }

        public override void AVTransport_Previous(uint InstanceID) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_Previous(InstanceID)));
                return;
            }
            Player.PlayPrevious();
        }

        public override void AVTransport_Pause(uint InstanceID) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_Pause(InstanceID)));
                return;
            }
            if (!g_Player.Paused) {
                g_Player.Pause();
            }
        }

        public override void AVTransport_Play(uint InstanceID, AVTransport.Enum_TransportPlaySpeed Speed) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_Play(InstanceID, Speed)));
                return;
            }
            if (g_Player.Paused) {
                g_Player.Pause();
            } else {
                Player.Play(Player.CurrentSong);
            }
        }

        public override void AVTransport_Stop(uint InstanceID) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_Stop(InstanceID)));
                return;
            }
            g_Player.Stop();
        }

        public override void AVTransport_Seek(uint InstanceID, AVTransport.Enum_A_ARG_TYPE_SeekMode Unit, string Target) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.BeginInvoke(new System.Action(() => AVTransport_Seek(InstanceID, Unit, Target)));
                return;
            }
            if (!g_Player.CanSeek) {
                return;
            }
            TimeSpan seekSpan;
            if (Unit == AVTransport.Enum_A_ARG_TYPE_SeekMode.REL_TIME && TimeSpan.TryParse(Target, out seekSpan)) {
                AVTransport.Enum_TransportState last = _aVTransport.TransportState;
                _aVTransport.TransportState = AVTransport.Enum_TransportState.TRANSITIONING;
                g_Player.SeekRelative(seekSpan.TotalSeconds);
                _aVTransport.TransportState = last;
            }
        }

        public override void RenderingControl_GetVolume(uint InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out ushort CurrentVolume) {
            CurrentVolume = ConvertToUPnP(VolumeHandler.Instance.Volume);
        }

        public override void RenderingControl_SetVolume(uint InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, ushort DesiredVolume) {
            VolumeHandler.Instance.Volume = ConvertToMePo(DesiredVolume);
        }

        public override void RenderingControl_GetMute(uint InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out bool CurrentMute) {
            CurrentMute = VolumeHandler.Instance.IsMuted;
        }

        public override void RenderingControl_SetMute(uint InstanceID, RenderingControl.Enum_A_ARG_TYPE_Channel Channel, bool DesiredMute) {
            VolumeHandler.Instance.IsMuted = DesiredMute;
        }

        private static ushort ConvertToUPnP(int mepoVolume) {
            return Convert.ToUInt16(((double)mepoVolume / (double)ushort.MaxValue) * 100);
        }

        private static int ConvertToMePo(ushort upnpVolume) {
            return (upnpVolume * ushort.MaxValue) / 100;
        }       
    }
}
