# PatchBuilder.NET 

A patch release package builder for .NET

Have a need, build a tool.

## USE CASE ##
Solution is deployed to client production environment (on windows) and they have a bug that requires a fix.
Fix involves a patch release rather than a full release.

If it is a large solution your fixed deployables may need to be deployed to multiple locations on the target environment (be it production, UAT or otherwise).

This tool does all the laborious work for you.

## HOW TO USE CONSOLE APPLICATION ##
This tool must be run on an environment that matches the target environment. 
The first argument is the root directory of the compiled solution on an environment that matches the target environment (say Test or Staging).

Place a file called FilesToProcess.txt in the same directory as the EXE.  This input file simply lists the names, including extensions, of the deployables that your patch consists of.
Current behaviour is inclusion of the matching pdb file for every dll.

Output is placed in C:\Temp\PatchBuilder.NET\<value of second argument>

For further help run the tool with no arguments or with the single argument 'help'.

If you navigate to the C:\Temp\PatchBuilder.NET\<value of second argument> directory you will find a collection of text files, a DOS batch file and two subdirectories.  
### patchfiles subdirectory ###
Copy the fixed versions of your deployables in this directory
### Documents subdirectory ###
Place deployment guide and release note documents in this folder for your client.
