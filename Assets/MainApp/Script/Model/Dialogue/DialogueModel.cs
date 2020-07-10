using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Iso.Model {
    public class DialogueModel
    {

        /// <summary>
        /// scene_id, list
        /// </summary>
        public Dictionary<string, List<DialogueComp>> cacheDialogueDict = new Dictionary<string, List<DialogueComp>>();
        

        public void ParseDialogueFromList(string[] csvPathList)
        {
            if (csvPathList == null) return;

            foreach (string csvPath in csvPathList) {

                string csvText = FileUtility.GetFileText(csvPath);

                CSVFile csvFile = new CSVFile(csvText);

                int csvCount = csvFile.length;

                for (int i = 0; i < csvCount; i++)
                {


                }

            }
        }

        private DialogueComp ParseDialogueCSV(CSVFile csvFile, int i) {

            DialogueComp dialogueComp = new DialogueComp();

            dialogueComp._id = csvFile.Get<string>(i, "_id");
            dialogueComp.scene_id = csvFile.Get<string>(i, "Scene_ID");
            dialogueComp.group_id = csvFile.Get<string>(i, "Group");
            dialogueComp.type = csvFile.Get<string>(i, "Type");
            dialogueComp.character_ids = csvFile.Get<string>(i, "Character");
            dialogueComp.animation = csvFile.Get<string>(i, "Animation");
            dialogueComp.constraint = csvFile.Get<string>(i, "Constraint");
            dialogueComp.effect = csvFile.Get<string>(i, "Effect");
            dialogueComp.mainValue = csvFile.Get<string>(i, "Main Value");
            

            return dialogueComp;
        }


    }
}