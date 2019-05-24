using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Four : Card
    {
        public override char Label => '4';

        public override bool HasEffect => true;

        public override void Effect()
        {
            Game.context.CurrentPlayer.SkipTurn++;
        }

        public override bool IsCounter(Card card)
        {
            return card.GetType() == GetType();
        }

        public override void Reset()
        {

        }
    }
}
