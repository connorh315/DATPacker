using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using ModLib;
using DATPack.Packer;

namespace DATPacker
{
    internal static class BuildWorker
    {
        private static BackgroundWorker worker;

        public static List<BuildTask> tasksToComplete = new List<BuildTask>();

        public static bool WorkIsComplete = false;

        private static Dictionary<BackgroundWorker, BuildTask> currentlyCompleting = new();

        private static void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (currentlyCompleting.ContainsKey(worker))
            {
                CustomProgressBar progressBar = currentlyCompleting[worker].ProgressBar;
                Button button = currentlyCompleting[worker].Button;
                progressBar.Value = e.ProgressPercentage;
                if (e.ProgressPercentage == 100)
                {
                    progressBar.CustomText = "Done.";
                    button.Enabled = true;
                }
                else
                {
                    progressBar.CustomText = "Building...";
                }
                progressBar.Invalidate();
            }
        }

        private static void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.Log("All archives successfully built.");
            MessageBox.Show("All archives successfully built!", "Done");
        }

        private static void FindWork(BackgroundWorker thisWorker)
        {
            BuildTask task;
            lock (tasksToComplete)
            {
                if (tasksToComplete.Count == 0) return;

                task = tasksToComplete[0];
                currentlyCompleting[thisWorker] = task;
                tasksToComplete.RemoveAt(0);
            }
            Logger.Log(new LogSeg("Packing archive: "), new LogSeg(task.Archive, ConsoleColor.Blue));
            Pack.PackFromCollection(task.Project.OutputLocation, task.Project.Files[task.Archive].ToArray(), ArchiveType.LegoDimensions, Path.Combine(task.Project.BuildLocation, task.Archive), thisWorker);
            
            FindWork(thisWorker);
        }

        private static void BuildWork(object sender, DoWorkEventArgs e)
        {
            FindWork((BackgroundWorker)sender);
        }

        public static void Setup()
        {
            if (worker != null) return;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true; // Might come in and do this down the line
            worker.DoWork += new DoWorkEventHandler(BuildWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkComplete);
            worker.ProgressChanged += ReportProgress;
        }

        public static void AddTask(BuildTask task)
        {
            tasksToComplete.Add(task);
        }

        public static void Start()
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }
    }

    internal class BuildTask
    {
        public DPProject Project;
        public string Archive;

        internal BuildTask(DPProject project, string archive)
        {
            this.Project = project;
            this.Archive = archive;
        }

        public CustomProgressBar ProgressBar;
        public Button Button;
    }
}
