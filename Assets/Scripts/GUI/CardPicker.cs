using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MakaoGame.GUI
{
    /// <summary>
    /// Klasa wyboru karty lub typu karty w GUI.
    /// </summary>
    public class CardPicker : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// Referencja do oryginalnej karty w grze.
        /// </summary>
        public Card original;

        void Start()
        {
            name = original.name;
        }

        /// <summary>
        /// Wybiera daną kartę po kliknięciu
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            var window = GetComponentInParent<CardPickWindow>();
            window.Select(this);
        }
    }
}
