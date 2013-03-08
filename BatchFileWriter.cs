using System.Text;

namespace PatchBuilder.NET {
    public class BatchFileWriter {
        private static StringBuilder BatchFile = new StringBuilder();

        public static void Build(string outputDirectory) {
            BatchFile.AppendLine("@ECHO OFF");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine("IF \"%1\"==\"\" GOTO HELP");
            BatchFile.AppendLine("IF \"%1\"==\"/?\" GOTO HELP");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine("SET SOURCE_PATH=.\\" + Constants.PATCH_FILES_DIRECTORY);
            BatchFile.AppendLine("SET TARGET_ROOT=%1");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine("SETLOCAL EnableDelayedExpansion");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine("FOR /F %%A IN (" + Constants.DEPLOY_FILE + ") DO (");
            BatchFile.AppendLine("	FOR /F %%B IN (%%A" + Constants.TARGETDIRS_SUFFIX + ") DO (");
            BatchFile.AppendLine("		REM Set other config properties");
            BatchFile.AppendLine("		ECHO Copy files to !TARGET_ROOT!\\%%B");
            BatchFile.AppendLine("		XCOPY /e/v/y/k/I !SOURCE_PATH!\\%%A !TARGET_ROOT!\\%%B");
            BatchFile.AppendLine("	)");
            BatchFile.AppendLine(")");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine("GOTO FINISH");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine(":HELP");
            BatchFile.AppendLine("ECHO Usage: DeployPatch Target");
            BatchFile.AppendLine("ECHO Target - Location of website root Eg: C:\\PRODUCTION");
            BatchFile.AppendLine(string.Empty);
            BatchFile.AppendLine(":FINISH");
            BatchFile.Append("ECHO End batch");

            FileWriter.Write(outputDirectory, Constants.BATCH_FILE_NAME, BatchFile.ToString());
        }
    }
}