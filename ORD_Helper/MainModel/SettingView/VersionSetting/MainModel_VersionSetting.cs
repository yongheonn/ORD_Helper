using ORD_Helper_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ORD_Helper
{
    partial class MainModel
    {
        #region ICommand

        #region VersionSetting_VersionAddCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand VersionSetting_VersionAddCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void VersionSetting_OnVersionAdd()
        {
            int index = VersionSetting_VersionList.Count;
            int count = 0;
            bool retry = true;
            while (retry == true)
            {
                retry = false;
                for (int n = 0; n < VersionSetting_VersionList.Count; n++)
                {
                    if ("버전 " + count == VersionSetting_VersionList[n].Text)
                    {
                        retry = true;
                        count++;
                    }
                }
            }
            VersionSetting_VersionList.Add(new VersionItem() { Index = index, Text = "버전 " + count.ToString() });
            OnPropertyChanged("VersionSetting_VersionList");
            vh.verNum++;
            vh.ResizeVer(vh.verNum);
            unsafe
            {
                vh.ver(index).verName = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(VersionSetting_VersionList[index].Text);
            }
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Version/" + VersionSetting_VersionList[index].Text);

            // 만약 폴더가 존재하지 않으면
            if (di.Exists == false)
                di.Create();
            Add_VersionList();
            vh.SaveVerInfo();
        }

        #endregion

        #region VersionSetting_VersionCopyCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand VersionSetting_VersionCopyCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void VersionSetting_OnVersionCopy()
        {
            int count = 0;
            int index = VersionSetting_VersionList.Count;
            bool retry = true;
            while (retry == true)
            {
                retry = false;
                for (int n = 0; n < VersionSetting_VersionList.Count; n++)
                {
                    if (VersionSetting_SelectedVersionItem.Text + " 복사본" == VersionSetting_VersionList[n].Text && count == 0)
                    {
                        retry = true;
                        count++;
                    }
                    else if(VersionSetting_SelectedVersionItem.Text + " 복사본" + "(" + count + ")" == VersionSetting_VersionList[n].Text)
                    {
                        retry = true;
                        count++;
                    }
                }
            }
            vh.verNum++;
            vh.ResizeVer(vh.verNum);
            if (count == 0)
                VersionSetting_VersionList.Add(new VersionItem() { Index = index, Text = VersionSetting_SelectedVersionItem.Text + " 복사본" });
            else
                VersionSetting_VersionList.Add(new VersionItem() { Index = index, Text = VersionSetting_SelectedVersionItem.Text + " 복사본" + "(" + count + ")" });
            OnPropertyChanged("VersionSetting_VersionList");
            unsafe
            {
                vh.ver(index).verName = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(VersionSetting_VersionList[index].Text);
            }

            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Version/" + VersionSetting_VersionList[index].Text);

            // 만약 폴더가 존재하지 않으면
            if (di.Exists == false)
                di.Create();
            CopyAdd_VersionList(VersionSetting_SelectedVersionItem.Index);
        }

        #endregion

        #region VersionSetting_VersionDeleteCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand VersionSetting_VersionDeleteCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void VersionSetting_OnVersionDelete()
        {
            vh.RemoveVer(VersionSetting_SelectedVersionItem.Index);
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Version/" + VersionSetting_SelectedVersionItem.Text);

            // 만약 폴더가 존재하면
            if (di.Exists == true)
                di.Delete(true);

            Delete_VersionList();
            for (int n = VersionSetting_SelectedVersionItem.Index + 1; n < VersionSetting_VersionList.Count; n++)
                VersionSetting_VersionList[n].Index--;
            VersionSetting_VersionList.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            OnPropertyChanged("VersionSetting_VersionList");
            vh.SaveVerInfo();
        }

        #endregion

        #region VersionSetting_SaveCommand

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 ICommand
        /// </summary>
        public ICommand VersionSetting_SaveCommand { get; set; }

        /// <summary>
        /// 유닛 설정 탭_유닛 편집 Command 실행 시
        /// </summary>
        private void VersionSetting_OnSave()
        {
            Copy_VersionList();
            vh.SaveVerInfo();
        }

        #endregion

        #endregion

        #region Data

        #region VersionSetting_VersionList

        /// <summary>
        /// 유닛 설정 탭_버젼 리스트 Property
        /// <para>Binding: ItemsSource</para>
        /// </summary>
        public ObservableCollection<VersionItem> VersionSetting_VersionList
        {
            get;
            set;
        }

        #endregion

        #region VersionSetting_SelectedVersionItem

        private VersionItem VersionSetting_selectedVersionItem;

        /// <summary>
        /// 유닛 설정 탭_선택된 버젼 아이템 Property
        /// <para>Type: VersionItem</para>
        /// <para>Binding: SelectedItem</para>
        /// </summary>
        public VersionItem VersionSetting_SelectedVersionItem
        {
            get { return VersionSetting_selectedVersionItem; }
            set
            {
                VersionSetting_selectedVersionItem = value;
                if (value != null)
                    VersionSetting_versionName = value.Text;
                else
                    VersionSetting_versionName = null;
                OnPropertyChanged("VersionSetting_VersionName");
                OnPropertyChanged("VersionSetting_SelectedVersionItem");
            }
        }

        #endregion

        #region VersionSetting_VersionName

        private string VersionSetting_versionName;

        /// <summary>
        /// 유닛 추가 팝업_유닛 이름 텍스트박스 Property
        /// <para>Type: string</para>
        /// <para>Binding: Text</para>
        /// </summary>
        public string VersionSetting_VersionName
        {
            get { return VersionSetting_versionName; }
            set
            {
                VersionSetting_versionName = value; 
                DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Version/" + VersionSetting_SelectedVersionItem.Text);
                VersionSetting_SelectedVersionItem.Text = value;
                // 만약 폴더가 존재하면
                if (di.Exists == true)
                    di.MoveTo(AppDomain.CurrentDomain.BaseDirectory + "Version/" + value);

                unsafe
                {
                    vh.ver(VersionSetting_SelectedVersionItem.Index).verName = (sbyte*)(void*)Marshal.StringToHGlobalAnsi(VersionSetting_VersionList[VersionSetting_SelectedVersionItem.Index].Text);
                }
                OnPropertyChanged("VersionSetting_VersionName");
                OnPropertyChanged("VersionSetting_VersionList");
                Copy_VersionList();
                vh.SaveVerInfo();
            }
        }

        #endregion

        #endregion

        #region Method

        public void Load_VersionSetting()
        {
            VersionSetting_VersionAddCommand = new RelayCommand<object>(p => VersionSetting_OnVersionAdd());
            VersionSetting_VersionCopyCommand = new RelayCommand<object>(p => VersionSetting_OnVersionCopy());
            VersionSetting_VersionDeleteCommand = new RelayCommand<object>(p => VersionSetting_OnVersionDelete());
            VersionSetting_SaveCommand = new RelayCommand<object>(p => VersionSetting_OnSave());

            VersionSetting_VersionList = new ObservableCollection<VersionItem>();
            unsafe
            {
                for (int n = 0; n < vh.verNum; n++)
                {
                    VersionSetting_VersionList.Add(new VersionItem() { Index = n, Text = Marshal.PtrToStringAnsi((IntPtr)vh.ver(n).verName) });
                }
            }
        }

        public void CopyAdd_VersionList(int index)
        {
            GameStateAddressSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            gah_temp.Add(new GameStateAddressHandler());
            gah_temp[gah_temp.Count - 1].Copy(gah_temp[index]);
            gah.Add(new GameStateAddressHandler());
            gah[gah.Count - 1].Copy(gah[index]);

            ProcessSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            ph_temp.Add(new ProcessHandler());
            ph_temp[ph_temp.Count - 1].Copy(ph_temp[index]);
            ph.Add(new ProcessHandler());
            ph[ph.Count - 1].Copy(ph[index]);

            ResourcesAddressSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            rah_temp.Add(new ResourcesAddressHandler());
            rah_temp[rah_temp.Count - 1].Copy(rah_temp[index]);
            rah.Add(new ResourcesAddressHandler());
            rah[rah.Count - 1].Copy(rah[index]);

            UnitAddressSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            uah_temp.Add(new UnitAddressHandler());
            uah_temp[uah_temp.Count - 1].Copy(uah_temp[index]);
            uah.Add(new UnitAddressHandler());
            uah[uah.Count - 1].Copy(uah[index]);

            UnitSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            uh_temp.Add(new UnitHandler());
            uh_temp[uh_temp.Count - 1].Copy(uh_temp[index]);
            uh.Add(new UnitHandler());
            uh[uh.Count - 1].Copy(uh[index]);

            rh.Add(new ResourcesHandler());
            rh[rh.Count - 1].Copy(rh[index]);
            gh.Add(new GameStateHandler());
            gh[gh.Count - 1].Copy(gh[index]);

            vh.SaveVerInfo();
            unsafe
            {
                uh[vh.verNum - 1].SaveUnitInfo(vh.ver(vh.verNum - 1).verName);
                ph[vh.verNum - 1].SaveProcess(vh.ver(vh.verNum - 1).verName);
                uah[vh.verNum - 1].SaveAddress(vh.ver(vh.verNum - 1).verName);
                rah[vh.verNum - 1].gold.SaveAddress(vh.ver(vh.verNum - 1).verName);
                rah[vh.verNum - 1].tree.SaveAddress(vh.ver(vh.verNum - 1).verName);
                gah[vh.verNum - 1].SaveAddress(vh.ver(vh.verNum - 1).verName);
            }
        }

        public void Add_VersionList()
        {
            GameStateAddressSetting_VersionList.Add(new VersionItem()
            { Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            gah_temp.Add(new GameStateAddressHandler());
            gah.Add(new GameStateAddressHandler());

            ProcessSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            ph_temp.Add(new ProcessHandler());
            ph.Add(new ProcessHandler());

            ResourcesAddressSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            rah_temp.Add(new ResourcesAddressHandler());
            rah.Add(new ResourcesAddressHandler());

            UnitAddressSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            uah_temp.Add(new UnitAddressHandler());
            uah.Add(new UnitAddressHandler());

            UnitSetting_VersionList.Add(new VersionItem()
            {
                Index = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Index,
                Text = VersionSetting_VersionList[VersionSetting_VersionList.Count - 1].Text
            });
            uh_temp.Add(new UnitHandler());
            uh.Add(new UnitHandler());

            rh.Add(new ResourcesHandler());
            gh.Add(new GameStateHandler());

            vh.SaveVerInfo();
            unsafe
            {
                uh[vh.verNum - 1].SaveUnitInfo(vh.ver(vh.verNum - 1).verName);
                ph[vh.verNum - 1].SaveProcess(vh.ver(vh.verNum - 1).verName);
                uah[vh.verNum - 1].SaveAddress(vh.ver(vh.verNum - 1).verName);
                rah[vh.verNum - 1].gold.SaveAddress(vh.ver(vh.verNum - 1).verName);
                rah[vh.verNum - 1].tree.SaveAddress(vh.ver(vh.verNum - 1).verName);
                gah[vh.verNum - 1].SaveAddress(vh.ver(vh.verNum - 1).verName);
            }
        }

        public void Delete_VersionList()
        {
            for (int n = VersionSetting_SelectedVersionItem.Index + 1; n < VersionSetting_VersionList.Count; n++)
                GameStateAddressSetting_VersionList[n].Index--;
            GameStateAddressSetting_VersionList.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            gah_temp.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            gah.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            for (int n = VersionSetting_SelectedVersionItem.Index + 1; n < VersionSetting_VersionList.Count; n++)
                ProcessSetting_VersionList[n].Index--;
            ProcessSetting_VersionList.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            ph_temp.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            ph.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            for (int n = VersionSetting_SelectedVersionItem.Index + 1; n < VersionSetting_VersionList.Count; n++)
                ResourcesAddressSetting_VersionList[n].Index--;
            ResourcesAddressSetting_VersionList.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            rah_temp.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            rah.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            for (int n = VersionSetting_SelectedVersionItem.Index + 1; n < VersionSetting_VersionList.Count; n++)
                UnitAddressSetting_VersionList[n].Index--;
            UnitAddressSetting_VersionList.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            uah_temp.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            uah.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            for (int n = VersionSetting_SelectedVersionItem.Index + 1; n < VersionSetting_VersionList.Count; n++)
                UnitSetting_VersionList[n].Index--;
            UnitSetting_VersionList.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            uh_temp.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            uh.RemoveAt(VersionSetting_SelectedVersionItem.Index);

            gh.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            rh.RemoveAt(VersionSetting_SelectedVersionItem.Index);
            vh.SaveVerInfo();
        }

        public void Copy_VersionList()
        {
            for (int n = 0; n < GameStateAddressSetting_VersionList.Count; n++)
            {
                GameStateAddressSetting_VersionList[n].Index = VersionSetting_VersionList[n].Index;
                GameStateAddressSetting_VersionList[n].Text = VersionSetting_VersionList[n].Text;
            }

            for (int n = 0; n < ProcessSetting_VersionList.Count; n++)
            {
                ProcessSetting_VersionList[n].Index = VersionSetting_VersionList[n].Index;
                ProcessSetting_VersionList[n].Text = VersionSetting_VersionList[n].Text;
            }

            for (int n = 0; n < ResourcesAddressSetting_VersionList.Count; n++)
            {
                ResourcesAddressSetting_VersionList[n].Index = VersionSetting_VersionList[n].Index;
                ResourcesAddressSetting_VersionList[n].Text = VersionSetting_VersionList[n].Text;
            }

            for (int n = 0; n < UnitAddressSetting_VersionList.Count; n++)
            {
                UnitAddressSetting_VersionList[n].Index = VersionSetting_VersionList[n].Index;
                UnitAddressSetting_VersionList[n].Text = VersionSetting_VersionList[n].Text;
            }

            for (int n = 0; n < UnitSetting_VersionList.Count; n++)
            {
                UnitSetting_VersionList[n].Index = VersionSetting_VersionList[n].Index;
                UnitSetting_VersionList[n].Text = VersionSetting_VersionList[n].Text;
            }
        }

        #endregion
    }
}
