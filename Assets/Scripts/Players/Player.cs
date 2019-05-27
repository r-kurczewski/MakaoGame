using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame
{
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] protected Transform cardFolder;
        [SerializeField] protected TMP_Text nameLabel;
        [SerializeField] protected Transform WaitTurnLabel;
        [SerializeField] protected Image ActiveTurnIndicator;

        [SerializeField] protected int _skipTurn;
        [SerializeField] protected List<Card> cards = new List<Card>();
        public int CardNumber { get => cards.Count; }
        public List<Card> PlayableCards
        {
            get
            {
                var pileCard = Game.context.Pile.TopCard;
                if (pileCard == null) return cards;
                var type = pileCard.GetType();
                var color = pileCard.CardColor;
                return cards.Where(x => x.GetType() == type || x.CardColor == color).ToList();
            }
        }

        void Start()
        {
            nameLabel.text = name;
        }

        public int SkipTurn { get => _skipTurn; set => _skipTurn = value; }

        public abstract void GiveCard(Card card);

        public abstract void MakeAMove();

        private void SortCards()
        {

        }

        public abstract void ApplyAction(List<Card> action);

        public void Play(Card card)
        {
            cards.Remove(card);
            Game.context.Pile.PutCard(card);
            Debug.Log($"{this.name} plays {card.name}");
        }

        public void Counter(Card card)
        {
            cards.Remove(card);
            Game.context.Pile.PutCounterCard(card);
            Debug.Log($"{this.name} counter {Game.context.Pile.TopCard.name} with {card.name}");
        }

        public void Wait()
        {
            SkipTurn--;
            Debug.Log($"{this.name} must wait a turn. ({SkipTurn + 1}->{SkipTurn})");
        }

        public void AcceptAction(List<Card> action)
        {
            foreach (var card in action)
            {
                card.Effect();
            }
            Debug.Log($"{this.name} takes an action.");
        }

        public void DrawACard()
        {
            var card = Game.context.Deck.DrawCard();
            GiveCard(card);
            Debug.Log($"{this.name} draws {card.name}");
        }

        public abstract void Win();

        public abstract void Lose();

        public void UpdateGUILabels()
        {
            ActiveTurnIndicator.color = Game.context.CurrentPlayer == this ? Color.green : Color.white;
            if (SkipTurn > 0)
            {
                WaitTurnLabel.gameObject.SetActive(true);
                WaitTurnLabel.GetComponentInChildren<TMP_Text>().text = SkipTurn.ToString();
            }
            else
            {
                WaitTurnLabel.gameObject.SetActive(false);
            }
        }
    }
}