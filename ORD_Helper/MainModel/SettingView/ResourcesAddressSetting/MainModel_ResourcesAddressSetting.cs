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

        #region ResourcesAddressSetting_GoldOffsetAddCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand ResourcesAddressSetting_GoldOffsetAddCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void ResourcesAddressSetting_GoldOnOffsetAdd()
        {
            rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.addressInfo.offsetNum++;
            rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.addressInfo.ResizeOffset(rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.addressInfo.offsetNum);
            ResourcesAddressSetting_GoldOffsetList.Add(new ResourcesAddressSetting_GoldOffsetItem() { Offset = "0x00", Address = "0x00", Index = rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.addressInfo.offsetNum - 1 });

            OnPropertyChanged("ResourcesAddressSetting_GoldOffsetList");
        }

        #endregion

        #region ResourcesAddressSetting_GoldOffsetDeleteCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand ResourcesAddressSetting_GoldOffsetDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void ResourcesAddressSetting_GoldOnOffsetDelete()
        {
            rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.addressInfo.RemoveOffset(ResourcesAddressSetting_GoldSelectedOffsetItem.Index);
            for (int n = ResourcesAddressSetting_GoldSelectedOffsetItem.Index + 1; n < ResourcesAddressSetting_GoldOffsetList.Count; n++)
            {
                ResourcesAddressSetting_GoldOffsetList[n].Index--;
            }

            ResourcesAddressSetting_GoldOffsetList.RemoveAt(ResourcesAddressSetting_GoldSelectedOffsetItem.Index);

            OnPropertyChanged("ResourcesAddressSetting_GoldOffsetList");
        }

        #endregion

        #region ResourcesAddressSetting_TreeOffsetAddCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand ResourcesAddressSetting_TreeOffsetAddCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void ResourcesAddressSetting_TreeOnOffsetAdd()
        {
            rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.addressInfo.offsetNum++;
            rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.addressInfo.ResizeOffset(rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.addressInfo.offsetNum);
            ResourcesAddressSetting_TreeOffsetList.Add(new ResourcesAddressSetting_TreeOffsetItem() { Offset = "0x00", Address = "0x00", Index = rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.addressInfo.offsetNum - 1 });

            OnPropertyChanged("ResourcesAddressSetting_TreeOffsetList");
        }

        #endregion

        #region ResourcesAddressSetting_TreeOffsetDeleteCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand ResourcesAddressSetting_TreeOffsetDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void ResourcesAddressSetting_TreeOnOffsetDelete()
        {
            rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.addressInfo.RemoveOffset(ResourcesAddressSetting_TreeSelectedOffsetItem.Index);
            for (int n = ResourcesAddressSetting_TreeSelectedOffsetItem.Index + 1; n < ResourcesAddressSetting_TreeOffsetList.Count; n++)
            {
                ResourcesAddressSetting_TreeOffsetList[n].Index--;
            }

            ResourcesAddressSetting_TreeOffsetList.RemoveAt(ResourcesAddressSetting_TreeSelectedOffsetItem.Index);

            OnPropertyChanged("ResourcesAddressSetting_TreeOffsetList");
        }

        #endregion

        #region ResourcesAddressSetting_SaveCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand ResourcesAddressSetting_SaveCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void ResourcesAddressSetting_OnSave()
        {
            for (int n = 0; n < ResourcesAddressSetting_GoldOffsetList.Count; n++)
            {
                unsafe
                {
                    rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.addressInfo.SetOffset(Convert.ToUInt32(ResourcesAddressSetting_GoldOffsetList[n].Offset, 16), ResourcesAddressSetting_GoldOffsetList[n].Index);
                }
            }
            for (int n = 0; n < ResourcesAddressSetting_TreeOffsetList.Count; n++)
            {
                unsafe
                {
                    rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.addressInfo.SetOffset(Convert.ToUInt32(ResourcesAddressSetting_TreeOffsetList[n].Offset, 16), ResourcesAddressSetting_TreeOffsetList[n].Index);
                }
            }
            rah[ResourcesAddressSetting_SelectedVersionItem.Index].Copy(rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index]);
            unsafe
            {
                rah[ResourcesAddressSetting_SelectedVersionItem.Index].gold.SaveAddress(vh.ver(ResourcesAddressSetting_SelectedVersionItem.Index).verName);
                rah[ResourcesAddressSetting_SelectedVersionItem.Index].tree.SaveAddress(vh.ver(ResourcesAddressSetting_SelectedVersionItem.Index).verName);
            }
        }

        #endregion

        #endregion

        #region Data

        #region ResourcesAddressSetting_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> ResourcesAddressSetting_VersionList
        {
            get;
            set;
        }

        #endregion

        #region ResourcesAddressSetting_SelectedVersionItem

        private VersionItem ResourcesAddressSetting_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem ResourcesAddressSetting_SelectedVersionItem
        {
            get { return ResourcesAddressSetting_selectedVersionItem; }
            set
            {
                ResourcesAddressSetting_selectedVersionItem = value;
                if (value != null)
                {
                    Clear_ResourcesAddressSetting();
                    unsafe
                    {
                        if (rah_temp[value.Index].gold.addressInfo.address != null)
                        {
                            ResourcesAddressSetting_GoldAddressName = new string(rah_temp[value.Index].gold.addressInfo.address);
                        }
                    }
                    for (int n = 0; n < rah_temp[value.Index].gold.addressInfo.offsetNum; n++)
                        ResourcesAddressSetting_GoldOffsetList.Add(new ResourcesAddressSetting_GoldOffsetItem() { Offset = rah_temp[value.Index].gold.addressInfo.GetOffset(n).ToString("X4"), Address = "0x00", Index = n });


                    unsafe
                    {

                        if (rah_temp[value.Index].tree.addressInfo.address != null)
                        {
                            ResourcesAddressSetting_TreeAddressName = new string(rah_temp[value.Index].tree.addressInfo.address);
                        }
                    }
                    for (int n = 0; n < rah_temp[value.Index].tree.addressInfo.offsetNum; n++)
                        ResourcesAddressSetting_TreeOffsetList.Add(new ResourcesAddressSetting_TreeOffsetItem() { Offset = rah_temp[value.Index].tree.addressInfo.GetOffset(n).ToString("X4"), Address = "0x00", Index = n });
                }

                OnPropertyChanged("ResourcesAddressSetting_SelectedVersionItem");
            }
        }

        #endregion

        #region ResourcesAddressSetting_GoldAddressName

        private string ResourcesAddressSetting_goldAddressName = null;

        /// <summary>
        /// 유닛 주소 세팅_주소 이름
        /// </summary>
        public string ResourcesAddressSetting_GoldAddressName
        {
            get { return ResourcesAddressSetting_goldAddressName; }
            set
            {
                ResourcesAddressSetting_goldAddressName = value;

                unsafe
                {
                    rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].gold.SetAddress((sbyte*)(void*)Marshal.StringToHGlobalAnsi(value));
                }
                OnPropertyChanged("ResourcesAddressSetting_GoldAddressName");
            }
        }

        #endregion

        #region ResourcesAddressSetting_GoldAddressNameColor

        private Brush ResourcesAddressSetting_goldAddressNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 주소 설정_주소 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush ResourcesAddressSetting_GoldAddressNameColor
        {
            get { return ResourcesAddressSetting_goldAddressNameColor; }
            set
            {
                ResourcesAddressSetting_goldAddressNameColor = value;
                OnPropertyChanged("ResourcesAddressSetting_GoldAddressNameColor");
            }
        }

        #endregion

        #region ResourcesAddressSetting_GoldOffsetItem

        /// <summary>
        /// 등급 아이템 class
        /// </summary>
        public class ResourcesAddressSetting_GoldOffsetItem : ViewModelBase
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

        #region ResourcesAddressSetting_GoldOffsetList

        /// <summary>
        /// 유닛 주소 설정_오프셋 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<ResourcesAddressSetting_GoldOffsetItem> ResourcesAddressSetting_GoldOffsetList
        {
            get;
            set;
        }

        #endregion

        #region ResourcesAddressSetting_GoldSelectedOffsetItem

        private ResourcesAddressSetting_GoldOffsetItem ResourcesAddressSetting_goldSelectedOffsetItem;

        /// <summary>
        /// 유닛 주소 설정_선택된 오프셋 아이템
        /// <para>Type: OffsetItem</para>
        /// </summary>
        public ResourcesAddressSetting_GoldOffsetItem ResourcesAddressSetting_GoldSelectedOffsetItem
        {
            get { return ResourcesAddressSetting_goldSelectedOffsetItem; }
            set
            {
                ResourcesAddressSetting_goldSelectedOffsetItem = value;
            }
        }

        #endregion

        #region ResourcesAddressSetting_TreeAddressName

        private string ResourcesAddressSetting_treeAddressName = null;

        /// <summary>
        /// 유닛 주소 세팅_주소 이름
        /// </summary>
        public string ResourcesAddressSetting_TreeAddressName
        {
            get { return ResourcesAddressSetting_treeAddressName; }
            set
            {
                ResourcesAddressSetting_treeAddressName = value;

                unsafe
                {
                    rah_temp[ResourcesAddressSetting_SelectedVersionItem.Index].tree.SetAddress((sbyte*)(void*)Marshal.StringToHGlobalAnsi(value));
                }
                OnPropertyChanged("ResourcesAddressSetting_TreeAddressName");
            }
        }

        #endregion

        #region ResourcesAddressSetting_TreeAddressNameColor

        private Brush ResourcesAddressSetting_treeAddressNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 주소 설정_주소 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush ResourcesAddressSetting_TreeAddressNameColor
        {
            get { return ResourcesAddressSetting_treeAddressNameColor; }
            set
            {
                ResourcesAddressSetting_treeAddressNameColor = value;
                OnPropertyChanged("ResourcesAddressSetting_TreeAddressNameColor");
            }
        }

        #endregion

        #region ResourcesAddressSetting_TreeOffsetItem

        /// <summary>
        /// 등급 아이템 class
        /// </summary>
        public class ResourcesAddressSetting_TreeOffsetItem : ViewModelBase
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

        #region ResourcesAddressSetting_TreeOffsetList

        /// <summary>
        /// 유닛 주소 설정_오프셋 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<ResourcesAddressSetting_TreeOffsetItem> ResourcesAddressSetting_TreeOffsetList
        {
            get;
            set;
        }

        #endregion

        #region ResourcesAddressSetting_TreeSelectedOffsetItem

        private ResourcesAddressSetting_TreeOffsetItem ResourcesAddressSetting_treeSelectedOffsetItem;

        /// <summary>
        /// 유닛 주소 설정_선택된 오프셋 아이템
        /// <para>Type: OffsetItem</para>
        /// </summary>
        public ResourcesAddressSetting_TreeOffsetItem ResourcesAddressSetting_TreeSelectedOffsetItem
        {
            get { return ResourcesAddressSetting_treeSelectedOffsetItem; }
            set
            {
                ResourcesAddressSetting_treeSelectedOffsetItem = value;
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_ResourcesAddressSetting()
        {
            for (int n = 0; n < vh.verNum; n++)
            {
                rah_temp.Add(new ResourcesAddressHandler());
                rah_temp[n].Copy(rah[n]);
            }
            ResourcesAddressSetting_SaveCommand = new RelayCommand<object>(p => ResourcesAddressSetting_OnSave());

            ResourcesAddressSetting_VersionList = new ObservableCollection<VersionItem>();

            ResourcesAddressSetting_GoldOffsetAddCommand = new RelayCommand<object>(p => ResourcesAddressSetting_GoldOnOffsetAdd());
            ResourcesAddressSetting_GoldOffsetDeleteCommand = new RelayCommand<object>(p => ResourcesAddressSetting_GoldOnOffsetDelete());
            ResourcesAddressSetting_GoldOffsetList = new ObservableCollection<ResourcesAddressSetting_GoldOffsetItem>();

            ResourcesAddressSetting_TreeOffsetAddCommand = new RelayCommand<object>(p => ResourcesAddressSetting_TreeOnOffsetAdd());
            ResourcesAddressSetting_TreeOffsetDeleteCommand = new RelayCommand<object>(p => ResourcesAddressSetting_TreeOnOffsetDelete());
            ResourcesAddressSetting_TreeOffsetList = new ObservableCollection<ResourcesAddressSetting_TreeOffsetItem>();

            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    ResourcesAddressSetting_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
            ResourcesAddressSetting_SelectedVersionItem = ResourcesAddressSetting_VersionList[0];
        }

        public void Clear_ResourcesAddressSetting()
        {
            ResourcesAddressSetting_goldAddressName = null;
            ResourcesAddressSetting_treeAddressName = null;
            ResourcesAddressSetting_GoldOffsetList.Clear();
            ResourcesAddressSetting_TreeOffsetList.Clear();
        }

        #endregion
    }
}
