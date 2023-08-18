using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORD_Helper_Data;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region SettingView_BackCommand

        /// <summary>
        /// 설정 창_뷰 전환 ICommand
        /// </summary>
        public ICommand SettingView_BackCommand { get; set; }

        /// <summary>
        /// 설정 창_뷰 전환 Command 실행 시
        /// </summary>
        /// <param name="index">설정 탭 index
        /// <para>Type: int</para></param>
        private void SettingView_OnBack()
        {
            dataThread.ResumeThread("SearchData");
            dataThread.ResumeThread("SearchUnitInfo");
            Window_SwitchView = 0;
        }

        #endregion

        #region Setting_SwitchViewCommand

        /// <summary>
        /// 설정 창_뷰 전환 ICommand
        /// </summary>
        public ICommand Setting_SwitchViewCommand { get; set; }

        /// <summary>
        /// 설정 창_뷰 전환 Command 실행 시
        /// </summary>
        /// <param name="index">설정 탭 index
        /// <para>Type: int</para></param>
        private void Setting_OnSwitchView(object index)
        {
            Setting_SwitchView = int.Parse(index.ToString());
        }

        #endregion

        #region Setting_PopupCommand

        /// <summary>
        /// 설정 창_팝업 전환 ICommand
        /// </summary>
        public ICommand Setting_PopupCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void Setting_OnPopup(object index)
        {
            Setting_Popup = int.Parse(index.ToString());
        }

        #endregion

        #endregion

        #region Data

        #region SettingView_BackImage

        public string SettingView_BackImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/back_icon.png"; }
        }

        #endregion

        #region Setting_SwitchView

        private int Setting_switchView;

        /// <summary>
        /// 설정 탭 전환 Property
        /// <para>Type: int</para>
        /// </summary>
        public int Setting_SwitchView
        {
            get
            {
                return Setting_switchView;
            }
            set
            {
                Setting_switchView = value;
                if(value == 7)
                    dataThread.ResumeThread("SearchIndex_SearchListMethod");
                else
                    dataThread.PauseThread("SearchIndex_SearchListMethod");
                OnPropertyChanged("Setting_SwitchView");
            }
        }

        #endregion

        #region Setting_popup

        private int Setting_popup = 0;

        /// <summary>
        /// 설정 창_팝업 전환 Property
        /// <para>Type: int</para>
        /// </summary>
        public int Setting_Popup
        {
            get
            {
                return Setting_popup;
            }
            set
            {
                Setting_popup = value;
                
                OnPropertyChanged("Setting_Popup");
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_SettingView()
        {
            Setting_SwitchView = 0;
            Setting_Popup = 0;

            SettingView_BackCommand = new RelayCommand<object>(p => SettingView_OnBack());
            Setting_SwitchViewCommand = new RelayCommand<object>(p => Setting_OnSwitchView(p));
            Setting_PopupCommand = new RelayCommand<object>(p => Setting_OnPopup(p));
        }

        #endregion
    }
}
