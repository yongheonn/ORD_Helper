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
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region UnitAdd_LoadImageCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 ICommand
        /// </summary>
        public ICommand UnitAdd_LoadImageCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitAdd_OnLoadImage()
        {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if(openFileDialog.ShowDialog().ToString() == "OK")
                {
                    FileInfo fi = new FileInfo(openFileDialog.FileName);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.UriSource = new Uri(openFileDialog.FileName);
                bi.EndInit();
                UnitAdd_UnitImage = bi;
                UnitAdd_unitImageString = openFileDialog.FileName;
                }
        }

        #endregion

        #region UnitAdd_MixDeleteCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 ICommand
        /// </summary>
        public ICommand UnitAdd_MixDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitAdd_OnMixDelete()
        {
            if (UnitAdd_SelectedMixItem != null)
            {
                for (int n = UnitAdd_SelectedMixItem.Index + 1; n < UnitAdd_MixList.Count(); n++)
                {
                    UnitAdd_MixList[n].Index--;
                }

                UnitAdd_MixList.RemoveAt(UnitAdd_SelectedMixItem.Index);

                OnPropertyChanged("UnitAdd_MixList");
            }
        }

        #endregion

        #region UnitAdd_MixAddCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 ICommand
        /// </summary>
        public ICommand UnitAdd_MixAddCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitAdd_OnMixAdd()
        {
            MixAdd_GradeList.Clear();
            for (int n = 0; n < UnitSetting_GradeList.Count; n++)
            {
                MixAdd_GradeList.Add(new GradeItem() { Index = UnitSetting_GradeList[n].Index, Name = UnitSetting_GradeList[n].Name });
            }
            MixAdd_Popup = 1;
            Setting_Popup = 3;
        }

        #endregion

        #region UnitAdd_IndexSearchCommand

        /// <summary>
        /// 유닛 추가 팝업_인덱스 검색 ICommand
        /// </summary>
        public ICommand UnitAdd_IndexSearchCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitAdd_OnIndexSearch()
        {
            dataThread.ResumeThread("SearchList");
            UnitIndex_Popup = 1;
            Setting_Popup = 4;
        }

        #endregion

        #region UnitAdd_ExitCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 ICommand
        /// </summary>
        public ICommand UnitAdd_ExitCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitAdd_OnExit()
        {
            Clear_UnitAdd();
            Setting_Popup = 0;
        }

        #endregion

        #region UnitAdd_ConfirmCommand

        /// <summary>
        /// 유닛 추가 팝업_확인 ICommand
        /// </summary>
        public ICommand UnitAdd_ConfirmCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_확인 Command 실행 시
        /// </summary>
        private void UnitAdd_OnConfirm()
        {
            if (UnitAdd_ConfirmEnable)
            {
                unsafe
                {
                    List<SubUnitMix> subUnitMix = new List<SubUnitMix>();
                    MixResources mixResources = new MixResources();
                    for (int n = 0; n < UnitAdd_MixList.Count(); n++)
                    {
                        if (UnitAdd_MixList[n].Type == 1)
                            subUnitMix.Add(new SubUnitMix() { basicNum = UnitAdd_MixList[n].Num, gradeIndex = UnitAdd_MixList[n].GradeIndex,
                            unitIndex = UnitAdd_MixList[n].UnitIndex});
                        else if(UnitAdd_MixList[n].Type == 2)
                            mixResources.basicGold = UnitAdd_MixList[n].Num;
                        else if (UnitAdd_MixList[n].Type == 3)
                            mixResources.basicTree = UnitAdd_MixList[n].Num;
                    }
                    uh_temp[UnitSetting_selectedVersionItem.Index].AddUnit(UnitSetting_SelectedGradeItem.Index, (sbyte*)(void*)Marshal.StringToHGlobalAnsi(UnitAdd_UnitName), 
                        subUnitMix.ToArray<SubUnitMix>(), mixResources, Convert.ToUInt32(UnitAdd_UnitIndex, 16));
                }
                if (UnitAdd_unitImageString != null)
                {
                    FileInfo fi = new FileInfo(UnitAdd_unitImageString);
                    if (UnitAdd_unitImageString != AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name +
                                    "\\" + UnitAdd_unitName + ".jpg")
                    {
                        fi.CopyTo(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name +
                                    "\\" + UnitAdd_unitName + ".jpg", true);
                    }
                }
                

                UnitSetting_UpdateUnitItem = true;
                UnitSetting_ChangedUnitItem = true;
                Clear_UnitAdd();
                Setting_Popup = 0;
            }
        }

        #endregion

        #endregion

        #region Data

        #region UnitAdd_UnitName

        private string UnitAdd_unitName;

        /// <summary>
        /// 유닛 추가 팝업_유닛 이름 텍스트박스 Property
        /// <para>Type: string</para>
        /// <para>Binding: Text</para>
        /// </summary>
        public string UnitAdd_UnitName
        {
            get { return UnitAdd_unitName; }
            set
            {
                UnitAdd_unitName = value;
                UnitAdd_UnitNameColor = new SolidColorBrush(Colors.Black);
                if (UnitAdd_unitName != null)
                {
                    FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name +
                                    "\\" + UnitAdd_unitName + ".jpg");
                    if (fi.Exists)
                    {
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name +
                                        "\\" + UnitAdd_unitName + ".jpg");
                        bi.EndInit();
                        UnitAdd_UnitImage = bi;
                        UnitAdd_unitImageString = AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name +
                                        "\\" + UnitAdd_unitName + ".jpg";
                    }
                }
                OnPropertyChanged("UnitAdd_UnitName");
                OnPropertyChanged("UnitAdd_ConfirmEnable");
            }
        }

        #endregion

        #region UnitAdd_UnitNameColor

        private Brush UnitAdd_unitNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 추가 팝업_유닛 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush UnitAdd_UnitNameColor
        {
            get { return UnitAdd_unitNameColor; }
            set
            {
                UnitAdd_unitNameColor = value;
                OnPropertyChanged("UnitAdd_UnitNameColor");
            }
        }

        #endregion

        #region UnitAdd_unitImageString

        private string UnitAdd_unitImageString;

        #endregion

        #region UnitAdd_UnitImage

        private ImageSource UnitAdd_unitImage;

        /// <summary>
        /// 
        /// </summary>
        public ImageSource UnitAdd_UnitImage
        {
            get { return UnitAdd_unitImage; }
            set
            {
                UnitAdd_unitImage = value;
                OnPropertyChanged("UnitAdd_UnitImage");
            }
        }

        #endregion

        #region UnitAdd_MixList

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<MixItem> UnitAdd_MixList
        {
            get;
            set;
        }

        #endregion

        #region UnitAdd_SelectedMixItem

        private MixItem UnitAdd_selectedMixItem;

        /// <summary>
        /// 유닛 추가 팝업_선택된 조합 목록 아이템
        /// <para>Type: MixItem</para>
        /// </summary>
        public MixItem UnitAdd_SelectedMixItem
        {
            get { return UnitAdd_selectedMixItem; }
            set
            {
                UnitAdd_selectedMixItem = value;
                OnPropertyChanged("UnitAdd_CanMixDelete");
            }
        }

        #endregion

        #region UnitAdd_CanMixDelete

        /// <summary>
        /// 
        /// </summary>
        public bool UnitAdd_CanMixDelete
        {
            get
            {
                if (UnitAdd_SelectedMixItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitAdd_UnitIndex

        private string UnitAdd_unitIndex = "0x00000000";
        
        /// <summary>
        /// 
        /// </summary>
        public string UnitAdd_UnitIndex
        {
            get { return UnitAdd_unitIndex; }
            set
            {
                try
                {
                    if (Convert.ToUInt32(value, 16) >= 0)
                    {
                        UnitAdd_unitIndex = Convert.ToUInt32(value, 16).ToString("X4");
                        UnitAdd_UnitIndexColor = new SolidColorBrush(Colors.Black);
                    }
                    else
                        UnitAdd_UnitIndexColor = new SolidColorBrush(Colors.Red);
                }

                catch
                {
                    UnitAdd_UnitIndexColor = new SolidColorBrush(Colors.Red);
                }
                OnPropertyChanged("UnitAdd_UnitIndex");
                OnPropertyChanged("UnitAdd_ConfirmEnable");
            }
        }

        #endregion

        #region UnitAdd_UnitIndexColor

        private Brush UnitAdd_unitIndexColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 추가 팝업_유닛 인덱스 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush UnitAdd_UnitIndexColor
        {
            get { return UnitAdd_unitIndexColor; }
            set
            {
                UnitAdd_unitIndexColor = value;
                OnPropertyChanged("UnitAdd_UnitIndexColor");
            }
        }

        #endregion

        #region UnitAdd_ConfirmEnable

        /// <summary>
        /// 
        /// </summary>
        public bool UnitAdd_ConfirmEnable
        {
            get
            {
                try
                {
                    if (UnitAdd_UnitName != null && Convert.ToUInt32(UnitAdd_UnitIndex, 16) >= 0)
                        return true;

                    if(UnitAdd_UnitName == null)
                        UnitAdd_UnitNameColor = new SolidColorBrush(Colors.Red);

                    if(Convert.ToUInt32(UnitAdd_UnitIndex, 16) < 0)
                        UnitAdd_UnitIndexColor = new SolidColorBrush(Colors.Red);
                }
                catch { UnitAdd_UnitIndexColor = new SolidColorBrush(Colors.Red); }
                
                return false;
            }
        }

        #endregion

        #region UnitAdd_CloseImage

        public string UnitAdd_CloseImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/close.png"; }
        }

        #endregion

        #endregion

        #region Method

            public void Load_UnitAdd()
        {
            UnitAdd_LoadImageCommand = new RelayCommand<object>(p => UnitAdd_OnLoadImage());
            UnitAdd_MixAddCommand = new RelayCommand<object>(p => UnitAdd_OnMixAdd());
            UnitAdd_MixDeleteCommand = new RelayCommand<object>(p => UnitAdd_OnMixDelete());
            UnitAdd_IndexSearchCommand = new RelayCommand<object>(p => UnitAdd_OnIndexSearch());
            UnitAdd_ConfirmCommand = new RelayCommand<object>(p => UnitAdd_OnConfirm());
            UnitAdd_ExitCommand = new RelayCommand<object>(p => UnitAdd_OnExit());

            UnitAdd_MixList = new ObservableCollection<MixItem>();
        }

        public void Clear_UnitAdd()
        {
            UnitAdd_UnitName = null;
            UnitAdd_unitImage = null;
            UnitAdd_unitImageString = null;
            UnitAdd_MixList.Clear();
            UnitAdd_SelectedMixItem = null;
            UnitAdd_unitIndex = "0x00000000";
        }

        #endregion
    }
}
