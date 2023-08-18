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

        #region UnitAddressSetting_OffsetAddCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand UnitAddressSetting_OffsetAddCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void UnitAddressSetting_OnOffsetAdd()
        {
            uah_temp[UnitAddressSetting_SelectedVersionItem.Index].addressInfo.offsetNum++;
            uah_temp[UnitAddressSetting_SelectedVersionItem.Index].addressInfo.ResizeOffset(uah_temp[UnitAddressSetting_SelectedVersionItem.Index].addressInfo.offsetNum);
            UnitAddressSetting_OffsetList.Add(new UnitAddressSetting_OffsetItem(){Offset = "0x00", Address = "0x00", Index = uah_temp[UnitAddressSetting_SelectedVersionItem.Index].addressInfo.offsetNum - 1 });

            OnPropertyChanged("UnitAddressSetting_OffsetList");
        }

        #endregion

        #region UnitAddressSetting_OffsetDeleteCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand UnitAddressSetting_OffsetDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void UnitAddressSetting_OnOffsetDelete()
        {
            uah_temp[UnitAddressSetting_SelectedVersionItem.Index].addressInfo.RemoveOffset(UnitAddressSetting_SelectedOffsetItem.Index);
            for(int n = UnitAddressSetting_SelectedOffsetItem.Index + 1; n < UnitAddressSetting_OffsetList.Count; n++)
            {
                UnitAddressSetting_OffsetList[n].Index--;
            }

            UnitAddressSetting_OffsetList.RemoveAt(UnitAddressSetting_SelectedOffsetItem.Index);

            OnPropertyChanged("UnitAddressSetting_OffsetList");
        }

        #endregion

        #region UnitAddressSetting_SaveCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand UnitAddressSetting_SaveCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void UnitAddressSetting_OnSave()
        {
            unsafe
            {
                for (int n = 0; n < UnitAddressSetting_OffsetList.Count; n++)
                {
                    uah_temp[UnitAddressSetting_SelectedVersionItem.Index].addressInfo.SetOffset(Convert.ToUInt32(UnitAddressSetting_OffsetList[n].Offset, 16), UnitAddressSetting_OffsetList[n].Index);
                }
            }
            uah[UnitAddressSetting_SelectedVersionItem.Index].Copy(uah_temp[UnitAddressSetting_SelectedVersionItem.Index]);
            unsafe
            {
                uah[UnitAddressSetting_SelectedVersionItem.Index].SaveAddress(vh.ver(UnitAddressSetting_SelectedVersionItem.Index).verName);
            }
            OnPropertyChanged("UnitAddressSetting_OffsetList");
        }

        #endregion

        #endregion

        #region Data

        #region UnitAddressSetting_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> UnitAddressSetting_VersionList
        {
            get;
            set;
        }

        #endregion

        #region UnitAddressSetting_SelectedVersionItem

        private VersionItem UnitAddressSetting_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem UnitAddressSetting_SelectedVersionItem
        {
            get { return UnitAddressSetting_selectedVersionItem; }
            set
            {
                UnitAddressSetting_selectedVersionItem = value;
                if (value != null)
                {
                    unsafe
                    {

                        if (uah_temp[value.Index].addressInfo.address != null)
                        {
                            UnitAddressSetting_AddressName = new string(uah_temp[value.Index].addressInfo.address);
                        }
                    }
                    for (int n = 0; n < uah_temp[value.Index].addressInfo.offsetNum; n++)
                        UnitAddressSetting_OffsetList.Add(new UnitAddressSetting_OffsetItem() { Offset = uah_temp[value.Index].addressInfo.GetOffset(n).ToString("X4"), Address = "0x00", Index = n });
                }

                OnPropertyChanged("UnitAddressSetting_SelectedVersionItem");
            }
        }

        #endregion

        #region UnitAddressSetting_AddressName

        private string UnitAddressSetting_addressName = null;

        /// <summary>
        /// 유닛 주소 세팅_주소 이름
        /// </summary>
        public string UnitAddressSetting_AddressName
        {
            get { return UnitAddressSetting_addressName; }
            set
            {
                UnitAddressSetting_addressName = value;

                    unsafe
                    {
                        uah_temp[UnitAddressSetting_SelectedVersionItem.Index].SetAddress((sbyte*)(void*)Marshal.StringToHGlobalAnsi(value));
                    }
                OnPropertyChanged("UnitAddressSetting_AddressName");
            }
        }

        #endregion

        #region UnitAddressSetting_AddressNameColor

        private Brush UnitAddressSetting_addressNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 주소 설정_주소 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush UnitAddressSetting_AddressNameColor
        {
            get { return UnitAdd_unitIndexColor; }
            set
            {
                UnitAddressSetting_addressNameColor = value;
                OnPropertyChanged("UnitAddressSetting_AddressNameColor");
            }
        }

        #endregion

        #region UnitAddressSetting_OffsetItem

        /// <summary>
        /// 등급 아이템 class
        /// </summary>
        public class UnitAddressSetting_OffsetItem : ViewModelBase
        {
            private string offset = "0x00";
            /// <summary>
            /// 오프셋
            /// <para>Type: string</para>
            /// </summary>
            public string Offset
            {
                get { return offset; }
                set
                {
                    bool check = false;
                    if (value != null)
                    {
                        try
                        {
                            if (Convert.ToInt32(value, 16) >= 0)
                            {
                                offset = Convert.ToUInt32(value, 16).ToString("X4");
                                OffsetColor = new SolidColorBrush(Colors.Black);
                                check = true;
                            }
                        }
                        catch { }
                    }

                    if (check == false)
                    {
                        OffsetColor = new SolidColorBrush(Colors.Red);
                    }
                    OnPropertyChanged("Offset");
                }
            }

            private Brush offsetColor = new SolidColorBrush(Colors.Black);

            /// <summary>
            /// 오프셋 배경색 Property
            /// <para>Type: Brush</para>
            /// <para>Binding: BackGround</para>
            /// </summary>
            public Brush OffsetColor
            {
                get { return offsetColor; }
                set
                {
                    offsetColor = value;
                    OnPropertyChanged("OffsetColor");
                }
            }

            private string address;

            /// <summary>
            /// 오프셋 해당 주소
            /// <para>Type: int</para>
            /// </summary>
            public string Address
            {
                get { return address; }
                set
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }

            private int index;

            /// <summary>
            /// 
            /// </summary>
            public int Index
            {
                get { return index; }
                set
                {
                    index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        #endregion

        #region UnitAddressSetting_OffsetList

        /// <summary>
        /// 유닛 주소 설정_오프셋 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<UnitAddressSetting_OffsetItem> UnitAddressSetting_OffsetList
        {
            get;
            set;
        }

        #endregion

        #region UnitAddressSetting_SelectedOffsetItem

        private UnitAddressSetting_OffsetItem UnitAddressSetting_selectedOffsetItem;

        /// <summary>
        /// 유닛 주소 설정_선택된 오프셋 아이템
        /// <para>Type: OffsetItem</para>
        /// </summary>
        public UnitAddressSetting_OffsetItem UnitAddressSetting_SelectedOffsetItem
        {
            get { return UnitAddressSetting_selectedOffsetItem; }
            set
            {
                UnitAddressSetting_selectedOffsetItem = value;
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_UnitAddressSetting()
        {
            for (int n = 0; n < vh.verNum; n++)
            {
                uah_temp.Add(new UnitAddressHandler());
                uah_temp[n].Copy(uah[n]);
            }

            UnitAddressSetting_OffsetAddCommand = new RelayCommand<object>(p => UnitAddressSetting_OnOffsetAdd());
            UnitAddressSetting_OffsetDeleteCommand = new RelayCommand<object>(p => UnitAddressSetting_OnOffsetDelete());
            UnitAddressSetting_SaveCommand = new RelayCommand<object>(p => UnitAddressSetting_OnSave());
            UnitAddressSetting_VersionList = new ObservableCollection<VersionItem>();
            UnitAddressSetting_OffsetList = new ObservableCollection<UnitAddressSetting_OffsetItem>();
            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    UnitAddressSetting_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
            UnitAddressSetting_SelectedVersionItem = UnitAddressSetting_VersionList[0];
        }

        #endregion
    }
}
