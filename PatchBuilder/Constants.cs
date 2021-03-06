﻿namespace PatchBuilder.NET {
    public class Constants {
        public const string DIR_SEP = @"\";
        public const string DEFAULT_OUTPUT_DIRECTORY = @"C:\Temp\PatchBuilder.NET\";
        public const string WORKING_DIRECTORY = @"Working\";
        public const string LOG_DIRECTORY = @"Log\";
        public const string POLL_DIRECTORY = @"DeployMe\";
        public const string PATCH_FILES_DIRECTORY = @"patchfiles";
        public const string DOCUMENTS_DIRECTORY = @"Documents";
        public const string PROCESS_FILE = @"FilesToProcess.txt";
        public const string DEPLOY_FILE = @"FilesToDeploy.txt";
        public const string TARGETDIRS_SUFFIX = ".TargetDirectories.txt";
        public const string BATCH_FILE_NAME = "DeployPatch.bat";
        public const string PERIOD = ".";
        public const string DLL_EXTENSION = "dll";
        public const string PDB_EXTENSION = "pdb";
    }
}