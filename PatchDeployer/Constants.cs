namespace PatchDeployer {
    public class Constants {
        public const string DIR_SEP = @"\";
        public const string DEFAULT_OUTPUT_DIRECTORY = @"C:\Temp\PatchBuilder.NET\";
        public const string WORKING_DIRECTORY = DEFAULT_OUTPUT_DIRECTORY + @"Working\";
        public const string LOG_DIRECTORY = DEFAULT_OUTPUT_DIRECTORY + @"Log\";
        public const string POLL_DIRECTORY = DEFAULT_OUTPUT_DIRECTORY + @"DeployMe\";
        public const string INPROCESS_DIRECTORY = DEFAULT_OUTPUT_DIRECTORY + @"Processing\";
        public const string PATCH_FILES_DIRECTORY = WORKING_DIRECTORY + @"patchfiles";
        public const string PROCESS_FILE = @"FilesToProcess.txt";
        public const string BATCH_FILE_NAME = WORKING_DIRECTORY + "DeployPatch.bat";
        public const string PERIOD = ".";
        public const string DLL_EXTENSION = "dll";
        public const string PDB_EXTENSION = "pdb";
    }
}
