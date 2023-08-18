using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORD_Helper_Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region Window_ClosingCommand

        /// <summary>
        /// 메인 창_뷰 전환 ICommand
        /// </summary>
        public ICommand Window_ClosingCommand { get; set; }

        /// <summary>
        /// 메인 창_뷰 전환 Command 실행 시
        /// </summary>
        /// <param name="index">메인 창 뷰 index
        /// <para>Type: int</para></param>
        private void Window_OnClosing()
        {
            dataThread.EndThread();
        }

        #endregion

        #region Window_SwitchViewCommand

        /// <summary>
        /// 메인 창_뷰 전환 ICommand
        /// </summary>
        public ICommand Window_SwitchViewCommand { get; set; }

        /// <summary>
        /// 메인 창_뷰 전환 Command 실행 시
        /// </summary>
        /// <param name="index">메인 창 뷰 index
        /// <para>Type: int</para></param>
        private void Window_OnSwitchView(object index)
        {
            Window_SwitchView = int.Parse(index.ToString());
        }

        #endregion

        #endregion

        #region Data

        #region Window_SwitchView

        private int Window_switchView;

        /// <summary>
        /// 메인 창_뷰 전환 Property
        /// <para>Type: int</para>
        /// </summary>
        public int Window_SwitchView
        {
            get
            {
                return Window_switchView;
            }
            set
            {
                Window_switchView = value;
                OnPropertyChanged("Window_SwitchView");
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_Window()
        {
            Window_SwitchView = 0;

            Window_ClosingCommand = new RelayCommand<object>(p => Window_OnClosing());
            Window_SwitchViewCommand = new RelayCommand<object>(p => Window_OnSwitchView(p));
        }

        #endregion
    }
}
