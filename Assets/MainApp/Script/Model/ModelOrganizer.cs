using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utility;
using System.Threading.Tasks;
using Iso.Model;

public class ModelOrganizer
{

    private System.Action ReadyCallback;
    private static string streamingAssetsPath;
    private static string persistentDataPath;


    private DialogueModel _dialogueModel;
    public DialogueModel dialogueModel => _dialogueModel;

    private WorldModel _worldModel;
    public WorldModel worldModel => _worldModel;

    public async void Init(System.Action ReadyCallback) {
        this.ReadyCallback = ReadyCallback;

        streamingAssetsPath = Application.streamingAssetsPath;
        persistentDataPath = Application.persistentDataPath;

        await Task.Run(() =>
        {
            CopyStreamingsToApplication();
            SetupModels();
        });

        if (this.ReadyCallback != null)
            this.ReadyCallback();
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
            if (File.Exists(destFilePath)) {
                File.Delete(destFilePath);                
            }

            File.Copy(copyFilePaths[i], destFilePath);

            Debug.Log(string.Format("Filepath {0} {1}", fileInfo.name, fileInfo.format));
        }
    }

    private void SetupModels() {
        _worldModel = new WorldModel();

        _dialogueModel = new DialogueModel(_worldModel);
        _dialogueModel.ParseDialogueFromCSVPathList(ParameterFlag.GoogleSheetPath.Dialogue(persistentDataPath));


    }
}
