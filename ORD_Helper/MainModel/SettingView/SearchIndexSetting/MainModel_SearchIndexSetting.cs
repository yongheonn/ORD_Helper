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
using System.IO;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region SearchIndex_SearchCommnad

        /// <summary>
        /// 유닛 인덱스 팝업_연결 리스트 검색 ICommand
        /// </summary>
        public ICommand SearchIndex_SearchCommand { get; set; }

        /// <summary>
        /// 유닛 인덱스 팝업_연결 리스트 검색 Command 실행 시
        /// </summary>
        private void SearchIndex_OnSearch()
        {
            if (SearchIndex_SearchEnable)
            {
                switch (SearchIndex_SelectedSearchItem.Index)
                {
                    case 3:
                        {
                            try
                            {
                                if (Convert.ToUInt32(SearchIndex_SearchValue, 16) >= 0)
                                {
                                    for (int n = 0; n < SearchIndex_IndexList.Count; n++)
                                    {
                                        if (SearchIndex_IndexList[n].Address == SearchIndex_SearchValue)
                                        {
                                            SearchIndex_SelectedIndexItem = SearchIndex_IndexList[n];
                                        }
                                    }
                                    OnPropertyChanged("SearchIndex_SelectedIndexItem");
                                    OnPropertyChanged("SearchIndex_IndexList");
                                }
                            }
                            catch { }
                        }
                        break;

                    case 4:
                        {
                            try
                            {
                                if (Convert.ToUInt32(SearchIndex_SearchValue, 16) >= 0)
                                {
                                    for (int n = 0; n < SearchIndex_IndexList.Count; n++)
                                    {
                                        if (SearchIndex_IndexList[n].Index == SearchIndex_SearchValue)
                                        {
                                            SearchIndex_SelectedIndexItem = SearchIndex_IndexList[n];
                                        }
                                    }
                                    OnPropertyChanged("SearchIndex_IndexList");
                                }
                            }
                            catch { }
                        }
                        break;

                    case 5:
                        {
                            try
                            {
                                if (Convert.ToUInt32(SearchIndex_SearchValue) >= 0)
                                {
                                    int count = 0;
                                    for (int n = 0; n < SearchIndex_IndexList.Count; n++)
                                    {
                                        if (SearchIndex_IndexList[n].Num == Convert.ToUInt32(SearchIndex_SearchValue))
                                        {
                                            SearchIndex_IndexList[n].ItemIndex = count;
                                            count++;
                                            SearchIndex_TempIndexList.Add(SearchIndex_IndexList[n]);
                                        }
                                    }
                                    SearchIndex_IndexList.Clear();
                                    SearchIndex_IndexList = SearchIndex_TempIndexList;
                                    SearchIndex_TempIndexList.Clear();
                                    OnPropertyChanged("SearchIndex_IndexList");
                                }
                            }
                            catch { }
                        }
                        break;
                }
            }
        }

        #endregion

        #region SearchIndex_ConfirmCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand SearchIndex_ConfirmCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void SearchIndex_OnConfirm()
        {
            if (SearchIndex_CanConfirm)
            {
                dataThread.PauseThread("SearchIndex_SearchListMethod");
                Add_IndexAdd();
                Setting_Popup = 6;
            }
        }

        #endregion

        #endregion

        #region Data

        #region SearchIndex_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> SearchIndex_VersionList
        {
            get;
            set;
        }

        #endregion

        #region SearchIndex_SelectedVersionItem

        private VersionItem SearchIndex_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem SearchIndex_SelectedVersionItem
        {
            get { return SearchIndex_selectedVersionItem; }
            set
            {
                SearchIndex_selectedVersionItem = value;
                if (value != null)
                {

                }

                OnPropertyChanged("SearchIndex_SelectedVersionItem");
            }
        }

        #endregion

        #region SearchIndex_Popup

        private int SearchIndex_popup;

        /// <summary>
        /// 
        /// </summary>
        public int SearchIndex_Popup
        {
            get { return SearchIndex_popup; }
            set
            {
                SearchIndex_popup = value;
            }
        }

        #endregion

        #region SearchIndex_SearchList

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_연결 리스트 검색 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<SearchItem> SearchIndex_SearchList
        {
            get;
            set;
        }

        #endregion

        #region SearchIndex_SelectedSearchItem

        private SearchItem SearchIndex_selectedSearchItem;

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_선택된 연결 리스트 검색 아이템
        /// <para>Type: IndexItem</para>
        /// </summary>
        public SearchItem SearchIndex_SelectedSearchItem
        {
            get { return SearchIndex_selectedSearchItem; }
            set
            {
                SearchIndex_selectedSearchItem = value;
                OnPropertyChanged("SearchIndex_SearchBoxEnable");
                OnPropertyChanged("SearchIndex_SearchEnable");
            }
        }

        #endregion

        #region SearchIndex_SearchValue

        private string SearchIndex_searchValue;

        /// <summary>
        /// 
        /// </summary>
        public string SearchIndex_SearchValue
        {
            get { return SearchIndex_searchValue; }
            set
            {
                if (SearchIndex_SelectedSearchItem != null)
                {
                    switch (SearchIndex_SelectedSearchItem.Index)
                    {
                        case 1:
                            {
                                try
                                {
                                    if (Convert.ToUInt32(value, 16) >= 0)
                                    {
                                        SearchIndex_searchValue = Convert.ToUInt32(value, 16).ToString("X4");
                                        SearchIndex_SearchColor = new SolidColorBrush(Colors.Black);
                                    }
                                    else
                                        SearchIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }

                                catch
                                {
                                    SearchIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }
                            }
                            break;

                        case 2:
                            {
                                try
                                {
                                    if (Convert.ToUInt32(value, 16) >= 0)
                                    {
                                        SearchIndex_searchValue = Convert.ToUInt32(value, 16).ToString("X4");
                                        SearchIndex_SearchColor = new SolidColorBrush(Colors.Black);
                                    }
                                    else
                                        SearchIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }

                                catch
                                {
                                    SearchIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }
                            }
                            break;

                        case 3:
                            {
                                try
                                {
                                    if (Convert.ToUInt32(value) >= 0)
                                    {
                                        SearchIndex_searchValue = Convert.ToUInt32(value).ToString();
                                        SearchIndex_SearchColor = new SolidColorBrush(Colors.Black);
                                    }
                                    else
                                        SearchIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }

                                catch
                                {
                                    SearchIndex_SearchColor = new SolidColorBrush(Colors.Red);
                                }
                            }
                            break;
                    }
                    OnPropertyChanged("SearchIndex_SearchValue");
                    OnPropertyChanged("SearchIndex_SearchEnable");
                }
            }
        }

        #endregion

        #region SearchIndex_SearchBoxEnable

        /// <summary>
        /// 
        /// </summary>
        public bool SearchIndex_SearchBoxEnable
        {
            get
            {
                if (SearchIndex_SelectedSearchItem != null)
                    return true;

                return false;
            }
        }

        #endregion

        #region SearchIndex_SearchEnable

        /// <summary>
        /// 
        /// </summary>
        public bool SearchIndex_SearchEnable
        {
            get
            {
                if (SearchIndex_SelectedSearchItem != null && SearchIndex_SearchValue != null)
                    return true;

                return false;
            }
        }

        #endregion

        #region SearchIndex_SearchColor

        private Brush SearchIndex_searchColor = new SolidColorBrush(Colors.Black);

        public Brush SearchIndex_SearchColor
        {
            get { return SearchIndex_searchColor; }
            set
            {
                SearchIndex_searchColor = value;
                OnPropertyChanged("SearchIndex_SearchColor");
            }
        }

        #endregion

        #region SearchIndex_IndexList

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_인덱스 연결 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<IndexItem> SearchIndex_IndexList
        {
            get;
            set;
        }

        #endregion

        #region SearchIndex_TempIndexList

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_인덱스 연결 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<IndexItem> SearchIndex_TempIndexList
        {
            get;
            set;
        }

        #endregion

        #region SearchIndex_SelectedIndexItem

        private IndexItem SearchIndex_selectedIndexItem;

        /// <summary>
        /// 유닛 인덱스 찾기 팝업_선택된 인덱스 연결 리스트 아이템
        /// <para>Type: IndexItem</para>
        /// </summary>
        public IndexItem SearchIndex_SelectedIndexItem
        {
            get { return SearchIndex_selectedIndexItem; }
            set
            {
                if (value != null)
                {
                    SearchIndex_searchValue = null;
                    OnPropertyChanged("SearchIndex_SearchValue");
                    SearchIndex_selectedIndexItem = value;
                    OnPropertyChanged("SearchIndex_CanConfirm");
                }
                OnPropertyChanged("SearchIndex_SelectedIndexItem");
            }
        }

        #endregion

        #region SearchIndex_CanConfirm

        /// <summary>
        /// 
        /// </summary>
        public bool SearchIndex_CanConfirm
        {
            get
            {
                if (SearchIndex_SelectedIndexItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_SearchIndex()
        {
            SearchIndex_SearchCommand = new RelayCommand<object>(p => SearchIndex_OnSearch());
            SearchIndex_ConfirmCommand = new RelayCommand<object>(p => SearchIndex_OnConfirm());

            SearchIndex_VersionList = new ObservableCollection<VersionItem>();
            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    SearchIndex_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }

            SearchIndex_SearchList = new System.Collections.ObjectModel.ObservableCollection<SearchItem>();
            SearchIndex_SearchList.Add(new SearchItem() { Index = 1, Text = "번호로 검색" });
            SearchIndex_SearchList.Add(new SearchItem() { Index = 2, Text = "이름으로 검색" });
            SearchIndex_SearchList.Add(new SearchItem() { Index = 3, Text = "주소로 검색" });
            SearchIndex_SearchList.Add(new SearchItem() { Index = 4, Text = "인덱스로 검색" });
            SearchIndex_SearchList.Add(new SearchItem() { Index = 5, Text = "값으로 검색" });

            SearchIndex_IndexList = new ObservableCollection<IndexItem>();
            SearchIndex_TempIndexList = new ObservableCollection<IndexItem>();

            SearchIndex_SelectedVersionItem = SearchIndex_VersionList[0];

            dataThread.CreateThread(SearchIndex_SearchListMethod, 300);
        }

        public void Clear_SearchIndex()
        {
            SearchIndex_selectedSearchItem = null;
            SearchIndex_selectedIndexItem = null;
            SearchIndex_searchValue = null;
        }

        public void SearchIndex_SearchListMethod()
        {
            if (ph[SearchIndex_selectedVersionItem.Index].process.processState == 1)
            {
                uah[SearchIndex_selectedVersionItem.Index].SearchList(uh[SearchIndex_selectedVersionItem.Index], ph[SearchIndex_selectedVersionItem.Index]);
                DispatcherService.Invoke((System.Action)(() =>
                {
                for (int n = 0; n < SearchIndex_IndexList.Count; n++)
                {
                    
                        unsafe
                        {
                            if (uah[SearchIndex_selectedVersionItem.Index].indexList(n).grade >= 0)
                            {
                                FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[SearchIndex_selectedVersionItem.Index].grade(uah[SearchIndex_selectedVersionItem.Index].indexList(n).grade).name) +
                                "/" + Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name) + ".jpg");
                                if (fi.Exists)
                                {
                                    BitmapImage bi = new BitmapImage();
                                    bi.BeginInit();
                                    bi.CacheOption = BitmapCacheOption.OnLoad;
                                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                    bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[SearchIndex_selectedVersionItem.Index].grade(uah[SearchIndex_selectedVersionItem.Index].indexList(n).grade).name) +
                                    "/" + Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name) + ".jpg");
                                    bi.EndInit();
                                    SearchIndex_IndexList[n].Image = bi;
                                }
                                SearchIndex_IndexList[n].Address = uah[SearchIndex_selectedVersionItem.Index].indexList(n).address.ToString("X4");
                                SearchIndex_IndexList[n].Index = uah[SearchIndex_selectedVersionItem.Index].indexList(n).index.ToString("X4");
                                SearchIndex_IndexList[n].Num = uah[SearchIndex_selectedVersionItem.Index].indexList(n).num;
                                SearchIndex_IndexList[n].Name = Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name);
                                SearchIndex_IndexList[n].ItemIndex = n;
                            }
                            else
                            {
                                SearchIndex_IndexList[n].Address = uah[SearchIndex_selectedVersionItem.Index].indexList(n).address.ToString("X4");
                                SearchIndex_IndexList[n].Index = uah[SearchIndex_selectedVersionItem.Index].indexList(n).index.ToString("X4");
                                SearchIndex_IndexList[n].Num = uah[SearchIndex_selectedVersionItem.Index].indexList(n).num;
                                SearchIndex_IndexList[n].ItemIndex = n;
                            }
                        }
                    

                }
                if (SearchIndex_IndexList.Count < uah[SearchIndex_selectedVersionItem.Index].indexListSize)
                {
                        unsafe
                        {
                            for (int n = SearchIndex_IndexList.Count; n < uah[SearchIndex_selectedVersionItem.Index].indexListSize; n++)
                            {
                                if (uah[SearchIndex_selectedVersionItem.Index].indexList(n).grade >= 0)
                                {
                                    FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[SearchIndex_selectedVersionItem.Index].grade(uah[SearchIndex_selectedVersionItem.Index].indexList(n).grade).name) + "/" +
                                        Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name) + ".jpg");
                                    if (fi.Exists)
                                    {
                                        BitmapImage bi = new BitmapImage();
                                        bi.BeginInit();
                                        bi.CacheOption = BitmapCacheOption.OnLoad;
                                        bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                        bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[SearchIndex_selectedVersionItem.Index].grade(uah[SearchIndex_selectedVersionItem.Index].indexList(n).grade).name) + "/" +
                                            Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name) + ".jpg");
                                        bi.EndInit();
                                        SearchIndex_IndexList.Add(new IndexItem()
                                        {
                                            Address = uah[SearchIndex_selectedVersionItem.Index].indexList(n).address.ToString("X4"),
                                            Index = uah[SearchIndex_selectedVersionItem.Index].indexList(n).index.ToString("X4"),
                                            Num = uah[SearchIndex_selectedVersionItem.Index].indexList(n).num,
                                            Name = Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name),
                                            Image = bi,
                                            ItemIndex = n
                                        });
                                    }
                                    else
                                        SearchIndex_IndexList.Add(new IndexItem()
                                        {
                                            Address = uah[SearchIndex_selectedVersionItem.Index].indexList(n).address.ToString("X4"),
                                            Index = uah[SearchIndex_selectedVersionItem.Index].indexList(n).index.ToString("X4"),
                                            Num = uah[SearchIndex_selectedVersionItem.Index].indexList(n).num,
                                            Name = Marshal.PtrToStringAnsi((IntPtr)uah[SearchIndex_selectedVersionItem.Index].indexList(n).name),
                                            ItemIndex = n
                                        });
                                }
                                else
                                    SearchIndex_IndexList.Add(new IndexItem()
                                    {
                                        Address = uah[SearchIndex_selectedVersionItem.Index].indexList(n).address.ToString("X4"),
                                        Index = uah[SearchIndex_selectedVersionItem.Index].indexList(n).index.ToString("X4"),
                                        Num = uah[SearchIndex_selectedVersionItem.Index].indexList(n).num,
                                        ItemIndex = n
                                    });
                            }
                        }
                }
                }));
                OnPropertyChanged("SearchIndex_IndexList");
            }
        }

        #endregion
    }
}