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
		public static Game instance;

		public AudioSource audioSource;

		[Header("Data")]
		[SerializeField] private int _currentPlayerId = 0;
		[SerializeField] GameObject GUIBlock = null;
		[SerializeField] private Deck _deck;
		[SerializeField] private Pile _pile;
		[SerializeField] private List<Player> _players;
		private bool clockwise = true;

		/// <summary>
		/// Lista kart w aktualnej akcji
		/// </summary>
		public List<Card> actionChain = new List<Card>();

		/// <summary>
		/// Indeks aktualnego gracza 
		/// </summary>
		private int CurrentPlayerId { get { return _currentPlayerId; } set { _currentPlayerId = (4 + value) % 4; } }

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
		/// Zwraca referencję do gracza wykonującego aktualnie turę.
		/// </summary>
		public Player CurrentPlayer
		{
			get
			{
				return Players[CurrentPlayerId];
			}
		}

		public void SetCounterClockwise()
		{
			clockwise = false;
		}

		private void PreviousPlayerTurn()
		{
			if (CurrentPlayer != null)
			{
				CurrentPlayer.finishTurn = false;
			}
			CurrentPlayerId--;
		}

		private void NextPlayerTurn()
		{
			if(CurrentPlayer != null)
            {
				CurrentPlayer.finishTurn = false;
            } 
			CurrentPlayerId++;
		}

		void Start()
		{
			instance = this;
			Debug.Log("Game started...");

			// Tworzenie talii
			Deck.Create();
			Deck.Shuffle();

            // Rozdanie kart graczom
            foreach (var player in Players)
            {
                for (int i = 0; i < 5; i++)
                {
                    Card card = Deck.DrawCard(playSound: false);
                    player.GiveCard(card);
                }
            }
            UpdateGUILabels();
			StartCoroutine(IManageTurns());
		}

		/// <summary>
		/// Odpowiada za zarządzanie turami graczy.
		/// </summary>
		public IEnumerator IManageTurns()
		{
			while (true)
			{
				if (CurrentPlayer is null)
				{
					NextPlayerTurn();
					continue;
				}
				UpdateGUILabels();
				StartCoroutine(CurrentPlayer.Move());
				yield return new WaitUntil(() => CurrentPlayer.finishTurn);
				CheckWinningCondition();
				UpdateGUILabels();
				if (!clockwise)
				{
					PreviousPlayerTurn();
					clockwise = true;
				}
				else NextPlayerTurn();
			}
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