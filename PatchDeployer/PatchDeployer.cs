using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace PatchDeployer {
    public partial class PatchDeployer : ServiceBase {
        public PatchDeployer() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            fsWatcher.Path = Constants.POLL_DIRECTORY;
            //Add event handlers.
            fsWatcher.Created += OnChanged;
            //Begin watching.
            fsWatcher.EnableRaisingEvents = true;
        }

        protected override void OnStop() {
        }

        //Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e) {
            //Specify what is done when a file is changed, created, or deleted.
            //Move files from POLL to PROCESSING directory
            FileMover.MoveFiles(Constants.POLL_DIRECTORY, Constants.INPROCESS_DIRECTORY);

            //Create FilesToProcess.txt
            FileWriter.WriteFilesToProcess(Directory.GetFiles(Constants.POLL_DIRECTORY));

            //Run PatchBuilder.NET to create package
            RunPatchBuilder();

            //Move files from PROCESSING directory to patchfile
            FileMover.MoveFiles(Constants.INPROCESS_DIRECTORY, Constants.PATCH_FILES_DIRECTORY);

            //Execute DeployPatch.bat
            if (File.Exists(Constants.BATCH_FILE_NAME)) {
                RunBatchFile();
            }
        }

        private static void RunPatchBuilder(string rootDir, string packageName) {
            Process cmdProcess = CreateCmdProcess();

            cmdProcess.Start();
            cmdProcess.StandardInput.WriteLine("PatchBuilder.NET " + rootDir + " " + packageName);
            ExitProcess(cmdProcess);
        }

        private static void RunBatchFile(string rootDir) {
            Process cmdProcess = CreateCmdProcess();

            cmdProcess.Start();
            cmdProcess.StandardInput.WriteLine("DeployPatch.bat " + rootDir);
            ExitProcess(cmdProcess);
        }

        private static Process CreateCmdProcess() {
            Process cmdProcess = new Process();

            cmdProcess.StartInfo = new ProcessStartInfo(@"c:\windows\system32\cmd.exe");
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.StartInfo.ErrorDialog = false;
            cmdProcess.StartInfo.RedirectStandardError = true;
            cmdProcess.StartInfo.RedirectStandardInput = true;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            return cmdProcess;
        }

        private static void ExitProcess(Process process) {
            if (!process.HasExited) {
                process.WaitForExit(120000); //give 2 minutes for process to finish
                if (!process.HasExited) {
                    process.Kill(); //took too long, kill it off
                }
            }
        }
    }
}