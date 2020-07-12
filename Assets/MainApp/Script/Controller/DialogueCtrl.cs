using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Iso.Model;
using Isolation.View;

public class DialogueCtrl : Observer
{

    private delegate void ProcessDialogueComp(DialogueComp dialogueComp);

    ModelOrganizer modelOrganizer;

    DialogueModel dialogueModel;
    DialogueProcessor dialogueProcessor;

    private string scene_id;

    private Dictionary<string, ProcessDialogueComp> dialogueTypeTable;

    private DialogueComp currentDialogueComp;

    public void SetUp(ModelOrganizer modelOrganizer)
    {
        this.modelOrganizer = modelOrganizer;
        this.dialogueModel = modelOrganizer.dialogueModel;
        this.dialogueProcessor = modelOrganizer.dialogueModel.dialogueProcessor;

        this.dialogueTypeTable = new Dictionary<string, ProcessDialogueComp>();
        this.dialogueTypeTable.Add(EventFlag.DialogueType.TEXT, ProcessTextLog);
    }

    public void ExecuteSceneByID(string scene_id) {
        this.scene_id = scene_id;

        var dialogueList = modelOrganizer.dialogueModel.GetDialogueListBySceneID(scene_id);

        ExamineDialogue( dialogueProcessor.SetUpScenario(scene_id, dialogueList) );
    }

    private void ExamineNextLog(DialogueComp currentLog) {
        ExamineDialogue(dialogueProcessor.Process(currentLog));
    }

    private void ExamineDialogue(DialogueComp dialogueComp) {
        currentDialogueComp = dialogueComp;

        Debug.Log(string.Format("ID {0} type {1} vlaue {2}", currentDialogueComp._id, currentDialogueComp.type, currentDialogueComp.mainValue));

        if (dialogueTypeTable.TryGetValue(dialogueComp.type, out ProcessDialogueComp processor)) {
            processor(dialogueComp);

            return;
        }

        //Fallback method
        Debug.LogError("No dialogue processor is support " + dialogueComp.type);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ExamineNextLog(currentDialogueComp);
        }
    }

    #region Dialogue Processor
    private void ProcessTextLog(DialogueComp dialogueComp) {
        DialogueView dialogueView = MainAppManager.Instance.FindViewObject<DialogueView>(ParameterFlag.ViewUIPath.DialogueBox);
        dialogueView.Show(true);

        dialogueView.SetAvatar(null);
        dialogueView.SetMessage(dialogueComp.mainValue);
        dialogueView.SetTitle(null);
    }

    private void ProcessChoiceLog(DialogueComp dialogueComp)
    {

    }

    private void ProcessPhoneLog(DialogueComp dialogueComp)
    {

    }

    private void ProcessJumpLog(DialogueComp dialogueComp)
    {

    }

    private void ProcessImageLog(DialogueComp dialogueComp)
    {

    }

    private void ProcessExploreLog(DialogueComp dialogueComp)
    {

    }
    #endregion
}
