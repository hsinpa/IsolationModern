using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Isolation.View
{
    public class UIView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        public void Show(bool isShow) {
            canvasGroup.alpha = (isShow) ? 1 : 0;
            canvasGroup.blocksRaycasts = isShow;
            canvasGroup.interactable = isShow;
        }
    }
}