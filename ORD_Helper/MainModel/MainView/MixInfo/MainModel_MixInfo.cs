using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        #region MixInfo_BackCommand

        /// <summary>
        /// 설정 창_뷰 전환 ICommand
        /// </summary>
        public ICommand MixInfo_BackCommand { get; set; }

        /// <summary>
        /// 설정 창_뷰 전환 Command 실행 시
        /// </summary>
        /// <param name="index">설정 탭 index
        /// <para>Type: int</para></param>
        private void MixInfo_OnBack()
        {
            dataThread.PauseThread("SearchMixInfo");
            Clear_MixInfo();
            dataThread.ResumeThread("SearchUnitInfo");
            MainView_Popup = 0;
        }

        #endregion

        #endregion

        #region Data

        #region MixInfo_BackImage

        public string MixInfo_BackImage
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Resources/back_icon.png"; }
        }

        #endregion

        public int MixInfo_GradeIndex;

        public int MixInfo_UnitIndex;

        #region MixInfo_UnitWispPercent

        private int MixInfo_unitWispPercent;
        public int MixInfo_UnitWispPercent
        {
            get { return MixInfo_unitWispPercent; }
            set
            {
                MixInfo_unitWispPercent = value;
                OnPropertyChanged("MixInfo_UnitWispPercent");
            }
        }

        #endregion

        #region MixInfo_UnitPercent

        private int MixInfo_unitPercent;
        public int MixInfo_UnitPercent
        {
            get { return MixInfo_unitPercent; }
            set
            {
                MixInfo_unitPercent = value;
                OnPropertyChanged("MixInfo_UnitPercent");
            }
        }

        #endregion

        #region MixInfo_UnitHeroPercent

        private int MixInfo_unitHeroPercent;
        public int MixInfo_UnitHeroPercent
        {
            get { return MixInfo_unitHeroPercent; }
            set
            {
                MixInfo_unitHeroPercent = value;
                OnPropertyChanged("MixInfo_UnitHeroPercent");
            }
        }

        #endregion

        #region MixInfo_UnitWispPercentColor

        private Brush MixInfo_unitWispPercentColor;
        public Brush MixInfo_UnitWispPercentColor
        {
            get { return MixInfo_unitWispPercentColor; }
            set
            {
                MixInfo_unitWispPercentColor = value;
                OnPropertyChanged("MixInfo_UnitWispPercentColor");
            }
        }

        #endregion

        #region MixInfo_UnitPercentColor

        private Brush MixInfo_unitPercentColor;
        public Brush MixInfo_UnitPercentColor
        {
            get { return MixInfo_unitPercentColor; }
            set
            {
                MixInfo_unitPercentColor = value;
                OnPropertyChanged("MixInfo_UnitPercentColor");
            }
        }

        #endregion

        #region MixInfo_UnitHeroPercentColor

        private Brush MixInfo_unitHeroPercentColor;
        public Brush MixInfo_UnitHeroPercentColor
        {
            get { return MixInfo_unitHeroPercentColor; }
            set
            {
                MixInfo_unitHeroPercentColor = value;
                OnPropertyChanged("MixInfo_UnitHeroPercentColor");
            }
        }

        #endregion

        #region MixInfo_UnitImage

        private string MixInfo_unitImage;
        public string MixInfo_UnitImage
        {
            get { return MixInfo_unitImage; }
            set
            {
                MixInfo_unitImage = value;
                OnPropertyChanged("MixInfo_UnitImage");
            }
        }

        #endregion

        #region MixInfo_UnitGradeName

        private string MixInfo_unitGradeName;
        public string MixInfo_UnitGradeName
        {
            get { return MixInfo_unitGradeName; }
            set
            {
                MixInfo_unitGradeName = value;
                OnPropertyChanged("MixInfo_UnitGradeName");
            }
        }

        #endregion

        #region MixInfo_UnitName

        private string MixInfo_unitName;
        public string MixInfo_UnitName
        {
            get { return MixInfo_unitName; }
            set
            {
                MixInfo_unitName = value;
                OnPropertyChanged("MixInfo_UnitName");
            }
        }

        #endregion

        #region MixInfo_UnitPercentString

        private string MixInfo_unitPercentString;
        public string MixInfo_UnitPercentString
        {
            get { return MixInfo_unitPercentString; }
            set
            {
                MixInfo_unitPercentString = value;
                OnPropertyChanged("MixInfo_UnitPercentString");
            }
        }

        #endregion

        #region MixInfo_UnitNum

        private string MixInfo_unitNum;
        public string MixInfo_UnitNum
        {
            get { return MixInfo_unitNum; }
            set
            {
                MixInfo_unitNum = value;
                OnPropertyChanged("MixInfo_UnitNum");
            }
        }

        #endregion

        #region MixInfo_HighestList

        public ObservableCollection<MixInfo_ListItem> MixInfo_HighestList
        {
            get;
            set;
        }

        #endregion

        #region Mixinfo_SelectedHighestItem

        private MixInfo_ListItem Mixinfo_selectedHighestItem;

        public MixInfo_ListItem MixInfo_SelectedHighestItem
        {
            get { return Mixinfo_selectedHighestItem; }
            set
            {
                if (value != null)
                {
                    Clear_MixInfo();
                    Mixinfo_selectedHighestItem = value;
                    MixInfo_GradeIndex = Mixinfo_selectedHighestItem.Grade;
                    MixInfo_UnitIndex = Mixinfo_selectedHighestItem.Unit;
                    MixInfo_AddUnit();
                    MixInfo_AddList();
                }
                OnPropertyChanged("MixInfo_SelectedHighestItem");
            }
        }

        #endregion

        #region MixInfo_UpperList

        public ObservableCollection<MixInfo_ListItem> MixInfo_UpperList
        {
            get;
            set;
        }

        #endregion

        #region Mixinfo_SelectedUpperItem

        private MixInfo_ListItem Mixinfo_selectedUpperItem;

        public MixInfo_ListItem MixInfo_SelectedUpperItem
        {
            get { return Mixinfo_selectedUpperItem; }
            set
            {
                if (value != null)
                {
                    Clear_MixInfo();
                    Mixinfo_selectedUpperItem = value;
                    MixInfo_GradeIndex = Mixinfo_selectedUpperItem.Grade;
                    MixInfo_UnitIndex = Mixinfo_selectedUpperItem.Unit;
                    MixInfo_AddUnit();
                    MixInfo_AddList();
                }
                OnPropertyChanged("MixInfo_SelectedUpperItem");
            }
        }

        #endregion

        #region MixInfo_SubList

        public ObservableCollection<MixInfo_ListItem> MixInfo_SubList
        {
            get;
            set;
        }

        #endregion

        #region Mixinfo_SelectedSubItem

        private MixInfo_ListItem Mixinfo_selectedSubItem;

        public MixInfo_ListItem MixInfo_SelectedSubItem
        {
            get { return Mixinfo_selectedSubItem; }
            set
            {
                if(value != null)
                {
                    Clear_MixInfo();
                    Mixinfo_selectedSubItem = value;
                    MixInfo_GradeIndex = Mixinfo_selectedSubItem.Grade;
                    MixInfo_UnitIndex = Mixinfo_selectedSubItem.Unit;
                    MixInfo_AddUnit();
                    MixInfo_AddList();
                }
                OnPropertyChanged("MixInfo_SelectedSubItem");
            }
        }

        #endregion

        #region MixInfo_LowestList

        public ObservableCollection<MixInfo_ListItem> MixInfo_LowestList
        {
            get;
            set;
        }

        #endregion

        #region Mixinfo_SelectedLowestItem

        private MixInfo_ListItem Mixinfo_selectedLowestItem;

        public MixInfo_ListItem Mixinfo_SelectedLowestItem
        {
            get { return Mixinfo_selectedLowestItem; }
            set
            {
                Mixinfo_selectedLowestItem = value;
                OnPropertyChanged("Mixinfo_SelectedLowestItem");
            }
        }

        #endregion

        #region MixInfo_ListItem

        public class MixInfo_ListItem : ViewModelBase
        {
            public int Grade;

            public int Unit;

            private int wispPercent;
            public int WispPercent
            {
                get { return wispPercent; }
                set
                {
                    wispPercent = value;
                    OnPropertyChanged("WispPercent");
                }
            }

            private int percent;
            public int Percent
            {
                get { return percent; }
                set
                {
                    percent = value;
                    OnPropertyChanged("Percent");
                }
            }

            private int heroPercent;
            public int HeroPercent
            {
                get { return heroPercent; }
                set
                {
                    heroPercent = value;
                    OnPropertyChanged("HeroPercent");
                }
            }

            private Brush wispPercentColor = new SolidColorBrush(Colors.Yellow);

            public Brush WispPercentColor
            {
                get { return wispPercentColor; }
                set
                {
                    wispPercentColor = value;
                    OnPropertyChanged("WispPercentColor");
                }
            }

            private Brush percentColor = new SolidColorBrush(Colors.LightGray);
            public Brush PercentColor
            {
                get { return percentColor; }
                set
                {
                    percentColor = value;
                    OnPropertyChanged("PercentColor");
                }
            }

            private Brush heroPercentColor = new SolidColorBrush(Colors.Blue);

            public Brush HeroPercentColor
            {
                get { return heroPercentColor; }
                set
                {
                    heroPercentColor = value;
                    OnPropertyChanged("HeroPercentColor");
                }
            }

            private string image;
            public string Image
            {
                get { return image; }
                set
                {
                    image = value;
                    OnPropertyChanged("Image");
                }
            }

            private string gradeName;
            public string GradeName
            {
                get { return gradeName; }
                set
                {
                    gradeName = value;
                    OnPropertyChanged("GradeName");
                }
            }

            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string percentString;
            public string PercentString
            {
                get { return percentString; }
                set
                {
                    percentString = value;
                    OnPropertyChanged("Value");
                }
            }

            private string num;
            public string Num
            {
                get { return num; }
                set
                {
                    num = value;
                    OnPropertyChanged("Num");
                }
            }

            private string needNum;
            public string NeedNum
            {
                get { return needNum; }
                set
                {
                    needNum = value;
                    OnPropertyChanged("NeedNum");
                }
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_MixInfo()
        {
            MixInfo_BackCommand = new RelayCommand<object>(p => MixInfo_OnBack());
            MixInfo_HighestList = new ObservableCollection<MixInfo_ListItem>();
            MixInfo_UpperList = new ObservableCollection<MixInfo_ListItem>();
            MixInfo_SubList = new ObservableCollection<MixInfo_ListItem>();
            MixInfo_LowestList = new ObservableCollection<MixInfo_ListItem>();

            dataThread.CreateThread(SearchMixInfo, 500);
        }

        public void Clear_MixInfo()
        {
            MixInfo_unitImage = null;
            MixInfo_unitName = null;
            MixInfo_unitPercent = 0;
            MixInfo_unitWispPercent = 0;
            MixInfo_unitHeroPercent = 0;
            MixInfo_unitPercentString = null;
            MixInfo_unitPercentColor = null;
            MixInfo_unitNum = null;

            MixInfo_HighestList.Clear();
            Mixinfo_selectedHighestItem = null;
            MixInfo_UpperList.Clear();
            Mixinfo_selectedUpperItem = null;
            MixInfo_SubList.Clear();
            Mixinfo_selectedSubItem = null;
            MixInfo_LowestList.Clear();
            Mixinfo_selectedLowestItem = null;
        }

        public void MixInfo_AddUnit()
        {
            unsafe
            {
                MixInfo_UnitImage = AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).name) + "/" +
                                                Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).name) + ".jpg";
                MixInfo_UnitGradeName = "(" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).name) + ")";
                MixInfo_UnitName = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).name);
                MixInfo_UnitWispPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).wispPercent;
                MixInfo_UnitHeroPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).heroPercent;
                MixInfo_UnitPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).percent;
                MixInfo_UnitPercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).percent + "%";

                if (MixInfo_unitPercent == 100)
                    MixInfo_UnitPercentColor = new SolidColorBrush(Colors.LightGreen);
                else
                    MixInfo_UnitPercentColor = new SolidColorBrush(Colors.LightGray);
                MixInfo_UnitWispPercentColor = new SolidColorBrush(Colors.Yellow);
                MixInfo_UnitHeroPercentColor = new SolidColorBrush(Colors.Blue);
                MixInfo_UnitWispPercentColor.Opacity = 0.5;
                MixInfo_UnitHeroPercentColor.Opacity = 0.5;
                MixInfo_UnitPercentColor.Opacity = 1;
                MixInfo_UnitNum = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).num.ToString();
            }
        }

        public void MixInfo_AddList()
        {
            for(int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).highestNum; n++)
            {
                int highestGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).highestUnitMix(n).gradeIndex;
                int highestUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).highestUnitMix(n).unitIndex;
                unsafe
                {
                    MixInfo_HighestList.Add(new MixInfo_ListItem()
                    {
                        Grade = highestGrade, Unit = highestUnit,
                        Image = AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(highestGrade).name) + "/" +
                                            Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).name) + ".jpg",
                        GradeName = "(" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(highestGrade).name) + ")",
                        Name = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).name),
                        Percent = uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).percent,
                        WispPercent = uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).wispPercent,
                        HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).heroPercent,
                        PercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).percent + "%",
                        Num = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).num.ToString()
                    });
                    if (MixInfo_HighestList[n].Percent == 100)
                        MixInfo_HighestList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                    else
                        MixInfo_HighestList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    MixInfo_HighestList[n].WispPercentColor.Opacity = 0.5;
                    MixInfo_HighestList[n].HeroPercentColor.Opacity = 0.5;
                    MixInfo_HighestList[n].PercentColor.Opacity = 1;
                }
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).upperNum; n++)
            {
                int upperGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).upperUnitMix(n).gradeIndex;
                int upperUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).upperUnitMix(n).unitIndex;
                unsafe
                {
                    MixInfo_UpperList.Add(new MixInfo_ListItem()
                    {
                        Grade = upperGrade,
                        Unit = upperUnit,
                        Image = AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(upperGrade).name) + "/" +
                                            Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).name) + ".jpg",
                        GradeName = "(" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(upperGrade).name) + ")",
                        Name = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).name),
                        Percent = uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).percent,
                        WispPercent = uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).wispPercent,
                        HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).heroPercent,
                        PercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).percent + "%",
                        Num = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).num.ToString()
                    });
                    if (MixInfo_UpperList[n].Percent == 100)
                        MixInfo_UpperList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                    else
                        MixInfo_UpperList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    MixInfo_UpperList[n].WispPercentColor.Opacity = 0.5;
                    MixInfo_UpperList[n].HeroPercentColor.Opacity = 0.5;
                    MixInfo_UpperList[n].PercentColor.Opacity = 1;
                }
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subNum; n++)
            {
                int subGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).gradeIndex;
                int subUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).unitIndex;
                unsafe
                {
                    MixInfo_SubList.Add(new MixInfo_ListItem()
                    {
                        Grade = subGrade,
                        Unit = subUnit,
                        Image = AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(subGrade).name) + "/" +
                                            Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(subGrade).unit(subUnit).name) + ".jpg",
                        GradeName = "(" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(subGrade).name) + ")",
                        Name = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(subGrade).unit(subUnit).name),
                        Percent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).percent,
                        WispPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).wispPercent,
                        HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).heroPercent,
                        PercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).percent + "%",
                        Num = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(subGrade).unit(subUnit).num.ToString(),
                        NeedNum = "재료 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).basicNum.ToString()
                    });
                    if (MixInfo_SubList[n].Percent == 100)
                        MixInfo_SubList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                    else
                        MixInfo_SubList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    MixInfo_SubList[n].PercentColor.Opacity = 1;
                    MixInfo_SubList[n].WispPercentColor.Opacity = 0.5;
                    MixInfo_SubList[n].HeroPercentColor.Opacity = 0.5;
                }
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestNum; n++)
            {
                int lowestGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).gradeIndex;
                int lowestUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).unitIndex;
                unsafe
                {
                    MixInfo_LowestList.Add(new MixInfo_ListItem()
                    {
                        Image = AppDomain.CurrentDomain.BaseDirectory + "Resources/" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(lowestGrade).name) + "/" +
                                            Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(lowestGrade).unit(lowestUnit).name) + ".jpg",
                        GradeName = "(" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(lowestGrade).name) + ")",
                        Name = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(lowestGrade).unit(lowestUnit).name),
                        Percent = 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100),
                        WispPercent = 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needWispNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100),
                        HeroPercent = 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needHeroNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100),
                        PercentString = "퍼센트: " + (100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100)).ToString() + "%",
                        Num = "필요 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum.ToString(),
                        NeedNum = "재료 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum.ToString()
                    });
                    if (MixInfo_LowestList[n].Percent == 100)
                        MixInfo_LowestList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                    else
                        MixInfo_LowestList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    MixInfo_LowestList[n].PercentColor.Opacity = 1;
                    MixInfo_LowestList[n].WispPercentColor.Opacity = 0.5;
                    MixInfo_LowestList[n].HeroPercentColor.Opacity = 0.5;
                }
            }
        }

        void SearchMixInfo()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                unsafe
            {
                if (MixInfo_UnitWispPercent != uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).wispPercent)
                {
                    MixInfo_UnitWispPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).wispPercent;
                    if (MixInfo_unitPercent == 100)
                        MixInfo_UnitPercentColor = new SolidColorBrush(Colors.LightGreen);
                    else
                        MixInfo_UnitPercentColor = new SolidColorBrush(Colors.LightGray);
                }
                if(MixInfo_UnitHeroPercent != uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).heroPercent)
                    MixInfo_UnitHeroPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).heroPercent;
                if(MixInfo_UnitPercent != uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).percent)
                    MixInfo_UnitPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).percent;
                if(MixInfo_UnitPercentString != "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).percent + "%")
                    MixInfo_UnitPercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).percent + "%";
                if(MixInfo_UnitNum != "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).num.ToString())
                    MixInfo_UnitNum = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).num.ToString();
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).highestNum; n++)
            {
                int highestGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).highestUnitMix(n).gradeIndex;
                int highestUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).highestUnitMix(n).unitIndex;
                unsafe
                {
                    if (MixInfo_HighestList[n].Percent != uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).percent)
                    {
                        MixInfo_HighestList[n].Percent = uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).percent;
                        if (MixInfo_HighestList[n].Percent == 100)
                            MixInfo_HighestList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                        else
                            MixInfo_HighestList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    }
                    if(MixInfo_HighestList[n].WispPercent != uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).wispPercent)
                        MixInfo_HighestList[n].WispPercent = uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).wispPercent;
                    if(MixInfo_HighestList[n].HeroPercent != uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).heroPercent)
                        MixInfo_HighestList[n].HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).heroPercent;
                    if(MixInfo_HighestList[n].PercentString != "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).percent + "%")
                        MixInfo_HighestList[n].PercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).percent + "%";
                    if (MixInfo_HighestList[n].Num != "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).num.ToString())
                        MixInfo_HighestList[n].Num = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(highestGrade).unit(highestUnit).num.ToString();
                }
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).upperNum; n++)
            {
                int upperGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).upperUnitMix(n).gradeIndex;
                int upperUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).upperUnitMix(n).unitIndex;
                unsafe
                {
                    if (MixInfo_UpperList[n].Percent != uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).percent)
                    {
                        MixInfo_UpperList[n].Percent = uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).percent;
                        if (MixInfo_UpperList[n].Percent == 100)
                            MixInfo_UpperList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                        else
                            MixInfo_UpperList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    }
                    if(MixInfo_UpperList[n].WispPercent != uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).wispPercent)
                        MixInfo_UpperList[n].WispPercent = uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).wispPercent;
                    if(MixInfo_UpperList[n].HeroPercent != uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).heroPercent)
                        MixInfo_UpperList[n].HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).heroPercent;
                    if(MixInfo_UpperList[n].PercentString != "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).percent + "%")
                        MixInfo_UpperList[n].PercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).percent + "%";
                    if(MixInfo_UpperList[n].Num != "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).num.ToString())
                        MixInfo_UpperList[n].Num = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(upperGrade).unit(upperUnit).num.ToString();
                }
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subNum; n++)
            {
                int subGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).gradeIndex;
                int subUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).unitIndex;
                unsafe
                {
                    if (MixInfo_SubList[n].Percent != uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).percent)
                    {
                        MixInfo_SubList[n].Percent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).percent;
                        if (MixInfo_SubList[n].Percent == 100)
                            MixInfo_SubList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                        else
                            MixInfo_SubList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    }
                    if(MixInfo_SubList[n].WispPercent != uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).wispPercent)
                        MixInfo_SubList[n].WispPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).wispPercent;
                    if(MixInfo_SubList[n].HeroPercent != uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).heroPercent)
                        MixInfo_SubList[n].HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).heroPercent;
                    if(MixInfo_SubList[n].PercentString != "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).percent + "%")
                        MixInfo_SubList[n].PercentString = "퍼센트: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).percent + "%";
                    if(MixInfo_SubList[n].Num != "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(subGrade).unit(subUnit).num.ToString())
                        MixInfo_SubList[n].Num = "현재 개수: " + uh[MainView_SelectedVersionItem.Index].grade(subGrade).unit(subUnit).num.ToString();
                    if(MixInfo_SubList[n].NeedNum != "재료 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).basicNum.ToString())
                        MixInfo_SubList[n].NeedNum = "재료 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).subUnitMix(n).basicNum.ToString();
                }
            }

            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestNum; n++)
            {
                int lowestGrade = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).gradeIndex;
                int lowestUnit = uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).unitIndex;
                unsafe
                {
                    if (MixInfo_LowestList[n].Percent != 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100))
                    {
                        MixInfo_LowestList[n].Percent = 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum /
                            (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100);
                        if (MixInfo_LowestList[n].Percent == 100)
                            MixInfo_LowestList[n].PercentColor = new SolidColorBrush(Colors.LightGreen);
                        else
                            MixInfo_LowestList[n].PercentColor = new SolidColorBrush(Colors.LightGray);
                    }
                    if(MixInfo_LowestList[n].WispPercent != 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needWispNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100))
                        MixInfo_LowestList[n].WispPercent = 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needWispNum /
                            (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100);
                    if (MixInfo_LowestList[n].HeroPercent != 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needHeroNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100))
                        MixInfo_LowestList[n].HeroPercent = 100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needHeroNum /
                            (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100);
                    if(MixInfo_LowestList[n].PercentString != "퍼센트: " + (100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum /
                        (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100)).ToString() + "%")
                        MixInfo_LowestList[n].PercentString = "퍼센트: " + (100 - (int)(((float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum /
                            (float)uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum) * 100)).ToString() + "%";
                    if(MixInfo_LowestList[n].Num != "필요 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum.ToString())
                        MixInfo_LowestList[n].Num = "필요 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).needNum.ToString();
                    if(MixInfo_LowestList[n].NeedNum != "재료 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum.ToString())
                        MixInfo_LowestList[n].NeedNum = "재료 개수: " + uh[MainView_SelectedVersionItem.Index].grade(MixInfo_GradeIndex).unit(MixInfo_UnitIndex).lowestUnitMix(n).basicNum.ToString();
                }
            }
            }));
        }

        #endregion
    }
}
