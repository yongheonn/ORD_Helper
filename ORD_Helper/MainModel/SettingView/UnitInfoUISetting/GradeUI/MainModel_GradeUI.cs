using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region Data

        public List<GradeUI_GradeUIItem> GradeUI_GradeList { get; set; }

        public class GradeUI_GradeUIItem
        { 
            public string GradeName { get; set; }

            /// <summary>
            /// 유닛 설정 탭_버젼 리스트 Property
            /// <para>Binding: ItemsSource</para>
            /// </summary>
            public ObservableCollection<GradeUI_UnitUIItem> UnitList
            {
                get;
                set;
            }
        }

        public class GradeUI_UnitUIItem
        {
            public string Image { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region Method

        public void Load_GradeUI()
        {
            GradeUI_GradeList = new List<GradeUI_GradeUIItem>();
            for (int n = 0; n < uh[0].gradeNum; n++)
            {
                unsafe
                {
                    GradeUI_GradeList.Add(new GradeUI_GradeUIItem()
                    {
                        GradeName = Marshal.PtrToStringAnsi((IntPtr)uh[0].grade(n).name),
                        UnitList = new ObservableCollection<GradeUI_UnitUIItem>()
                    });

                    for (int i = 0; i < uh[0].grade(n).unitNum; i++)
                    {
                        GradeUI_GradeList[n].UnitList.Add(new GradeUI_UnitUIItem()
                        {
                            Image = AppDomain.CurrentDomain.BaseDirectory + "Resources\\" + Marshal.PtrToStringAnsi((IntPtr)uh[0].grade(n).name)
                                        + "\\" + Marshal.PtrToStringAnsi((IntPtr)uh[0].grade(n).unit(i).name) + ".jpg",
                            Name = Marshal.PtrToStringAnsi((IntPtr)uh[0].grade(n).unit(i).name)
                        });
                    }
                }
            }
        }

        #endregion
    }
}
