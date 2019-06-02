using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class King : Card
    {
        public override string Label => "K";

        public override void CounterPlay()
        {
            if (CardColor == CardSuit.Pike)
            {
                Game.context.Pile.AddToPile(this);
                Game.context.actionChain.Add(this);
                Game.context.PassAction(false);
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
                case CardSuit.Heart:
                case CardSuit.Pike:
                    for (int i = 0; i < 5; i++)
                    {
                        Game.context.CurrentPlayer.GiveCard(Game.context.Deck.DrawCard());
                        Game.context.actionChain.Remove(this);
                    }
                    break;
            }
        }

        public override bool IsCounterTo(Card card)
        {
            return ((card.GetType() == typeof(King)) && ((CardColor == CardSuit.Heart) || (CardColor == CardSuit.Pike)))
                    || ((card.GetType() == typeof(Two) || card.GetType() == typeof(Three)) && (CardColor == card.CardColor));
        }

        public override void Play()
        {
            Game.context.Pile.AddToPile(this);
            if (CardColor == CardSuit.Pike)
            {
                Game.context.actionChain.Add(this);
                Game.context.PassAction(false);
            }
            else if (CardColor == CardSuit.Heart)
            {
                Game.context.actionChain.Add(this);
                Game.context.PassAction();
            }
            else
            {
                Game.context.EndPlayerTurn();
            }
        }
    }
}
