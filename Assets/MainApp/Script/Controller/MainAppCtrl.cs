using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;

public class MainAppCtrl : Observer
{
    ModelOrganizer modelOrganizer;

    DialogueCtrl dialogueCtrl;

    public override void OnNotify(string p_event, params object[] p_objects)
    {
        base.OnNotify(p_event, p_objects);

        switch (p_event)
        {
            case EventFlag.InGameEvent.Setup:
                modelOrganizer = (ModelOrganizer)p_objects[0];

                SetUp();
                break;
        }
    }

    private void SetUp() {
        dialogueCtrl = GetComponent<DialogueCtrl>();
        dialogueCtrl.SetUp(modelOrganizer);

        TestScene1();
    }

    private void TestScene1() {
        string sceneID = "i-chapter-1-1";
        dialogueCtrl.ExecuteSceneByID(sceneID);
    }

}