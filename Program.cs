using System;
using System.IO;

namespace PatchBuilder.NET {
    public class Program {
        private static readonly TargetDirectoriesItemCollection TargetDirectoryOutputFiles = new TargetDirectoriesItemCollection();
        
        public static void Main(string[] args) {
            if (args.Length == 0 || args[0].ToLower().Equals("help")) {
                PrintHelp();
            } else {
                if (args.Length != 2) {
                    Console.WriteLine("Must enter 2 arguments.  Enter help as first parameter for assistance.");
                    Environment.Exit(0);
                } else {
                    if (!Directory.Exists(args[0])) {
                        Console.WriteLine("Solution directory specified does not exist. Exiting.");
                        Environment.Exit(0);
                    } else {
                        //Correct number of arguments - ready to attempt processing.
                        string[] filesToProcess = File.ReadAllLines(Constants.PROCESS_FILE);
                        WalkDirectoryTree(filesToProcess, args[0], String.Empty, new DirectoryInfo(args[0]));
                        TargetDirectoryOutputFiles.WriteToDisk(args[1]);

                        FileWriter.BuildDeployFile(args[1], Constants.PROCESS_FILE); //Adjust for PDB Files
                        BatchFileWriter.Build(args[1]);
                        FileWriter.AddPackageDirectories(args[1]);
                    }
                }
            }
        }

        private static void PrintHelp() {
            Console.WriteLine("Enter the root directory of the solution to be processed and release package name (e.g. \"<client> Applications v1.34.2\")." + Environment.NewLine +
                              "For Example: PatchBuilder.NET C:\\MySolutionRoot \"Client Applications v1.34.2\"" + Environment.NewLine +
                              "Output will be placed in C:\\Temp\\PatchBuilder.NET\\<release package name>");
            Environment.Exit(0);
        }

        private static void WalkDirectoryTree(string[] filesToProcess, string appRoot, string modifiedPath, DirectoryInfo root) {
            //Now find all the subdirectories under this directory.
            DirectoryInfo[] subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs) {
                //Recursive call for each subdirectory.
                WalkDirectoryTree(filesToProcess, appRoot, modifiedPath + dirInfo.Name + Constants.DIR_SEP, dirInfo);

                foreach (string fileToProcess in filesToProcess) {
                    TargetDirectoryOutputFiles.Add(fileToProcess);
                    string file = appRoot + Constants.DIR_SEP + modifiedPath + dirInfo.Name + Constants.DIR_SEP + fileToProcess;
                    if (File.Exists(file)) {
                        TargetDirectoryOutputFiles.Append(fileToProcess, modifiedPath + dirInfo.Name + Constants.DIR_SEP);
                    }
                }
            }
        }
    }
}