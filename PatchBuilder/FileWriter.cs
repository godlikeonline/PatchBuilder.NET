using System;
using System.IO;
using System.Text;

namespace PatchBuilder.NET {
    public class FileWriter {
        private static readonly StringBuilder FilesToDeploy = new StringBuilder();

        public static void CleanDirectory(string packageDirectory) {
            var finalOutputDirectory = Constants.DEFAULT_OUTPUT_DIRECTORY + packageDirectory;
            EnsureDirectoryExists(Constants.DEFAULT_OUTPUT_DIRECTORY);
            EnsureDirectoryExists(finalOutputDirectory);
            foreach(var filePath in Directory.GetFiles(finalOutputDirectory)) {
                File.Delete(filePath);
            }
        }

        public static void Write(string outDirectory, string filename, string content) {
            File.WriteAllText(Constants.DEFAULT_OUTPUT_DIRECTORY + EnsureTrailingSlash(outDirectory) + filename, content);
        }

        public static void AddPackageDirectories(string packageDirectory) {
            var rootDirectory = Constants.DEFAULT_OUTPUT_DIRECTORY + EnsureTrailingSlash(packageDirectory);
            if(!Directory.Exists(rootDirectory + Constants.PATCH_FILES_DIRECTORY)) {
                Directory.CreateDirectory(rootDirectory + Constants.PATCH_FILES_DIRECTORY);
            }
            if(!Directory.Exists(rootDirectory + Constants.DOCUMENTS_DIRECTORY)) {
                Directory.CreateDirectory(rootDirectory + Constants.DOCUMENTS_DIRECTORY);
            }
        }

        private static void EnsureDirectoryExists(string directory) {
            if(!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
        }

        private static string EnsureTrailingSlash(string s) {
            return s.TrimEnd('\\') + @"\";
        }

        public static void BuildDeployFile(string packageDirectory, string processFile) {
            var files = File.ReadAllLines(processFile);
            foreach(var file in files) {
                FilesToDeploy.Append(file + Environment.NewLine);
                if(file.Contains(Constants.DLL_EXTENSION)) {
                    //Also deploy the pdb
                    var lastDot = file.LastIndexOf('.');
                    var pdbFilename = file.Substring(0, lastDot) + Constants.PERIOD + Constants.PDB_EXTENSION;
                    FilesToDeploy.Append(pdbFilename + Environment.NewLine);
                }
            }
            var deployFile = Constants.DEFAULT_OUTPUT_DIRECTORY + EnsureTrailingSlash(packageDirectory) + Constants.DEPLOY_FILE;
            File.WriteAllText(deployFile, FilesToDeploy.ToString());
        }
    }
}