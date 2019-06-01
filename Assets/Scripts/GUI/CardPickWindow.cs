using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.GUI
{
    public class CardPickWindow : ListWindow
    {
        [SerializeField] protected CardPicker pick;
        public CardPicker Pick { get => pick; private set => pick = value; }

        public static new CardPickWindow Create(List<Card> cards, string title = default)
        {
            var window = Instantiate(Resources.Load<CardPickWindow>("Prefabs/CardPickWindow"), Game.context.transform);
            window.title.text = title;
            foreach (var card in cards)
            {
                CardPicker pick = Instantiate(card.Obj, window.listView).AddComponent<CardPicker>();
                pick.original = card;
            }
            return window;
        }

        public void Select(CardPicker selection)
        {
            if (Pick) Pick.transform.Find("Select").GetComponent<Image>().enabled = false;
            Pick = selection;
            Pick.transform.Find("Select").GetComponent<Image>().enabled = true;
            Accept.interactable = true;
        }
    }
}

