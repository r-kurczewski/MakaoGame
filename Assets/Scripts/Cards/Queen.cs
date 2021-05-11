namespace MakaoGame.Cards
{
    /// <summary>
    /// Implementuje klasę <see cref="Card"/>
    /// </summary>
    public class Queen : Card
	{
		public override string Label => "Q";

		public override void CounterPlay()
		{
			Game.instance.Pile.AddToPile(this);
			Game.instance.actionChain.Clear();
			Game.instance.CurrentPlayer.finishTurn = true;
		}

		public override bool IsCounterTo(Card card)
		{
			return card.CardColor == CardColor;
		}
	}
}
