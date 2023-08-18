using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region MainView_SettingCommand

        /// <summary>
        /// 설정 창_뷰 전환 ICommand
        /// </summary>
        public ICommand MainView_SettingCommand { get; set; }

        /// <summary>
        /// 설정 창_뷰 전환 Command 실행 시
        /// </summary>
        /// <param name="index">설정 탭 index
        /// <para>Type: int</para></param>
        private void MainView_OnSetting()
        {
            dataThread.PauseThread("SearchData");
            dataThread.PauseThread("SearchUnitInfo");
            Window_SwitchView = 1;
        }

        #endregion

        #endregion

        #region Data

        #region MainView_SettingImage

        public string MainView_SettingImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/setting_icon.png"; }
        }

        #endregion

        #region MainView_Popup

        private int MainView_popup = 0;

        /// <summary>
        /// 설정 창_팝업 전환 Property
        /// <para>Type: int</para>
        /// </summary>
        public int MainView_Popup
        {
            get
            {
                return MainView_popup;
            }
            set
            {
                MainView_popup = value;
                OnPropertyChanged("MainView_Popup");
            }
        }


        #endregion

        #region MainView_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> MainView_VersionList
        {
            get;
            set;
        }

        #endregion

        #region MainView_SelectedVersionItem

        private VersionItem MainView_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem MainView_SelectedVersionItem
        {
            get { return MainView_selectedVersionItem; }
            set
            {
                MainView_selectedVersionItem = value;
                
                OnPropertyChanged("MainView_SelectedVersionItem");
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_MainView()
        {
            MainView_VersionList = new ObservableCollection<VersionItem>();
            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    MainView_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
            MainView_SelectedVersionItem = MainView_VersionList[0];
            MainView_SettingCommand = new RelayCommand<object>(p => MainView_OnSetting());
            dataThread.CreateThread(SearchData, 300);
            dataThread.ResumeThread("SearchData");
        }

        public void SearchData()
        {
            if (MainView_selectedVersionItem != null)
            {
                ph[MainView_SelectedVersionItem.Index].CheckProcessInfo();
                if (ph[MainView_SelectedVersionItem.Index].process.processState == 1)
                {
                    uah[MainView_SelectedVersionItem.Index].SearchAddress(uh[MainView_SelectedVersionItem.Index], ph[MainView_SelectedVersionItem.Index]);
                    rah[MainView_SelectedVersionItem.Index].gold.SearchAddress(rh[MainView_SelectedVersionItem.Index], ph[MainView_SelectedVersionItem.Index]);
                    rah[MainView_SelectedVersionItem.Index].tree.SearchAddress(rh[MainView_SelectedVersionItem.Index], ph[MainView_SelectedVersionItem.Index]);
                    gah[MainView_SelectedVersionItem.Index].SearchAddress(gh[MainView_SelectedVersionItem.Index], ph[MainView_SelectedVersionItem.Index]);
                    gh[MainView_SelectedVersionItem.Index].ResearchGameState(ph[MainView_SelectedVersionItem.Index]);
                    uh[MainView_SelectedVersionItem.Index].SearchUnitNum(ph[MainView_SelectedVersionItem.Index], gh[MainView_SelectedVersionItem.Index]);
                    rh[MainView_SelectedVersionItem.Index].SearchResources(ph[MainView_SelectedVersionItem.Index], gh[MainView_SelectedVersionItem.Index]);
                    uh[MainView_SelectedVersionItem.Index].SearchUnitMix(rh[MainView_SelectedVersionItem.Index]);
                }
            }
        }

        #endregion
    }
}
