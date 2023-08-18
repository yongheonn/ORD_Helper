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

        #region MixAdd_ConfirmCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand MixAdd_ConfirmCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void MixAdd_OnConfirm()
        {
            if(MixAdd_Popup == 1)
            {
                if (MixAdd_CanConfirm && MixAdd_SelectionMix == 1)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(MixAdd_SelectedMixImage);
                    bi.EndInit();
                    UnitAdd_MixList.Add(new MixItem()
                    {
                        Name = MixAdd_SelectedMixName,
                        Image = bi,
                        Num = MixAdd_SelectedMixNum,
                        GradeIndex = MixAdd_SelectedGradeItem.Index,
                        UnitIndex = MixAdd_SelectedUnitItem.Index,
                        Type = MixAdd_SelectionMix
                    });
                }
                else if (MixAdd_CanConfirm)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(MixAdd_SelectedMixImage);
                    bi.EndInit();
                    UnitAdd_MixList.Add(new MixItem()
                    {
                        Name = MixAdd_SelectedMixName,
                        Image = bi,
                        Num = MixAdd_SelectedMixNum,
                        Type = MixAdd_SelectionMix
                    });
                }

            if (UnitAdd_MixList.Count() == 1)
                UnitAdd_MixList[0].Index = 0;
            else if(UnitAdd_MixList.Count() > 1)
                UnitAdd_MixList[UnitAdd_MixList.Count() - 1].Index = UnitAdd_MixList[UnitAdd_MixList.Count() - 2].Index + 1;

            Clear_MixAdd();
            Setting_Popup = 2;
            OnPropertyChanged("Setting_Popup");

            }
            else if(MixAdd_Popup == 2)
            {
                if (MixAdd_CanConfirm && MixAdd_SelectionMix == 1)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(MixAdd_SelectedMixImage);
                    bi.EndInit();
                    UnitSetting_MixList.Add(new MixItem()
                    {
                        Name = MixAdd_SelectedMixName,
                        Image = bi,
                        Num = MixAdd_SelectedMixNum,
                        GradeIndex = MixAdd_SelectedGradeItem.Index,
                        UnitIndex = MixAdd_SelectedUnitItem.Index,
                        Type = MixAdd_SelectionMix
                    });
                    SubUnitMix subUnitMix = new SubUnitMix();
                    subUnitMix.basicNum = MixAdd_SelectedMixNum;
                    subUnitMix.gradeIndex = MixAdd_SelectedGradeItem.Index;
                    subUnitMix.unitIndex = MixAdd_SelectedUnitItem.Index;
                    uh_temp[UnitSetting_selectedVersionItem.Index].AddSubUnitMix(UnitSetting_SelectedGradeItem.Index, UnitSetting_SelectedUnitItem.Index, subUnitMix);
                }
                else if (MixAdd_CanConfirm)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(MixAdd_SelectedMixImage);
                    bi.EndInit();
                    UnitSetting_MixList.Add(new MixItem()
                    {
                        Name = MixAdd_SelectedMixName,
                        Image = bi,
                        Num = MixAdd_SelectedMixNum,
                        Type = MixAdd_SelectionMix
                    });
                }
                if (UnitSetting_MixList.Count() == 1)
                    UnitSetting_MixList[UnitSetting_MixList.Count() - 1].Index = 0;
                else if (UnitSetting_MixList.Count() > 1)
                    UnitSetting_MixList[UnitSetting_MixList.Count() - 1].Index = UnitSetting_MixList[UnitSetting_MixList.Count() - 2].Index + 1;
                if(MixAdd_SelectionMix == 2)
                    uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).mixResources.basicGold = MixAdd_SelectedMixNum;
                else if(MixAdd_SelectionMix == 3)
                    uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).mixResources.basicTree = MixAdd_SelectedMixNum;

                Clear_MixAdd();
                Setting_Popup = 0;
                OnPropertyChanged("Setting_Popup");
            }
        }

        #endregion

        #region MixAdd_ExitCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand MixAdd_ExitCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void MixAdd_OnExit()
        {
            Clear_MixAdd();
            if(MixAdd_Popup == 1)
                Setting_Popup = 2;
            else if(MixAdd_Popup == 2)
                Setting_Popup = 0;
        }

        #endregion

        #region MixAdd_EnterCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 ICommand
        /// </summary>
        public ICommand MixAdd_EnterCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 추가 Command 실행 시
        /// </summary>
        private void MixAdd_OnEnter()
        {
            MixAdd_SearchUnit = MixAdd_SearchUnit;
        }

        #endregion

        #endregion

        #region Data

        #region MixAdd_Popup

        private int MixAdd_popup;

        /// <summary>
        /// 
        /// </summary>
        public int MixAdd_Popup
        {
            get { return MixAdd_popup; }
            set
            {
                MixAdd_popup = value;
            }
        }

        #endregion

        #region MixAdd_AddList

        /// <summary>
        /// 조합 추가 팝업_추가할 조합 유형 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<MixTypeItem> MixAdd_AddList
        {
            get;
            set;
        }

        #endregion

        #region MixAdd_SelectedAddItem

        private MixTypeItem MixAdd_selectedAddItem;

        /// <summary>
        /// 
        /// </summary>
        public MixTypeItem MixAdd_SelectedAddItem
        {
            get { return MixAdd_selectedAddItem; }
            set
            {
                MixAdd_SelectedUnitItem = null;
                MixAdd_SelectedGradeItem = null;
                MixAdd_mixUnitNum = 0;
                MixAdd_mixGoldNum = 0;
                MixAdd_mixTreeNum = 0;
                MixAdd_SelectedMixName = null;
                MixAdd_SelectedMixImage = null;
                MixAdd_SelectedMixNum = 0;
                MixAdd_color = new SolidColorBrush(Colors.Black);
                if (value != null)
                {
                    MixAdd_selectedAddItem = value;
                    MixAdd_SelectionMix = value.Index;
                }
            }
        }

        #endregion

        #region MixAdd_SelectionMix

        private int MixAdd_selectionMix = 0;

        public int MixAdd_SelectionMix
        {
            get { return MixAdd_selectionMix; }
            set
            {
                MixAdd_selectionMix = value;
                if(MixAdd_selectionMix == 2)
                    MixAdd_SelectedMixName = "골드";
                else if(MixAdd_selectionMix == 3)
                    MixAdd_SelectedMixName = "나무";

                OnPropertyChanged("MixAdd_SelectionMix");
            }
        }

        #endregion

        #region MixAdd_GradeList

        /// <summary>
        /// 유닛 설정 탭_등급 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<GradeItem> MixAdd_GradeList
        {
            get;
            set;
        }

        #endregion

        #region MixAdd_SelectedGradeItem

        private GradeItem MixAdd_selectedGradeItem;

        /// <summary>
        /// 조합 추가_선택된 등급 아이템
        /// <para>Type: GradeItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public GradeItem MixAdd_SelectedGradeItem
        {
            get { return MixAdd_selectedGradeItem; }
            set
            {
                MixAdd_selectedGradeItem = value;
                MixAdd_ChangedUnitItem = true;
            }
        }

        #endregion

        #region MixAdd_SearchUnit

        private string MixAdd_searchUnit;
        private int MixAdd_searchUnitIndex = -1;

        public string MixAdd_SearchUnit
        {
            get { return MixAdd_searchUnit; }
            set
            {
                bool check = false;
                int count = 0;
                MixAdd_searchUnit = value;
                OnPropertyChanged("MixAdd_SearchUnit");
                if (MixAdd_searchUnit != null)
                {
                    while (!check && count < 2 && MixAdd_UnitList.Count > 0)
                    {
                        for (int n = MixAdd_searchUnitIndex + 1; n < MixAdd_UnitList.Count; n++)
                        {
                            if (MixAdd_UnitList[n].Name.ToUpper().StartsWith(MixAdd_searchUnit.ToUpper()))
                            {
                                MixAdd_SelectedUnitItem = MixAdd_UnitList[n];
                                MixAdd_searchUnitIndex = MixAdd_UnitList[n].Index;
                                check = true;
                                break;
                            }
                            if (check == false && count < 2 && n == MixAdd_UnitList.Count - 1)
                            {
                                MixAdd_searchUnitIndex = -1;
                                count++;
                            }
                        }
                        if (MixAdd_searchUnitIndex + 1 == MixAdd_UnitList.Count)
                            MixAdd_searchUnitIndex = -1;
                    }
                }
                else
                {
                    MixAdd_searchUnitIndex = 0;
                    MixAdd_SelectedUnitItem = null;
                }
            }
        }

        #endregion

        #region MixAdd_UnitList

        /// <summary>
        /// 조합 추가 팝업_유닛 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<UnitItem> MixAdd_UnitList
        {
            get;
            set;
        }

        #endregion

        #region MixAdd_SelectedUnitItem

        private UnitItem MixAdd_selectedUnitItem;

        /// <summary>
        /// 조합 추가 팝업_선택된 유닛 아이템
        /// <para>Type: UnitItem</para>
        /// </summary>
        public UnitItem MixAdd_SelectedUnitItem
        {
            get { return MixAdd_selectedUnitItem; }
            set
            {
                MixAdd_selectedUnitItem = value;
                if(MixAdd_selectedUnitItem != null)
                    MixAdd_SelectedMixName = MixAdd_selectedUnitItem.Name;
                OnPropertyChanged("MixAdd_CanConfirm");
                OnPropertyChanged("MixAdd_SelectedUnitItem");
            }
        }

        #endregion

        #region MixAdd_ChangedUnitItem

        private bool MixAdd_changedUnitItem = false;

        /// <summary>
        /// 조합 추가 팝업_유닛 아이템 변경 Property
        /// <para>Type: bool</para>
        /// </summary>
        public bool MixAdd_ChangedUnitItem
        {
            get { return MixAdd_changedUnitItem; }
            set
            {
                MixAdd_changedUnitItem = value;
                if (MixAdd_changedUnitItem == true && MixAdd_selectedGradeItem != null)
                {
                    MixAdd_UnitList.Clear();

                    string[] unitName = new string[uh_temp[UnitSetting_selectedVersionItem.Index].grade(MixAdd_selectedGradeItem.Index).unitNum];
                    for (int n = 0; n < uh_temp[UnitSetting_selectedVersionItem.Index].grade(MixAdd_selectedGradeItem.Index).unitNum; n++)
                    {
                        unsafe
                        {
                            unitName[n] = Marshal.PtrToStringAnsi((IntPtr)uh_temp[UnitSetting_selectedVersionItem.Index].grade(MixAdd_selectedGradeItem.Index).unit(n).name);
                        }
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/" + MixAdd_SelectedGradeItem.Name + "/" + unitName[n] + ".jpg");
                        bi.EndInit();
                        MixAdd_UnitList.Add(new UnitItem() { Name = unitName[n], Image = bi, Index = n });
                    }
                    OnPropertyChanged("MixAdd_UnitList");
                }
                else if (MixAdd_SelectedGradeItem == null)
                {
                    MixAdd_UnitList.Clear();
                    OnPropertyChanged("MixAdd_UnitList");
                }
            }
        }

        #endregion

        #region MixAdd_MixUnitNum

        private int MixAdd_mixUnitNum = 0;

        /// <summary>
        /// 
        /// </summary>
        public string MixAdd_MixUnitNum
        {
            get { return MixAdd_mixUnitNum.ToString(); }
            set
            {
                try
                {
                    if (Convert.ToUInt32(value) > 0)
                    {
                        MixAdd_mixUnitNum = Convert.ToInt32(value);
                        MixAdd_SelectedMixNum = MixAdd_mixUnitNum;
                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Black);
                        OnPropertyChanged("MixAdd_CanConfirm");
                    }
                    else
                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                }

                catch
                {
                    UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                }
            }
        }

        #endregion

        #region MixAdd_MixGoldNum

        private int MixAdd_mixGoldNum = 0;

        /// <summary>
        /// 
        /// </summary>
        public string MixAdd_MixGoldNum
        {
            get { return MixAdd_mixGoldNum.ToString(); }
            set
            {
                try
                {
                    if (Convert.ToUInt32(value) > 0)
                    {
                        MixAdd_mixGoldNum = Convert.ToInt32(value);
                        MixAdd_SelectedMixNum = MixAdd_mixGoldNum;
                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Black);
                        OnPropertyChanged("MixAdd_CanConfirm");
                    }
                    else
                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                }

                catch
                {
                    UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                }
            }
        }

        #endregion

        #region MixAdd_MixTreeNum

        private int MixAdd_mixTreeNum = 0;

        /// <summary>
        /// 
        /// </summary>
        public string MixAdd_MixTreeNum
        {
            get { return MixAdd_mixTreeNum.ToString(); }
            set
            {
                try
                {
                    if (Convert.ToUInt32(value) > 0)
                    {
                        MixAdd_mixTreeNum = Convert.ToInt32(value);
                        MixAdd_SelectedMixNum = MixAdd_mixTreeNum;
                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Black);
                        OnPropertyChanged("MixAdd_CanConfirm");
                    }
                    else
                        UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                }

                catch
                {
                    UnitIndex_SearchColor = new SolidColorBrush(Colors.Red);
                }
            }
        }

        #endregion

        #region MixAdd_Color

        private Brush MixAdd_color = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 조합 추가 팝업_배경색 Property
        /// <para>Type: Brush</para>
        /// </summary>
        public Brush MixAdd_Color
        {
            get { return MixAdd_color; }
            set
            {
                MixAdd_color = value;
                OnPropertyChanged("MixAdd_Color");
            }
        }

        #endregion

        #region MixAdd_SelectedMixName

        private string MixAdd_selectedMixName;

        /// <summary>
        /// 
        /// </summary>
        public string MixAdd_SelectedMixName
        {
            get { return MixAdd_selectedMixName; }
            set
            {
                MixAdd_selectedMixName = value;
                unsafe
                {
                    if (value == "골드" || value == "나무")
                        MixAdd_SelectedMixImage = AppDomain.CurrentDomain.BaseDirectory + "Resources/자원/" + value + ".jpg";
                    else if (MixAdd_SelectedGradeItem != null)
                        MixAdd_SelectedMixImage = AppDomain.CurrentDomain.BaseDirectory + "Resources/" + MixAdd_SelectedGradeItem.Name + "/" + value + ".jpg";
                }
            }
        }

        #endregion

        #region MixAdd_SelectedMixImage

        private string MixAdd_selectedMixImage;

        /// <summary>
        /// 조합 추가 탭_선택된 조합 이미지 정보
        /// </summary>
        public string MixAdd_SelectedMixImage
        {
            get { return MixAdd_selectedMixImage; }
            set
            {
                MixAdd_selectedMixImage = value;
            }
        }

        #endregion

        #region MixAdd_SelectedMixNum

        private int MixAdd_selectedMixNum;

        /// <summary>
        /// 조합 추가 탭_선택된 조합 숫자 정보
        /// <para>Type: int</para>
        /// </summary>
        public int MixAdd_SelectedMixNum
        {
            get { return MixAdd_selectedMixNum; }
            set
            {
                MixAdd_selectedMixNum = value;
            }
        }

        #endregion

        #region MixAdd_CanConfirm

        /// <summary>
        /// 조합 추가 탭_확인 가능 여부
        /// <para>Type: bool</para>
        /// <para>Binding: IsEnabled</para>
        /// </summary>
        public bool MixAdd_CanConfirm
        {
            get
            {
                if (MixAdd_SelectedMixName != null && MixAdd_SelectedMixImage != null && MixAdd_SelectedMixNum > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region MixAdd_CloseImage

        public string MixAdd_CloseImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/close.png"; }
        }

        #endregion

        #endregion

        #region Method

        public void Load_MixAdd()
        {
            MixAdd_GradeList = new ObservableCollection<GradeItem>();

            MixAdd_AddList = new ObservableCollection<MixTypeItem>();
            MixAdd_AddList.Add(new MixTypeItem() { Text = "유닛 추가", Index = 1 });
            MixAdd_AddList.Add(new MixTypeItem() { Text = "골드 추가", Index = 2 });
            MixAdd_AddList.Add(new MixTypeItem() { Text = "나무 추가", Index = 3 });

            MixAdd_UnitList = new System.Collections.ObjectModel.ObservableCollection<UnitItem>();

            MixAdd_ConfirmCommand = new RelayCommand<object>(p => MixAdd_OnConfirm());
            MixAdd_ExitCommand = new RelayCommand<object>(p => MixAdd_OnExit());
            MixAdd_EnterCommand = new RelayCommand<object>(p => MixAdd_OnEnter());
        }

        public void Clear_MixAdd()
        {
            MixAdd_GradeList.Clear();
            MixAdd_SelectedAddItem = new MixTypeItem() { Text = "추가할 조합 목록의 유형을 선택", Index = 0 };
            OnPropertyChanged("MixAdd_SelectedAddItem");
            MixAdd_SelectionMix = 0;
            MixAdd_SelectedGradeItem = null;
            MixAdd_searchUnit = null;
            MixAdd_searchUnitIndex = 0;
            MixAdd_UnitList.Clear();
            MixAdd_SelectedUnitItem = null;
            MixAdd_ChangedUnitItem = false;
            MixAdd_mixUnitNum = 0;
            MixAdd_Color = new SolidColorBrush(Colors.Black);
            MixAdd_mixGoldNum = 0;
            MixAdd_mixTreeNum = 0;
            MixAdd_SelectedMixName = null;
            MixAdd_SelectedMixImage = null;
            MixAdd_SelectedMixNum = 0;
        }

        #endregion
    }
}
