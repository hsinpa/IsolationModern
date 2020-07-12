using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ParameterFlag
{
    public class Path {
        public const string SheetFolder = "StreamingAssets/Database";
    }

    public class GoogleSheetNmae {
        public const string Character = "Character";
        public const string Chapter1Log = "Chapter 1 - log";
        public const string Chapter1Ques = "Chapter 1 - question";
        public const string Parameter = "Parameter";
    }

    public class GoogleSheetPath {
        public static string[] Dialogue(string rootPath) {
            return new string[] {
                System.IO.Path.Combine(rootPath, "Database", GoogleSheetNmae.Chapter1Log+".csv")
            };       
        }
    }

    public class ViewUIPath {
        public const string DialogueBox = "ui/UniversalCanvas/DialogueBox";
    }
}