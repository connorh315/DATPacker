using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using ModLib;

using DATExtract;

namespace DATPacker
{
    internal class ExtractWorker
    {
        private static BackgroundWorker worker;

        public static List<DATFile> primaryTasksToComplete = new List<DATFile>();
        public static List<DATFile> secondaryTasksToComplete = new List<DATFile>();

        public static string extractLocation;

        public static bool WorkIsComplete = false;

        private static void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage);
        }

        private static void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.Log("All archives successfully extracted.");
        }

        private static void FindWork(BackgroundWorker thisWorker)
        {
            DATFile task;
            lock (primaryTasksToComplete)
            {
                if (primaryTasksToComplete.Count == 0) return;

                task = primaryTasksToComplete[0];
                primaryTasksToComplete.RemoveAt(0);
            }
            task.ExtractAll(extractLocation, thisWorker);
            FindWork(thisWorker);
        }

        private static void FindSecondaryWork(BackgroundWorker thisWorker)
        {
            DATFile task;
            lock (secondaryTasksToComplete)
            {
                if (secondaryTasksToComplete.Count == 0) return;

                task = secondaryTasksToComplete[0];
                secondaryTasksToComplete.RemoveAt(0);
            }
            task.ExtractAll(extractLocation, thisWorker);
            FindSecondaryWork(thisWorker);
        }

        private static void ExtractWork(object sender, DoWorkEventArgs e)
        {
            FindWork((BackgroundWorker)sender);
            FindSecondaryWork((BackgroundWorker)sender);
        }

        public void Setup()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true; // Might come in and do this down the line
            worker.DoWork += new DoWorkEventHandler(ExtractWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkComplete);
        }

        public static void AddPrimaryTask(DATFile file)
        {
            primaryTasksToComplete.Add(file);
        }

        public static void AddSecondaryTask(DATFile file)
        {
            secondaryTasksToComplete.Add(file);
        }

        public void Start()
        {
            worker.RunWorkerAsync();
        }
    }
}
