using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.Cards
{
    /// <summary>
    /// Implementuje klasę <see cref="Card"/>
    /// </summary>
    public class Ace : Card
    {
        public override string Label => "A";

        [SerializeField] protected CardSuit _virtualColor;

        public override CardSuit CardColor { get => _virtualColor; protected set => _virtualColor = value; }

        new private void Start()
        {
            base.Start();
            _cardColor = CardColor;
        }

        public override void Effect()
        {
            Game.instance.CurrentPlayer.ChooseAceColor(this);
        }

        public override bool IsCounterTo(Card card)
        {
            return false;
        }

        public override void Reset()
        {
            _virtualColor = _cardColor;
            foreach (var comp in transform.Find("Card/Front/Ace").GetComponentsInChildren<MonoBehaviour>())
            {
                comp.enabled = false;
            };
        }

        /// <summary>
        /// Odpowiada za zmianę koloru karty i jej wyglądu 
        /// </summary>
        /// <param name="cardColor"></param>
        public void ChangeColor(CardSuit cardColor)
        {
            Transform acePanel = transform.Find("Card/Front/Ace");
            foreach (var comp in acePanel.GetComponentsInChildren<MonoBehaviour>())
            {
                comp.enabled = true;
            }
            var aceSymbol = acePanel.GetComponentInChildren<TMP_Text>();
            GetSymbolParameters(cardColor, out char symbolstring, out Color color);

            CardColor = cardColor;
            acePanel.GetComponentInChildren<Image>().enabled = true;
            aceSymbol.text = symbolstring.ToString();
            aceSymbol.color = color;
        }

        public override void Play()
        {
            Game.instance.Pile.AddToPile(this);
            Game.instance.CurrentPlayer.ChooseAceColor(this);
        }
    }
}
