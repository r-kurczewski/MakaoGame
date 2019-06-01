using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MakaoGame.GUI
{
    public class ColorPicker : MonoBehaviour, IPointerClickHandler
    {
        public CardSuit color;

        void Start()
        {
            //name = color.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var window = GetComponentInParent<AceEffectWindow>();
            window.Select(this);
        }
    }
}