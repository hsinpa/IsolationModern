using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utility {
    public class FileUtility
    {

        public struct FileInfo {
            public string name;
            public string format;
        }

        public static FileInfo GetFileInfo(string fullpath) {
            try {
                FileInfo fileInfo = new FileInfo();
                string filename = Path.GetFileName(fullpath);
                string format = filename.Substring(filename.LastIndexOf(".") + 1);

                fileInfo.name = filename;
                fileInfo.format = format;

                return fileInfo;          
            }
            catch {
                Debug.LogError("GetFileName fail file path");
            }

            return default(FileInfo);
        }


        public static string GetFileText(string filePath) {

            if (!File.Exists(filePath)) return null;

            return File.ReadAllText(filePath);
        }

    }
}