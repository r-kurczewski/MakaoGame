using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MakaoGame.GUI
{
    public class CardPicker : MonoBehaviour, IPointerClickHandler
    {
        public Card original;

        void Start()
        {
            name = original.name;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var window = GetComponentInParent<CardPickWindow>();
            window.Select(this);
        }
    }
}
