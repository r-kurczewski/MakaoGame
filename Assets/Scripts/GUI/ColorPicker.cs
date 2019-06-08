using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MakaoGame.GUI
{
    /// <summary>
    /// Klasa wyboru koloru w GUI.
    /// </summary>
    public class ColorPicker : MonoBehaviour, IPointerClickHandler
    {
        public CardSuit color;

        /// <summary>
        /// Wybiera dany kolor w oknie.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            var window = GetComponentInParent<AceEffectWindow>();
            window.Select(this);
        }
    }
}