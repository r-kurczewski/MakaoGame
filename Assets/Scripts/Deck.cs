using MakaoGame.Cards;
using MakaoGame.Players;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static MakaoGame.Card;

namespace MakaoGame
{
	/// <summary>
	/// Klasa reprezentująca talię kart.
	/// </summary>
	[SelectionBase]
	public class Deck : MonoBehaviour, IPointerDownHandler
	{
		public AudioClip drawCardSound;

		[SerializeField] private TMP_Text cardNumber = default;

		[SerializeField] private List<Card> cards = new List<Card>();

		void Start()
		{
			cardNumber.text = cards.Count.ToString();
		}

		/// <summary>
		/// Odpowiada za tasowanie talii.
		/// </summary>
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

		/// <summary>
		/// Odpowiada za wrzucenie karty do talii.
		/// </summary>
		/// <param name="card"></param>
		public void PutCard(Card card)
		{
			card.transform.SetParent(transform.Find("Cards"));
			card.transform.rotation = Quaternion.identity;
			card.gameObject.SetActive(false);
			cards.Add(card);
		}

		/// <summary>
		/// Dodaje karty ze stosu (oprócz ostatniej) do talii.
		/// </summary>
		public void AddCardsFromPile()
		{
			foreach (var card in Game.instance.Pile.ClearPile())
			{
				card.Reset();
				PutCard(card);
			}
		}

		/// <summary>
		/// Tworzy talię standardową (52 karty)
		/// </summary>
		public void Create()
		{
			List<Card> deck = new List<Card>();
			foreach (CardSuit color in System.Enum.GetValues(typeof(CardSuit)))
			{
				{
					deck.Add(Create<Two>(color));
					deck.Add(Create<Three>(color));
					deck.Add(Create<Four>(color));
                    deck.Add(Create<Five>(color));
                    deck.Add(Create<Six>(color));
                    deck.Add(Create<Seven>(color));
                    deck.Add(Create<Eight>(color));
                    deck.Add(Create<Nine>(color));
                    deck.Add(Create<Ten>(color));
                    deck.Add(Create<Jack>(color));
					deck.Add(Create<Queen>(color));
					deck.Add(Create<King>(color));
					deck.Add(Create<Ace>(color));
				}
			}
			foreach (var card in deck)
			{
				PutCard(card);
			}
		}

		/// <summary>
		/// Pobiera kartę z wierzchu.
		/// </summary>
		/// <returns>Pobrana karta</returns>
		public Card DrawCard(bool playSound = true)
		{
			if(playSound) Game.instance.audioSource.PlayOneShot(drawCardSound);
			if (cards.Count == 0) AddCardsFromPile();
			Card card = cards.Last();
			cards.Remove(card);
			cardNumber.text = cards.Count.ToString();
			card.gameObject.SetActive(true);
			return card;
		}

		/// <summary>
		/// Odpowiada za dobranie karty przez użytkownika.
		/// </summary>
		/// <param name="eventData"></param>
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Game.instance.CurrentPlayer is HumanPlayer)
			{
				Game.instance.CurrentPlayer.DrawCardAndEndTurn();
				Game.instance.CurrentPlayer.finishTurn = true;
			}

			else Debug.LogWarning("Now is not a player turn!");
		}
	}
}
