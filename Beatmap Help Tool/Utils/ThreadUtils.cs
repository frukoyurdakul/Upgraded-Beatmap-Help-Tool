using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public static class ThreadUtils
    {
        private static readonly Thread workerThread = new Thread(Loop);
        private static bool shouldLoop = true;
        private static readonly object mLock = new object();
        private static readonly Queue<Action> runnableQueue = new Queue<Action>();
        
        static ThreadUtils()
        {
            workerThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            workerThread.Start();
        }

        static void Loop()
        {
            while (shouldLoop)
            {
                lock(mLock)
                {
                    while (runnableQueue.Count > 0)
                    {
                        runnableQueue.Dequeue().Invoke();
                    }
                    Thread.Sleep(100);
                }
            }
        }

        public static void ExitLooperThread()
        {
            shouldLoop = false;
        }

        public static void ExecuteOnBackground(Action method)
        {
            lock(mLock)
            {
                runnableQueue.Enqueue(method);
            }
        }
    }
}
