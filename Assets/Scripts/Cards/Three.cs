namespace MakaoGame.Cards
{
    /// <summary>
    /// Implementuje klasę <see cref="Card"/>
    /// </summary>
    public class Three : Card
	{
		public override string Label => "3";

		public override void Effect()
		{
			var game = Game.instance;
			for (int i = 0; i < 3; i++)
			{
				game.CurrentPlayer.GiveCard(game.Deck.DrawCard());
			}
			game.actionChain.Remove(this);
			game.CurrentPlayer.finishTurn = true;
		}

		public override bool IsCounterTo(Card card)
		{

			return card is Three || ((card is Two || card is King) && CardColor == card.CardColor);
		}

		public override void Play()
		{
			Game.instance.actionChain.Add(this);
			base.Play();
		}
	}
}
