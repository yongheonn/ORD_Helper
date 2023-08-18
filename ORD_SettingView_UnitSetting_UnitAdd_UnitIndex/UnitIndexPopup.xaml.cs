using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ORD_SettingView_UnitSetting_UnitAdd_UnitIndex
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UnitIndexPopup : UserControl
    {
        public UnitIndexPopup()
        {
            InitializeComponent();
        }

        private void UnitIndex_ScrollToSelected(object sender, SelectionChangedEventArgs e)
        {
            if(((DataGrid)sender).SelectedItem != null)
                ((DataGrid)sender).ScrollIntoView(e.AddedItems[0]);
        }
    }
}
