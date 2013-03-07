using System;
using System.IO;

namespace PatchBuilder.NET {
    public class Program {
        private static TargetDirectoriesItemCollection TargetDirectoryOutputFiles = new TargetDirectoriesItemCollection();
        private const string DIR_SEP = @"\";
        private const string PROCESS_FILE = @"FilesToProcess.txt";

        public static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Must enter 1 arguments.  Enter help as first parameter for assistance.");
                Environment.Exit(0);
            } else {
                if (args[0].ToLower().Equals("help")) {
                    Console.WriteLine("Enter the root directory of the solution to be processed.  Output files will be placed in C:\\Temp\\PatchReleaseHelper\n" +
                                      "For Example: PatchReleaseHelper C:\\MySolutionRoot");
                    Environment.Exit(0);
                } else {
                    if (!Directory.Exists(args[0])) {
                        Console.WriteLine("Solution directory specified does not exist. Exiting.");
                        Environment.Exit(0);
                    } else {
                        //Correct number of arguments - ready to attempt processing.
                        string[] filesToProcess = File.ReadAllLines(PROCESS_FILE);
                        WalkDirectoryTree(filesToProcess, args[0], String.Empty, new DirectoryInfo(args[0]));
                        TargetDirectoryOutputFiles.WriteToDisk();

                        //Adjust for PDB Files
                        FileWriter.BuildDeployFile(PROCESS_FILE);
                    }
                }
            }
        }

        private static void WalkDirectoryTree(string[] filesToProcess, string appRoot, string modifiedPath, DirectoryInfo root) {
            //Now find all the subdirectories under this directory.
            DirectoryInfo[] subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs) {
                //Recursive call for each subdirectory.
                WalkDirectoryTree(filesToProcess, appRoot, modifiedPath + dirInfo.Name + DIR_SEP, dirInfo);

                foreach (string fileToProcess in filesToProcess) {
                    TargetDirectoryOutputFiles.Add(fileToProcess);
                    string file = appRoot + DIR_SEP + modifiedPath + dirInfo.Name + DIR_SEP + fileToProcess;
                    if (File.Exists(file)) {
                        TargetDirectoryOutputFiles.Append(fileToProcess, modifiedPath + dirInfo.Name + DIR_SEP);
                    }
                }
            }
        }
    }
}