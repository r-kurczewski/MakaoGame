using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame.GUI
{
    /// <summary>
    /// Klasa okna wyboru żądania.
    /// </summary>
    class JackEffectWindow : CardPickWindow
    {
        /// <summary>
        /// Tworzenie okna
        /// </summary>
        /// <param name="cards">Lista kart których można żądać od gracza</param>
        /// <returns></returns>
        public static JackEffectWindow Create(List<Card> cards)
        {
            var window = Instantiate(Resources.Load<JackEffectWindow>("Prefabs/JackEffectWindow"), Game.instance.transform);
            foreach (var card in cards)
            {
                CardPicker pick = Instantiate(card, window.listView).gameObject.AddComponent<CardPicker>();
                pick.original = card;
            }
            return window;
        }
    }
}