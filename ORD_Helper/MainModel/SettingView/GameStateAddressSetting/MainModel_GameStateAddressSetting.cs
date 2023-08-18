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

        #region GameStateAddressSetting_OffsetAddCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand GameStateAddressSetting_OffsetAddCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void GameStateAddressSetting_OnOffsetAdd()
        {
            gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].addressInfo.offsetNum++;
            gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].addressInfo.ResizeOffset(gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].addressInfo.offsetNum);
            GameStateAddressSetting_OffsetList.Add(new GameStateAddressSetting_OffsetItem() { Offset = "0x00", Address = "0x00", Index = gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].addressInfo.offsetNum - 1 });

            OnPropertyChanged("GameStateAddressSetting_OffsetList");
        }

        #endregion

        #region GameStateAddressSetting_OffsetDeleteCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand GameStateAddressSetting_OffsetDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void GameStateAddressSetting_OnOffsetDelete()
        {
            gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].addressInfo.RemoveOffset(GameStateAddressSetting_SelectedOffsetItem.Index);
            for (int n = GameStateAddressSetting_SelectedOffsetItem.Index + 1; n < GameStateAddressSetting_OffsetList.Count; n++)
            {
                GameStateAddressSetting_OffsetList[n].Index--;
            }

            GameStateAddressSetting_OffsetList.RemoveAt(GameStateAddressSetting_SelectedOffsetItem.Index);

            OnPropertyChanged("GameStateAddressSetting_OffsetList");
        }

        #endregion

        #region GameStateAddressSetting_SaveCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand GameStateAddressSetting_SaveCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void GameStateAddressSetting_OnSave()
        {
            for (int n = 0; n < GameStateAddressSetting_OffsetList.Count; n++)
            {
                unsafe
                {
                    gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].addressInfo.SetOffset(Convert.ToUInt32(GameStateAddressSetting_OffsetList[n].Offset, 16), GameStateAddressSetting_OffsetList[n].Index);
                }
            }
            gah[GameStateAddressSetting_SelectedVersionItem.Index].Copy(gah_temp[GameStateAddressSetting_SelectedVersionItem.Index]);
            unsafe
            {
                gah[GameStateAddressSetting_SelectedVersionItem.Index].SaveAddress(vh.ver(GameStateAddressSetting_SelectedVersionItem.Index).verName);
            }

            OnPropertyChanged("GameStateAddressSetting_OffsetList");
        }

        #endregion

        #endregion

        #region Data

        #region GameStateAddressSetting_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> GameStateAddressSetting_VersionList
        {
            get;
            set;
        }

        #endregion

        #region GameStateAddressSetting_SelectedVersionItem

        private VersionItem GameStateAddressSetting_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem GameStateAddressSetting_SelectedVersionItem
        {
            get { return GameStateAddressSetting_selectedVersionItem; }
            set
            {
                GameStateAddressSetting_selectedVersionItem = value;
                if (value != null)
                {
                    GameStateAddressSetting_OffsetList.Clear();
                    unsafe
                    {
                        if (gah_temp[value.Index].addressInfo.address != null)
                        {
                            GameStateAddressSetting_AddressName = new string(gah_temp[value.Index].addressInfo.address);
                        }
                    }
                    for (int n = 0; n < gah_temp[value.Index].addressInfo.offsetNum; n++)
                        GameStateAddressSetting_OffsetList.Add(new GameStateAddressSetting_OffsetItem() { Offset = gah_temp[value.Index].addressInfo.GetOffset(n).ToString("X4"), Address = "0x00", Index = n });
                }

                OnPropertyChanged("GameStateAddressSetting_SelectedVersionItem");
            }
        }

        #endregion

        #region GameStateAddressSetting_AddressName

        private string GameStateAddressSetting_addressName = null;

        /// <summary>
        /// 유닛 주소 세팅_주소 이름
        /// </summary>
        public string GameStateAddressSetting_AddressName
        {
            get { return GameStateAddressSetting_addressName; }
            set
            {
                GameStateAddressSetting_addressName = value;

                unsafe
                {
                    gah_temp[GameStateAddressSetting_SelectedVersionItem.Index].SetAddress((sbyte*)(void*)Marshal.StringToHGlobalAnsi(value));
                }
                OnPropertyChanged("GameStateAddressSetting_AddressName");
            }
        }

        #endregion

        #region GameStateAddressSetting_AddressNameColor

        private Brush GameStateAddressSetting_addressNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 주소 설정_주소 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush GameStateAddressSetting_AddressNameColor
        {
            get { return UnitAdd_unitIndexColor; }
            set
            {
                GameStateAddressSetting_addressNameColor = value;
                OnPropertyChanged("GameStateAddressSetting_AddressNameColor");
            }
        }

        #endregion

        #region GameStateAddressSetting_OffsetItem

        /// <summary>
        /// 등급 아이템 class
        /// </summary>
        public class GameStateAddressSetting_OffsetItem : ViewModelBase
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

        #region GameStateAddressSetting_OffsetList

        /// <summary>
        /// 유닛 주소 설정_오프셋 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<GameStateAddressSetting_OffsetItem> GameStateAddressSetting_OffsetList
        {
            get;
            set;
        }

        #endregion

        #region GameStateAddressSetting_SelectedOffsetItem

        /// <summary>
        /// 유닛 주소 설정_선택된 오프셋 아이템
        /// <para>Type: OffsetItem</para>
        /// </summary>
        public GameStateAddressSetting_OffsetItem GameStateAddressSetting_SelectedOffsetItem
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Method

        public void Load_GameStateAddressSetting()
        {
            for (int n = 0; n < vh.verNum; n++)
            {
                gah_temp.Add(new GameStateAddressHandler());
                gah_temp[n].Copy(gah[n]);
            }
            GameStateAddressSetting_VersionList = new ObservableCollection<VersionItem>();

            GameStateAddressSetting_OffsetAddCommand = new RelayCommand<object>(p => GameStateAddressSetting_OnOffsetAdd());
            GameStateAddressSetting_OffsetDeleteCommand = new RelayCommand<object>(p => GameStateAddressSetting_OnOffsetDelete());
            GameStateAddressSetting_SaveCommand = new RelayCommand<object>(p => GameStateAddressSetting_OnSave());
            GameStateAddressSetting_OffsetList = new ObservableCollection<GameStateAddressSetting_OffsetItem>();

            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    GameStateAddressSetting_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
            GameStateAddressSetting_SelectedVersionItem = GameStateAddressSetting_VersionList[0];
        }

        #endregion
    }
}