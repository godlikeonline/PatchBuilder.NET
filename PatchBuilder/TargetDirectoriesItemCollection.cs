using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatchBuilder.NET {
    public class TargetDirectoriesItemCollection {
        private static readonly List<TargetDirectoriesItem> Collection = new List<TargetDirectoriesItem>();
        private static readonly List<string> Names = new List<string>();
        
        public void Add(string name) {
            if(!HasItemWithName(name)) {
                Collection.Add(new TargetDirectoriesItem {Name = name, StringBuilder = new StringBuilder()});
                Names.Add(name);
            }
        }

        public bool HasItemWithName(string name) {
            return Names.Contains(name);
        }

        public string[] GetNames() {
            return Collection.Select(item => item.Name).ToArray();
        }

        public void Append(string name, string stringToAppend) {
            var itemToAppendTo = GetItem(name);
            itemToAppendTo.StringBuilder.Append(stringToAppend + Environment.NewLine);
        }

        public void WriteToDisk(string outDirectory) {
            FileWriter.CleanDirectory(outDirectory);
            foreach(var item in Collection) {
                var content = item.StringBuilder.ToString();
                FileWriter.Write(outDirectory, item.Name + Constants.TARGETDIRS_SUFFIX, content);
                if(item.Name.Contains(Constants.DLL_EXTENSION)) {
                    var lastDot = item.Name.LastIndexOf('.');
                    var pdbFilename = item.Name.Substring(0, lastDot) + Constants.PERIOD + Constants.PDB_EXTENSION;
                    FileWriter.Write(outDirectory, pdbFilename + Constants.TARGETDIRS_SUFFIX, content);
                }
            }
        }
        
        private TargetDirectoriesItem GetItem(string name) {
            return Collection.FirstOrDefault(item => item.Name.Equals(name));
        }
    }
}