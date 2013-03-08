using System;
using System.IO;
using System.Text;

namespace PatchBuilder.NET {
    public class FileWriter {
        
        private static StringBuilder _FilesToDeploy = new StringBuilder();

        public static void CleanDirectory(string packageDirectory) {
            string finalOutputDirectory = Constants.DEFAULT_OUTPUT_DIRECTORY + packageDirectory;
            EnsureDirectoryExists(Constants.DEFAULT_OUTPUT_DIRECTORY);
            EnsureDirectoryExists(finalOutputDirectory);
            foreach (string filePath in Directory.GetFiles(finalOutputDirectory)) {
                File.Delete(filePath);
            }
        }

        public static void Write(string outDirectory, string filename, string content) {
            File.WriteAllText(Constants.DEFAULT_OUTPUT_DIRECTORY + EnsureTrailingSlash(outDirectory) + filename, content);
        }

        public static void AddPackageDirectories(string packageDirectory) {
            string rootDirectory = Constants.DEFAULT_OUTPUT_DIRECTORY + EnsureTrailingSlash(packageDirectory);
            if (!Directory.Exists(rootDirectory + Constants.PATCH_FILES_DIRECTORY)) {
                Directory.CreateDirectory(rootDirectory + Constants.PATCH_FILES_DIRECTORY);
            }
            if (!Directory.Exists(rootDirectory + Constants.DOCUMENTS_DIRECTORY)) {
                Directory.CreateDirectory(rootDirectory + Constants.DOCUMENTS_DIRECTORY);
            }
        }

        private static void EnsureDirectoryExists(string directory) {
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
        }

        private static string EnsureTrailingSlash(string s) {
            return s.TrimEnd('\\') + @"\";
        }

        public static void BuildDeployFile(string packageDirectory, string processFile) {
            string[] files = File.ReadAllLines(processFile);
            foreach (string file in files) {
                _FilesToDeploy.Append(file + Environment.NewLine);
                if (file.Contains("dll")) {
                    //Also deploy the pdb
                    int lastDot = file.LastIndexOf('.');
                    string pdbFilename = file.Substring(0, lastDot) + ".pdb";
                    _FilesToDeploy.Append(pdbFilename + Environment.NewLine);
                }
            }
            string deployFile = Constants.DEFAULT_OUTPUT_DIRECTORY + EnsureTrailingSlash(packageDirectory) + Constants.DEPLOY_FILE;
            File.WriteAllText(deployFile, _FilesToDeploy.ToString());
        }
    }
}