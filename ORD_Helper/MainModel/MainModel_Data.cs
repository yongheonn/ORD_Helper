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

namespace ORD_Helper
{
    partial class MainModel
    {
        #region DataHandler

        #region VersionHandler

        /// <summary>
        /// 유닛 데이터 핸들러
        /// <para>Type: UnitHandler</para>
        /// </summary>
        static VersionHandler vh = new VersionHandler();

        #endregion

        #region UnitHandler
        /// <summary>
        /// 유닛 데이터 핸들러
        /// <para>Type: UnitHandler</para>
        /// </summary>
        static List<UnitHandler> uh = new List<UnitHandler>();
        /// <summary>
        /// 유닛 설정용 임시 유닛 데이터 핸들러
        /// <para>Type: UnitHandler</para>
        /// </summary>
        static List<UnitHandler> uh_temp = new List<UnitHandler>();
        #endregion

        #region ProcessHandler

        /// <summary>
        /// 프로세스 데이터 핸들러
        /// <para>Type: ProcessHandler</para>
        /// </summary>
        static List<ProcessHandler> ph = new List<ProcessHandler>();

        /// <summary>
        /// 프로세스 데이터 핸들러
        /// <para>Type: ProcessHandler</para>
        /// </summary>
        static List<ProcessHandler> ph_temp = new List<ProcessHandler>();

        #endregion

        #region GameStateHandler

        /// <summary>
        /// 게임 상태 데이터 핸들러
        /// <para>Type: GameStateHandler</para>
        /// </summary>
        static List<GameStateHandler> gh = new List<GameStateHandler>();

        #endregion

        #region ResourcesHandler

        /// <summary>
        /// 자원 데이터 핸들러
        /// <para>Type: ResourcesHandler</para>
        /// </summary>
        static List<ResourcesHandler> rh = new List<ResourcesHandler>();

        #endregion

        #region AddressHandler

        #region UnitAddressHandler

        /// <summary>
        /// 유닛 주소 데이터 핸들러
        /// <para>Type: UnitAddressHandler</para>
        /// </summary>
        static List<UnitAddressHandler> uah = new List<UnitAddressHandler>();

        /// <summary>
        /// 
        /// </summary>
        static List<UnitAddressHandler> uah_temp = new List<UnitAddressHandler>();

        #endregion

        #region ResourcesAddressHandler

        /// <summary>
        /// 자원 주소 데이터 핸들러
        /// <para>Type: ResourcesAddressHandler</para>
        /// </summary>
        static List<ResourcesAddressHandler> rah = new List<ResourcesAddressHandler>();

        /// <summary>
        /// 자원 주소 데이터 핸들러
        /// <para>Type: ResourcesAddressHandler</para>
        /// </summary>
        static List<ResourcesAddressHandler> rah_temp = new List<ResourcesAddressHandler>();

        #endregion

        #region GameStateAddressHandler

        /// <summary>
        /// 게임 상태 주소 데이터 핸들러
        /// <para>Type: GameStateAddressHandler</para>
        /// </summary>
        static List<GameStateAddressHandler> gah = new List<GameStateAddressHandler>();

        /// <summary>
        /// 게임 상태 주소 데이터 핸들러
        /// <para>Type: GameStateAddressHandler</para>
        /// </summary>
        static List<GameStateAddressHandler> gah_temp = new List<GameStateAddressHandler>();

        #endregion

        #endregion

        #endregion

        #region Grade

        #region GradeItem
        /// <summary>
        /// 등급 아이템 class
        /// </summary>
        public class GradeItem : ViewModelBase
        {
            private string name;
            /// <summary>
            /// 등급 아이템 이름
            /// <para>Type: string</para>
            /// </summary>
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
            /// <summary>
            /// 등급 아이템 인덱스
            /// <para>Type: int</para>
            /// </summary>
            public int Index { get; set; }
        }
        #endregion

        #endregion

        #region Unit

        #region class UnitItem

        /// <summary>
        /// 유닛 아이템 class
        /// </summary>
        public class UnitItem : ViewModelBase
        {
            private string name;
            /// <summary>
            /// 유닛 아이템 이름
            /// <para>Type: string</para>
            /// </summary>
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

            private ImageSource image;
            /// <summary>
            /// 유닛 아이템 이미지
            /// <para>Type: string</para>
            /// </summary>
            public ImageSource Image
            {
                get { return image; }
                set
                {
                    image = value;
                    OnPropertyChanged("Image");
                }
            }

            /// <summary>
            /// 유닛 아이템 인덱스
            /// <para>Type: int</para>
            /// </summary>
            public int Index { get; set; }
        }

        #endregion

        #endregion

        #region Mix

        #region MixItem

        /// <summary>
        /// 추가할 조합 목록 아이템 class
        /// </summary>
        public class MixItem
        {
            /// <summary>
            /// 추가할 조합 아이템 이름
            /// <para>Type: string</para>
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 추가할 조합 아이템 이미지
            /// <para>Type: string</para>
            /// </summary>
            public ImageSource Image { get; set; }

            /// <summary>
            /// 추가할 조합 아이템 수
            /// <para>Type: int</para>
            /// </summary>
            public int Num { get; set; }

            /// <summary>
            /// 추가할 조합 아이템 등급 인덱스
            /// <para>Type: int</para>
            /// </summary>
            public int GradeIndex { get; set; }

            /// <summary>
            /// 추가할 조합 아이템 유닛 인덱스
            /// <para>Type: int</para>
            /// </summary>
            public int UnitIndex { get; set; }

            /// <summary>
            /// 추가할 조합 아이템 유형
            /// <para>Type: int</para>
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// 추가할 조합 아이템 인덱스
            /// <para>Type: int</para>
            /// </summary>
            public int Index { get; set; }
        }

        #endregion

        #region MixTypeitem

        /// <summary>
        /// 추가할 조합 목록 아이템 class
        /// </summary>
        public class MixTypeItem
        {
            /// <summary>
            /// 추가할 조합 목록 유형 텍스트
            /// <para>Type: string</para>
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 추가할 조합 목록 유형 인덱스
            /// <para>Type: int</para>
            /// </summary>
            public int Index { get; set; }
        }

        #endregion

        #endregion

        #region Index

        #region SearchItem

        public class SearchItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int Index { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Text { get; set; }
        }

        #endregion

        #region IndexItem

        public class IndexItem :ViewModelBase
        {
            private string address;

            /// <summary>
            /// 
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

            private string index;

            /// <summary>
            /// 
            /// </summary>
            public string Index
            {
                get { return index; }
                set
                {
                    index = value;
                    OnPropertyChanged("Index");
                }
            }

            private int num;

            /// <summary>
            /// 
            /// </summary>
            public int Num
            {
                get { return num; }
                set
                {
                    num = value;
                    OnPropertyChanged("Num");
                }
            }

            public string name;

            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

            private ImageSource image;

            public ImageSource Image
            {
                get { return image; }
                set
                {
                    image = value;
                    OnPropertyChanged("Image");
                }
            }

            public int ItemIndex { get; set; }
        }

        #endregion

        #endregion

        #region Version

        #region VersionItem

        public class VersionItem :ViewModelBase
        {
            /// <summary>
            /// 
            /// </summary>
            public int Index { get; set; }

            private string text;

            /// <summary>
            /// 
            /// </summary>
            public string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_Data()
        {
            vh.LoadVerInfo();
            for (int n = 0; n < vh.verNum; n++)
            {
                rh.Add(new ResourcesHandler());
                gh.Add(new GameStateHandler());
                ph.Add(new ProcessHandler());
                uah.Add(new UnitAddressHandler());
                rah.Add(new ResourcesAddressHandler());
                gah.Add(new GameStateAddressHandler());
                uh.Add(new UnitHandler());
                unsafe
                {
                    ph[n].LoadProcess(vh.ver(n).verName);
                    uah[n].LoadAddress(vh.ver(n).verName);
                    rah[n].gold.LoadAddress(vh.ver(n).verName);
                    rah[n].tree.LoadAddress(vh.ver(n).verName);
                    gah[n].LoadAddress(vh.ver(n).verName);
                    uh[n].LoadUnitInfo(vh.ver(n).verName);

                    /*
                    for (int i = 0; i < uh[n].gradeNum; i++)
                    {
                        for (int j = 0; j < uh[n].grade(i).unitNum; j++)
                        {
                            uh[n].grade(i).unit(j).lowestNum = 0;
                            uh[n].grade(i).unit(j).ResizeLowestUnitMix(uh[n].grade(i).unit(j).lowestNum);
                        }
                    }
                    for (int i = 0; i < uh[n].gradeNum; i++)
                    {
                        for (int j = 0; j < uh[n].grade(i).unitNum; j++)
                        {
                            for (int k = 0; k < uh[n].grade(i).unit(j).lowestNum; k++)
                                uh[n].grade(i).unit(j).lowestUnitMix(k).basicNum = 0;
                            uh[n].SetLowestMix(i, j, uh[n].grade(i).unit(j), 1);
                        }
                    }
                    uh[n].SaveUnitInfo(vh.ver(n).verName);
                    */
                    /*
                    if (n == 1)
                    {
                        for (int a = 0; a < uh[1].grade(3).unit(22).upperNum; a++)
                        {
                            if (uh[1].grade(3).unit(22).upperUnitMix(a).gradeIndex == 4 && uh[1].grade(3).unit(22).upperUnitMix(a).unitIndex == 23)
                                uh[1].grade(3).unit(22).RemoveUpperUnitMix(a);
                        }
                    }
                    */
                    /*
                    uh[n].grade(0).unit(7).upperNum = 6;
                    uh[n].grade(0).unit(7).ResizeUpperUnitMix(6);
                    uh[n].grade(0).unit(7).upperUnitMix(0).gradeIndex = 1;
                    uh[n].grade(0).unit(7).upperUnitMix(0).unitIndex = 0;
                    uh[n].grade(0).unit(7).upperUnitMix(1).gradeIndex = 1;
                    uh[n].grade(0).unit(7).upperUnitMix(1).unitIndex = 5;
                    uh[n].grade(0).unit(7).upperUnitMix(2).gradeIndex = 1;
                    uh[n].grade(0).unit(7).upperUnitMix(2).unitIndex = 6;
                    uh[n].grade(0).unit(7).upperUnitMix(3).gradeIndex = 1;
                    uh[n].grade(0).unit(7).upperUnitMix(3).unitIndex = 12;
                    uh[n].grade(0).unit(7).upperUnitMix(4).gradeIndex = 2;
                    uh[n].grade(0).unit(7).upperUnitMix(4).unitIndex = 13;
                    uh[n].grade(0).unit(7).upperUnitMix(5).gradeIndex = 2;
                    uh[n].grade(0).unit(7).upperUnitMix(5).unitIndex = 25;
                    */
                    /*
                    for (int i = 0; i < uh[n].gradeNum; i++)
                    {
                        for (int j = 0; j < uh[n].grade(i).unitNum; j++)
                        {
                            uh[n].grade(i).unit(j).highestNum = 0;
                            uh[n].grade(i).unit(j).ResizeHighestUnitMix(uh[n].grade(i).unit(j).highestNum);
                        }
                    }
                    for (int i = 0; i < uh[n].gradeNum; i++)
                    {
                        for (int j = 0; j < uh[n].grade(i).unitNum; j++)
                        {
                            HighestUnitMix highestUnitMix = new HighestUnitMix(i, j);
                            uh[n].SetHighestMix(i, j, highestUnitMix);
                        }
                    }
                    uh[n].SaveUnitInfo(vh.ver(n).verName);
                    */
                }

            }   
        }

        #endregion
    }
}
