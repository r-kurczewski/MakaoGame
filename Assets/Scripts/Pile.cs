using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame
{
	/// <summary>
	/// Klasa reprezentująca stos kart rzuconych.
	/// </summary>
	[SelectionBase]
	public class Pile : MonoBehaviour
	{
		public AudioClip putCardSound;
		[SerializeField] List<Card> cards = new List<Card>();

		/// <summary>
		/// Karta z wierzchu
		/// </summary>
		public Card TopCard { get { return cards.LastOrDefault(); } }

		/// <summary>
		/// Dodaje kartę do stosu
		/// </summary>
		/// <param name="card"></param>
		public void AddToPile(Card card)
		{
			Game.instance.audioSource.PlayOneShot(putCardSound);
			cards.Add(card);
			card.transform.SetParent(transform, false);
			card.transform.localPosition = Vector3.zero;
			card.Obj.transform.localPosition = Vector3.zero;
			card.Show();
		}

		/// <summary>
		/// Zwraca ze stosu listę kart do ponownego wtasowania
		/// </summary>
		/// <returns></returns>
		public List<Card> ClearPile()
		{
			var pileCards = cards.Take(cards.Count - 1).ToList();
			cards.RemoveAll(card => pileCards.Contains(card));
			return pileCards;
		}
	}
}
