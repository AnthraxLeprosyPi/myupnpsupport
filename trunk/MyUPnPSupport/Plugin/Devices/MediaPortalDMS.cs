using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.Music.Database;
using MediaPortal.Player;
using MediaPortal.Playlists;
using MediaPortal.Util;
using MediaPortal.Video.Database;
using PH.DataTree;
using Platinum;
using UPnPStack.Devices;
using UPnPStack.Services;

namespace MyUPnPSupport.Plugin.Devices {
    public class MediaPortalDMS : DigitalMediaServer {

        DTreeNode<MediaObject> Root { get; set; }

        static MediaPortalDMS() {

        }


        public MediaPortalDMS(string name, string guid)
            : base(name,
                    "Anthrax",
                    "MyUPnPSupport MediaPortal-Plugin",
                    "Custom root-device for the MyUPnPSupport MediaPortal-Plugin",
                    new Guid(guid),
                    new Uri("http://www.team-mediaportal.com"),
                    Properties.Resources.upnp_dms_l,
                    Properties.Resources.upnp_dms_s) {

            Root = BuildStaticFolderStructure();

        }

        public override void ContentDirectory_Browse(string ObjectID, ContentDirectory.Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, string Filter, uint StartingIndex, uint RequestedCount, string SortCriteria, out string Result, out uint NumberReturned, out uint TotalMatches, out uint UpdateID) {
            var didl = Didl.header;
            
            switch (BrowseFlag) {
                case ContentDirectory.Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA:
                    if (ObjectID.Equals("0")) {
                        ContentDirectory_Browse(ObjectID, ContentDirectory.Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN, Filter, StartingIndex, RequestedCount, SortCriteria, out Result, out NumberReturned, out TotalMatches, out UpdateID);
                        return;
                    }

                    try {
                        MediaObject item = Root.BreadthFirstEnumerator.First(o => o.ObjectID == ObjectID);
                        var resource = new MediaResource();
                        resource.ProtoInfo = ProtocolInfo.GetProtocolInfoFromMimeType("audio/mp3", true);
                        // get list of ips and make sure the ip the request came from is used for the first resource returned
                        // this ensures that clients which look only at the first resource will be able to reach the item
                        // iterate through all ips and create a resource for each
                        foreach (String ip in UPnP.GetIpAddresses(true)) {
                            resource.URI = new Uri("http://" + ip + ":" + "4982" + "/" + item.ReferenceID).ToString();
                            item.AddResource(resource);
                        }
                        didl += item.ToDidl(Filter);
                        didl += Didl.footer;
                        Result = didl;
                        NumberReturned = 1;
                        TotalMatches = 1;
                        UpdateID = 1;

                    } catch {
                        #region this pleases the mighty WMP gods
                        switch (ObjectID) {
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
                                Result = "0";
                                break;
                            default:
                                Result = "-1";
                                break;
                        }
                        #endregion
                    }
                    break;
                case ContentDirectory.Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN:
                    try {

                        var parentNode = Root.BreadthFirstNodeEnumerator.First(o => o.Value.ObjectID == ObjectID);
                        int returnCount = 0;
                        if (RequestedCount == 0) {
                            RequestedCount = (uint)parentNode.Nodes.Count;
                        }
                        for (int i = (int)StartingIndex; i < StartingIndex + RequestedCount && i < parentNode.Nodes.Count; i++) {
                            var currentItem = parentNode.Nodes[i].Value;
                            var resource = new MediaResource();
                            resource.ProtoInfo = ProtocolInfo.GetProtocolInfoFromMimeType("audio/mp3", true);

                            // get list of ips and make sure the ip the request came from is used for the first resource returned
                            // this ensures that clients which look only at the first resource will be able to reach the item
                            List<String> ips = UPnP.GetIpAddresses(true);


                            // iterate through all ips and create a resource for each
                            foreach (String ip in ips) {
                                resource.URI = new Uri("http://" + ip + ":" + "4982" + "/" + currentItem.ObjectID).ToString();
                                currentItem.AddResource(resource);
                            }
                            didl += currentItem.ToDidl(Filter);
                            returnCount++;
                        }
                        didl += Didl.footer;
                        Result = didl;
                        NumberReturned = (uint)returnCount;
                        TotalMatches = (uint)parentNode.Nodes.Count;
                        UpdateID = 1;

                    } catch {
                        #region this pleases the mighty WMP gods
                        switch (ObjectID) {
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
                                Result = "0";
                                break;
                            default:
                                Result = "-1";
                                break;

                        }
                        #endregion
                    }
                    break;
            }
            NumberReturned = 0;
            TotalMatches = 0;
            UpdateID = 0;
            Result = "-1";
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
            //albums.NodesAccessedInitially += new DTreeNode<MediaObject>.NodesAccessedInitiallyHandler(albums_NodesAccessedInitially);

            // Subsections of Video
            DTreeNode<MediaObject> allVideos = video.Nodes.Add(new MediaContainer() {
                ObjectID = "8",
                ParentID = "2",
                Title = "All Videos",
                Class = new ObjectClass("object.item.videoItem", "All Videos")
            });
            //allVideos.NodesAccessedInitially += new DTreeNode<MediaObject>.NodesAccessedInitiallyHandler(allVideos_NodesAccessedInitially);
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
                foreach (var actor in movie.Cast.Split('\n')) {
                    currentMovie.People.AddActor(new PersonRole(actor));
                }
                currentMovie.People.Director = movie.Director;
                currentMovie.Description.DescriptionText = movie.Plot;
                currentMovie.Description.Rating = movie.Rating.ToString();
                currentMovie.Extra.AddGenre(movie.SingleGenre);
                MediaResource resource = new MediaResource();
                resource.ProtoInfo = ProtocolInfo.GetProtocolInfo(movie.Path);
                resource.Duration = (uint)movie.RunTime * 60;
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
                currentSong.Affiliation.Album = parentNode.Value.Title;
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
    }
}
