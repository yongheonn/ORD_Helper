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
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region UnitSetting_SaveCommand

        /// <summary>
        /// 유닛 설정 탭_저장 ICommand
        /// </summary>
        public ICommand UnitSetting_SaveCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_저장 Command 실행 시
        /// </summary>
        private void UnitSetting_OnSave()
        {
            dataThread.PauseThread("SearchList");

            uh[UnitSetting_selectedVersionItem.Index].Copy(uh_temp[UnitSetting_selectedVersionItem.Index]);
            unsafe
            {
                uh[UnitSetting_selectedVersionItem.Index].SaveUnitInfo(vh.ver(UnitSetting_selectedVersionItem.Index).verName);
            }
                UnitSetting_ChangedUnitItem = false;
        }

        #endregion

        #region UnitSetting_GradeDeleteCommand

        /// <summary>
        /// 유닛 설정 탭_등급 삭제 ICommand
        /// </summary>
        public ICommand UnitSetting_GradeDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_등급 삭제 Command 실행 시
        /// </summary>
        private void UnitSetting_OnGradeDelete()
        {
            if (UnitSetting_selectedGradeItem != null)
            {
                bool check = false;
                for (int n = UnitSetting_selectedGradeItem.Index + 1; n < uh_temp[UnitSetting_selectedVersionItem.Index].gradeNum; n++)
                {
                    UnitSetting_GradeList[n].Index--;
                }
                for(int n = 0; n < vh.verNum; n++)
                {
                    if (n != UnitSetting_selectedVersionItem.Index)
                    {
                        for (int i = 0; i < uh_temp[n].gradeNum; i++)
                        {
                            unsafe
                            {
                                if (uh_temp[n].grade(i).name == uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_selectedGradeItem.Index).name)
                                {
                                    check = true;
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name);
                    di.Delete(true);
                }

                uh_temp[UnitSetting_selectedVersionItem.Index].RemoveGrade(UnitSetting_selectedGradeItem.Index);
                UnitSetting_GradeList.RemoveAt(UnitSetting_selectedGradeItem.Index);
                

                UnitSetting_UpdateUnitItem = true;
                UnitSetting_ChangedUnitItem = true;

                OnPropertyChanged("UnitSetting_GradeList");
            }
        }

        #endregion

        #region UnitSetting_GradeEditCommand

        /// <summary>
        /// 유닛 설정 탭_등급 편집 ICommand
        /// </summary>
        public ICommand UnitSetting_GradeEditCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_등급 편집 Command 실행 시
        /// </summary>
        private void UnitSetting_OnGradeEdit()
        {
            if (UnitSetting_selectedGradeItem != null)
            {
                UnitSetting_GradeEnable = true;
            }
        }

        #endregion

        #region UnitSetting_UnitDeleteCommand
        /// <summary>
        /// 유닛 설정 탭_유닛 삭제 ICommand
        /// </summary>
        public ICommand UnitSetting_UnitDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 삭제 Command 실행 시
        /// </summary>
        private void UnitSetting_OnUnitDelete()
        {
            if (UnitSetting_selectedUnitItem != null)
            {
                bool check = false;
                for (int n = 0; n < vh.verNum; n++)
                {
                    if (n != UnitSetting_selectedVersionItem.Index)
                    {
                        for (int i = 0; i < uh_temp[n].gradeNum; i++)
                        {
                            for(int j = 0; j < uh_temp[n].grade(i).unitNum; j++)
                            {
                                unsafe
                                {
                                    if (uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).name
                                        == uh_temp[n].grade(i).unit(j).name)
                                        check = true;
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_selectedGradeItem.Name + "\\" + UnitSetting_selectedUnitItem.Name + ".jpg");
                    fi.Delete();
                }

                for (int n = UnitSetting_selectedUnitItem.Index + 1; n < uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_selectedGradeItem.Index).unitNum; n++)
                {
                    UnitSetting_UnitList[n].Index--;
                }

                uh_temp[UnitSetting_selectedVersionItem.Index].RemoveUnit(UnitSetting_selectedGradeItem.Index, UnitSetting_selectedUnitItem.Index);
                UnitSetting_UnitList.RemoveAt(UnitSetting_selectedUnitItem.Index);

                UnitSetting_UpdateUnitItem = true;
                UnitSetting_ChangedUnitItem = true;
            }
        }

        #endregion

        #region UnitSetting_UnitEditCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand UnitSetting_UnitEditCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void UnitSetting_OnUnitEdit()
        {
            if (UnitSetting_selectedUnitItem != null)
            {
                UnitSetting_UnitEnable = true;
            }
        }

        #endregion

        #region UnitSetting_LoadImageCommand

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 ICommand
        /// </summary>
        public ICommand UnitSetting_LoadImageCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitSetting_OnLoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog().ToString() == "OK")
            {
                FileInfo fi = new FileInfo(openFileDialog.FileName);
                if(openFileDialog.FileName != AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_SelectedGradeItem.Name + "\\" + UnitSetting_SelectedUnitItem.Name +
                        ".jpg")
                    fi.CopyTo(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_SelectedGradeItem.Name + "\\" + UnitSetting_SelectedUnitItem.Name +
                        ".jpg", true);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_SelectedGradeItem.Name + "\\" + UnitSetting_SelectedUnitItem.Name +
                        ".jpg");
                bi.EndInit();
                UnitSetting_SelectedUnitItem.Image = bi;
                UnitSetting_UnitImage = bi;
                OnPropertyChanged("UnitSetting_UnitList");
                OnPropertyChanged("UnitSetting_UnitImage");
            }
        }

        #endregion

        #region UnitSetting_MixAddCommand

        /// <summary>
        /// 유닛 세팅_조합 목록 추가 ICommand
        /// </summary>
        public ICommand UnitSetting_MixAddCommand { get; set; }

        /// <summary>
        /// 유닛 세팅_조합 목록 추가 Command 실행 시
        /// </summary>
        private void UnitSetting_OnMixAdd()
        {
            MixAdd_GradeList.Clear();
            for (int n = 0; n < UnitSetting_GradeList.Count; n++)
            {
                MixAdd_GradeList.Add(new GradeItem() {Index = UnitSetting_GradeList[n].Index, Name = UnitSetting_GradeList[n].Name});
            }
            MixAdd_Popup = 2;
            Setting_Popup = 3;
        }

        #endregion

        #region UnitSetting_MixDeleteCommand

        /// <summary>
        /// 유닛 세팅_조합 목록 삭제 ICommand
        /// </summary>
        public ICommand UnitSetting_MixDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 세팅_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitSetting_OnMixDelete()
        {
            if (UnitSetting_SelectedMixItem != null)
            {
                uh_temp[UnitSetting_selectedVersionItem.Index].RemoveSubUnitMix(UnitSetting_SelectedGradeItem.Index, UnitSetting_SelectedUnitItem.Index, UnitSetting_SelectedMixItem.Index);
                for (int n = UnitSetting_SelectedMixItem.Index + 1; n < UnitSetting_MixList.Count(); n++)
                {
                    UnitSetting_MixList[n].Index--;
                }

                UnitSetting_MixList.RemoveAt(UnitSetting_SelectedMixItem.Index);

                OnPropertyChanged("UnitSetting_MixList");
            }
        }

        #endregion

        #region UnitSetting_IndexSearchCommand

        /// <summary>
        /// 유닛 추가 팝업_인덱스 검색 ICommand
        /// </summary>
        public ICommand UnitSetting_IndexSearchCommand { get; set; }

        /// <summary>
        /// 유닛 추가 팝업_조합 목록 삭제 Command 실행 시
        /// </summary>
        private void UnitSetting_OnIndexSearch()
        {
            dataThread.PauseThread("SearchList");
            UnitIndex_Popup = 2;
            Setting_Popup = 4;
        }

        #endregion

        #endregion

        #region Data

        #region UnitSetting_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> UnitSetting_VersionList
        {
            get;
            set;
        }

        #endregion

        #region UnitSetting_SelectedVersionItem

        private VersionItem UnitSetting_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem UnitSetting_SelectedVersionItem
        {
            get { return UnitSetting_selectedVersionItem; }
            set
            {
                UnitSetting_selectedVersionItem = value;
                Clear_UnitSetting();
                for (int n = 0; n < uh[value.Index].gradeNum; n++)
                {
                    unsafe
                    {
                        UnitSetting_GradeList.Add(new GradeItem() { Name = Marshal.PtrToStringAnsi((IntPtr)uh[value.Index].grade(n).name), Index = n });
                    }
                }
                OnPropertyChanged("UnitSetting_SelectedVersionItem");
            }
        }

        #endregion

        #region UnitSetting_GradeList
        /// <summary>
        /// 유닛 설정 탭_등급 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<GradeItem> UnitSetting_GradeList
        {
            get;
            set;
        }

        #endregion

        #region UnitSetting_SelecetedGradeItem

        private GradeItem UnitSetting_selectedGradeItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 등급 아이템 Property
        /// <para>Type: GradeItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public GradeItem UnitSetting_SelectedGradeItem
        {
            get { return UnitSetting_selectedGradeItem; }
            set
            {
                UnitSetting_selectedGradeItem = value;
                UnitSetting_UpdateUnitItem = true;
                UnitSetting_GradeEnable = false;
                UnitSetting_UniqueGradeCheck = false;
                UnitSetting_HeroGradeCheck = false;
                UnitSetting_WispGradeCheck = false;
                if (UnitSetting_SelectedGradeItem != null)
                {
                    UnitSetting_GradeName = UnitSetting_selectedGradeItem.Name;

                    if (UnitSetting_SelectedGradeItem.Index == uh[UnitSetting_SelectedVersionItem.Index].uniqueGradeIndex)
                        UnitSetting_UniqueGradeCheck = true;
                    else if (UnitSetting_SelectedGradeItem.Index == uh[UnitSetting_SelectedVersionItem.Index].heroGradeIndex)
                        UnitSetting_HeroGradeCheck = true;
                    else if (UnitSetting_SelectedGradeItem.Index == uh[UnitSetting_SelectedVersionItem.Index].wispGradeIndex)
                        UnitSetting_WispGradeCheck = true;
                }
                else
                    UnitSetting_GradeName = null;

                OnPropertyChanged("UnitSetting_GradeDeleteEnable");
                OnPropertyChanged("UnitSetting_GradeEditEnable");
                OnPropertyChanged("UnitSetting_UnitAddEnable");
            }
        }

        #endregion

        #region UnitSetting_UnitList

        /// <summary>
        /// 유닛 설정 탭_유닛 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<UnitItem> UnitSetting_UnitList
        {
            get;
            set;
        }

        #endregion

        #region UnitSetting_SelectedUnitItem

        private UnitItem UnitSetting_selectedUnitItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 유닛 아이템 Property
        /// <para>Type: UnitItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public UnitItem UnitSetting_SelectedUnitItem
        {
            get { return UnitSetting_selectedUnitItem; }
            set
            {
                UnitSetting_selectedUnitItem = value;
                UnitSetting_UnitEnable = false;

                UnitSetting_SelectWispUnitCheck = false;

                if(value != null)
                {
                    if (UnitSetting_SelectedGradeItem.Index == uh[UnitSetting_SelectedVersionItem.Index].wispGradeIndex && UnitSetting_SelectedUnitItem.Index == uh[UnitSetting_SelectedVersionItem.Index].selectWispUnitIndex)
                        UnitSetting_SelectWispUnitCheck = true;
                    UnitSetting_UnitName = UnitSetting_SelectedUnitItem.Name;
                    UnitSetting_UnitImage = UnitSetting_SelectedUnitItem.Image;
                    UnitSetting_MixList.Clear();
                    int subNum_temp = uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).subNum;
                    if (subNum_temp > 0)
                    {
                        for (int n = 0; n < subNum_temp; n++)
                        {
                            SubUnitMix subUnitMix_temp = uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).subUnitMix(n);
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            unsafe
                            {
                                bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + Marshal.PtrToStringAnsi((IntPtr)uh_temp[UnitSetting_selectedVersionItem.Index].grade(subUnitMix_temp.gradeIndex).name)
                                    + "\\" + Marshal.PtrToStringAnsi((IntPtr)uh_temp[UnitSetting_selectedVersionItem.Index].grade(subUnitMix_temp.gradeIndex).unit(subUnitMix_temp.unitIndex).name) + ".jpg");
                            bi.EndInit();
                            
                                UnitSetting_MixList.Add(new MixItem()
                                {
                                    GradeIndex = subUnitMix_temp.gradeIndex,
                                    UnitIndex = subUnitMix_temp.unitIndex,
                                    Num = subUnitMix_temp.basicNum,
                                    Name = Marshal.PtrToStringAnsi((IntPtr)uh_temp[UnitSetting_selectedVersionItem.Index].grade(subUnitMix_temp.gradeIndex).unit(subUnitMix_temp.unitIndex).name),
                                    Image = bi,
                                    Type = 1,
                                    Index = n
                                });
                            }
                        }
                    }
                    MixResources resources_temp = uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).mixResources;
                    unsafe
                    {
                        if (resources_temp.basicGold > 0)
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/자원/골드.jpg");
                            bi.EndInit();
                            UnitSetting_MixList.Add(new MixItem()
                            {
                                Name = "골드",
                                Image = bi,
                                Type = 2,
                                Index = subNum_temp,
                                Num = resources_temp.basicGold
                            });
                            subNum_temp++;
                        }
                        if (resources_temp.basicTree > 0)
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources/자원/나무.jpg");
                            bi.EndInit();
                            UnitSetting_MixList.Add(new MixItem()
                            {
                                Name = "나무",
                                Image = bi,
                                Type = 3,
                                Index = subNum_temp,
                                Num = resources_temp.basicTree
                            });
                        }
                    }
                    UnitSetting_UnitIndex = uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).index.ToString("X4");
                    OnPropertyChanged("UnitSetting_UnitIndex");
                }
                else
                {
                    UnitSetting_UnitName = null;
                    UnitSetting_MixList.Clear();
                }
                OnPropertyChanged("UnitSetting_UnitDeleteEnable");
                OnPropertyChanged("UnitSetting_UnitEditEnable");
                OnPropertyChanged("UnitSetting_MixList");
            }
        }

        #endregion

        #region UnitSetting_UpdateUnitItem

        private bool UnitSetting_updateUnitItem = false;

        /// <summary>
        /// 유닛 설정 탭_유닛 아이템 변화 여부 Property
        /// <para>Type: bool</para>
        /// </summary>
        public bool UnitSetting_UpdateUnitItem
        {
            get { return UnitSetting_updateUnitItem; }
            set
            {
                UnitSetting_updateUnitItem = value;
                UnitSetting_UnitList.Clear();
                if (UnitSetting_updateUnitItem == true && UnitSetting_selectedGradeItem != null)
                {
                    if (uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_selectedGradeItem.Index).unitNum > 0)
                    {
                        string[] unitName = new string[uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_selectedGradeItem.Index).unitNum];
                        for (int n = 0; n < uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_selectedGradeItem.Index).unitNum; n++)
                        {
                            unsafe
                            {
                                unitName[n] = Marshal.PtrToStringAnsi((IntPtr)uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_selectedGradeItem.Index).unit(n).name);
                            }

                            FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_SelectedGradeItem.Name + "\\" + unitName[n] +
                                    ".jpg");
                            if (fi.Exists)
                            {
                                BitmapImage bi = new BitmapImage();
                                bi.BeginInit();
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                                bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + UnitSetting_SelectedGradeItem.Name + "\\" + unitName[n] +
                                        ".jpg");
                                bi.EndInit();

                                UnitSetting_UnitList.Add(new UnitItem() { Name = unitName[n], Image = bi, Index = n });
                            }
                            else
                                UnitSetting_UnitList.Add(new UnitItem() { Name = unitName[n], Index = n });
                        }
                    }
                }
                OnPropertyChanged("UnitSetting_UnitList");
            }
        }

        #endregion

        #region UnitSetting_ChangedUnitItem

        private bool UnitSetting_changedUnitItem = false;

        /// <summary>
        /// 
        /// </summary>
        public bool UnitSetting_ChangedUnitItem
        {
            get { return UnitSetting_changedUnitItem; }
            set
            {
                UnitSetting_changedUnitItem = value;
                OnPropertyChanged("UnitSetting_Save");
            }
        }

        #endregion

        #region UnitSetting_GradeDeleteEnable

        /// <summary>
        /// 유닛 설정 탭_등급 편집 버튼 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_GradeDeleteEnable
        {
            get
            {
                if (UnitSetting_SelectedGradeItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitSetting_GradeEditEnable

        /// <summary>
        /// 유닛 설정 탭_등급 삭제 버튼 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_GradeEditEnable
        {
            get
            {
                if (UnitSetting_SelectedGradeItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitSetting_GradeName

        private string UnitSetting_gradeName;

        /// <summary>
        /// 
        /// </summary>
        public string UnitSetting_GradeName
        {
            get { return UnitSetting_gradeName; }
            set
            {
                if (UnitSetting_GradeEnable == true)
                {
                    unsafe
                    {
                        uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).name = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(value);
                    }

                    UnitSetting_GradeList[UnitSetting_selectedGradeItem.Index].Name = value;
                    UnitSetting_UpdateUnitItem = true;

                    OnPropertyChanged("UnitSetting_GradeList");
                }
                UnitSetting_gradeName = value;
                OnPropertyChanged("UnitSetting_GradeName");
            }
        }

        #endregion

        #region UnitSetting_GradeEnable

        private bool UnitSetting_gradeEnable = false;

        /// <summary>
        /// 유닛 설정 탭_등급 편집 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_GradeEnable
        {
            get { return UnitSetting_gradeEnable; }
            set
            {
                UnitSetting_gradeEnable = value;
                OnPropertyChanged("UnitSetting_GradeEnable");
            }
        }

        #endregion

        #region UnitSetting_UnitEnable

        private bool UnitSetting_unitEnable = false;

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_UnitEnable
        {
            get { return UnitSetting_unitEnable; }
            set
            {
                UnitSetting_unitEnable = value;
                OnPropertyChanged("UnitSetting_UnitEnable");
            }
        }

        #endregion

        #region UnitSetting_UnitAddEnable

        /// <summary>
        /// 유닛 설정 탭_유닛 추가 버튼 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_UnitAddEnable
        {
            get
            {
                if (UnitSetting_SelectedGradeItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitSetting_UnitDeleteEnable

        /// <summary>
        /// 유닛 설정 탭_유닛 삭제 버튼 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_UnitDeleteEnable
        {
            get
            {
                if (UnitSetting_SelectedUnitItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitSetting_UnitEditEnable

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 버튼 활성화 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_UnitEditEnable
        {
            get
            {
                if (UnitSetting_SelectedUnitItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitSetting_UnitName

        private string UnitSetting_unitName;

        /// <summary>
        /// 유닛 세팅_유닛 이름 텍스트박스 Property
        /// <para>Type: string</para>
        /// <para>Binding: Text</para>
        /// </summary>
        public string UnitSetting_UnitName
        {
            get { return UnitSetting_unitName; }
            set
            {
                if (UnitSetting_UnitEnable == true)
                {
                    unsafe
                    {
                        uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).name = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(value);
                    }

                    UnitSetting_UnitList[UnitSetting_SelectedUnitItem.Index].Name = value;
                    UnitSetting_UpdateUnitItem = true;
                }
                UnitSetting_unitName = value;
                UnitSetting_UnitNameColor = new SolidColorBrush(Colors.Black);
                OnPropertyChanged("UnitSetting_UnitName");
            }
        }

        #endregion

        #region UnitSetting_UnitNameColor

        private Brush UnitSetting_unitNameColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 세팅_유닛 이름 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush UnitSetting_UnitNameColor
        {
            get { return UnitSetting_unitNameColor; }
            set
            {
                UnitSetting_unitNameColor = value;
                OnPropertyChanged("UnitSetting_UnitNameColor");
            }
        }

        #endregion

        #region UnitSetting_UnitImage

        private ImageSource UnitSetting_unitImage;

        /// <summary>
        /// 
        /// </summary>
        public ImageSource UnitSetting_UnitImage
        {
            get { return UnitSetting_unitImage; }
            set
            {
                UnitSetting_unitImage = value;
                OnPropertyChanged("UnitSetting_UnitImage");
            }
        }

        #endregion

        #region UnitSetting_MixList

        /// <summary>
        /// 유닛 세팅_조합 목록 리스트
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<MixItem> UnitSetting_MixList
        {
            get;
            set;
        }

        #endregion

        #region UnitSetting_SelectedMixItem

        private MixItem UnitSetting_selectedMixItem;

        /// <summary>
        /// 유닛 세팅_선택된 조합 목록 아이템
        /// <para>Type: MixItem</para>
        /// </summary>
        public MixItem UnitSetting_SelectedMixItem
        {
            get { return UnitSetting_selectedMixItem; }
            set
            {
                UnitSetting_selectedMixItem = value;
                OnPropertyChanged("UnitSetting_CanMixDelete");
            }
        }

        #endregion

        #region UnitSetting_CanMixDelete

        /// <summary>
        /// 
        /// </summary>
        public bool UnitSetting_CanMixDelete
        {
            get
            {
                if (UnitSetting_SelectedMixItem != null)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region UnitSetting_UnitIndex

        private string UnitSetting_unitIndex = "0x00000000";

        /// <summary>
        /// 
        /// </summary>
        public string UnitSetting_UnitIndex
        {
            get { return UnitSetting_unitIndex; }
            set
            {
                try
                {
                    if (Convert.ToUInt32(value, 16) >= 0)
                    {
                        UnitSetting_unitIndex = Convert.ToUInt32(value, 16).ToString("X4");
                        uh_temp[UnitSetting_selectedVersionItem.Index].grade(UnitSetting_SelectedGradeItem.Index).unit(UnitSetting_SelectedUnitItem.Index).index = Convert.ToUInt32(value, 16);
                        UnitSetting_UnitIndexColor = new SolidColorBrush(Colors.Black);
                    }
                    else
                        UnitSetting_UnitIndexColor = new SolidColorBrush(Colors.Red);
                }

                catch
                {
                    UnitSetting_UnitIndexColor = new SolidColorBrush(Colors.Red);
                }
            }
        }

        #endregion

        #region UnitSetting_UnitIndexColor

        private Brush UnitSetting_unitIndexColor = new SolidColorBrush(Colors.Black);

        /// <summary>
        /// 유닛 추가 팝업_유닛 인덱스 배경색 Property
        /// <para>Type: Brush</para>
        /// <para>Binding: BackGround</para>
        /// </summary>
        public Brush UnitSetting_UnitIndexColor
        {
            get { return UnitSetting_unitIndexColor; }
            set
            {
                UnitSetting_unitIndexColor = value;
                OnPropertyChanged("UnitSetting_unitIndexColor");
            }
        }

        #endregion

        #region UnitSetting_UniqueGradeCheck

        private bool UnitSetting_uniqueGradeCheck;
        public bool UnitSetting_UniqueGradeCheck
        {
            get { return UnitSetting_uniqueGradeCheck; }
            set
            {
                UnitSetting_uniqueGradeCheck = value;
                if(UnitSetting_uniqueGradeCheck == true)
                    uh_temp[UnitSetting_selectedVersionItem.Index].uniqueGradeIndex = UnitSetting_SelectedGradeItem.Index;
                OnPropertyChanged("UnitSetting_UniqueGradeCheck");
            }
        }

        #endregion

        #region UnitSetting_HeroGradeCheck

        private bool UnitSetting_heroGradeCheck;
        public bool UnitSetting_HeroGradeCheck
        {
            get { return UnitSetting_heroGradeCheck; }
            set
            {
                UnitSetting_heroGradeCheck = value;
                if (UnitSetting_heroGradeCheck == true)
                    uh_temp[UnitSetting_selectedVersionItem.Index].heroGradeIndex = UnitSetting_SelectedGradeItem.Index;
                OnPropertyChanged("UnitSetting_HeroGradeCheck");
            }
        }

        #endregion

        #region UnitSetting_WispGradeCheck

        private bool UnitSetting_wispGradeCheck;
        public bool UnitSetting_WispGradeCheck
        {
            get { return UnitSetting_wispGradeCheck; }
            set
            {
                UnitSetting_wispGradeCheck = value;
                if (UnitSetting_wispGradeCheck == true)
                    uh_temp[UnitSetting_selectedVersionItem.Index].wispGradeIndex = UnitSetting_SelectedGradeItem.Index;
                OnPropertyChanged("UnitSetting_WispGradeCheck");
            }
        }

        #endregion

        #region UnitSetting_SelectWispUnitCheck

        private bool UnitSetting_selectWispUnitCheck;
        public bool UnitSetting_SelectWispUnitCheck
        {
            get { return UnitSetting_selectWispUnitCheck; }
            set
            {
                UnitSetting_selectWispUnitCheck = value;
                if (UnitSetting_selectWispUnitCheck == true)
                    uh_temp[UnitSetting_selectedVersionItem.Index].selectWispUnitIndex = UnitSetting_SelectedUnitItem.Index;
                OnPropertyChanged("UnitSetting_SelectWispUnitCheck");
            }
        }

        #endregion

        #region UnitSetting_Save

        /// <summary>
        /// 유닛 설정 탭_저장 Property
        /// <para>Type: bool</para>
        /// <para>Binding: isEnabled</para>
        /// </summary>
        public bool UnitSetting_Save
        {
            get
            {
                /*
                if (UnitSetting_ChangedUnitItem)
                    return true;
                else
                    return false;
                    */
                return true;
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_UnitSettingView()
        {
            UnitSetting_VersionList = new ObservableCollection<VersionItem>();
            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    uh_temp.Add(new UnitHandler());
                    uh_temp[n].Copy(uh[n]);
                    UnitSetting_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
            UnitSetting_GradeList = new ObservableCollection<GradeItem>();
            UnitSetting_UnitList = new ObservableCollection<UnitItem>();
            UnitSetting_MixList = new ObservableCollection<MixItem>();

            UnitSetting_SaveCommand = new RelayCommand<object>(p => UnitSetting_OnSave());
            UnitSetting_GradeDeleteCommand = new RelayCommand<object>(p => UnitSetting_OnGradeDelete());
            UnitSetting_GradeEditCommand = new RelayCommand<object>(p => UnitSetting_OnGradeEdit());
            UnitSetting_UnitDeleteCommand = new RelayCommand<object>(p => UnitSetting_OnUnitDelete());
            UnitSetting_UnitEditCommand = new RelayCommand<object>(p => UnitSetting_OnUnitEdit());
            UnitSetting_LoadImageCommand = new RelayCommand<object>(p => UnitSetting_OnLoadImage());
            UnitSetting_MixAddCommand = new RelayCommand<object>(p => UnitSetting_OnMixAdd());
            UnitSetting_IndexSearchCommand = new RelayCommand<object>(p => UnitSetting_OnIndexSearch());
            UnitSetting_MixDeleteCommand = new RelayCommand<object>(p => UnitSetting_OnMixDelete());

            UnitSetting_SelectedVersionItem = UnitSetting_VersionList[0];
        }

        public void Clear_UnitSetting()
        {
            UnitSetting_selectedGradeItem = null;
            if(UnitSetting_GradeList != null)
                UnitSetting_GradeList.Clear();
            UnitSetting_selectedUnitItem = null;
            if (UnitSetting_UnitList != null)
                UnitSetting_UnitList.Clear();
            UnitSetting_selectedMixItem = null;
            if (UnitSetting_MixList != null)
                UnitSetting_MixList.Clear();

            OnPropertyChanged("UnitSetting_GradeList");
            OnPropertyChanged("UnitSetting_UnitList");
            OnPropertyChanged("UnitSetting_MixList");
        }
        #endregion
    }
}
