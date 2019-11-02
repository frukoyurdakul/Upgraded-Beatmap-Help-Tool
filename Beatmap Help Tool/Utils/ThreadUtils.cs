using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

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
            workerThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
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

        public static void exitLooperThread()
        {
            shouldLoop = false;
        }

        public static void executeOnBackground(Action method)
        {
            lock(mLock)
            {
                runnableQueue.Enqueue(method);
            }
        }
    }
}
