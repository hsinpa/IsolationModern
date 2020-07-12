using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Utility;

namespace Iso.Model {
    public class DialogueModel
    {

        /// <summary>
        /// scene_id, list
        /// </summary>
        private Dictionary<string, List<DialogueComp>> cacheDialogueDict = new Dictionary<string, List<DialogueComp>>();

        private DialogueProcessor _dialogueProcessor;
        public DialogueProcessor dialogueProcessor => _dialogueProcessor;

        private WorldModel worldModel;

        public DialogueModel(WorldModel worldModel)
        {
            this.worldModel = worldModel;
            _dialogueProcessor = new DialogueProcessor(worldModel, this);
        }

        #region Public API
        public List<DialogueComp> GetDialogueListBySceneID(string scene_id) {

            if (cacheDialogueDict.TryGetValue(scene_id, out List<DialogueComp> dialogueList)) {
                return dialogueList;
            }

            return new List<DialogueComp>();
        }

        public DialogueComp GetDialogueListByUniqueID(string scene_id, string uniqueID)
        {
            if (cacheDialogueDict.TryGetValue(scene_id, out List<DialogueComp> dialogueList))
            {
                return dialogueList.Find(x => x._id == uniqueID);
            }

            return default(DialogueComp);
        }

        public List<DialogueComp> GetDialogueListByGroupID(string scene_id, string group_id)
        {
            if (cacheDialogueDict.TryGetValue(scene_id, out List<DialogueComp> dialogueList))
            {
                    return dialogueList.FindAll(x=>x.group_id == group_id);
            }

            return new List<DialogueComp>();
        }

        public void ParseDialogueFromCSVPathList(string[] csvPathList)
        {
            if (csvPathList == null) return;

            foreach (string csvPath in csvPathList) {

                string csvText = FileUtility.GetFileText(csvPath);

                CSVFile csvFile = new CSVFile(csvText);

                int csvCount = csvFile.length;

                for (int i = 0; i < csvCount; i++)
                {

                    string scene_id = csvFile.Get<string>(i, "Scene_ID");

                    DialogueComp gDialogueComp = ParseDialogueCSV(csvFile, i);

                    cacheDialogueDict = UtilityMethod.EditDictionaryArray<DialogueComp>(cacheDialogueDict, scene_id, gDialogueComp);
                }
            }
        }
        #endregion
        private DialogueComp ParseDialogueCSV(CSVFile csvFile, int i) {

            DialogueComp dialogueComp = new DialogueComp();

            dialogueComp._id = csvFile.Get<string>(i, "ID");

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