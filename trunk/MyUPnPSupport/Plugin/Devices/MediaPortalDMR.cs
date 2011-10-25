using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUPnPSupport.UPnP.Devices;
using MediaPortal.Playlists;
using MediaPortal.GUI.Library;
using MediaPortal.Player;
using MyUPnPSupport.UPnP.Services;
using System.Windows.Forms;

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
            g_Player.PlayBackChanged += new g_Player.ChangedHandler(g_Player_PlayBackChanged);
            PositionTimer = new Timer();
            PositionTimer.Interval = 1000;
            PositionTimer.Tick += new EventHandler(PositionTimer_Tick);
        }

        void g_Player_PlayBackChanged(g_Player.MediaType type, int stoptime, string filename) {
            throw new NotImplementedException();
        }

        void g_Player_PlayBackEnded(g_Player.MediaType type, string filename) {
            AVTransport.TransportState = UPnP.Services.AVTransport.Enum_TransportState.NO_MEDIA_PRESENT;
        }

        void g_Player_PlayBackStarted(g_Player.MediaType type, string filename) {
            AVTransport.TransportState = UPnP.Services.AVTransport.Enum_TransportState.PLAYING;
        }

        void g_Player_PlayBackStopped(g_Player.MediaType type, int stoptime, string filename) {
            AVTransport.TransportState = UPnP.Services.AVTransport.Enum_TransportState.STOPPED;
        }

        public override void AVTransport_GetTransportInfo(uint InstanceID, out UPnP.Services.AVTransport.Enum_TransportState CurrentTransportState, out UPnP.Services.AVTransport.Enum_TransportStatus CurrentTransportStatus, out UPnP.Services.AVTransport.Enum_TransportPlaySpeed CurrentSpeed) {
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
        
        private string GetFormattedTime(double dTime) {
          return String.Format("{0}:{1}:{2}", (int)(dTime / 3600d), (int)((dTime % 3600d) / 60d), (int)(dTime % 60d)); 
        }
        
        void PositionTimer_Tick(object sender, EventArgs e) {
            if ((g_Player.CurrentPosition > 0) && (g_Player.CurrentPosition <= g_Player.Duration)) {
                AVTransport.CurrentMediaDuration = GetFormattedTime(g_Player.Duration);
                AVTransport.CurrentTrackDuration = AVTransport.CurrentMediaDuration;
                AVTransport.AbsoluteTimePosition = GetFormattedTime(g_Player.CurrentPosition);
                AVTransport.RelativeTimePosition = AVTransport.AbsoluteTimePosition;
            }
        }             

        public override void AVTransport_GetPositionInfo(uint InstanceID, out uint Track, out string TrackDuration, out string TrackMetaData, out string TrackURI, out string RelTime, out string AbsTime, out int RelCount, out int AbsCount) {
            Track = 0;
            TrackDuration = GetFormattedTime(g_Player.Duration);
            TrackMetaData = "<DIDL-Lite xmlns=\"urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\"><item id=\"X\" parentID=\"Y\" restricted=\"1\"><dc:title>Test-Title</dc:title><dc:creator>Anthrax</dc:creator><upnp:class>object.item.audioItem</upnp:class></item></DIDL-Lite>";
            TrackURI = g_Player.CurrentFile;
            RelTime = "";
            AbsTime = GetFormattedTime(g_Player.CurrentPosition);
            RelCount = 0;
            AbsCount = 0;
        }

        public override void AVTransport_SetAVTransportURI(uint InstanceID, string CurrentURI, string CurrentURIMetaData) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.Invoke(new System.Action(() => AVTransport_SetAVTransportURI(InstanceID, CurrentURI, CurrentURIMetaData)));
            }
            Player.g_Player.Play(CurrentURI);
            Log.Debug("AVTransport_SetAVTransportURI(" + InstanceID.ToString() + CurrentURI.ToString() + CurrentURIMetaData.ToString() + ")");
        }

        public override void AVTransport_Next(uint InstanceID) {
            Player.PlayNext();
        }

        public override void AVTransport_Previous(uint InstanceID) {
            Player.PlayPrevious();
        }

        public override void AVTransport_Pause(uint InstanceID) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.Invoke(new System.Action(() => AVTransport_Pause(InstanceID)));
                return;
            }
            if (!g_Player.Paused) {
                g_Player.Pause();
            }
        }

        public override void AVTransport_Play(uint InstanceID, UPnP.Services.AVTransport.Enum_TransportPlaySpeed Speed) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.Invoke(new System.Action(() => AVTransport_Play(InstanceID, Speed)));
                return;
            }
            if (g_Player.Paused) {
                g_Player.Pause();
            } else {
                Player.Play(Player.CurrentSong);
            }
        }

        public override void AVTransport_Stop(uint InstanceID) {
            g_Player.Stop();
        }

        public override void AVTransport_Seek(uint InstanceID, UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode Unit, string Target) {
            if (GUIGraphicsContext.form.InvokeRequired) {
                GUIGraphicsContext.form.Invoke(new System.Action(() => AVTransport_Seek(InstanceID, Unit, Target)));
                return;
            }
            if (g_Player.CanSeek) {
                UPnP.Services.AVTransport.Enum_TransportState last = AVTransport.TransportState;
                AVTransport.TransportState = UPnP.Services.AVTransport.Enum_TransportState.TRANSITIONING;
                switch (Unit) {
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.ABS_TIME:
                        g_Player.SeekAbsolute(double.Parse(Target));
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.REL_TIME:
                        g_Player.SeekRelative(double.Parse(Target));
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.ABS_COUNT:
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.REL_COUNT:
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.TRACK_NR:
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.CHANNEL_FREQ:
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.TAPE_INDEX:
                        break;
                    case MyUPnPSupport.UPnP.Services.AVTransport.Enum_A_ARG_TYPE_SeekMode.FRAME:
                        break;
                    default:
                        break;
                }
                AVTransport.TransportState = last;                
            }
        }

        public override void RenderingControl_GetVolume(uint InstanceID, UPnP.Services.RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out ushort CurrentVolume) {
            CurrentVolume = ConvertToUPnP(VolumeHandler.Instance.Volume);
        }

        public override void RenderingControl_SetVolume(uint InstanceID, UPnP.Services.RenderingControl.Enum_A_ARG_TYPE_Channel Channel, ushort DesiredVolume) {
            VolumeHandler.Instance.Volume = ConvertToMePo(DesiredVolume);
        }

        public override void RenderingControl_GetMute(uint InstanceID, UPnP.Services.RenderingControl.Enum_A_ARG_TYPE_Channel Channel, out bool CurrentMute) {
            CurrentMute = VolumeHandler.Instance.IsMuted;
        }

        public override void RenderingControl_SetMute(uint InstanceID, UPnP.Services.RenderingControl.Enum_A_ARG_TYPE_Channel Channel, bool DesiredMute) {
            VolumeHandler.Instance.IsMuted = DesiredMute;
        }

        private static ushort ConvertToUPnP(int mepoVolume) {
            return Convert.ToUInt16(((double)mepoVolume / (double)ushort.MaxValue) * 100);
        }

        private static int ConvertToMePo(ushort upnpVolume) {
            return (upnpVolume * ushort.MaxValue) / 100;
        }

        public bool Seeking { get; set; }
    }
}
