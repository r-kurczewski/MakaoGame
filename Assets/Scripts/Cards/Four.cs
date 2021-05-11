namespace MakaoGame.Cards
{
	/// <summary>
	/// Implementuje klasę <see cref="Card"/>
	/// </summary>
	public class Four : Card
	{
		public override string Label => "4";

		public override void Effect()
		{
			Game.instance.CurrentPlayer.SkipTurns++;
			Game.instance.actionChain.Remove(this);
		}

		public override bool IsCounterTo(Card card)
		{
			return card is Four;
		}

		public override void Play()
		{
			Game.instance.actionChain.Add(this);
			base.Play();
		}
	}
}
