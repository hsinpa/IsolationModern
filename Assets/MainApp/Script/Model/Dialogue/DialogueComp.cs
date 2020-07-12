using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso.Model
{
    public struct DialogueComp
    {
        public string _id;

        public string scene_id;

        public string group_id;

        public string type;

        public string character_ids;

        public string animation;

        public string constraint;

        public string effect;

        public string mainValue;

        public bool isValid => !string.IsNullOrEmpty(_id);
    }
}