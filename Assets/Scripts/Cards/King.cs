using static MakaoGame.CardSuit;

namespace MakaoGame.Cards
{
    /// <summary>
    /// Implementuje klasę <see cref="Card"/>
    /// </summary>
    public class King : Card
	{
		public override string Label => "K";

		public override void CounterPlay()
		{
			if (CardColor is CardSuit.Pike)
			{
				Game.instance.Pile.AddToPile(this);
				Game.instance.actionChain.Add(this);
				Game.instance.SetCounterClockwise();
			}
			else
			{
				base.CounterPlay();
			}
		}

		public override void Effect()
		{
			switch (CardColor)
			{
				case Heart:
				case Pike:
					for (int i = 0; i < 5; i++)
					{
						Game.instance.CurrentPlayer.GiveCard(Game.instance.Deck.DrawCard());
					}
					Game.instance.actionChain.Remove(this);
					Game.instance.CurrentPlayer.finishTurn = true;
					break;
			}
		}

		public override bool IsCounterTo(Card card)
		{
			if (CardColor is Clover || CardColor is Tile) return false;
			if (card is King && (card.CardColor is Heart || card.CardColor is Pike)) return true;
			else if ((card is Two || card is Three) && card.CardColor == CardColor) return true;
			else return false;
		}

		public override void Play()
		{
			Game.instance.Pile.AddToPile(this);
			if (CardColor == Pike)
			{
				Game.instance.actionChain.Add(this);
				Game.instance.SetCounterClockwise();
			}
			else if (CardColor is Heart)
			{
				Game.instance.actionChain.Add(this);
			}
			Game.instance.CurrentPlayer.finishTurn = true;
		}
	}
}
