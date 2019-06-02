using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MakaoGame.GUI
{
    class JackRequestWindow : CardPickWindow
    {
        public static JackRequestWindow Create()
        {
            #region creating list
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
            #endregion
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
