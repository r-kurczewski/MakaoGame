using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Two : Card
    {
        public override string Label => "2";

        public override void Effect()
        {
            var context = Game.context;
            for (int i = 0; i < 2; i++)
                context.CurrentPlayer.GiveCard(context.Deck.DrawCard());
        }

        public override bool IsCounterTo(Card card)
        {
            var type = card.GetType();
            return type == typeof(Two) || (type == typeof(Three) && CardColor == card.CardColor);
        }

        public override void Play()
        {
            Game.context.Pile.AddToPile(this);
            Game.context.Pile.actionChain.Add(this);
            Game.context.PassAction();
        }

    }
}
