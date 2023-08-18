using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORD_Helper_Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region GradeAdd_ConfirmCommand 

        /// <summary>
        /// 등급 추가 팝업_확인 ICommand
        /// </summary>
        public ICommand GradeAdd_ConfirmCommand { get; set; }

        /// <summary>
        /// 등급 추가 팝업_확인 Command 실행 시
        /// </summary>
        private void GradeAdd_OnConfirm()
        {
            if (GradeAdd_setGradeName != null)
            {
                
                DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + GradeAdd_setGradeName);
                if (di.Exists == false)
                {
                    di.Create();
                }
                unsafe
                {
                    uh_temp[UnitSetting_selectedVersionItem.Index].AddGrade((sbyte*)(void*)Marshal.StringToHGlobalAnsi(GradeAdd_setGradeName));
                }
                UnitSetting_GradeList.Add(new GradeItem() { Name = GradeAdd_setGradeName, Index = uh_temp[UnitSetting_selectedVersionItem.Index].gradeNum - 1 });
                
                UnitSetting_UpdateUnitItem = true;
                UnitSetting_ChangedUnitItem = true;
                OnPropertyChanged("UnitSetting_GradeList");
                Setting_Popup = 0;
            }
        }

        #endregion

        #endregion

        #region Data

        #region GradeAdd_SetGradeName

        private string GradeAdd_setGradeName;

        /// <summary>
        /// 등급 추가 팝업_등급 이름 텍스트박스 Property
        /// <para>Type: string</para>
        /// <para>Binding: Text</para>
        /// </summary>
        public string GradeAdd_SetGradeName
        {
            set
            {
                GradeAdd_setGradeName = value;
                OnPropertyChanged("GradeAdd_SetGradeName");
            }
        }

        #endregion

        #region GradeAdd_CloseImage

        public string GradeAdd_CloseImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/close.png"; }
        }

        #endregion

        #endregion

        #region Method

        public void Load_GradeAdd()
        {
            GradeAdd_ConfirmCommand = new RelayCommand<object>(p => GradeAdd_OnConfirm());
        }

        #endregion
    }
}
