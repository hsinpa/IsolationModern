using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFlag
{
    public class InGameEvent {
        public const string Setup = "event@ingame_setup";
    }

    public class DialogueType {
        public const string TEXT = "TEXT";
        public const string PHONE = "PHONE";
        public const string SOUND = "SOUND";
        public const string CHOICE = "CHOICE";
        public const string JUMP = "JUMP";
        public const string IMAGE = "IMAGE";
        public const string EXPLORE = "EXPLORE";
    }
}
