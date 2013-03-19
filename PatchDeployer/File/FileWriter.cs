using System.IO;
using System.Text;

namespace PatchDeployer {
    public class FileWriter {
        private static readonly StringBuilder LogFile = new StringBuilder();
        private static readonly StringBuilder FilesToProcessFile = new StringBuilder();

        public static void WriteFilesToProcess(string[] files) {
            int i;
            for(i = 0; i < files.Length - 1; i++) {
                if (!files[i].Contains("pdb")) {
                    FilesToProcessFile.AppendLine(files[i]);
                }
            }
            FilesToProcessFile.Append(files[i]);
            WriteFile(Constants.DEFAULT_OUTPUT_DIRECTORY + Constants.PROCESS_FILE, FilesToProcessFile.ToString());
        }

        private static void WriteFile(string filename, string content) {
            File.WriteAllText(filename, content);
        }
    }
}