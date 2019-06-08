using MakaoGame.Cards;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame
{
    /// <summary>
    /// Główna klasa graczy.
    /// </summary>
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] protected Transform cardFolder;
        [SerializeField] protected TMP_Text nameLabel;
        [SerializeField] protected Transform WaitTurnLabel;
        [SerializeField] protected Image ActiveTurnIndicator;

        [SerializeField] protected int _skipTurn;
        [SerializeField] protected List<Card> cards = new List<Card>();

        /// <summary>
        /// Zwraca liczbę kart gracza.
        /// </summary>
        public int CardNumber { get => cards.Count; }

        /// <summary>
        ///     Zwraca listę kart możliwych do położenia.
        /// </summary>
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

        /// <summary>
        /// Liczba tur które gracz musi czekać
        /// </summary>
        public int SkipTurn { get => _skipTurn; set => _skipTurn = value; }

        /// <summary>
        /// Przekazanie karty do ręki gracza
        /// </summary>
        /// <param name="card"></param>
        public abstract void GiveCard(Card card);

        /// <summary>
        /// Zleca danemu graczowi wykonanie ruchu
        /// </summary>
        public abstract void MakeAMove();

        /// <summary>
        /// Zleca graczowi zastosowanie się do akcji
        /// </summary>
        public abstract void ApplyAction();

        /// <summary>
        /// Zleca graczowi zagranie karty
        /// </summary>
        /// <param name="card"></param>
        public void Play(Card card)
        {
            cards.Remove(card);
            card.Play();
            Debug.Log($"{this.name} plays {card.name}");
        }

        /// <summary>
        /// Zleca graczowi skontrowanie akcji kartą
        /// </summary>
        /// <param name="card"></param>
        public void Counter(Card card)
        {
            Debug.Log($"{this.name} counter {Game.context.actionChain.Last().name} with {card.name}");
            cards.Remove(card);
            card.CounterPlay();
        }

        protected void Wait()
        {
            SkipTurn--;
            Debug.Log($"{this.name} must wait a turn. ({SkipTurn + 1}->{SkipTurn})");
        }

        protected void AcceptAction()
        {
            // Wywołuje każdy efekt od najnowszego
            for (int i = Game.context.actionChain.Count - 1; i >= 0; i--)
            {
                var card = Game.context.actionChain[i];
                card.Effect();
            }
            if (Game.context.actionChain.Count(c => c.GetType() == typeof(Jack) || c.GetType() == typeof(Queen)) == 0)
            {
                Game.context.EndPlayerTurn();
            }
            Debug.Log($"{this.name} takes an action.");
        }

        /// <summary>
        /// Zleca graczowi dobranie karty
        /// </summary>
        public void DrawACard()
        {
            var card = Game.context.Deck.DrawCard();
            GiveCard(card);
            Debug.Log($"{this.name} draws {card.name}");
        }

        /// <summary>
        /// Metoda wywoływana w wypadku zwycięstwa gracza.
        /// </summary>
        public abstract void Win();

        /// <summary>
        /// Metoda wywoływana w wypadku przegranej gracza.
        /// </summary>
        public abstract void Lose();

        /// <summary>
        /// Aktualizuje interfejs gracza
        /// </summary>
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

        /// <summary>
        /// Zleca graczowi wybranie efektu karty.
        /// </summary>
        /// <param name="ace"></param>
        public abstract void ChooseAceColor(Ace ace);

        /// <summary>
        /// Zleca graczowi wybranie efektu karty.
        /// </summary>
        /// <param name="jack"></param>
        public abstract void ChooseJackRequest(Jack jack);

        /// <summary>
        /// Zleca graczowi zareagowanie na efekt karty
        /// </summary>
        /// <param name="jack"></param>
        public abstract void JackEffect(Jack jack);
    }
}