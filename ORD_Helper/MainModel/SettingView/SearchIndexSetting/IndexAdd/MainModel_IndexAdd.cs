using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region IndexAdd_ExitCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand IndexAdd_ExitCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void IndexAdd_OnExit()
        {
            Clear_IndexAdd();
            Setting_Popup = 0;
        }

        #endregion

        #region IndexAdd_ConfirmCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand IndexAdd_ConfirmCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void IndexAdd_OnConfirm()
        {
            if (IndexAdd_CanConfirm)
            {
                uh[SearchIndex_SelectedVersionItem.Index].grade(IndexAdd_SelectedGradeItem.Index).unit(IndexAdd_SelectedUnitItem.Index).index = Convert.ToUInt32(SearchIndex_SelectedIndexItem.Index, 16);
                uh_temp[SearchIndex_SelectedVersionItem.Index].grade(IndexAdd_SelectedGradeItem.Index).unit(IndexAdd_SelectedUnitItem.Index).index = Convert.ToUInt32(SearchIndex_SelectedIndexItem.Index, 16);
                unsafe
                {
                    uh[SearchIndex_SelectedVersionItem.Index].SaveUnitInfo(vh.ver(SearchIndex_SelectedVersionItem.Index).verName);
                }
            }
            Clear_IndexAdd();
            Setting_Popup = 0;
        }

        #endregion

        #region IndexAdd_EnterCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand IndexAdd_EnterCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void IndexAdd_OnEnter()
        {
            IndexAdd_SearchUnit = IndexAdd_SearchUnit;
        }

        #endregion

        #endregion

        #region Data

        #region IndexAdd_CloseImage

        public string IndexAdd_CloseImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/close.png"; }
        }

        #endregion

        #region IndexAdd_GradeList

        /// <summary>
        /// 유닛 설정 탭_등급 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<GradeItem> IndexAdd_GradeList
        {
            get;
            set;
        }

        #endregion

        #region IndexAdd_SelectedGradeItem

        private GradeItem IndexAdd_selectedGradeItem;

        /// <summary>
        /// 조합 추가_선택된 등급 아이템
        /// <para>Type: GradeItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public GradeItem IndexAdd_SelectedGradeItem
        {
            get { return IndexAdd_selectedGradeItem; }
            set
            {
                IndexAdd_selectedGradeItem = value;
                IndexAdd_ChangedUnitItem = true;
            }
        }

        #endregion

        #region IndexAdd_SearchUnit

        private string IndexAdd_searchUnit;
        private int IndexAdd_searchUnitIndex = -1;

        public string IndexAdd_SearchUnit
        {
            get { return IndexAdd_searchUnit; }
            set
            {
                bool check = false;
                int count = 0;
                IndexAdd_searchUnit = value;
                OnPropertyChanged("IndexAdd_SearchUnit");
                if (IndexAdd_searchUnit != null)
                {
                    while (!check && count < 2 && IndexAdd_UnitList.Count > 0)
                    {
                        for (int n = IndexAdd_searchUnitIndex + 1; n < IndexAdd_UnitList.Count; n++)
                        {
                            if (IndexAdd_UnitList[n].Name.ToUpper().StartsWith(IndexAdd_searchUnit.ToUpper()))
                            {
                                IndexAdd_SelectedUnitItem = IndexAdd_UnitList[n];
                                IndexAdd_searchUnitIndex = IndexAdd_UnitList[n].Index;
                                check = true;
                                break;
                            }
                            if (check == false && count < 2 && n == IndexAdd_UnitList.Count - 1)
                            {
                                IndexAdd_searchUnitIndex = -1;
                                count++;
                            }
                        }
                        if (IndexAdd_searchUnitIndex + 1 == IndexAdd_UnitList.Count)
                            IndexAdd_searchUnitIndex = -1;
                    }
                }
                else
                {
                    IndexAdd_searchUnitIndex = 0;
                    IndexAdd_SelectedUnitItem = null;
                }
            }
        }

        #endregion

        #region IndexAdd_UnitList

        /// <summary>
        /// 조합 추가 팝업_유닛 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<UnitItem> IndexAdd_UnitList
        {
            get;
            set;
        }

        #endregion

        #region IndexAdd_SelectedUnitItem

        private UnitItem IndexAdd_selectedUnitItem;

        /// <summary>
        /// 조합 추가 팝업_선택된 유닛 아이템
        /// <para>Type: UnitItem</para>
        /// </summary>
        public UnitItem IndexAdd_SelectedUnitItem
        {
            get { return IndexAdd_selectedUnitItem; }
            set
            {
                IndexAdd_selectedUnitItem = value;
                OnPropertyChanged("IndexAdd_CanConfirm");
                OnPropertyChanged("IndexAdd_SelectedUnitItem");
            }
        }

        #endregion

        #region IndexAdd_ChangedUnitItem

        private bool IndexAdd_changedUnitItem = false;

        /// <summary>
        /// 조합 추가 팝업_유닛 아이템 변경 Property
        /// <para>Type: bool</para>
        /// </summary>
        public bool IndexAdd_ChangedUnitItem
        {
            get { return IndexAdd_changedUnitItem; }
            set
            {
                IndexAdd_changedUnitItem = value;
                if (IndexAdd_changedUnitItem == true && IndexAdd_selectedGradeItem != null)
                {
                    IndexAdd_UnitList.Clear();

                    string[] unitName = new string[uh_temp[SearchIndex_selectedVersionItem.Index].grade(IndexAdd_selectedGradeItem.Index).unitNum];
                    for (int n = 0; n < uh_temp[SearchIndex_selectedVersionItem.Index].grade(IndexAdd_selectedGradeItem.Index).unitNum; n++)
                    {
                        unsafe
                        {
                            unitName[n] = Marshal.PtrToStringAnsi((IntPtr)uh_temp[SearchIndex_selectedVersionItem.Index].grade(IndexAdd_selectedGradeItem.Index).unit(n).name);
                        }
                        FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + IndexAdd_SelectedGradeItem.Name + "/" + unitName[n] + ".jpg");
                        if (fi.Exists)
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + IndexAdd_SelectedGradeItem.Name + "/" + unitName[n] + ".jpg");
                            bi.EndInit();
                            IndexAdd_UnitList.Add(new UnitItem() { Name = unitName[n], Image = bi, Index = n });
                        }
                        else
                            IndexAdd_UnitList.Add(new UnitItem() { Name = unitName[n], Index = n });
                    }
                    OnPropertyChanged("IndexAdd_UnitList");
                }
                else if (IndexAdd_SelectedGradeItem == null)
                {
                    IndexAdd_UnitList.Clear();
                    OnPropertyChanged("IndexAdd_UnitList");
                }
            }
        }

        #endregion

        #region IndexAdd_CanConfirm

        /// <summary>
        /// 조합 추가 탭_확인 가능 여부
        /// <para>Type: bool</para>
        /// <para>Binding: IsEnabled</para>
        /// </summary>
        public bool IndexAdd_CanConfirm
        {
            get
            {
                if (IndexAdd_selectedGradeItem != null && IndexAdd_selectedUnitItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_IndexAdd()
        {
            IndexAdd_ExitCommand = new RelayCommand<object>(p => IndexAdd_OnExit());
            IndexAdd_EnterCommand = new RelayCommand<object>(p => IndexAdd_OnEnter());
            IndexAdd_ConfirmCommand = new RelayCommand<object>(p => IndexAdd_OnConfirm());

            IndexAdd_GradeList = new ObservableCollection<GradeItem>();
            IndexAdd_UnitList = new ObservableCollection<UnitItem>();
        }

        public void Add_IndexAdd()
        {
            for (int n = 0; n < uh[SearchIndex_SelectedVersionItem.Index].gradeNum; n++)
            {
                unsafe
                {
                    IndexAdd_GradeList.Add(new GradeItem() { Name = Marshal.PtrToStringAnsi((IntPtr)uh[SearchIndex_SelectedVersionItem.Index].grade(n).name), Index = n });
                }
            }
            OnPropertyChanged("IndexAdd_GradeList");
            IndexAdd_ChangedUnitItem = true;
        }

        public void Clear_IndexAdd()
        {
            IndexAdd_GradeList.Clear();
            IndexAdd_UnitList.Clear();
        }

        #endregion
    }
}
