namespace PatchBuilder.NET
{
    public class Constants {
        public const string DIR_SEP = @"\";
        public const string DEFAULT_OUTPUT_DIRECTORY = @"C:\Temp\PatchBuilder.NET\";
        public const string PATCH_FILES_DIRECTORY = @"patchfiles";
        public const string DOCUMENTS_DIRECTORY = @"Documents";
        public const string PROCESS_FILE = @"FilesToProcess.txt";
        public const string DEPLOY_FILE = @"FilesToDeploy.txt";
        public const string TARGETDIRS_SUFFIX = ".TargetDirectories.txt";
        public const string BATCH_FILE_NAME = "DeployPatch.bat";
    }
}