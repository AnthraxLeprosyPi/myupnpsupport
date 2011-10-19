/*   
Copyright 2006 - 2010 Intel Corporation

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections;
using OpenSource.UPnP.AV.CdsMetadata;

namespace OpenSource.UPnP.AV.RENDERER.CP
{
    /// <summary>
    /// Summary description for AVPlayList.
    /// </summary>
    public class AVPlayList
    {
        public static bool EnableExtendedM3U = true;
        class ToXmlData_Custom : ToXmlData
        {
            public Hashtable Mappings = new Hashtable();
        }



        private Hashtable FileInfoTable = new Hashtable();

        private object _Tag = null;
        private AVConnection.CurrentURIChangedHandler CurrentURIChangedHandler;

        private ManualResetEvent SetURI_Event = new ManualResetEvent(false);
        private ManualResetEvent SetURIEvent_Event = new ManualResetEvent(false);

        private object UriLock = new object();
        private object ConnectionLock = new object();

        private void PrintExtInfLine(StringBuilder M3U, IMediaResource R)
        {
            M3U.Append("#EXTINF:");
            if (R.HasDuration)
            {
                M3U.Append(Math.Ceiling(R.Duration.m_Value.TotalSeconds));
            }
            else
            {
                M3U.Append("-1");
            }

            if (R.Owner.Creator != "")
            {
                //				M3U.AppendFormat(",{0} - {1}\r\n", "SkippyCreator", "SkippyTitle");
                M3U.AppendFormat(",{0} - {1}\r\n", R.Owner.Creator, R.Owner.Title);
            }
            else
            {
                //				M3U.AppendFormat(",{0}\r\n", "SkippyTitle");
                M3U.AppendFormat(",{0}\r\n", R.Owner.Title);
            }
        }

        public class NoSupportedPlayListFormats : Exception
        {
            public NoSupportedPlayListFormats(String msg)
                : base(msg)
            {
            }
        }
        public enum PlayListMode
        {
            ASX,
            M3U,
            NEXT_URI,
            SINGLE_URI,
            NOT_A_PLAYLIST,
            NOT_SUPPORTED,
        }

        public PlayListMode CurrentPlayListMode
        {
            get
            {
                return (_CurrentPlayListMode);
            }
        }

        protected Hashtable TableOfHandles = Hashtable.Synchronized(new Hashtable());
        protected PlayListMode _CurrentPlayListMode = PlayListMode.NOT_SUPPORTED;
        public bool IsRecycled = false;
        protected AVRenderer _AVR;
        protected IMediaResource[] _Resources;
        protected MiniWebServer MWS;
        protected string M3UString = null;
        protected string PlayListUri = "";
        protected string PlayListMetaData = "";

        public delegate void ReadyHandler(AVPlayList sender, AVConnection c, object Tag);
        public event ReadyHandler OnReady;

        public delegate void FailedHandler(AVPlayList sender, AVRenderer.CreateFailedReason reason);
        public event FailedHandler OnFailed;

        protected int Handle = 0;
        protected int URIHandle = 0;
        protected int SingleURIHandle = 0;

        protected Queue FakePlayQueue = new Queue();

        public int PlayListHandle
        {
            get
            {
                return (Handle);
            }
        }

        ~AVPlayList()
        {
            if (MWS != null) MWS.Dispose();
        }
        public void Dispose()
        {
            if (MWS != null) MWS.Dispose();

            OnReady = null;
            OnFailed = null;
            _AVR = null;
        }

        public AVPlayList(AVConnection AVC, IMediaResource[] r, ReadyHandler rh, FailedHandler fh, object Tag)
        {
            OpenSource.Utilities.InstanceTracker.Add(this);
            AVRenderer AVR = AVC.Parent;
            _AVR = AVR;

            this.CurrentURIChangedHandler = new AVConnection.CurrentURIChangedHandler(UriEventSink);
            _Tag = Tag;

            /*
            ArrayList branches = new ArrayList();
            ToXmlData_Custom txdc = new ToXmlData_Custom();
            foreach(IMediaResource R in r)
            {
                if(branches.Contains(R.Owner)==false)
                {
                    branches.Add(R.Owner);
                }
                txdc.Mappings[R] = false;
            }

            MediaBuilder.container container = new MediaBuilder.container("Autogenerated Playlist");
            container.ID = Guid.NewGuid().ToString();
            container.IdIsValid = true;
            ToXmlFormatter txf = new ToXmlFormatter();
            txf.WriteResource = new ToXmlFormatter.ToXml_FormatResource(WriteResource);
            txdc.DesiredProperties = new ArrayList();
            txdc.IsRecursive = true;
            txdc.VirtualOwner =  MediaBuilder.CreateContainer(container);
			
            // Obtain the DIDL-Lite MetaData
            this.PlayListMetaData = MediaBuilder.BuildDidl(txf, txdc, branches);
            if(PlayListMetaData.Length>32768) PlayListMetaData = "";
            */

            int TempHandle = this.GetHashCode();
            bool Done = false;
            OnReady += rh;
            OnFailed += fh;

            AVR.OnCreateConnection2 += new AVRenderer.ConnectionHandler(ConnectionSink);
            AVR.OnRecycledConnection2 += new AVRenderer.ConnectionHandler(RecycledSink);
            AVR.OnCreateConnectionFailed2 += new AVRenderer.FailedConnectionHandler(FailedSink);

            if (r.Length == 1)
            {
                Done = true;
                _CurrentPlayListMode = PlayListMode.NOT_A_PLAYLIST;
                PlayListUri = ConvertLocalFileToHTTPResource(r[0]);
                lock (ConnectionLock)
                {
                    TableOfHandles[TempHandle] = TempHandle;
                    ConnectionSink(_AVR, AVC, TempHandle);
                }
            }

            if ((Done == false && AVR.SupportsProtocolInfo(new ProtocolInfoString("http-get:*:audio/mpegurl:*")))
                ||
                (Done == false && AVR.SupportsProtocolInfo(new ProtocolInfoString("http-get:*:audio/x-mpegurl:*"))))
            {
                // Build M3U
                Done = true;
                _CurrentPlayListMode = PlayListMode.M3U;

                CheckMiniWebServer();

                StringBuilder M3U = new StringBuilder();
                if (AVPlayList.EnableExtendedM3U)
                {
                    M3U.Append("#EXTM3U\r\n");
                }
                foreach (IMediaResource R in r)
                {
                    if (AVPlayList.EnableExtendedM3U)
                    {
                        PrintExtInfLine(M3U, R);
                    }
                    M3U.Append(ConvertLocalFileToHTTPResource(R) + "\r\n");
                }

                M3UString = M3U.ToString();
                PlayListUri = "http://" + AVR.Interface.ToString() + ":" + MWS.LocalIPEndPoint.Port.ToString() + "/item.m3u";

                lock (ConnectionLock)
                {
                    TableOfHandles[TempHandle] = TempHandle;
                    ConnectionSink(_AVR, AVC, TempHandle);
                }
            }

            // Use SINGLE_URI
            if (Done == false)
            {
                _CurrentPlayListMode = PlayListMode.SINGLE_URI;
                foreach (IMediaResource rsc in r)
                {
                    FakePlayQueue.Enqueue(rsc);
                }
                PlayListUri = r[0].ContentUri;
                FakePlayQueue.Dequeue();
                lock (ConnectionLock)
                {
                    TableOfHandles[TempHandle] = TempHandle;
                    ConnectionSink(_AVR, AVC, TempHandle);
                }
            }
        }


        public AVPlayList(AVRenderer AVR, IMediaResource[] r, ReadyHandler rh, FailedHandler fh, object Tag)
        {
            OpenSource.Utilities.InstanceTracker.Add(this);
            _AVR = AVR;
            string UseThisProtocolInfo = "";
            _Tag = Tag;

            /*
            ArrayList branches = new ArrayList();
            ToXmlData_Custom txdc = new ToXmlData_Custom();
            foreach(IMediaResource R in r)
            {
                if(branches.Contains(R.Owner)==false)
                {
                    branches.Add(R.Owner);
                }
                txdc.Mappings[R] = false;
            }

            MediaBuilder.container container = new MediaBuilder.container("Autogenerated Playlist");
            container.ID = Guid.NewGuid().ToString();
            container.IdIsValid = true;
            ToXmlFormatter txf = new ToXmlFormatter();
            txf.WriteResource = new ToXmlFormatter.ToXml_FormatResource(WriteResource);
            txdc.DesiredProperties = new ArrayList();
            txdc.IsRecursive = true;
            txdc.VirtualOwner =  MediaBuilder.CreateContainer(container);
			
            // Obtain the DIDL-Lite MetaData
            this.PlayListMetaData = MediaBuilder.BuildDidl(txf, txdc, branches);
            if(PlayListMetaData.Length>32768) PlayListMetaData = "";
            */

            int TempHandle = this.GetHashCode();
            bool Done = false;
            OnReady += rh;
            OnFailed += fh;

            //Subscribe to the internal events on the Renderer to initiate a connection
            AVR.OnCreateConnection2 += new AVRenderer.ConnectionHandler(ConnectionSink);
            AVR.OnRecycledConnection2 += new AVRenderer.ConnectionHandler(RecycledSink);
            AVR.OnCreateConnectionFailed2 += new AVRenderer.FailedConnectionHandler(FailedSink);

            if (r.Length == 1)
            {
                // There is only one resource, so no need to build a playlist
                Done = true;
                _CurrentPlayListMode = PlayListMode.NOT_A_PLAYLIST;
                PlayListUri = ConvertLocalFileToHTTPResource(r[0]);
                lock (ConnectionLock)
                {
                    TableOfHandles[TempHandle] = TempHandle;
                    AVR.CreateConnection(r[0].ProtocolInfo.ToString(), "", -1, TempHandle);
                }
            }

            if ((Done == false && AVR.SupportsProtocolInfo(new ProtocolInfoString("http-get:*:audio/mpegurl:*")))
                ||
                (Done == false && AVR.SupportsProtocolInfo(new ProtocolInfoString("http-get:*:audio/x-mpegurl:*"))))
            {
                // Build M3U
                Done = true;
                _CurrentPlayListMode = PlayListMode.M3U;

                CheckMiniWebServer();

                //OpenSource.Utilities.InstanceTracker.StartTimer();

                StringBuilder M3U = new StringBuilder();
                if (AVPlayList.EnableExtendedM3U)
                {
                    M3U.Append("#EXTM3U\r\n");
                }
                foreach (IMediaResource R in r)
                {
                    if (AVPlayList.EnableExtendedM3U)
                    {
                        PrintExtInfLine(M3U, R);
                    }
                    M3U.Append(ConvertLocalFileToHTTPResource(R) + "\r\n");
                }

                M3UString = M3U.ToString();

                //OpenSource.Utilities.InstanceTracker.StopTimer("Build M3U");

                PlayListUri = "http://" + AVR.Interface.ToString() + ":" + MWS.LocalIPEndPoint.Port.ToString() + "/item.m3u";

                if (AVR.SupportsProtocolInfo(new ProtocolInfoString("http-get:*:audio/x-mpegurl:*")))
                    UseThisProtocolInfo = "http-get:*:audio/x-mpegurl:*";
                if (AVR.SupportsProtocolInfo(new ProtocolInfoString("http-get:*:audio/mpegurl:*")))
                    UseThisProtocolInfo = "http-get:*:audio/mpegurl:*";

                //OpenSource.Utilities.InstanceTracker.StartTimer();

                lock (ConnectionLock)
                {
                    TableOfHandles[TempHandle] = TempHandle;
                    AVR.CreateConnection(UseThisProtocolInfo, "/", -1, TempHandle);
                }
            }

            if (Done == false)
            {
                // Fake a play list by switching Uri's on the fly

                _CurrentPlayListMode = PlayListMode.SINGLE_URI;
                foreach (IMediaResource rsc in r)
                {
                    FakePlayQueue.Enqueue(rsc);
                }
                PlayListUri = r[0].ContentUri;
                FakePlayQueue.Dequeue();
                lock (ConnectionLock)
                {
                    TableOfHandles[TempHandle] = TempHandle;
                    AVR.CreateConnection(r[0].ProtocolInfo.ToString(), "/", -1, TempHandle);
                }
            }
        }

        /// <summary>
        /// This method provides a custom implementation for printing resources.
        /// This custom implementation makes it so that the importUri attribute
        /// is not printed and also makes it so that the contentUri field
        /// is a relative
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="formatter"></param>
        /// <param name="data"></param>
        /// <param name="xmlWriter"></param>
        private void WriteResource(IMediaResource resource, ToXmlFormatter formatter, object data, XmlTextWriter xmlWriter)
        {
            ToXmlData_Custom _d = (ToXmlData_Custom)data;
            Tags T = Tags.GetInstance();
            xmlWriter.WriteStartElement(T[_DIDL.Res]);

            // write everything but importUri
            foreach (string attribName in resource.ValidAttributes)
            {
                if (attribName != T[_RESATTRIB.importUri])
                {
                    StringBuilder sb = new StringBuilder(20);
                    sb.AppendFormat("{0}@{1}", T[_DIDL.Res], attribName);
                    string filterName = sb.ToString();

                    if (_d.DesiredProperties == null)
                    {
                    }
                    else if ((_d.DesiredProperties.Count == 0) || _d.DesiredProperties.Contains(filterName))
                    {
                        xmlWriter.WriteAttributeString(attribName, resource[attribName].ToString());
                    }
                }
            }

            // write content uri as the value of the 'res' element
            // and note the mapping
            /*
            StringBuilder uri = new StringBuilder();
            uri.AppendFormat("{0}{1}", _d.BaseUri, _d.i);
            string str = uri.ToString();
            xmlWriter.WriteString(str);
            _d.Mappings[str] = resource;
            _d.i++;
            */
            if ((bool)_d.Mappings[resource] == false)
            {
                xmlWriter.WriteString(this.ConvertLocalFileToHTTPResource(resource));
                _d.Mappings[resource] = true;
            }

            xmlWriter.WriteEndElement();
        }

        public string GetFilenameFromURI(Uri MediaURI)
        {
            string request = MediaURI.PathAndQuery;
            int i = request.LastIndexOf("/");
            string tmp = request.Substring(1, i - 1);
            if (tmp.IndexOf("/") != -1)
            {
                tmp = tmp.Substring(0, tmp.IndexOf("/"));
            }

            if (FileInfoTable.ContainsKey(tmp))
            {
                if (FileInfoTable[tmp].GetType() == typeof(FileInfo))
                {
                    return (((FileInfo)FileInfoTable[tmp]).FullName);
                }
                else
                {
                    return ((string)FileInfoTable[tmp]);
                }
            }
            else
            {
                return (null);
            }
        }

        private string ConvertLocalFileToHTTPResource(IMediaResource resource)
        {
            if (resource.ContentUri.StartsWith(MediaResource.AUTOMAPFILE))
            {
                // Map File
                string tempName = resource.ContentUri.Substring(MediaResource.AUTOMAPFILE.Length);
                string fileName = tempName.GetHashCode().ToString() + "/" + tempName.Substring(tempName.LastIndexOf("\\") + 1);

                if (resource.Owner != null)
                {
                    fileName = tempName.GetHashCode().ToString() + "/" + resource.Owner.Creator + "  -  " + resource.Owner.Title + tempName.Substring(tempName.LastIndexOf("."));
                    //					fileName = tempName.GetHashCode().ToString() + "/" + tempName.Substring(tempName.LastIndexOf("."));			
                }
                FileInfoTable[tempName.GetHashCode().ToString()] = tempName;
                CheckMiniWebServer();

                string path = "http://" + _AVR.device.InterfaceToHost.ToString() + ":" + MWS.LocalIPEndPoint.Port.ToString() + "/" + fileName;
                return (HTTPMessage.EscapeString(path));
            }
            else
            {
                // Don't Map File
                return (resource.ContentUri);
            }
        }
        /// <summary>
        /// This instantiates a MiniWebServer if one is not already instantiated
        /// </summary>
        private void CheckMiniWebServer()
        {
            if (MWS == null)
            {
                MWS = new MiniWebServer(new IPEndPoint(_AVR.device.InterfaceToHost, NetworkInfo.GetFreePort(50000, 65500, _AVR.device.InterfaceToHost)));
                MWS.OnReceive += new MiniWebServer.HTTPReceiveHandler(ReceiveSink);
            }
        }

        /// <summary>
        /// This get called when the URI events are received
        /// </summary>
        /// <param name="sender"></param>
        private void UriEventSink(AVConnection sender)
        {
            sender.OnCurrentURIChanged -= new AVConnection.CurrentURIChangedHandler(UriEventSink);
            this.SetURIEvent_Event.Set();

            if (SetURI_Event.WaitOne(0, false) == true)
            {
                //				OpenSource.Utilities.InstanceTracker.StopTimer("SetAVTransport+Event [evt]");
                if (OnReady != null) OnReady(this, sender, _Tag);
                OnReady = null;
            }
        }

        /// <summary>
        /// This is called when PrepareForConnection failed on the renderer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        /// <param name="h"></param>
        protected void FailedSink(AVRenderer sender, AVRenderer.CreateFailedReason reason, object h)
        {
            lock (ConnectionLock)
            {
                if (TableOfHandles.ContainsKey(h) == false) return;
            }

            _AVR.OnCreateConnectionFailed2 -= new AVRenderer.FailedConnectionHandler(FailedSink);
            if (OnFailed != null) OnFailed(this, reason);

        }
        /// <summary>
        /// This is called when PrepareForConnection was successful
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="c"></param>
        /// <param name="h"></param>
        protected void ConnectionSink(AVRenderer sender, AVConnection c, object h)
        {
            lock (ConnectionLock)
            {
                if (TableOfHandles.ContainsKey(h) == false) return;
            }

            //			OpenSource.Utilities.InstanceTracker.StopTimer("PrepareForConnection");

            _AVR.OnCreateConnection2 -= new AVRenderer.ConnectionHandler(ConnectionSink);
            c.OnCurrentURIChanged += new AVConnection.CurrentURIChangedHandler(UriEventSink);

            if (this.CurrentPlayListMode == PlayListMode.SINGLE_URI)
            {
                c.OnPlayStateChanged += new AVConnection.PlayStateChangedHandler(PlayStateSink);
            }
            c.CurrentPlayList = this;

            OpenSource.Utilities.InstanceTracker.StartTimer();

            //			c.SetAVTransportURI(PlayListUri, "", c, new CpAVTransport.Delegate_OnResult_SetAVTransportURI(SetURISink));
            c.SetAVTransportURI(PlayListUri, PlayListMetaData, c, new CpAVTransport.Delegate_OnResult_SetAVTransportURI(SetURISink));

        }
        /// <summary>
        /// This is called when PrepareForConnection was successful and
        /// returned a recycled connection id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="c"></param>
        /// <param name="h"></param>
        protected void RecycledSink(AVRenderer sender, AVConnection c, object h)
        {
            lock (ConnectionLock)
            {
                if (TableOfHandles.ContainsKey(h) == false) return;
            }

            _AVR.OnRecycledConnection2 -= new AVRenderer.ConnectionHandler(RecycledSink);

            IsRecycled = true;
            c.OnCurrentURIChanged += new AVConnection.CurrentURIChangedHandler(UriEventSink);

            if (this.CurrentPlayListMode == PlayListMode.SINGLE_URI)
            {
                c.OnPlayStateChanged += new AVConnection.PlayStateChangedHandler(PlayStateSink);
            }
            c.CurrentPlayList = this;

            //			c.SetAVTransportURI(PlayListUri, "", c, new CpAVTransport.Delegate_OnResult_SetAVTransportURI(this.SetURISink));
            c.SetAVTransportURI(PlayListUri, PlayListMetaData, c, new CpAVTransport.Delegate_OnResult_SetAVTransportURI(this.SetURISink));
        }
        /// <summary>
        /// This method gets called when the PlayState of the connection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NewState"></param>
        protected void PlayStateSink(AVConnection sender, AVConnection.PlayState NewState)
        {
            if (NewState == AVConnection.PlayState.STOPPED)
            {
                if (FakePlayQueue.Count == 0) return;

                if (sender.MediaResource.ContentUri != PlayListUri)
                {
                    // Somebody else changed the Uri from under us, so we have no choice
                    // but to give up
                    FakePlayQueue.Clear();
                    return;
                }

                IMediaResource r = (IMediaResource)FakePlayQueue.Dequeue();
                PlayListUri = r.ContentUri;
                //ToDo: Add MetaData
                sender.SetAVTransportURI(r.ContentUri, "", sender, new CpAVTransport.Delegate_OnResult_SetAVTransportURI(FakePlayListSink));
            }
        }
        /// <summary>
        /// This method gets called when a new Uri has been set, when faking a playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="InstanceID"></param>
        /// <param name="CurrentURI"></param>
        /// <param name="CurrentURIMetaData"></param>
        /// <param name="e"></param>
        /// <param name="Tag"></param>
        protected void FakePlayListSink(CpAVTransport sender, System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData, UPnPInvokeException e, object Tag)
        {

            if (e == null)
            {
                ((AVConnection)Tag).Play();
            }

        }
        /// <summary>
        /// This method gets called when SetAVTransportURI completes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="InstanceID"></param>
        /// <param name="CurrentURI"></param>
        /// <param name="CurrentURIMetaData"></param>
        /// <param name="e"></param>
        /// <param name="Tag"></param>
        protected void SetURISink(CpAVTransport sender, System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData, UPnPInvokeException e, object Tag)
        {
            if (e == null)
            {
                SetURI_Event.Set();
                if (SetURIEvent_Event.WaitOne(0, false) == true)
                {
                    //					OpenSource.Utilities.InstanceTracker.StopTimer("SetAVTransport+Event");
                    if (OnReady != null) OnReady(this, (AVConnection)Tag, _Tag);
                    OnReady = null;
                }
            }
            else
            {
                if (OnFailed != null) OnFailed(this, AVRenderer.CreateFailedReason.SetAVTransport_FAILED);
                OnFailed = null;
                OnReady = null;
            }
        }
        /// <summary>
        /// This method gets called when we are done streaming the media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="S"></param>
        protected void DoneSink(HTTPSession sender, Stream S)
        {
            sender.CloseStreamObject(S);
            sender.Close();
        }
        /// <summary>
        /// This method gets called when a new Request is received
        /// </summary>
        /// <param name="request"></param>
        /// <param name="WebSession"></param>
        protected void ReceiveSink(HTTPMessage request, HTTPSession WebSession)
        {
            HTTPMessage rsp = new HTTPMessage();
            UTF8Encoding U = new UTF8Encoding();

            if ((request.Directive == "HEAD") ||
                (request.Directive == "GET"))
            {
                if (request.DirectiveObj == "/item.m3u")
                {/*
					rsp.StatusCode = 200;
					rsp.StatusData = "OK";
					rsp.ContentType = "audio/mpegurl";
					rsp.StringBuffer = M3UString;
					*/

                    string r = request.GetTag("Range");
                    if (r == "")
                    {
                        rsp.StatusCode = 200;
                        rsp.StatusData = "OK";
                        rsp.ContentType = "audio/mpegurl";
                        rsp.StringBuffer = M3UString;
                    }
                    else
                    {
                        rsp.StatusCode = 206;
                        rsp.StatusData = "Partial Content";
                        rsp.ContentType = "audio/mpegurl";

                        DText rp0 = new DText();
                        rp0.ATTRMARK = "=";
                        rp0[0] = r;
                        DText rp = new DText();
                        rp.ATTRMARK = "-";
                        rp[0] = rp0[2].Trim();
                        if (rp[1] == "")
                        {
                            // LastBytes
                            try
                            {
                                if (int.Parse(rp[2]) > M3UString.Length)
                                {
                                    rsp.AddTag("Content-Range", "bytes 0-" + M3UString.Length.ToString() + "/" + M3UString.Length.ToString());
                                    rsp.StringBuffer = M3UString;
                                }
                                else
                                {
                                    rsp.AddTag("Content-Range", "bytes " + (M3UString.Length - int.Parse(rp[2])).ToString() + "-" + M3UString.Length.ToString() + "/" + M3UString.Length.ToString());
                                    rsp.StringBuffer = M3UString.Substring(M3UString.Length - int.Parse(rp[2]));
                                }
                            }
                            catch (Exception)
                            {
                                rsp = new HTTPMessage();
                                rsp.StatusCode = 400;
                                rsp.StatusData = "Bad Request";
                            }
                        }
                        else if (rp[2] == "")
                        {
                            // Offset till end
                            try
                            {
                                rsp.AddTag("Content-Range", "bytes " + rp[1] + "-" + M3UString.Length.ToString() + "/" + M3UString.Length.ToString());
                                rsp.StringBuffer = M3UString.Substring(int.Parse(rp[1]));
                            }
                            catch (Exception)
                            {
                                rsp = new HTTPMessage();
                                rsp.StatusCode = 400;
                                rsp.StatusData = "Bad Request";
                            }

                        }
                        else
                        {
                            // Range
                            try
                            {
                                if (int.Parse(rp[2]) > M3UString.Length + 1)
                                {
                                    rsp.AddTag("Content-Range", "bytes " + rp[1] + "-" + (M3UString.Length - 1).ToString() + "/" + M3UString.Length.ToString());
                                    rsp.StringBuffer = M3UString.Substring(int.Parse(rp[1]));
                                }
                                else
                                {
                                    rsp.AddTag("Content-Range", "bytes " + rp[1] + "-" + rp[2] + "/" + M3UString.Length.ToString());
                                    rsp.StringBuffer = M3UString.Substring(int.Parse(rp[1]), 1 + int.Parse(rp[2]) - int.Parse(rp[1]));
                                }
                            }
                            catch (Exception)
                            {
                                rsp = new HTTPMessage();
                                rsp.StatusCode = 400;
                                rsp.StatusData = "Bad Request";
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        int i = request.DirectiveObj.LastIndexOf("/");
                        string tmp = request.DirectiveObj.Substring(1, i - 1);
                        bool hasRange = true;
                        if (tmp.IndexOf("/") != -1)
                        {
                            tmp = tmp.Substring(0, tmp.IndexOf("/"));
                        }

                        if (FileInfoTable.ContainsKey(tmp))
                        {
                            FileInfo f;
                            if (FileInfoTable[tmp].GetType() == typeof(string))
                            {
                                f = new FileInfo((string)FileInfoTable[tmp]);
                                FileInfoTable[tmp] = f;
                            }
                            else
                            {
                                f = (FileInfo)FileInfoTable[tmp];
                            }
                            ProtocolInfoString pis = ProtocolInfoString.CreateHttpGetProtocolInfoString(f);
                            rsp.StatusCode = 200;
                            rsp.StatusData = "OK";
                            rsp.ContentType = pis.MimeType;
                            if (request.Directive == "HEAD")
                            {
                                rsp.AddTag("Content-Length", f.Length.ToString());
                                rsp.OverrideContentLength = true;
                            }
                            else
                            {
                                HTTPSession.Range[] RNG = null;
                                if (request.GetTag("Range") != "")
                                {
                                    long x, y;
                                    ArrayList RList = new ArrayList();
                                    DText rp = new DText();
                                    rp.ATTRMARK = "=";
                                    rp.MULTMARK = ",";
                                    rp.SUBVMARK = "-";
                                    rp[0] = request.GetTag("Range");

                                    for (int I = 1; I <= rp.DCOUNT(2); ++I)
                                    {
                                        if (rp[2, I, 1] == "")
                                        {
                                            // Final Bytes
                                            y = long.Parse(rp[2, I, 2].Trim());
                                            x = f.Length - y;
                                            y = x + y;
                                        }
                                        else if (rp[2, I, 2] == "")
                                        {
                                            // Offset till end
                                            x = long.Parse(rp[2, I, 1].Trim());
                                            y = f.Length - x;
                                        }
                                        else
                                        {
                                            // Full Range
                                            x = long.Parse(rp[2, I, 1].Trim());
                                            y = long.Parse(rp[2, I, 2].Trim());
                                        }
                                        RList.Add(new HTTPSession.Range(x, 1 + y - x));
                                    }
                                    RNG = (HTTPSession.Range[])RList.ToArray(typeof(HTTPSession.Range));
                                }
                                else
                                {
                                    hasRange = false;
                                }

                                if (request.Version == "1.0")
                                {
                                    WebSession.OnStreamDone += new HTTPSession.StreamDoneHandler(DoneSink);
                                }
                                //								((HTTPMessage)(new UPnPDebugObject(WebSession)).GetField("Headers")).Version = "1.0";
                                if (hasRange)
                                {
                                    WebSession.SendStreamObject(f.OpenRead(), RNG, pis.MimeType);
                                }
                                else
                                {
                                    FileStream fs = f.OpenRead();
                                    long length = fs.Length;
                                    WebSession.SendStreamObject(fs, length, pis.MimeType);
                                }
                                return;
                            }
                        }
                        else
                        {
                            rsp.StatusCode = 404;
                            rsp.StatusData = "Not Found";
                        }
                    }
                    catch (Exception)
                    {
                        rsp.StatusCode = 404;
                        rsp.StatusData = "Not Found";
                    }
                }
                WebSession.Send(rsp);
                return;
            }

            rsp.StatusCode = 500;
            rsp.StatusData = "Not implemented";
            WebSession.Send(rsp);
        }

    }
}