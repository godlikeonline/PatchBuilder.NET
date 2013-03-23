using System.Collections.Generic;

namespace PatchDeployer {
    public class DeploymentSet {
        public List<string> AllFiles { get; set; }
        public List<string> DllFiles { get; set; }
        public List<string> PdbFiles { get; set; }
        private const string PERIOD = ".";
        private const string DLL_EXTENSION = "dll";
        private const string PDB_EXTENSION = "pdb";
        
        public DeploymentSet(string[] allFiles) {
            AllFiles = new List<string>(allFiles);
            DllFiles = FilterList(DLL_EXTENSION);
            PdbFiles = FilterList(PDB_EXTENSION);
        }
        
        public bool IsValid() {
            if(DllFiles.Count == 0) {
                return true;
            } else {
                //need to find matching pdbs for each dll
                foreach(string dllFile in DllFiles) {
                    if(!FoundMatchingPDB(dllFile)) {
                        return false;
                    }
                }
                return true;
            }
        }

        private List<string> FilterList(string extension) {
            List<string> newList = new List<string>();
            foreach(string file in AllFiles) {
                if(file.Contains(extension)) {
                    newList.Add(file);
                }
            }
            return newList;
        }

        private bool FoundMatchingPDB(string dllFile) {
            string dllFilename = RemoveExtension(dllFile);
            foreach(string pdbFile in PdbFiles) {
                string pdbFilename = RemoveExtension(pdbFile);
                if(pdbFilename.Equals(dllFilename)) {
                    return true;
                }
            }
            return false;
        }

        private string RemoveExtension(string fileName){
            return fileName.Substring(0, fileName.IndexOf(PERIOD));
        }
    }
}