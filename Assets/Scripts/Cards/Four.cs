using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Four : Card
    {
        public override string Label => "4";

        public override void Effect()
        {
            Game.context.CurrentPlayer.SkipTurn++;
            Game.context.actionChain.Remove(this);
        }

        public override bool IsCounterTo(Card card)
        {
            return card.GetType() == GetType();
        }

        public override void Play()
        {
            Game.context.Pile.AddToPile(this);
            Game.context.actionChain.Add(this);
            Game.context.PassAction();
        }
    }
}
