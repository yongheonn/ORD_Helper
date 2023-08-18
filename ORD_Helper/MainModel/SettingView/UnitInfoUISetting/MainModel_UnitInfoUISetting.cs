using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region UnitInfoUISetitng_GradeUICommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand UnitInfoUISetitng_GradeUICommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void UnitInfoUISetitng_OnGradeUI()
        {
            Setting_Popup = 5;
        }

        #endregion

        #endregion

        #region Method

        public void Load_UnitInfoUISetitng()
        {
            UnitInfoUISetitng_GradeUICommand = new RelayCommand<object>(p => UnitInfoUISetitng_OnGradeUI());
        }

        #endregion
    }
}
