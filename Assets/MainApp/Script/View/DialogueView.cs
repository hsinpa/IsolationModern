using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Isolation.View;

namespace Isolation.View {
    public class DialogueView : UIView
    {
        [SerializeField]
        private Text messageTxt;

        [SerializeField]
        private Image avatar;

        [SerializeField]
        private Transform titleObj;

        [SerializeField]
        private Text titleTxt;

        public void SetMessage(string message) {
            if (!string.IsNullOrEmpty(message)) {
                message = message.Replace("\\n", "\n");

                messageTxt.text = message;
            }
        }

        public void SetAvatar(Sprite sprite)
        {
            avatar.gameObject.SetActive(sprite != null);

            if (sprite != null) {
                avatar.sprite = sprite;
            }
        }

        public void SetTitle(string title) {
            bool hasTitle = !string.IsNullOrEmpty(title);

            titleObj.gameObject.SetActive(hasTitle);

            if (hasTitle) {
                titleTxt.text = title;
            }
        }


    }
}
