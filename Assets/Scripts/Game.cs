using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MakaoGame
{
    public class Game : MonoBehaviour
    {
        public static Game context;
        [SerializeField] private int _currentPlayerId = 0;
        [SerializeField] private Deck _deck;
        [SerializeField] private Pile _pile;
        [SerializeField] private List<Player> _players;
        public List<Card> actionChain = new List<Card>();
        public int CurrentPlayerId { get { return _currentPlayerId; } private set { _currentPlayerId = (4 + value) % 4; } }
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
        public Player HumanPlayer { get { return Players.FirstOrDefault(p => p.GetType() == typeof(HumanPlayer)); } }

        public Player CurrentPlayer
        {
            get
            {
                while (Players[CurrentPlayerId] == null)
                    CurrentPlayerId++;
                return Players[CurrentPlayerId];
            }
        }

        public Player NextPlayer
        {
            get
            {
                var i = CurrentPlayerId + 1;
                while (Players[i] == null)
                {
                    i++;
                }
                return Players[i];
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
                player.GiveCard(Card.Create<Jack>(CardSuit.Pike));
            }
            //HumanPlayer.GiveCard(Card.Create<Jack>(CardSuit.Heart));
            HumanPlayer.GiveCard(Card.Create<Jack>(CardSuit.Heart));
            Players[3].GiveCard(Card.Create<Jack>(CardSuit.Heart));
            UpdateGUILabels();
        }

        public void EndPlayerTurn()
        {
            CheckWinningCondition();
            NextPlayerTurn();
            UpdateGUILabels();
            StartCoroutine("IEndTurn");
        }


        public void PassAction(bool clockwise = true)
        {
            CheckWinningCondition();
            if (clockwise)
                NextPlayerTurn();
            else
                PreviousPlayerTurn();
            UpdateGUILabels();
            StartCoroutine("IPassAction");
        }

        readonly int delay = 2;

        private IEnumerator IPassAction()
        {
            // opoźnienie tur komputerów
            //if (CurrentPlayer != HumanPlayer || HumanPlayer?.SkipTurn != 0)
                yield return new WaitForSeconds(delay);
            CurrentPlayer.ApplyAction();
        }

        private IEnumerator IEndTurn()
        {
            //if (CurrentPlayer != HumanPlayer || HumanPlayer?.SkipTurn != 0)
                yield return new WaitForSeconds(delay);
            CurrentPlayer.MakeAMove();
        }

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
                    Players.FirstOrDefault().Lose();
                    Debug.Log($"{Players[0].name} lost");
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