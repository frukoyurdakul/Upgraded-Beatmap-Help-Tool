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
        private static readonly Thread workerThread = new Thread(loop);
        private static bool shouldLoop = true;
        private static readonly object mLock = new object();
        private static readonly Queue<Runnable> runnableQueue = new Queue<Runnable>();

        static ThreadUtils()
        {
            workerThread.Start();
        }

        static void loop()
        {
            while (shouldLoop)
            {
                lock(mLock)
                {
                    while (runnableQueue.Count > 0)
                    {
                        runnableQueue.Dequeue().run();
                    }
                }
            }
        }

        public static void exitLooperThread()
        {
            shouldLoop = false;
        }

        public static void executeOnBackground(Runnable runnable)
        {
            lock(mLock)
            {
                runnableQueue.Enqueue(runnable);
            }
        }
    }
}
