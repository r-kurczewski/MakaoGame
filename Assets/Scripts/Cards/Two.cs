using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Two : Card
    {
        public override char Label => '2';

        public override bool HasEffect => true;

        public override void Effect()
        {
            var context = Game.context;
            for (int i = 0; i < 2; i++)
                context.CurrentPlayer.GiveCard(context.Deck.DrawCard());
        }

        public override bool IsCounter(Card card)
        {
            var type = card.GetType();
            return type == typeof(Two) || (type == typeof(Three) && CardColor == card.CardColor);
        }

        public override void Reset()
        {

        }
    }
}
