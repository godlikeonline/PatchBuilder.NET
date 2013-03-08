using System;
using System.Collections.Generic;
using System.Text;

namespace PatchBuilder.NET {
    public class TargetDirectoriesItemCollection {
        private static readonly List<TargetDirectoriesItem> Collection = new List<TargetDirectoriesItem>();
        private static List<string> Names = new List<string>();
        
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
            List<string> a = new List<string>();
            foreach(TargetDirectoriesItem item in Collection) {
                a.Add(item.Name);
            }
            return a.ToArray();
        }

        public void Append(string name, string stringToAppend) {
            TargetDirectoriesItem itemToAppendTo = GetItem(name);
            itemToAppendTo.StringBuilder.Append(stringToAppend + Environment.NewLine);
        }

        public void WriteToDisk(string outDirectory) {
            FileWriter.CleanDirectory(outDirectory);
            foreach(TargetDirectoriesItem item in Collection) {
                string content = item.StringBuilder.ToString();
                FileWriter.Write(outDirectory, item.Name + Constants.TARGETDIRS_SUFFIX, content);
                if (item.Name.Contains("dll")) {
                    int lastDot = item.Name.LastIndexOf('.');
                    string pdbFilename = item.Name.Substring(0, lastDot) + ".pdb";
                    FileWriter.Write(outDirectory, pdbFilename + Constants.TARGETDIRS_SUFFIX, content);
                }
            }
        }
        
        private TargetDirectoriesItem GetItem(string name) {
            foreach(TargetDirectoriesItem item in Collection) {
                if (item.Name.Equals(name)) {
                    return item;
                }
            }
            return null;
        }
    }
}