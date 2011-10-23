using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUPnPSupport.UPnP.Devices;
using MediaPortal.Playlists;
using MediaPortal.GUI.Library;
using MediaPortal.Player;
using MyUPnPSupport.UPnP.Services;

namespace MyUPnPSupport.Plugin.Devices {
    class MediaPortalDMR : DigitalMediaRenderer {

        private static readonly PlayListPlayer Player;

        static MediaPortalDMR() {
            Player = PlayListPlayer.SingletonPlayer;
            Player.Init();
        }


        public MediaPortalDMR()
            : base("MediaPortal (DMR)",
                    "Anthrax",
                    "MyUPnPSupport MediaPortal-Plugin",
                    "Custom root-device for the MyUPnPSupport MediaPortal-Plugin",
                    new Guid("3F4EDD00-F8DD-4B91-AA76-BCABF3DCF193"),
                    new Uri("http://www.team-mediaportal.com"),
                    new Uri("http://www.team-mediaportal.com")) { }

        public override void AVTransport_GetTransportInfo(uint InstanceID, out UPnP.Services.AVTransport.Enum_TransportState CurrentTransportState, out UPnP.Services.AVTransport.Enum_TransportStatus CurrentTransportStatus, out UPnP.Services.AVTransport.Enum_TransportPlaySpeed CurrentSpeed) {
            if (Seeking) {
                CurrentTransportState = AVTransport.Enum_TransportState.TRANSITIONING;
            } else {
                if (g_Player.Player == null || g_Player.Stopped) {
                    CurrentTransportState = AVTransport.Enum_TransportState.STOPPED;
                } else if (g_Player.Paused) {
                    CurrentTransportState = AVTransport.Enum_TransportState.PAUSED_PLAYBACK;
                } else if (g_Player.Playing) {
                    CurrentTransportState = AVTransport.Enum_TransportState.PLAYING;
                } else {
                    CurrentTransportState = AVTransport.Enum_TransportState.NO_MEDIA_PRESENT;
                }
            }
            CurrentTransportStatus = AVTransport.Enum_TransportStatus.OK; 
            CurrentSpeed = AVTransport.Enum_TransportPlaySpeed._1;
            
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
            if (!g_Player.Paused) {
                g_Player.Pause();
            }
        }

        public override void AVTransport_Play(uint InstanceID, UPnP.Services.AVTransport.Enum_TransportPlaySpeed Speed) {
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
            Seeking = true;                
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
            Seeking = false;
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
            return  Convert.ToUInt16(((double)mepoVolume / (double)ushort.MaxValue) * 100);
        }

        private static int ConvertToMePo(ushort upnpVolume) {
            return (upnpVolume * ushort.MaxValue) / 100;
        }

        public bool Seeking { get; set; }
    }
}
