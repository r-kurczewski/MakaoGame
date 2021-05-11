using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.GUI
{
    /// <summary>
    /// Implementuje klasę <see cref="ListWindow"/>
    /// Klasa okna wyboru karty.
    /// </summary>
    public class CardPickWindow : ListWindow
    {
        [SerializeField] protected CardPicker pick;

        /// <summary>
        /// Aktualnie wybrana karta
        /// </summary>
        public CardPicker Pick { get => pick; private set => pick = value; }

        new public static CardPickWindow Create(List<Card> cards, string title = null)
        {
            var window = Instantiate(Resources.Load<CardPickWindow>("Prefabs/CardPickWindow"), Game.instance.transform);
            if (title != null) window.title.text = title;
            foreach (var card in cards)
            {
                CardPicker pick = Instantiate(card, window.listView).gameObject.AddComponent<CardPicker>();
                pick.original = card;
            }
            return window;
        }

        /// <summary>
        /// Wybiera daną kartę z listy
        /// </summary>
        /// <param name="selection"></param>
        public void Select(CardPicker selection)
        {
            if (Pick) Pick.transform.Find("Card/Select").GetComponent<Image>().enabled = false;
            Pick = selection;
            Pick.transform.Find("Card/Select").GetComponent<Image>().enabled = true;
            Accept.interactable = true;
        }
    }
}

