using System;
using System.Collections.Generic;
using System.Linq;

namespace PatchDeployer {
    public class DeploymentSet {
        public List<string> AllFiles { get; set; }
        public List<string> DllFiles { get; set; }
        public List<string> PdbFiles { get; set; }
        
        public DeploymentSet(IEnumerable<string> allFiles) {
            AllFiles = new List<string>(allFiles);
            DllFiles = FilterList(Constants.DLL_EXTENSION);
            PdbFiles = FilterList(Constants.PDB_EXTENSION);
        }
        
        public bool IsValid() {
            return DllFiles.Count == 0 || DllFiles.All(FoundMatchingPDB);
            //need to find matching pdbs for each dll

            //foreach (string dllFile in DllFiles) {
            //    if (!FoundMatchingPDB(dllFile)) {
            //        return false;
            //    }
            //}
            //return true;
        }

        private List<string> FilterList(string extension) {
            return AllFiles.Where(file => file.Contains(extension)).ToList();

            //var newList = new List<string>();
            //foreach (string file in AllFiles) {
            //    if (file.Contains(extension)) {
            //        newList.Add(file);
            //    }
            //}
            //return newList;
        }

        private bool FoundMatchingPDB(string dllFile) {
            var dllFilename = RemoveExtension(dllFile);
            return PdbFiles.Select(RemoveExtension).Any(pdbFilename => pdbFilename.Equals(dllFilename));

            //foreach (string pdbFile in PdbFiles) {
            //    var pdbFilename = RemoveExtension(pdbFile);
            //    if (pdbFilename.Equals(dllFilename)) {
            //        return true;
            //    }
            //}
            //return false;
        }

        private string RemoveExtension(string fileName){
            return fileName.Substring(0, fileName.IndexOf(Constants.PERIOD, StringComparison.Ordinal));
        }
    }
}