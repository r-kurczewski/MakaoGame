using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Game.context.Pile.AddToPile(this);
            Game.context.actionChain.Clear();
            Game.context.EndPlayerTurn();
        }

        public override bool IsCounterTo(Card card)
        {
            return card.CardColor == CardColor;
        }
    }
}
