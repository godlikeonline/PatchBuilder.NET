using System.IO;

namespace PatchDeployer.File {
    public class FileMover {
        public static int MoveFiles(string from, string to) {
            var fromDir = new DirectoryInfo(from);
            var toDir = new DirectoryInfo(to);

            var fromFiles = fromDir.GetFiles("*.*");
            if (fromFiles.Length > 0) {
                foreach (FileInfo file in fromFiles) {
                    string targetFile = toDir + file.Name;
                    if (System.IO.File.Exists(targetFile)) {
                        System.IO.File.Delete(targetFile);
                    }
                    file.MoveTo(targetFile);
                }
                return fromFiles.Length;
            }
            return 0;
        }
    }
}