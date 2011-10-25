#region #region Copyright (C) 2005-2011 Team MediaPortal

// 
// Copyright (C) 2005-2011 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MediaPortal is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MediaPortal is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.
// 

#endregion

using MediaPortal.GUI.Library;
using MyUPnPSupport.Plugin.Config;
using MyUPnPSupport.Plugin.Window.Items;
using WindowPlugins;
using MyUPnPSupport.UPnP;
using System.Windows.Forms;

namespace MyUPnPSupport.Plugin.Window {
    public partial class Main : WindowPluginBase {
        public Main() {
            GetID = Settings.PLUGIN_WINDOW_ID;
        }

        #region Enums

        #endregion

        #region Skin Controls

        [SkinControlAttribute(2011)]
        protected GUIImage ctrlBackgroundImage;

        #endregion

        #region GUIWindow Base Class Overrides

        public override bool SupportsDelayedLoad {
            get { return true; }
        }

        protected override string SerializeName {
            get { return Settings.PLUGIN_NAME; }
        }

        UPnPDeviceManager DeviceManager { get; set; }

        public override void DeInit() {
            base.DeInit();
            DeviceManager.Dispose();
            DeviceManager = null;
        }
        public override bool Init() {
            LoadSettings();
            DeviceManager = new UPnPDeviceManager(Settings.DMR_ENABLE, Settings.DMS_ENABLE, Settings.DMC_ENABLE);
            DeviceManager.StartDevices();
            return Load(Settings.SKINFILE_MAIN_WINDOW);
        }

        protected override void LoadSettings() {
            Settings.Load();
        }

        protected override void OnPageLoad() {
            base.OnPageLoad();
            GUIPropertyManager.SetProperty("#currentmodule", Settings.PLUGIN_NAME);
            GUIPropertyManager.SetProperty("#MyUPnPSupport.Buffering.State", string.Empty);
            RegisterEventHandlers();
        }

        protected override void OnPageDestroy(int new_windowId) {
            base.OnPageDestroy(new_windowId);
            UnRegisterEventHandlers();
        }

        protected override void OnShowViews() {
        }

        protected override void SwitchLayout() {
            //Navigation.CurrentItem.PreferredLayout = new Settings.PlexSectionLayout
            //                                             {
            //                                                 Layout = CurrentLayout,
            //                                                 Section = Navigation.CurrentItem.PreferredLayout.Section
            //                                             };
            //switch (Navigation.CurrentItem.PreferredLayout.Section) {
            //    case Settings.SectionType.Music:
            //        Load(GUIGraphicsContext.Skin + @"\MyUPnPSupport.Music.xml");
            //        break;
            //    case Settings.SectionType.Video:
            //        Load(GUIGraphicsContext.Skin + @"\MyUPnPSupport.Videos.xml");
            //        break;
            //    case Settings.SectionType.Photo:
            //        Load(GUIGraphicsContext.Skin + @"\MyUPnPSupport.Photos.xml");
            //        break;
            //    default:
            //        Load(Settings.SKINFILE_MAIN_WINDOW);
            //        break;
            //}
            base.SwitchLayout();
        }

        protected override bool AllowLayout(GUIFacadeControl.Layout layout) {
            switch (layout) {
                case GUIFacadeControl.Layout.CoverFlow:
                case GUIFacadeControl.Layout.Filmstrip:
                case GUIFacadeControl.Layout.LargeIcons:
                case GUIFacadeControl.Layout.List:
                case GUIFacadeControl.Layout.SmallIcons:
                    return true;

                default:
                    return false;
            }
        }

        protected override void OnInfo(int iItem) {
            if (facadeLayout[iItem] is IMenuItem) {
                ((IMenuItem)facadeLayout[iItem]).Parent.LastSelectedChildIndex = iItem;
                ((IMenuItem)facadeLayout[iItem]).OnInfo();
            } else {
                base.OnInfo(iItem);
            }
        }

        public override void OnAction(MediaPortal.GUI.Library.Action action) {
            switch (action.wID) {
                case MediaPortal.GUI.Library.Action.ActionType.ACTION_PREVIOUS_MENU:
                    //if (Navigation.CurrentItem == Navigation.RootItem) {
                    //    base.OnAction(action);
                    //    return;
                    //}
                    //Navigation.FetchPreviousMenu(Navigation.CurrentItem, facadeLayout.SelectedListItemIndex);
                    break;
                default:
                    base.OnAction(action);
                    break;
            }
        }

        protected override void OnClick(int iItem) {
            GUIListItem item = facadeLayout[iItem];
            if (facadeLayout[iItem] is IMenuItem) {
                ((IMenuItem)facadeLayout[iItem]).Parent.LastSelectedChildIndex = iItem;
                ((IMenuItem)facadeLayout[iItem]).OnClicked(this, null);
            } else {
                base.OnClick(iItem);
            }
        }

        protected override void OnShowContextMenu() {
            //Navigation.ShowCurrentContextMenu();
        }

        #endregion

        #region Private Methods

        private void RegisterEventHandlers() {
            //PlexInterface.OnPlexError += PlexInterface_OnPlexError;
            //PlexInterface.OnResponseProgress += PlexInterface_OnResponseProgress;
            //PlexItemBase.OnHasBackground += MenuItem_OnHasBackground;
            //MenuItem.OnMenuItemSelected += MenuItem_OnMenuItemSelected;
            //Navigation.OnMenuItemsFetchStarted += Navigation_OnMenuItemsFetchStarted;
            //Navigation.OnMenuItemsFetchCompleted += Navigation_OnMenuItemsFetchCompleted;
            //Navigation.OnErrorOccured += PlexInterface_OnPlexError;
        }

        private void UnRegisterEventHandlers() {
            //PlexInterface.OnPlexError -= PlexInterface_OnPlexError;
            //PlexInterface.OnResponseProgress -= PlexInterface_OnResponseProgress;
            //PlexItemBase.OnHasBackground -= MenuItem_OnHasBackground;
            //MenuItem.OnMenuItemSelected -= MenuItem_OnMenuItemSelected;
            //Navigation.OnMenuItemsFetchStarted -= Navigation_OnMenuItemsFetchStarted;
            //Navigation.OnMenuItemsFetchCompleted -= Navigation_OnMenuItemsFetchCompleted;
            //Navigation.OnErrorOccured -= PlexInterface_OnPlexError;
        }

        #endregion

        #region Plugin Event Handlers

        #endregion
    }
}