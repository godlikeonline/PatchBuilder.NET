using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using Logger;
using PatchDeployer.File;

namespace PatchDeployer {
    public partial class PatchDeployer : ServiceBase {
        private const string SERVICE_NAME = "PatchDeployer";
        
        public PatchDeployer() {
            InitializeComponent();
            ServiceName = SERVICE_NAME;
        }

        protected override void OnStart(string[] args) {
            fsWatcher.Path = Constants.POLL_DIRECTORY;
            //Add event handlers
            fsWatcher.Created += OnChanged;
            //Begin watching
            fsWatcher.EnableRaisingEvents = true;
        }

        protected override void OnStop() {
        }

        //Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e) {
            var logger = new PatchLogger(SERVICE_NAME);
            var deploymentSet = new DeploymentSet(Directory.GetFiles(Constants.POLL_DIRECTORY));
            
            if(deploymentSet.IsValid()) {
                //Move files from POLL to PROCESSING directory
                FileMover.MoveFiles(Constants.POLL_DIRECTORY, Constants.INPROCESS_DIRECTORY);

                //Create FilesToProcess.txt
                FileWriter.WriteFilesToProcess(deploymentSet.AllFiles);

                //Run PatchBuilder.NET to create package
                RunPatchBuilder(@"C:\PRODUCTION\", "ServiceDeployment");

                //Move files from PROCESSING directory to patchfile
                FileMover.MoveFiles(Constants.INPROCESS_DIRECTORY, Constants.PATCH_FILES_DIRECTORY);

                //Execute DeployPatch.bat
                if(System.IO.File.Exists(Constants.BATCH_FILE_NAME)) {
                    RunBatchFile(@"C:\PRODUCTION\");
                }
            } else {
                logger.EventLog.WriteEntry("Deployment set is invalid. Nothing deployed.", EventLogEntryType.Error);
            }
        }

        private static void RunPatchBuilder(string rootDir, string packageName) {
            var cmdProcess = CreateCmdProcess();
            cmdProcess.Start();
            cmdProcess.StandardInput.WriteLine("PatchBuilder.NET " + rootDir + " " + packageName);
            ExitProcess(cmdProcess);
        }

        private static void RunBatchFile(string rootDir) {
            var cmdProcess = CreateCmdProcess();
            cmdProcess.Start();
            cmdProcess.StandardInput.WriteLine("DeployPatch.bat " + rootDir);
            ExitProcess(cmdProcess);
        }

        private static Process CreateCmdProcess() {
            return new Process {
                                    StartInfo = new ProcessStartInfo(@"c:\windows\system32\cmd.exe") {
                                                CreateNoWindow = true,
                                                ErrorDialog = false,
                                                RedirectStandardError = true,
                                                RedirectStandardInput = true,
                                                RedirectStandardOutput = true,
                                                UseShellExecute = false,
                                                WindowStyle = ProcessWindowStyle.Hidden
                                        }
                                };
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