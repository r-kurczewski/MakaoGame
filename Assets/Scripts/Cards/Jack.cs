using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MakaoGame.Cards
{
	/// <summary>
	/// Implementuje klasę <see cref="Card"/>
	/// </summary>
	public class Jack : Card
	{
		[SerializeField] private Player throwingPlayer;
		[SerializeField] private Type _request;
		[SerializeField] private string requestType = "";

		public override string Label => "J";

		/// <summary>
		/// Przechowuje informację o żądaniu gracza.
		/// </summary>
		public Type Request
		{
			get => _request;
			set
			{
				string cardNumber = "";
				switch (value.Name)
				{
					case "Five":
						cardNumber = "5";
						break;
					case "Six":
						cardNumber = "6";
						break;
					case "Seven":
						cardNumber = "7";
						break;
					case "Eight":
						cardNumber = "8";
						break;
					case "Nine":
						cardNumber = "9";
						break;
					case "Ten":
						cardNumber = "10";
						break;
				}

				transform.Find("Card/Front/Jack").GetComponentInChildren<TMP_Text>().text = cardNumber;
				requestType = value.Name;
				_request = value;
			}
		}

		/// <summary>
		/// Przechowuje informację kto rzucił kartę
		/// </summary>
		public Player ThrowingPlayer { get => throwingPlayer; protected set => requestType = value.ToString(); }

		public override void CounterPlay()
		{
			throwingPlayer = Game.instance.CurrentPlayer;
			Game.instance.actionChain.Add(this);
			Game.instance.Pile.AddToPile(this);
			Game.instance.actionChain.RemoveAll(c => c != Game.instance.actionChain.Last());
			Game.instance.CurrentPlayer.ChooseJackRequest(this);
		}

		public override void Effect()
		{
			Game.instance.actionChain.RemoveAll(c => c != Game.instance.actionChain.Last());
			if (Game.instance.CurrentPlayer == throwingPlayer)
			{
				Game.instance.actionChain.Clear();
			}
			Game.instance.CurrentPlayer.JackEffect(this);
		}

		public override bool IsCounterTo(Card card)
		{
			return card is Jack;
		}

		public override void Play()
		{
			throwingPlayer = Game.instance.CurrentPlayer;
			Game.instance.Pile.AddToPile(this);
			Game.instance.actionChain.Add(this);
			Game.instance.CurrentPlayer.ChooseJackRequest(this);
		}

		public override void Reset()
		{
			_request = null;
			throwingPlayer = null;
			try { transform.Find("Card/Front/Jack").GetComponentInChildren<TMP_Text>().text = ""; } catch { Debug.Log(this.name); }
		}
	}
}
