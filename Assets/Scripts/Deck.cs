using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static MakaoGame.Card;

namespace MakaoGame
{
    [SelectionBase]
    public class Deck : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMP_Text cardNumber = default;

        [SerializeField] private List<Card> cards = new List<Card>();

        void Start()
        {
            cardNumber.text = cards.Count.ToString();
        }

        public void Shuffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int pos = i + Random.Range(0, cards.Count - i);

                var temp = cards[pos];
                cards[pos] = cards[i];
                cards[i] = temp;
            }
        }

        public void AddCard(Card card)
        {
            card.transform.SetParent(transform.Find("Cards"));
            card.transform.rotation = Quaternion.identity;
            card.gameObject.SetActive(false);
        }

        public void AddCardsFromPile()
        {
            foreach (var card in Game.context.Pile.ClearPile())
            {
                AddCard(card);
                cards.Add(card);
            }
        }

        public void Create()
        {
            foreach (CardSuit color in System.Enum.GetValues(typeof(CardSuit)))
            {
                {
                    cards.Add(Create<Two>(color));
                    cards.Add(Create<Three>(color));
                    cards.Add(Create<Four>(color));
                    cards.Add(Create<Ace>(color));
                    cards.Add(Create<Jack>(color));
                    cards.Add(Create<Queen>(color));
                    cards.Add(Create<King>(color));

                    cards.Add(Create<Two>(color));
                    cards.Add(Create<Three>(color));
                    cards.Add(Create<Four>(color));
                    cards.Add(Create<Ace>(color));
                    cards.Add(Create<Jack>(color));
                    cards.Add(Create<Queen>(color));
                    cards.Add(Create<King>(color));
                }
            }
            foreach (var card in cards)
            {
                AddCard(card);
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0) AddCardsFromPile();
            Card card = cards.Last();
            cards.Remove(card);
            cardNumber.text = cards.Count.ToString();
            card.gameObject.SetActive(true);
            return card;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Game.context.CurrentPlayer == Game.context.HumanPlayer)
            {
                Game.context.HumanPlayer.DrawACard();
                Game.context.EndPlayerTurn();
            }

            else Debug.LogWarning("Now is not a player turn!");
        }
    }
}
