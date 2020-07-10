using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utility;
using System.Threading.Tasks;

public class ModelOrganizer
{

    private System.Action ReadyCallback;

    private static string streamingAssetsPath;
    private static string persistentDataPath;

    public async void Init(System.Action ReadyCallback) {

        this.ReadyCallback = ReadyCallback;

        streamingAssetsPath = Application.streamingAssetsPath;
        persistentDataPath = Application.persistentDataPath;

        await Task.Run(() =>
        {
            CopyStreamingsToApplication();
        });


        if (ReadyCallback != null)
            ReadyCallback();
    }

    private void CopyStreamingsToApplication() {

        var copyDirectory = Path.Combine(streamingAssetsPath, "Database");
        var destDirectory = Path.Combine(persistentDataPath, "Database");

        if (!Directory.Exists(destDirectory))
            Directory.CreateDirectory(destDirectory);

        var copyFilePaths = Directory.GetFiles(copyDirectory);
        int copyLength = copyFilePaths.Length;

        for (int i = 0; i < copyLength; i++)
        {
            var fileInfo = FileUtility.GetFileInfo(copyFilePaths[i]);

            string destFilePath = Path.Combine(destDirectory, fileInfo.name);

            //Ignore meta file
            if (fileInfo.format == "meta") continue;

            //Check Dest replication
            if (File.Exists(destFilePath)) continue;

            File.Copy(copyFilePaths[i], destFilePath);

            Debug.Log(string.Format("Filepath {0} {1}", fileInfo.name, fileInfo.format));
        }
    }


}
