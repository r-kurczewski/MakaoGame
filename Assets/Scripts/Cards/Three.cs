using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var context = Game.context;
            for (int i = 0; i < 3; i++)
            {
                context.CurrentPlayer.GiveCard(context.Deck.DrawCard());
            }
                Game.context.actionChain.Remove(this);
        }

        public override bool IsCounterTo(Card card)
        {
            var type = card.GetType();
            return type == typeof(Three) || ((type == typeof(Two) || type == typeof(King)) && CardColor == card.CardColor);
        }

        public override void Play()
        {
            Game.context.Pile.AddToPile(this);
            Game.context.actionChain.Add(this);
            Game.context.PassAction();
        }
    }
}
