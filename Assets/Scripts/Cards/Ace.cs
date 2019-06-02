using MakaoGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame
{
    public class Ace : Card
    {

        public override string Label => "A";

        [SerializeField] protected CardSuit _virtualColor;

        public override CardSuit CardColor { get => _virtualColor; protected set => _virtualColor = value; }

        public override void Effect()
        {
            Game.context.CurrentPlayer.ChooseAceColor(this);
        }

        public override bool IsCounterTo(Card card)
        {
            return false;
        }

        public override void Reset()
        {
            _virtualColor = _cardColor;
            transform.Find("Card/Front/Ace").GetComponentInChildren<Image>().enabled = false;
        }

        public void ChangeColor(CardSuit cardColor)
        {
            Transform acePanel = transform.Find("Card/Front/Ace");
            var aceSymbol = acePanel.GetComponentInChildren<TMP_Text>();
            GetSymbolParameters(cardColor, out char symbolstring, out Color color);

            CardColor = cardColor;
            acePanel.GetComponentInChildren<Image>().enabled = true;
            aceSymbol.text = symbolstring.ToString();
            aceSymbol.color = color;
        }

        public override void Play()
        {
            Game.context.Pile.AddToPile(this);
            Game.context.CurrentPlayer.ChooseAceColor(this);
        }
    }
}
