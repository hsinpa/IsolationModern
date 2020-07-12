using Iso.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProcessor
{
    private DialogueModel dialogueModel;
    private WorldModel worldModel;

    private DialogueComp currentComp;

    private List<DialogueComp> dialogueCompList;

    private int dialogueCount;
    private string sceneID;

    public DialogueProcessor(WorldModel worldModel, DialogueModel dialogueModel) {
        this.worldModel = worldModel;
        this.dialogueModel = dialogueModel;
    }

    public DialogueComp SetUpScenario(string sceneID, List<DialogueComp> dialogueCompList) {
        this.sceneID = sceneID;
        this.dialogueCompList = dialogueCompList;
        this.dialogueCount = dialogueCompList.Count;

        if (dialogueCompList != null && this.dialogueCount > 0)
            return dialogueCompList[0];

        return default(DialogueComp);
    }

    //GroupID
    private DialogueComp ExamineDialogueGroup(List<DialogueComp> group) {
        foreach (DialogueComp dialogue in group) {

            Debug.Log("currentComp group " + dialogue._id);

            if (this.worldModel.CheckConstraint(dialogue.constraint)) {
                return dialogue;
            }
        }

        return default(DialogueComp);
    }

    public DialogueComp Process(DialogueComp targetComp) {

        int nextIndex = this.dialogueCompList.FindIndex(x => x._id == targetComp._id) + 1;

        if (nextIndex < dialogueCount) {
            currentComp = this.dialogueCompList[nextIndex];
            Debug.Log("currentComp " + currentComp._id);

            List<DialogueComp> compGroup = dialogueModel.GetDialogueListByGroupID(this.sceneID, currentComp.group_id);

            return ExamineDialogueGroup(compGroup);
        }

        return default(DialogueComp);
    }
}
