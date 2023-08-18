using ORD_Helper_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region ProcessSetting_SaveCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand ProcessSetting_SaveCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void ProcessSetting_OnSave()
        {
            ph[ProcessSetting_SelectedVersionItem.Index].Copy(ph_temp[ProcessSetting_SelectedVersionItem.Index]);
            unsafe
            {
                ph[ProcessSetting_SelectedVersionItem.Index].SaveProcess(vh.ver(ProcessSetting_SelectedVersionItem.Index).verName);
            }
        }

        #endregion

        #endregion

        #region Data

        #region ProcessSetting_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> ProcessSetting_VersionList
        {
            get;
            set;
        }

        #endregion

        #region ProcessSetting_SelectedVersionItem

        private VersionItem ProcessSetting_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem ProcessSetting_SelectedVersionItem
        {
            get { return ProcessSetting_selectedVersionItem; }
            set
            {
                ProcessSetting_selectedVersionItem = value;
                if (value != null)
                {
                    unsafe
                    {
                        if (ph_temp[value.Index].process.address != null)
                        {
                            ProcessSetting_AddressName = new string(ph_temp[value.Index].process.address);
                        }
                    }
                }
                OnPropertyChanged("ProcessSetting_SelectedVersionItem");
            }
        }

        #endregion

        #region ProcessSetting_AddressName

        private string ProcessSetting_addressName = null;

        /// <summary>
        /// 유닛 주소 세팅_주소 이름
        /// </summary>
        public string ProcessSetting_AddressName
        {
            get { return ProcessSetting_addressName; }
            set
            {
                ProcessSetting_addressName = value;

                unsafe
                {
                    ph_temp[ProcessSetting_SelectedVersionItem.Index].SetAddress((sbyte*)(void*)Marshal.StringToHGlobalAnsi(value));
                }
                OnPropertyChanged("ProcessSetting_AddressName");
            }
        }

        #endregion

        #region ProcessSetting_AddressNameColor

        private Brush ProcessSetting_addressNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 주소 설정_주소 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush ProcessSetting_AddressNameColor
        {
            get { return UnitAdd_unitIndexColor; }
            set
            {
                ProcessSetting_addressNameColor = value;
                OnPropertyChanged("ProcessSetting_AddressNameColor");
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_ProcessSetting()
        {
            for (int n = 0; n < vh.verNum; n++)
            {
                ph_temp.Add(new ProcessHandler());
                ph_temp[n].Copy(ph[n]);
            }
            ProcessSetting_VersionList = new ObservableCollection<VersionItem>();
            ProcessSetting_SaveCommand = new RelayCommand<object>(p => ProcessSetting_OnSave());

            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    ProcessSetting_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
            ProcessSetting_SelectedVersionItem = ProcessSetting_VersionList[0];
        }

        #endregion
    }
}
