using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MediaPortal.Music.Database;
using PH.DataTree;
using Platinum;
using MediaPortal.Video.Database;

namespace MyUPnPSupport {
    public class UPnPMediaServer : IDisposable {
        UPnP UPnP { get; set; }
        MediaConnect ms { get; set; }
        ControlPoint cp { get; set; }
        BindingList<DeviceData> Devices { get; set; }
        DTreeNode<MediaObject> Root { get; set; }

        public UPnPMediaServer() {
            Root = BuildStaticFolderStructure();
            UPnP = new UPnP();
            Devices = new BindingList<DeviceData>();
            UPnP.Start();

            ms = new MediaConnect("MediaPortal (DMS)", "AFA16A2F-8AEB-4B0F-82D6-940774095F41", 0);
            UPnP.AddDeviceHost(ms);
            ms.BrowseDirectChildren += new MediaServer.BrowseDirectChildrenDelegate(ms_BrowseDirectChildren);
            ms.BrowseMetadata += new MediaServer.BrowseMetadataDelegate(ms_BrowseMetadata);
            ms.SearchContainer += new MediaServer.SearchContainerDelegate(ms_SearchContainer);
            ms.ProcessFileRequest += new MediaServer.ProcessFileRequestDelegate(ms_ProcessFileRequest);

            ms.AddIcon(new DeviceIcon("image/png", 48, 48, 32, "/icon/upnp_dms_s.png"), ImageToByte(Properties.Resources.upnp_dms_s));
            ms.AddIcon(new DeviceIcon("image/png", 120, 120, 32, "/icon/upnp_dms_l.png"), ImageToByte(Properties.Resources.upnp_dms_l));

            cp = new ControlPoint(true);
            cp.DeviceAdded += cp_DeviceAdded;
            cp.DeviceRemoved += cp_DeviceRemoved;
            cp.EventNotify += cp_EventNotify;
            cp.ActionResponse += cp_ActionResponse;
            
            UPnP.AddControlPoint(cp);
        }

        void cp_ActionResponse(NeptuneException error, Platinum.Action action) {
           
        }

        void cp_EventNotify(Service srv, IEnumerable<StateVariable> vars) {
          
        }

        void cp_DeviceRemoved(DeviceData dev) {
            Devices.Remove(dev);
            foreach (var service in dev.Services) {
                cp.Unsubscribe(service);
            }
        }

        void cp_DeviceAdded(DeviceData dev) {
            Devices.Add(dev);
            foreach (var service in dev.Services) {
                cp.Subscribe(service);
            }
        }

        ~UPnPMediaServer() {
            this.Dispose();
        }

        public static byte[] ImageToByte(Image img) {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        int ms_SearchContainer(Platinum.Action action, string object_id, string searchCriteria, string filter, int starting_index, int requested_count, string sort_criteria, HttpRequestContext context) {
            return -1;
        }


        int ms_BrowseMetadata(Platinum.Action action, string object_id, string filter, int starting_index, int requested_count, string sort_criteria, HttpRequestContext context) {
            var didl = Didl.header;
            try {
                MediaObject item = Root.BreadthFirstEnumerator.First(o => o.ObjectID == object_id);
                var resource = new MediaResource();
                resource.ProtoInfo = ProtocolInfo.GetProtocolInfoFromMimeType("audio/mp3", true, context);
                // get list of ips and make sure the ip the request came from is used for the first resource returned
                // this ensures that clients which look only at the first resource will be able to reach the item
                List<String> ips = UPnP.GetIpAddresses(true);
                String localIP = context.LocalAddress.ip;
                if (localIP != "0.0.0.0") {
                    ips.Remove(localIP);
                    ips.Insert(0, localIP);
                }

                // iterate through all ips and create a resource for each
                foreach (String ip in ips) {
                    resource.URI = new Uri("http://" + ip + ":" + context.LocalAddress.port + "/" + item.ReferenceID).ToString();
                    item.AddResource(resource);
                }
                didl += item.ToDidl(filter);
                didl += Didl.footer;
                action.SetArgumentValue("Result", didl);
                action.SetArgumentValue("NumberReturned", "1");
                action.SetArgumentValue("TotalMatches", "1");
                action.SetArgumentValue("UpdateId", "1");
                return 0;
            } catch {
                #region this pleases the mighty WMP gods
                switch (object_id) {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "12":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "F":
                    case "14":
                    case "100":
                    case "107":
                    case "108":
                    case "101":
                    case "102":
                    case "103":
                    case "104":
                    case "106":
                    case "8":
                    case "9":
                    case "A":
                    case "E":
                    case "10":
                    case "15":
                    case "200":
                    case "201":
                    case "202":
                    case "203":
                    case "204":
                    case "205":
                    case "B":
                    case "C":
                    case "D":
                    case "D2":
                    case "11":
                    case "16":
                    case "300":
                    case "301":
                    case "302":
                    case "303":
                    case "304":
                    case "305":
                    case "306":
                    case "13":
                    case "17":
                        return 0;
                    default:
                        return -1;
                }
                #endregion
            }
        }

        int ms_BrowseDirectChildren(Platinum.Action action, string object_id, string filter, int starting_index, int requested_count, string sort_criteria, HttpRequestContext context) {
            try {
                var didl = Didl.header;
                var parentNode = Root.BreadthFirstNodeEnumerator.First(o => o.Value.ObjectID == object_id);
                int returnCount = 0;
                if (requested_count == 0) {
                    requested_count = parentNode.Nodes.Count;
                }
                for (int i = starting_index; i < starting_index + requested_count && i < parentNode.Nodes.Count; i++) {
                    var currentItem = parentNode.Nodes[i].Value;
                    var resource = new MediaResource();
                    resource.ProtoInfo = ProtocolInfo.GetProtocolInfoFromMimeType("audio/mp3", true, context);

                    // get list of ips and make sure the ip the request came from is used for the first resource returned
                    // this ensures that clients which look only at the first resource will be able to reach the item
                    List<String> ips = UPnP.GetIpAddresses(true);
                    String localIP = context.LocalAddress.ip;
                    if (localIP != "0.0.0.0") {
                        ips.Remove(localIP);
                        ips.Insert(0, localIP);
                    }

                    // iterate through all ips and create a resource for each
                    foreach (String ip in ips) {
                        resource.URI = new Uri("http://" + ip + ":" + context.LocalAddress.port + "/" + currentItem.ObjectID).ToString();
                        currentItem.AddResource(resource);
                    }
                    didl += currentItem.ToDidl(filter);
                    returnCount++;
                }
                didl += Didl.footer;
                action.SetArgumentValue("Result", didl);
                action.SetArgumentValue("NumberReturned", returnCount.ToString());
                action.SetArgumentValue("TotalMatches", parentNode.Nodes.Count.ToString());
                action.SetArgumentValue("UpdateId", "1");
                return 0;
            } catch {
                #region this pleases the mighty WMP gods
                switch (object_id) {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "12":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "F":
                    case "14":
                    case "100":
                    case "107":
                    case "108":
                    case "101":
                    case "102":
                    case "103":
                    case "104":
                    case "106":
                    case "8":
                    case "9":
                    case "A":
                    case "E":
                    case "10":
                    case "15":
                    case "200":
                    case "201":
                    case "202":
                    case "203":
                    case "204":
                    case "205":
                    case "B":
                    case "C":
                    case "D":
                    case "D2":
                    case "11":
                    case "16":
                    case "300":
                    case "301":
                    case "302":
                    case "303":
                    case "304":
                    case "305":
                    case "306":
                    case "13":
                    case "17":
                        return 0;
                    default:
                        return -1;
                }
                #endregion
            }
        }

        private int ms_ProcessFileRequest(HttpRequestContext context, HttpResponse response) {
            try {
                string path = context.Request.URI.AbsolutePath.Remove(0, 1);
                MediaObject item = Root.DepthFirstEnumerator.First(o => o.ObjectID == path);
                MediaConnect.SetResponseFilePath(context, response, item.ReferenceID);
                return 0;
            } catch {
                return -1;
            }
        }


        public DTreeNode<MediaObject> BuildStaticFolderStructure() {
            //The root node - MediaPortal Library
            DTreeNode<MediaObject> root = new DTreeNode<MediaObject>(new MediaContainer() {
                ObjectID = "0",
                ParentID = "-1",
                Title = "MediaPortal Library",
                ChildrenCount = 4,
                Class = new ObjectClass("object.container", "MediaPortal Library")
            });
            // Toplevel sections Music, Video, Pictures, Playlists
            DTreeNode<MediaObject> music = root.Nodes.Add(new MediaContainer() {
                ObjectID = "1",
                ParentID = "0",
                Title = "Music",
                ChildrenCount = 10,
                Class = new ObjectClass("object.container", "Music")
            });
            DTreeNode<MediaObject> video = root.Nodes.Add(new MediaContainer() {
                ObjectID = "2",
                ParentID = "0",
                Title = "Video",
                ChildrenCount = 7,
                Class = new ObjectClass("object.container", "Video")
            });
            DTreeNode<MediaObject> pictures = root.Nodes.Add(new MediaContainer() {
                ObjectID = "3",
                ParentID = "0",
                Title = "Pictures",
                ChildrenCount = 7,
                Class = new ObjectClass("object.container", "Pictures")
            });
            DTreeNode<MediaObject> playlists = root.Nodes.Add(new MediaContainer() {
                ObjectID = "12",
                ParentID = "0",
                Title = "Playlists",
                ChildrenCount = 2,
                Class = new ObjectClass("object.container", "Playlists")
            });
            // Subsections of Music
            DTreeNode<MediaObject> albums = music.Nodes.Add(new MediaContainer() {
                ObjectID = "7",
                ParentID = "1",
                Title = "By Album",
                Class = new ObjectClass("object.container.album.musicAlbum", "By Album")
            });
            albums.NodesAccessedInitially += new DTreeNode<MediaObject>.NodesAccessedInitiallyHandler(albums_NodesAccessedInitially);

            // Subsections of Video
            DTreeNode<MediaObject> allVideos = video.Nodes.Add(new MediaContainer() {
                ObjectID = "8",
                ParentID = "2",
                Title = "All Videos",
                Class = new ObjectClass("object.item.videoItem", "All Videos")
            });
            allVideos.NodesAccessedInitially += new DTreeNode<MediaObject>.NodesAccessedInitiallyHandler(allVideos_NodesAccessedInitially);
            return root;
        }

        void allVideos_NodesAccessedInitially(DTreeNode<MediaObject> parentNode, ref DTreeNodeCollection<MediaObject> nodesCollection) {
            ArrayList videos = new ArrayList();
            VideoDatabase.GetMovies(ref videos);
            string parentID = string.Join("/", parentNode.GetIndexPath().Select(x => x.ToString()).ToArray());
            int i = 0;
            foreach (IMDBMovie movie in videos) {
                MediaItem currentMovie = new MediaItem();
                currentMovie.Title = movie.Title;
                currentMovie.ParentID = parentID;
                currentMovie.ObjectID = parentID + "/" + i++.ToString();
                currentMovie.Class = new ObjectClass("object.item.videoItem", "Video");
                currentMovie.ReferenceID = movie.File;
                currentMovie.People.AddActor(new PersonRole(movie.Actor));
                currentMovie.People.Director = movie.Director;
                currentMovie.Description.DescriptionText = movie.Plot;
                currentMovie.Description.Rating = movie.Rating.ToString();
                currentMovie.Extra.AddGenre(movie.SingleGenre);
                MediaResource resource = new MediaResource();
                resource.ProtoInfo = ProtocolInfo.GetProtocolInfo(movie.Path);
                resource.Duration = (uint)movie.RunTime * 60 * 60;
                currentMovie.AddResource(resource);
                DTreeNode<MediaObject> movieNode = nodesCollection.Add(currentMovie);
            }
        }

        void albums_NodesAccessedInitially(DTreeNode<MediaObject> parentNode, ref DTreeNodeCollection<MediaObject> nodesCollection) {
            List<AlbumInfo> albums = new List<AlbumInfo>();
            MusicDatabase.Instance.GetAllAlbums(ref albums);
            string parentID = string.Join("/", parentNode.GetIndexPath().Select(x => x.ToString()).ToArray());
            int i = 0;
            foreach (var album in albums) {
                MediaContainer currentAlbum = new MediaContainer() {
                    Title = album.Album,
                    ParentID = parentID,
                    ObjectID = parentID + "/" + i++.ToString(),
                    Class = new ObjectClass("object.container.album.musicAlbum", "Album")
                };
                currentAlbum.Extra.AddGenre(album.Genre);
                currentAlbum.Extra.AddAlbumArtInfo(new AlbumArtInfo(album.Image));
                DTreeNode<MediaObject> albumNode = nodesCollection.Add(currentAlbum);
                albumNode.NodesAccessedInitially += album_NodesAccessedInitially;
            }
        }

        void album_NodesAccessedInitially(DTreeNode<MediaObject> parentNode, ref DTreeNodeCollection<MediaObject> nodesCollection) {
            ArrayList songs = new ArrayList();
            MusicDatabase.Instance.GetSongsByAlbum(parentNode.Value.Title, ref songs);
            string parentID = string.Join("/", parentNode.GetIndexPath().Select(x => x.ToString()).ToArray());
            int i = 0;
            foreach (Song song in songs) {
                MediaItem currentSong = new MediaItem();
                currentSong.Title = song.Title;
                currentSong.ParentID = parentID;
                currentSong.ObjectID = parentID + "/" + i++.ToString();
                currentSong.Class = new ObjectClass("object.item.audioItem.musicTrack", "Track");
                currentSong.ReferenceID = song.FileName;
                currentSong.People.AddArtist(new PersonRole(song.Artist));
                currentSong.Extra.AddGenre(song.Genre);
                MediaResource resource = new MediaResource();
                resource.ProtoInfo = ProtocolInfo.GetProtocolInfo(song.FileName);
                resource.Bitrate = (uint)song.BitRate;
                resource.Duration = (uint)song.Duration;
                currentSong.AddResource(resource);
                DTreeNode<MediaObject> songNode = nodesCollection.Add(currentSong);
            }
        }

        public void Dispose() {
            ms.BrowseDirectChildren -= ms_BrowseDirectChildren;
            ms.BrowseMetadata -= ms_BrowseMetadata;
            ms.ProcessFileRequest -= ms_ProcessFileRequest;
            UPnP.RemoveDeviceHost(ms);
            UPnP.Stop();
            UPnP.Dispose();
        }
    }
}
