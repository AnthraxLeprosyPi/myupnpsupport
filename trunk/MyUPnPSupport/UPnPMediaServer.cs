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

namespace MyUPnPSupport {
    public class UPnPMediaServer : IDisposable {
        UPnP UPnP { get; set; }
        MediaConnect ms { get; set; }
        BindingList<DeviceData> Devices { get; set; }
        DTreeNode<MediaObject> Root { get; set; }

        public UPnPMediaServer() {

            Root = BuildStaticFolderStructure();
            //log4net.Config.BasicConfigurator.Configure();
            UPnP = new UPnP();
            Devices = new BindingList<DeviceData>();
            ms = new MediaConnect("Anthrax", "AFA16A2F-8AEB-4B0F-82D6-940774095F41", 0);
            ms.BrowseDirectChildren += new MediaServer.BrowseDirectChildrenDelegate(ms_BrowseDirectChildren);
            ms.BrowseMetadata += new MediaServer.BrowseMetadataDelegate(ms_BrowseMetadata);
            ms.SearchContainer += new MediaServer.SearchContainerDelegate(ms_SearchContainer);
            ms.ProcessFileRequest += new MediaServer.ProcessFileRequestDelegate(ms_ProcessFileRequest);

            UPnP.AddDeviceHost(ms);
            UPnP.Start();
            ms.AddIcon(new DeviceIcon("image/png", 48, 48, 32, "/icon/upnp_dms_s.png"), ImageToByte(Properties.Resources.upnp_dms_s));
            ms.AddIcon(new DeviceIcon("image/png", 120, 120, 32, "/icon/upnp_dms_l.png"), ImageToByte(Properties.Resources.upnp_dms_l));

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
                didl += item.ToDidl(filter);

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

                didl += Didl.footer;
                action.SetArgumentValue("Result", didl);
                action.SetArgumentValue("NumberReturned", "1");
                action.SetArgumentValue("TotalMatches", "1");
                action.SetArgumentValue("UpdateId", "1");
                return 0;
            } catch {
                return -1;
            }


            //if (object_id == "0") {
            //    var root = new MediaContainer();
            //    root.Title = "Root";
            //    root.ObjectID = "0";
            //    root.ParentID = "-1";
            //    root.Class = new ObjectClass("object.container.storageFolder", "");

            //    var didl = Didl.header + root.ToDidl(filter) + Didl.footer;
            //    action.SetArgumentValue("Result", didl);
            //    action.SetArgumentValue("NumberReturned", "1");
            //    action.SetArgumentValue("TotalMatches", "1");

            //    // update ID may be wrong here, it should be the one of the container?
            //    // TODO: We need to keep track of the overall updateID of the CDS
            //    action.SetArgumentValue("UpdateId", "1");

            //    return 0;
            //} else if (object_id == "1") {
            //    var item = new MediaItem();
            //    item.Title = "Item";
            //    item.ObjectID = "1";
            //    item.ParentID = "0";
            //    item.Class = new ObjectClass("object.item.audioItem.musicTrack", "");

            //    var resource = new MediaResource();
            //    resource.ProtoInfo = ProtocolInfo.GetProtocolInfoFromMimeType("audio/mp3", true, context);

            //    // get list of ips and make sure the ip the request came from is used for the first resource returned
            //    // this ensures that clients which look only at the first resource will be able to reach the item
            //    List<String> ips = UPnP.GetIpAddresses(true);
            //    String localIP = context.LocalAddress.ip;
            //    if (localIP != "0.0.0.0") {
            //        ips.Remove(localIP);
            //        ips.Insert(0, localIP);
            //    }

            //    // iterate through all ips and create a resource for each
            //    foreach (String ip in ips) {
            //        resource.URI = new Uri("http://" + ip + ":" + context.LocalAddress.port + "/test/test.mp3").ToString();
            //        item.AddResource(resource);
            //    }

            //    var didl = Didl.header + item.ToDidl(filter) + Didl.footer;
            //    action.SetArgumentValue("Result", didl);
            //    action.SetArgumentValue("NumberReturned", "1");
            //    action.SetArgumentValue("TotalMatches", "1");

            //    // update ID may be wrong here, it should be the one of the container?
            //    // TODO: We need to keep track of the overall updateID of the CDS
            //    action.SetArgumentValue("UpdateId", "1");

            //    return 0;
            //}

            // return -1;

        }

        int ms_BrowseDirectChildren(Platinum.Action action, string object_id, string filter, int starting_index, int requested_count, string sort_criteria, HttpRequestContext context) {
            var didl = Didl.header;
            try {
                int count = 0;
                foreach (var node in Root.BreadthFirstNodeEnumerator.First(o => o.Value.ObjectID == object_id).Nodes) {
                    didl += node.Value.ToDidl(filter);
                    count++;

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
                        resource.URI = new Uri("http://" + ip + ":" + context.LocalAddress.port + "/" + node.Value.ObjectID).ToString();
                        node.Value.AddResource(resource);
                    }
                }
                didl += Didl.footer;
                action.SetArgumentValue("Result", didl);
                action.SetArgumentValue("NumberReturned", count.ToString());
                action.SetArgumentValue("TotalMatches", count.ToString());
                action.SetArgumentValue("UpdateId", "1");
                return 0;
            } catch {
                return -1;
            }

            //if (Root.Nodes.Find(object_id, true).Count() > 0) {
            //    var didl = Didl.header;
            //    foreach (var node in Root.Nodes.Find(object_id, true)[0].Nodes) {
            //        if (node is ContainerNode) {
            //            didl += ((ContainerNode)node).MediaContainer.ToDidl(filter);
            //        }
            //    }
            //    didl += Didl.footer;
            //    action.SetArgumentValue("Result", didl);
            //    action.SetArgumentValue("NumberReturned", Root.Nodes.Find(object_id, true)[0].Nodes.Count.ToString());
            //    action.SetArgumentValue("TotalMatches", Root.Nodes.Find(object_id, true)[0].Nodes.Count.ToString());
            //    action.SetArgumentValue("UpdateId", "1");
            //    return 0;
            //} else {
            //    return -1;
            //}
            //if (object_id != "0") return -1;

            //var item = new MediaItem();
            //item.Title = "Item";
            //item.ObjectID = "1";
            //item.ParentID = "0";
            //item.Class = new ObjectClass("object.item.audioItem.musicTrack", "");

            //var resource = new MediaResource();
            //resource.ProtoInfo = ProtocolInfo.GetProtocolInfoFromMimeType("audio/mp3", true, context);

            //// get list of ips and make sure the ip the request came from is used for the first resource returned
            //// this ensures that clients which look only at the first resource will be able to reach the item
            //List<String> ips = UPnP.GetIpAddresses(true);
            //String localIP = context.LocalAddress.ip;
            //if (localIP != "0.0.0.0") {
            //    ips.Remove(localIP);
            //    ips.Insert(0, localIP);
            //}

            //// iterate through all ips and create a resource for each
            //foreach (String ip in ips) {
            //    resource.URI = new Uri("http://" + ip + ":" + context.LocalAddress.port + "/test/test.mp3").ToString();
            //    item.AddResource(resource);
            //}

            //var didl = Didl.header + item.ToDidl(filter) + Didl.footer;
            //action.SetArgumentValue("Result", didl);
            //action.SetArgumentValue("NumberReturned", "1");
            //action.SetArgumentValue("TotalMatches", "1");

            //// update ID may be wrong here, it should be the one of the container?
            //// TODO: We need to keep track of the overall updateID of the CDS
            //action.SetArgumentValue("UpdateId", "1");

            //return 0;
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
                Title = "Album",
                ChildrenCount = 10,
                Class = new ObjectClass("object.container.album.musicAlbum", "Album")
            });
            albums.NodesAccessedInitially += new DTreeNode<MediaObject>.NodesAccessedInitiallyHandler(albums_NodesAccessedInitially);
            return root;
        }

        void albums_NodesAccessedInitially(DTreeNode<MediaObject> parentNode, ref DTreeNodeCollection<MediaObject> nodesCollection) {
            MusicDatabase db = MusicDatabase.Instance;
            List<AlbumInfo> albums = new List<AlbumInfo>();
            db.GetAllAlbums(ref albums);
            string parentID = string.Join("/", parentNode.GetIndexPath().Select(x => x.ToString()).ToArray());
            int i = 0;
            foreach (var album in albums) {
                MediaContainer currentAlbum = new MediaContainer() {
                    Title = album.Album,
                    ParentID = parentID,
                    ObjectID = parentID + "/" + i++.ToString(),
                    Class = new ObjectClass("object.container.album.musicAlbum", "Album")
                };
                DTreeNode<MediaObject> albumNode = nodesCollection.Add(currentAlbum);
                albumNode.NodesAccessedInitially += album_NodesAccessedInitially;
            }
        }

        void album_NodesAccessedInitially(DTreeNode<MediaObject> parentNode, ref DTreeNodeCollection<MediaObject> nodesCollection) {
            MusicDatabase db = MusicDatabase.Instance;
            ArrayList songs = new ArrayList();
            db.GetSongsByAlbum(parentNode.Value.Title, ref songs);
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
