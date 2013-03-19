using System.IO;
namespace PatchDeployer {
    public class FileMover {
        public static int MoveFiles(string from, string to) {
            DirectoryInfo fromDir = new DirectoryInfo(from);
            DirectoryInfo toDir = new DirectoryInfo(to);

            FileInfo[] fromFiles = fromDir.GetFiles("*.*");
            if (fromFiles.Length > 0) {
                foreach (FileInfo file in fromFiles) {
                    string targetFile = toDir + file.Name;
                    if (File.Exists(targetFile)) {
                        File.Delete(targetFile);
                    }
                    file.MoveTo(targetFile);
                }
                return fromFiles.Length;
            }
            return 0;
        }
    }
}