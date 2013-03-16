# PatchBuilder.NET 

A patch release package builder for .NET

Have a need, build a tool.

## USE CASE ##
Your solution is already deployed to your client production environment (on windows) and they have a bug that requires a fix.
You (or a manager) decides that the approach will be to apply a patch release rather than a full release to the client environment to address the issue(s).

If it is a large solution your fixed deployables may need to be deployed to multiple locations (directories) on the target environment (be it production, UAT or otherwise).

This tool does all the laborious work for you by working out where your patch files will need to be copied to.

## HOW TO USE CONSOLE APPLICATION ##
1. First you copy this tool's executable to an environment that matches the target environment (e.g. staging or production support environment)
2. In the same directory that you place this tool's executable, place a file called FilesToProcess.txt that lists the files that consist the patch / fix.  This input file simply lists the names (including extensions), of the deployables that your patch consists of.
(Current behaviour is inclusion of the matching pdb file for every dll.)

The first argument is the root directory of the solution deployed to an environment that matches the target environment (say Test or Staging).

Output is placed in C:\Temp\PatchBuilder.NET\<value of second argument>

For further help run the tool with no arguments or with the single argument 'help'.


## HOW DOES IT WORK? ##
During phase 1, the discovery phase, the tool loops through all the subdirectories of the root folder searching for the files listed in FilesToProcess.txt.  
During phase 2, the emission phase, the locations of these files are written to text files (one per entry in FilesToProcess.txt) and a batch file. The batch file uses the text files written by this tool to perform the copy operation on the target environment.

## AFTER YOU HAVE RUN THE TOOL, WHAT NOW? ##
Navigate to the C:\Temp\PatchBuilder.NET\<value of second argument> directory and you will find a collection of text files, a DOS batch file and two subdirectories:
### patchfiles subdirectory ###
Copy the fixed versions of your deployables (that you have compiled in your IDE) into this directory
### Documents subdirectory ###
Place a deployment guide and release notes document in this folder for your client.
The deployment guide will tell them to run batch file in their target environment.
