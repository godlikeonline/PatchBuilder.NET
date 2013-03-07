using System;
using System.IO;
using System.Text;

namespace PatchBuilder.NET {
    public class FileWriter {
        private const string OUTPUT_DIRECTORY = @"C:\Temp\PatchReleaseHelper\";
        private const string DEPLOY_FILE = @"FilesToDeploy.txt";
        private static StringBuilder _FilesToDeploy = new StringBuilder();

        public static void CleanDirectory() {
            if (!Directory.Exists(OUTPUT_DIRECTORY)) {
                Directory.CreateDirectory(OUTPUT_DIRECTORY);
            } else {
                foreach (string filePath in Directory.GetFiles(OUTPUT_DIRECTORY)) {
                    File.Delete(filePath);
                }
            }
        }

        public static void Write(string filename, string content) {
            File.WriteAllText(OUTPUT_DIRECTORY + filename, content);
        }

        public static void BuildDeployFile(string processFile) {
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
            File.WriteAllText(OUTPUT_DIRECTORY + DEPLOY_FILE, _FilesToDeploy.ToString());
        }
    }
}