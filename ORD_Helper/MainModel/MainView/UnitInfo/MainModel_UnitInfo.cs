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

        public void UnitInfo_OnGradeInfo(object para)
        {
            
        }

        #endregion

        public void UnitInfo_OnMixInfo(object para)
        {
            var index = (Tuple<object, object>)para;
            dataThread.PauseThread("SearchUnitInfo");
            MixInfo_GradeIndex = int.Parse(index.Item1.ToString());
            MixInfo_UnitIndex = int.Parse(index.Item2.ToString());
            MixInfo_AddUnit();
            MixInfo_AddList();
            dataThread.ResumeThread("SearchMixInfo");
            MainView_Popup = 1;
        }

        #region Data

        public List<GradeUIItem> UnitInfo_GradeList { get; set; }

        public class GradeUIItem : ViewModelBase
        {
            public ICommand GradeInfoCommand { get; set; }

            public string GradeName { get; set; }

            /// <summary>
            /// 유닛 설정 탭_버젼 리스트 Property
            /// <para>Binding: ItemsSource</para>
            /// </summary>
            public ObservableCollection<UnitUIItem> UnitList
            {
                get;
                set;
            }

            public int Index { get; set; }
        }

        public class UnitUIItem :ViewModelBase
        {
            public ICommand SelectedCommand { get; set; }

            public string Image { get; set; }
            public string Name { get; set; }

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

            private string percentString;
            public string PercentString
            {
                get { return percentString; }
                set
                {
                    percentString = value;
                    OnPropertyChanged("PercentString");
                }
            }
            private string num;
            public string Num {
                get { return num; }
                set
                {
                    num = value;
                    OnPropertyChanged("Num");
                }
            }

            public int GradeIndex { get; set; }
            public int UnitIndex { get; set; }
        }

        #endregion

        #region Method

        public void Load_UnitInfo()
        {
            UnitInfo_GradeList = new List<GradeUIItem>();
            for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].gradeNum; n++)
            {
                unsafe
                {
                    UnitInfo_GradeList.Add(new GradeUIItem() { GradeInfoCommand = new RelayCommand<object>(p => UnitInfo_OnGradeInfo(p)), Index = n,
                        GradeName = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).name), UnitList = new ObservableCollection<UnitUIItem>() });

                    for (int i = 0; i < uh[MainView_SelectedVersionItem.Index].grade(n).unitNum; i++)
                    {
                        UnitInfo_GradeList[n].UnitList.Add(new UnitUIItem()
                        {
                            Image = AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).name)
                                        + "\\" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).name) + ".jpg",
                            Name = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).name),
                            Percent = 0,
                            WispPercent = 0,
                            HeroPercent = 0,
                            PercentString = 0 + "%", GradeIndex = n, UnitIndex = i,
                            SelectedCommand = new RelayCommand<object>(p => UnitInfo_OnMixInfo(p))
                        });
                        UnitInfo_GradeList[n].UnitList[i].PercentColor.Opacity = 1;
                        UnitInfo_GradeList[n].UnitList[i].WispPercentColor.Opacity = 0.5;
                        UnitInfo_GradeList[n].UnitList[i].HeroPercentColor.Opacity = 0.5;
                    }
                }
            }
            dataThread.CreateThread(SearchUnitInfo, 500);
            dataThread.ResumeThread("SearchUnitInfo");
        }

        public void SearchUnitInfo()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                for(int n = UnitInfo_GradeList.Count; n < uh[MainView_SelectedVersionItem.Index].gradeNum; n++)
                {
                    unsafe
                    {
                        UnitInfo_GradeList.Add(new GradeUIItem()
                        {
                            GradeInfoCommand = new RelayCommand<object>(p => UnitInfo_OnGradeInfo(p)),
                            Index = n,
                            GradeName = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).name),
                            UnitList = new ObservableCollection<UnitUIItem>()
                        });
                    }
                }
                for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].gradeNum; n++)
                {
                    for (int i = UnitInfo_GradeList[n].UnitList.Count; i < uh[MainView_SelectedVersionItem.Index].grade(n).unitNum; i++)
                    {
                            unsafe
                            {
                                UnitInfo_GradeList[n].UnitList.Add(new UnitUIItem()
                                {
                                    Image = AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).name)
                                                        + "\\" + Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).name) + ".jpg",
                                    Name = Marshal.PtrToStringAnsi((IntPtr)uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).name),
                                    Percent = 0,
                                    WispPercent = 0,
                                    HeroPercent = 0,
                                    PercentString = 0 + "%",
                                    GradeIndex = n,
                                    UnitIndex = i,
                                    SelectedCommand = new RelayCommand<object>(p => UnitInfo_OnMixInfo(p))
                                });
                                UnitInfo_GradeList[n].UnitList[i].PercentColor.Opacity = 1;
                                UnitInfo_GradeList[n].UnitList[i].WispPercentColor.Opacity = 0.5;
                                UnitInfo_GradeList[n].UnitList[i].HeroPercentColor.Opacity = 0.5;
                            }
                    }
                }
                for (int n = 0; n < uh[MainView_SelectedVersionItem.Index].gradeNum; n++)
                {
                unsafe
                {
                        for (int i = 0; i < uh[MainView_SelectedVersionItem.Index].grade(n).unitNum; i++)
                        {
                            if (UnitInfo_GradeList[n].UnitList[i].Percent != uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).percent)
                            {
                                UnitInfo_GradeList[n].UnitList[i].Percent = uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).percent;
                                if (uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).percent == 100)
                                    UnitInfo_GradeList[n].UnitList[i].PercentColor = new SolidColorBrush(Colors.LightGreen);
                                else
                                    UnitInfo_GradeList[n].UnitList[i].PercentColor = new SolidColorBrush(Colors.LightGray);
                            }
                            if (UnitInfo_GradeList[n].UnitList[i].WispPercent != uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).wispPercent)
                                UnitInfo_GradeList[n].UnitList[i].WispPercent = uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).wispPercent;
                            if (UnitInfo_GradeList[n].UnitList[i].HeroPercent != uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).heroPercent)
                                UnitInfo_GradeList[n].UnitList[i].HeroPercent = uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).heroPercent;
                            if (UnitInfo_GradeList[n].UnitList[i].PercentString != uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).percent.ToString() + "%")
                                UnitInfo_GradeList[n].UnitList[i].PercentString = uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).percent.ToString() + "%";
                            if (UnitInfo_GradeList[n].UnitList[i].Num != uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).num.ToString())
                                UnitInfo_GradeList[n].UnitList[i].Num = uh[MainView_SelectedVersionItem.Index].grade(n).unit(i).num.ToString();
                        }
                }
            }
            }));
        }

            #endregion
        }
}
