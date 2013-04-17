using System.Collections.Generic;
using System.Text;

namespace PatchDeployer.File {
    public class FileWriter {
        private static readonly StringBuilder LogFile = new StringBuilder();
        private static readonly StringBuilder FilesToProcessFile = new StringBuilder();

        public static void WriteFilesToProcess(List<string> files) {
            int i = 0;
            for(; i < files.Count; i++) {
                if(!files[i].Contains("pdb")) {
                    if(i != files.Count - 1) {
                        FilesToProcessFile.AppendLine(files[i]);
                    } else {
                        FilesToProcessFile.Append(files[i]);
                    }
                }
            }
            WriteFile(Constants.DEFAULT_OUTPUT_DIRECTORY + Constants.PROCESS_FILE, FilesToProcessFile.ToString());
        }

        private static void WriteFile(string filename, string content) {
            System.IO.File.WriteAllText(filename, content);
        }
    }
}