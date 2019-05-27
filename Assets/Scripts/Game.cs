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
        [SerializeField] private bool clockwise = true;
        [SerializeField] private int _currentPlayerId = 0;
        public int CurrentPlayerId { get { return _currentPlayerId; } private set { _currentPlayerId = value % 4; } }
        [SerializeField] private List<Player> _players;
        [SerializeField] private Deck _deck;
        [SerializeField] private Pile _pile;

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
        public Player PreviousPlayer { get { return !clockwise ? Players[(CurrentPlayerId + 1) % 4] : Players[(4 + CurrentPlayerId - 1) % 4]; } }
        //public Player CurrentPlayer { get { return Players[CurrentPlayerId]; } }
        public Player CurrentPlayer
        {
            get
            {
                while (Players[CurrentPlayerId] == null)
                    CurrentPlayerId++;
                return Players[CurrentPlayerId];
            }
        }
        public Player NextPlayer { get { return clockwise ? Players[(CurrentPlayerId + 1) % 4] : Players[(4 + CurrentPlayerId - 1) % 4]; } }

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
                for (int i = 0; i < 1; i++)
                {
                    Card card = Deck.DrawCard();
                    player.GiveCard(card);
                }
                // Debug
                //player.GiveCard(Card.Create<Four>(CardSuit.Heart));
            }
            //HumanPlayer.GiveCard(Card.Create<Four>(CardSuit.Tile));
            UpdateGUILabels();
        }

        public void EndPlayerTurn()
        {
            CheckWinningCondition();
            CurrentPlayerId++;
            UpdateGUILabels();
            StartCoroutine("IEndTurn");
        }

        public void PassAction(List<Card> action)
        {
            CheckWinningCondition();
            CurrentPlayerId++;
            UpdateGUILabels();
            StartCoroutine("IPassAction", action);
        }

        readonly int delay = 2;

        private IEnumerator IPassAction(List<Card> action)
        {
            if (CurrentPlayer != HumanPlayer || HumanPlayer?.SkipTurn != 0)
                yield return new WaitForSeconds(delay);
            CurrentPlayer.ApplyAction(action);
        }

        private IEnumerator IEndTurn()
        {
            if (CurrentPlayer != HumanPlayer || HumanPlayer?.SkipTurn != 0)
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