using MakaoGame.GUI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame
{
    [SelectionBase]
    public class AIPlayer : Player
    {
        public override void GiveCard(Card card)
        {
            card.transform.SetParent(cardFolder, false);
            card.transform.localScale = Vector3.one;
            cards.Add(card);
            //card.Hide();
        }

        public override void MakeAMove()
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.EndPlayerTurn();
                return;
            }
            Card card;
            if (PlayableCards.Count > 0)
            {
                card = PlayableCards[Random.Range(0, PlayableCards.Count)];
                Play(card);
            }
            else
            {
                DrawACard();
                Game.context.EndPlayerTurn();

            }
        }

        public override void ApplyAction()
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.PassAction();
                return;
            }
            Card toCounter = Game.context.Pile.actionChain.Last();
            bool wantToCounter = true;
            var counters = cards.Where(c => c.IsCounterTo(toCounter)).ToList();
            if (counters.Count > 0 && wantToCounter)
            {
                Card counter = counters[Random.Range(0, counters.Count)];
                Counter(counter);
                return;
            }
            else
            {
                AcceptAction();
                Game.context.EndPlayerTurn();
            }
        }

        public override void Win()
        {
            ActiveTurnIndicator.color = Color.grey;
        }

        public override void Lose()
        {

        }

        public override void AceEffect(Ace card)
        {
            card.ChangeColor((CardSuit)Random.Range(0, System.Enum.GetValues(typeof(CardSuit)).Length));
            Game.context.EndPlayerTurn();
        }
    }
}
