using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame.GUI
{
    class JackEffectWindow : CardPickWindow
    {
        public static JackEffectWindow Create(List<Card> cards)
        {
            var window = Instantiate(Resources.Load<JackEffectWindow>("Prefabs/JackEffectWindow"), Game.context.transform);
            foreach (var card in cards)
            {
                CardPicker pick = Instantiate(card, window.listView).gameObject.AddComponent<CardPicker>();
                pick.original = card;
            }
            return window;
        }
    }
}