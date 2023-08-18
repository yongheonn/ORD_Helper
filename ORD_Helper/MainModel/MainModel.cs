using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ORD_Helper
{
    partial class MainModel : ViewModelBase
    {
        public MainModel()
        {
            Load_Data();
            Load_Window();
            Load_MainView();
            Load_UnitInfo();
            Load_MixInfo();
            Load_SettingView();
            Load_VersionSetting();
            Load_UnitInfoUISetitng();
            Load_GradeUI();
            Load_UnitAddressSetting();
            Load_ResourcesAddressSetting();
            Load_GameStateAddressSetting();
            Load_ProcessSetting();
            Load_UnitSettingView();
            Load_GradeAdd();
            Load_UnitAdd();
            Load_MixAdd();
            Load_UnitIndex();
            Load_SearchIndex();
            Load_IndexAdd();
        }

        public DataThread dataThread = new DataThread();
    }
}
