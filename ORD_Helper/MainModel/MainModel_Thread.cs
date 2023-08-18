using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ORD_Helper
{
    partial class MainModel
    {
        public static class DispatcherService
        {
            public static void Invoke(Action action)
            {
                Dispatcher dispatchObject = Application.Current != null ? Application.Current.Dispatcher : null;
                if (dispatchObject == null || dispatchObject.CheckAccess())
                    action();
                else
                    dispatchObject.Invoke(action);
            }
        }

        public class DataThread
        {
            private List<Thread> thread = new List<Thread>();
            private List<ManualResetEvent> pauseEvent = new List<ManualResetEvent>();
            private bool endEvent = false;
            private int count = 0;
            private List<string> methodName = new List<string>();

            private void DoWork(int index, int sleepTime, Action action)
            {
                while (pauseEvent[index].WaitOne() && !endEvent)
                {
                    action();
                    Thread.Sleep(sleepTime);
                }
            }

            public void CreateThread(Action action, int sleepTime)
            {
                int index = count;
                pauseEvent.Add(new ManualResetEvent(false));
                methodName.Add(action.Method.Name);
                thread.Add(new Thread(new ThreadStart(() => DoWork(index, sleepTime, action))));
                thread[count].IsBackground = true;
                thread[count].Start();
                while (!thread[count].IsAlive) ;

                count++;
            }

            public void PauseThread(string methodName)
            {
                for (int n = 0; n < count; n++)
                {
                    if (this.methodName[n] == methodName)
                    {
                        pauseEvent[n].Reset();
                    }
                }
            }

            public void ResumeThread(string methodName)
            {
                for (int n = 0; n < count; n++)
                {
                    if (this.methodName[n] == methodName)
                    {
                        pauseEvent[n].Set();
                    }
                }
            }

            public void EndThread()
            {
                endEvent = true;
                for (int n = 0; n < count; n++)
                {
                    pauseEvent[n].Set();
                    thread[n].Join();
                }
            }
        }

        

        

        
    }
}
