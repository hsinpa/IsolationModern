using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Organize gameobjects in the scene.
/// </summary>
public class GoogleSheetDownloader : Object
{
    /// <summary>
    /// Main app instance.
    /// </summary>
	static void UnityDownloadGoogleSheet(Dictionary<string, string> url_clone)
    {
        string path = Path.Combine(Application.dataPath, ParameterFlag.Path.SheetFolder);

        if (url_clone.Count > 0)
        {
            KeyValuePair<string, string> firstItem = url_clone.First();

            WebRequest myRequest = WebRequest.Create(firstItem.Value);

            //store the response in myResponse 
            WebResponse myResponse = myRequest.GetResponse();

            //register I/O stream associated with myResponse
            Stream myStream = myResponse.GetResponseStream();

            //create StreamReader that reads characters one at a time
            StreamReader myReader = new StreamReader(myStream);

            string s = myReader.ReadToEnd();
            myReader.Close();//Close the reader and underlying stream

            File.WriteAllText(path + "/" + firstItem.Key + ".csv", s);
            url_clone.Remove(firstItem.Key);
            UnityDownloadGoogleSheet(url_clone);
            Debug.Log(firstItem.Key);

        }
        else
        {
            Debug.Log("Done");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
    
    [MenuItem("Assets/Hsinpa/Database/Reset", false, 0)]
    static public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Caching.ClearCache();
    }

    [MenuItem("Assets/Hsinpa/Database/DownloadGoogleSheet", false, 0)]
    static public void OnDatabaseDownload()
    {

        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQQdvM9dOlIaeuoUOOQOgcAloDlWmHDX3Rx63FFzG2PNFk46wIBn2e0XnFC8adWlEpFw9FSc64DdJY9/pub?gid=:id&single=true&output=csv";
        UnityDownloadGoogleSheet(new Dictionary<string, string> {
            { ParameterFlag.GoogleSheetNmae.Character, Regex.Replace( url, ":id", "2123416800")},
            { ParameterFlag.GoogleSheetNmae.Chapter1Log, Regex.Replace( url, ":id", "0")},
            { ParameterFlag.GoogleSheetNmae.Chapter1Ques, Regex.Replace( url, ":id", "1797740515")},
            { ParameterFlag.GoogleSheetNmae.Parameter, Regex.Replace( url, ":id", "1739801868")},
        });
    }

}
