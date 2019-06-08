using MakaoGame.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame.GUI
{
    /// <summary>
    /// Klasa okna wyboru żadania karty
    /// </summary>
    class JackRequestWindow : CardPickWindow
    {
        /// <summary>
        /// Tworzy okno.
        /// </summary>
        /// <returns></returns>
        public static JackRequestWindow Create()
        {
            CardSuit color = CardSuit.Pike;
            var possibleRequests = new List<Card>
            {
                Card.Create<Five>(color),
                Card.Create<Six>(color),
                Card.Create<Seven>(color),
                Card.Create<Eight>(color),
                Card.Create<Nine>(color),
                Card.Create<Ten>(color),
            };
            var window = Instantiate(Resources.Load<JackRequestWindow>("Prefabs/JackRequestWindow"), Game.context.transform);
            foreach (var card in possibleRequests)
            {
                CardPicker pick = card.gameObject.AddComponent<CardPicker>();
                pick.transform.SetParent(window.listView, false);
                pick.original = card;
            }
            return window;
        }
    }
}
