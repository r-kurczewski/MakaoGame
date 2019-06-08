using MakaoGame.Cards;
using MakaoGame.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame
{
    /// <summary>
    /// Klasa główna gry odpowiadająca za działanie całęgo programu.
    /// </summary>
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// Rreferencja do obiektu gry.
        /// </summary>
        public static Game context;
        [SerializeField] private int _currentPlayerId = 0;
        [SerializeField] GameObject GUIBlock;
        [SerializeField] private Deck _deck;
        [SerializeField] private Pile _pile;
        [SerializeField] private List<Player> _players;

        /// <summary>
        /// Lista kart w aktualnej akcji
        /// </summary>
        public List<Card> actionChain = new List<Card>();

        /// <summary>
        /// Indeks aktualnego gracza 
        /// </summary>
        public int CurrentPlayerId { get { return _currentPlayerId; } private set { _currentPlayerId = (4 + value) % 4; } }

        /// <summary>
        /// Lista graczy w grze
        /// </summary>
        public List<Player> Players
        {
            get
            {
                return _players;
            }
            private set
            {
                _players = value;
            }
        }

        /// <summary>
        /// Referencja do klasy talii kart.
        /// </summary>
        public Deck Deck
        {
            get
            {
                return _deck;
            }
            private set
            {
                _deck = value;
            }
        }

        /// <summary>
        /// Referencja do klasy stosu kart.
        /// </summary>
        public Pile Pile
        {
            get
            {
                return _pile;
            }
            private set
            {
                _pile = value;
            }
        }

        /// <summary>
        /// Zwraca referencję do gracza pod kontrolą użytkownika
        /// </summary>
        public Player HumanPlayer { get { return Players.FirstOrDefault(p => p.GetType() == typeof(HumanPlayer)); } }

        /// <summary>
        /// Zwraca referencję do gracza wykonującego aktualnie turę.
        /// </summary>
        public Player CurrentPlayer
        {
            get
            {
                while (Players[CurrentPlayerId] == null)
                    CurrentPlayerId++;
                return Players[CurrentPlayerId];
            }
        }

        private void PreviousPlayerTurn()
        {
            do
            {
                CurrentPlayerId--;
            }
            while (Players[CurrentPlayerId] == null);
        }

        private void NextPlayerTurn()
        {
            do
            {
                CurrentPlayerId++;
            }
            while (Players[CurrentPlayerId] == null);
        }

        void Start()
        {
            context = this;
            Debug.Log("Game started...");

            // Tworzenie talii
            Deck.Create();
            Deck.Shuffle();

            // Rozdanie kart graczom
            foreach (var player in Players)
            {
                for (int i = 0; i < 5; i++)
                {
                    Card card = Deck.DrawCard();
                    player.GiveCard(card);
                }
                //player.GiveCard(Card.Create<Two>(CardSuit.Tile)); // Debug
            }
            //HumanPlayer.GiveCard(Card.Create<Four>(CardSuit.Heart)); // Debug
            //Players[1].GiveCard(Card.Create<Queen>(CardSuit.Heart)); // Debug
            UpdateGUILabels();
        }

        readonly private int delay = 3;

        /// <summary>
        /// Odpowiada za zakończenie ruchu gracza.
        /// </summary>
        public void EndPlayerTurn()
        {
            CheckWinningCondition();
            NextPlayerTurn();
            UpdateGUILabels();
            StopAllCoroutines();
            StartCoroutine("IEndTurn");
        }

        /// <summary>
        /// Odpowida za przekazanie akcji następnemu graczowi.
        /// </summary>
        /// <param name="clockwise">Czy kierunek obrotu zgodny ze wskazówkami zegara?</param>
        public void PassAction(bool clockwise = true)
        {
            CheckWinningCondition();
            if (clockwise)
                NextPlayerTurn();
            else
                PreviousPlayerTurn();
            UpdateGUILabels();
            StopAllCoroutines();
            StartCoroutine("IPassAction");
        }

        private IEnumerator IPassAction()
        {
            GUIBlock.SetActive(true);
            yield return new WaitForSeconds(delay);
            GUIBlock.SetActive(false);
            CurrentPlayer.ApplyAction();
        }

        private IEnumerator IEndTurn()
        {
            yield return new WaitForSeconds(delay);
            CurrentPlayer.MakeAMove();
        }

        /// <summary>
        /// Sprawdza warunki końcowe gry i odpowiada za zakończenie rozgrywki.
        /// </summary>
        private void CheckWinningCondition()
        {
            if (CurrentPlayer.CardNumber == 0)
            {
                Player player = CurrentPlayer;
                Players[CurrentPlayerId] = null;
                player.Win();

                Debug.Log($"{player.name} wins.");
                if (Players.Count(p => p != null) == 1)
                {
                    try
                    {
                        Players.FirstOrDefault().Lose();
                        Debug.Log($"{Players[0].name} lost");
                    }
                    catch (NullReferenceException)
                    {
                        Debug.LogWarning("Couldn't load Lose method.");
                    }
                }
            }
        }

        private void UpdateGUILabels()
        {
            foreach (var player in Players)
            {
                player?.UpdateGUILabels();
            }
        }
    }
}