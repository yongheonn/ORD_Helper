using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORD_Helper_Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region UnitIndex_SearchCommnad

        /// <summary>
        /// 유닛 인덱스 팝업_연결 리스트 검색 ICommand
        /// </summary>
        public ICommand UnitIndex_SearchCommand { get; set; }

        /// <summary>
        /// 유닛 인덱스 팝업_연결 리스트 검색 Command 실행 시
        /// </summary>
        private void UnitIndex_OnSearch()
        {
            if (UnitIndex_SearchEnable)
            {
                switch (UnitIndex_SelectedSearchItem.Index)
                {
                    case 3:
                        {
                            try
                            {
                                if (Convert.ToUInt32(UnitIndex_SearchValue, 16) >= 0)
                                {
                                    for (int n = 0; n < UnitIndex_IndexList.Count; n++)
                                    {
                                        if (UnitIndex_IndexList[n].Address == UnitIndex_SearchValue)
                                        {
                                            UnitIndex_SelectedIndexItem = UnitIndex_IndexList[n];
                                        }
                                    }
                                    OnPropertyChanged("UnitIndex_SelectedIndexItem");
                                    OnPropertyChanged("UnitIndex_IndexList");
                                }
                            }
                            catch { }
                        }
                        break;

                    case 4:
                        {
                            try
                            {
                                if (Convert.ToUInt32(UnitIndex_SearchValue, 16) >= 0)
                                {
                                    for (int n = 0; n < UnitIndex_IndexList.Count; n++)
                                    {
                                        if (UnitIndex_IndexList[n].Index == UnitIndex_SearchValue)
                                        {
                                            UnitIndex_SelectedIndexItem = UnitIndex_IndexList[n];
                                        }
                                    }
                                    OnPropertyChanged("UnitIndex_IndexList");
                                }
                            }
                            catch { }
                        }
                        break;

                    case 5:
                        {
                            try
                            {
                                if (Convert.ToUInt32(UnitIndex_SearchValue) >= 0)
                                {
                                    int count = 0;
                                    for (int n = 0; n < UnitIndex_IndexList.Count; n++)
                                    {
                                        if (UnitIndex_IndexList[n].Num == Convert.ToUInt32(UnitIndex_SearchValue))
                                        {
                                            UnitIndex_IndexList[n].ItemIndex = count;
                                            count++;
                                            UnitIndex_TempIndexList.Add(UnitIndex_IndexList[n]);
                                        }
                                    }
                                    UnitIndex_IndexList.Clear();
                                    UnitIndex_IndexList = UnitIndex_TempIndexList;
                                    UnitIndex_TempIndexList.Clear();
                                    OnPropertyChanged("UnitIndex_IndexList");
                                }
                            }
                            catch { }
                        }
                        break;
                }
            }
        }

        #endregion

        #region UnitIndex_ExitCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand UnitIndex_ExitCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void UnitIndex_OnExit()
        {
            dataThread.PauseThread("SearchList");
            if (UnitIndex_Popup == 1)
                Setting_Popup = 2;
            else if (UnitIndex_Popup == 2)
                Setting_Popup = 0;
        }

        #endregion

        #region UnitIndex_ConfirmCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand UnitIndex_ConfirmCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void UnitIndex_OnConfirm()
        {
            if (UnitIndex_CanConfirm)
            {
                dataThread.PauseThread("SearchList");
                if (UnitIndex_Popup == 1)
                {
                    UnitAdd_UnitIndex = UnitIndex_SelectedIndexItem.Index;
                    Setting_Popup = 2;
                }
                else if (UnitIndex_Popup == 2)
                {
                    UnitSetting_UnitIndex = UnitIndex_SelectedIndexItem.Index;
                    Setting_Popup = 0;
                }
            }
        }

        #endregion

        #endregion

        #region Data



        #region UnitIndex_Popup

        private int UnitIndex_popup;

        /// <summary>
        /// 
        /// </summary>
        public int UnitIndex_Popup
        {
            get { return UnitIndex_popup; }
            set
            {
                UnitIndex_popup = value;
            }
        }

        #endregion

        #region UnitIndex_SearchList

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_연결 리스트 검색 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<SearchItem> UnitIndex_SearchList
        {
            get;
            set;
        }

        #endregion

        #region UnitIndex_SelectedSearchItem

        private SearchItem UnitIndex_selectedSearchItem;

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_선택된 연결 리스트 검색 아이템
        /// <para>Type: IndexItem</para>
        /// </summary>
        public SearchItem UnitIndex_SelectedSearchItem
        {
            get { return UnitIndex_selectedSearchItem; }
            set
            {
                UnitIndex_selectedSearchItem = value;
                OnPropertyChanged("UnitIndex_SearchBoxEnable");
                OnPropertyChanged("UnitIndex_SearchEnable");
            }
        }

        #endregion

        #region UnitIndex_SearchValue

        private string UnitIndex_searchValue;

        /// <summary>
        /// 
        /// </summary>
        public string UnitIndex_SearchValue
        {
            get { return UnitIndex_searchValue; }
            set
            {
                if (UnitIndex_SelectedSearchItem != null)
                {
                    switch (UnitIndex_SelectedSearchItem.Index)
                    {
                        case 1:
                            {
                                try
                                {
                                    if (Convert.ToUInt32(value, 16) >= 0)
                                    {
                                        UnitIndex_searchValue = Convert.ToUInt32(value, 16).ToString("X4");
                                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Black);
                                    }
                                    else
                                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }

                                catch
                                {
                                    UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }
                            }
                            break;

                        case 2:
                            {
                                try
                                {
                                    if (Convert.ToUInt32(value, 16) >= 0)
                                    {
                                        UnitIndex_searchValue = Convert.ToUInt32(value, 16).ToString("X4");
                                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Black);
                                    }
                                    else
                                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }

                                catch
                                {
                                    UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }
                            }
                            break;

                        case 3:
                            {
                                try
                                {
                                    if (Convert.ToUInt32(value) >= 0)
                                    {
                                        UnitIndex_searchValue = Convert.ToUInt32(value).ToString();
                                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Black);
                                    }
                                    else
                                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }

                                catch
                                {
                                    UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }
                            }
                            break;
                    }
                    OnPropertyChanged("UnitIndex_SearchValue");
                    OnPropertyChanged("UnitIndex_SearchEnable");
                }
            }
        }

        #endregion

        #region UnitIndex_SearchBoxEnable

        /// <summary>
        /// 
        /// </summary>
        public bool UnitIndex_SearchBoxEnable
        {
            get
            {
                if (UnitIndex_SelectedSearchItem != null)
                    return true;

                return false;
            }
        }

        #endregion

        #region UnitIndex_SearchEnable

        /// <summary>
        /// 
        /// </summary>
        public bool UnitIndex_SearchEnable
        {
            get
            {
                if (UnitIndex_SelectedSearchItem != null && UnitIndex_SearchValue != null)
                    return true;

                return false;
            }
        }

        #endregion

        #region UnitIndex_SearchColor

        private Brush UnitIndex_searchColor = new SolidColorBrush(Colors.Black);

        public Brush UnitIndex_SearchColor
        {
            get { return UnitIndex_searchColor; }
            set
            {
                UnitIndex_searchColor = value;
                OnPropertyChanged("UnitIndex_SearchColor");
            }
        }

        #endregion

        #region UnitIndex_IndexList

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_인덱스 연결 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<IndexItem> UnitIndex_IndexList
        {
            get;
            set;
        }

        #endregion

        #region UnitIndex_TempIndexList

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_인덱스 연결 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<IndexItem> UnitIndex_TempIndexList
        {
            get;
            set;
        }

        #endregion

        #region UnitIndex_SelectedIndexItem

        private IndexItem UnitIndex_selectedIndexItem;

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_선택된 인덱스 연결 리스트 아이템
        /// <para>Type: IndexItem</para>
        /// </summary>
        public IndexItem UnitIndex_SelectedIndexItem
        {
            get { return UnitIndex_selectedIndexItem; }
            set
            {
                if (value != null)
                {
                    UnitIndex_searchValue = null;
                    OnPropertyChanged("UnitIndex_SearchValue");
                    OnPropertyChanged("UnitIndex_CanConfirm");
                    UnitIndex_selectedIndexItem = value;
                }
                OnPropertyChanged("UnitIndex_SelectedIndexItem");
            }
        }

        #endregion

        #region UnitIndex_CanConfirm

        /// <summary>
        /// 
        /// </summary>
        public bool UnitIndex_CanConfirm
        {
            get
            {
                if (UnitIndex_SelectedIndexItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitIndex_CloseImage

        public string UnitIndex_CloseImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/close.png"; }
        }

        #endregion

        #endregion

        #region Method

        public void Load_UnitIndex()
        {
            UnitIndex_SearchCommand = new RelayCommand<object>(p => UnitIndex_OnSearch());
            UnitIndex_ExitCommand = new RelayCommand<object>(p => UnitIndex_OnExit());
            UnitIndex_ConfirmCommand = new RelayCommand<object>(p => UnitIndex_OnConfirm());

            UnitIndex_SearchList = new System.Collections.ObjectModel.ObservableCollection<SearchItem>();
            UnitIndex_SearchList.Add(new SearchItem() { Index = 1, Text = "번호로 검색" });
            UnitIndex_SearchList.Add(new SearchItem() { Index = 2, Text = "이름으로 검색" });
            UnitIndex_SearchList.Add(new SearchItem() { Index = 3, Text = "주소로 검색"});
            UnitIndex_SearchList.Add(new SearchItem() { Index = 4, Text = "인덱스로 검색" });
            UnitIndex_SearchList.Add(new SearchItem() { Index = 5, Text = "값으로 검색" });

            UnitIndex_IndexList = new ObservableCollection<IndexItem>();
            UnitIndex_TempIndexList = new System.Collections.ObjectModel.ObservableCollection<IndexItem>();
            dataThread.CreateThread(SearchList, 300);
        }

        public void Clear_UnitIndex()
        {
            UnitIndex_selectedSearchItem = null;
            UnitIndex_selectedIndexItem = null;
            UnitIndex_searchValue = null;
        }

        public void SearchList()
        {
            if (ph[UnitSetting_selectedVersionItem.Index].process.processState == 1)
            {
                uah[UnitSetting_selectedVersionItem.Index].SearchList(uh[UnitSetting_selectedVersionItem.Index], ph[UnitSetting_selectedVersionItem.Index]);

                for (int n = 0; n < UnitIndex_IndexList.Count; n++)
                {
                    DispatcherService.Invoke((System.Action)(() =>
                    {
                        unsafe
                        {
                            if (uah[UnitSetting_selectedVersionItem.Index].indexList(n).grade >= 0)
                            {
                                BitmapImage bi = new BitmapImage();
                                bi.BeginInit();
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[UnitSetting_selectedVersionItem.Index].grade(uah[UnitSetting_selectedVersionItem.Index].indexList(n).grade).name) +
                                "/" + Marshal.PtrToStringAnsi((IntPtr)uah[UnitSetting_selectedVersionItem.Index].indexList(n).name) + ".jpg");
                                bi.EndInit();
                                UnitIndex_IndexList[n].Address = uah[UnitSetting_selectedVersionItem.Index].indexList(n).address.ToString("X4");
                                UnitIndex_IndexList[n].Index = uah[UnitSetting_selectedVersionItem.Index].indexList(n).index.ToString("X4");
                                UnitIndex_IndexList[n].Num = uah[UnitSetting_selectedVersionItem.Index].indexList(n).num;
                                UnitIndex_IndexList[n].Name = Marshal.PtrToStringAnsi((IntPtr)uah[UnitSetting_selectedVersionItem.Index].indexList(n).name);
                                UnitIndex_IndexList[n].Image = bi;
                                UnitIndex_IndexList[n].ItemIndex = n;
                            }
                            else
                            {
                                UnitIndex_IndexList[n].Address = uah[UnitSetting_selectedVersionItem.Index].indexList(n).address.ToString("X4");
                                UnitIndex_IndexList[n].Index = uah[UnitSetting_selectedVersionItem.Index].indexList(n).index.ToString("X4");
                                UnitIndex_IndexList[n].Num = uah[UnitSetting_selectedVersionItem.Index].indexList(n).num;
                                UnitIndex_IndexList[n].ItemIndex = n;
                            }
                        }
                    }));

                }
                if (UnitIndex_IndexList.Count < uah[UnitSetting_selectedVersionItem.Index].indexListSize)
                {
                    DispatcherService.Invoke((System.Action)(() =>
                    {
                        unsafe
                        {
                            for (int n = UnitIndex_IndexList.Count; n < uah[UnitSetting_selectedVersionItem.Index].indexListSize; n++)
                            {
                                if (uah[UnitSetting_selectedVersionItem.Index].indexList(n).grade >= 0)
                                {
                                    BitmapImage bi = new BitmapImage();
                                    bi.BeginInit();
                                    bi.CacheOption = BitmapCacheOption.OnLoad;
                                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[UnitSetting_selectedVersionItem.Index].grade(uah[UnitSetting_selectedVersionItem.Index].indexList(n).grade).name) + "/" +
                                        Marshal.PtrToStringAnsi((IntPtr)uah[UnitSetting_selectedVersionItem.Index].indexList(n).name) + ".jpg");
                                    bi.EndInit();
                                    UnitIndex_IndexList.Add(new IndexItem()
                                    {
                                        Address = uah[UnitSetting_selectedVersionItem.Index].indexList(n).address.ToString("X4"),
                                        Index = uah[UnitSetting_selectedVersionItem.Index].indexList(n).index.ToString("X4"),
                                        Num = uah[UnitSetting_selectedVersionItem.Index].indexList(n).num,
                                        Name = Marshal.PtrToStringAnsi((IntPtr)uah[UnitSetting_selectedVersionItem.Index].indexList(n).name),
                                        Image = bi,
                                        ItemIndex = n
                                    });
                                }
                                else
                                    UnitIndex_IndexList.Add(new IndexItem()
                                    {
                                        Address = uah[UnitSetting_selectedVersionItem.Index].indexList(n).address.ToString("X4"),
                                        Index = uah[UnitSetting_selectedVersionItem.Index].indexList(n).index.ToString("X4"),
                                        Num = uah[UnitSetting_selectedVersionItem.Index].indexList(n).num,
                                        ItemIndex = n
                                    });
                            }
                        }
                    }));
                }
                OnPropertyChanged("UnitIndex_IndexList");
            }
        }

            #endregion
        }
}
