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

        public override void ApplyAction(List<Card> action)
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.PassAction(action);
                return;
            }
            Card toCounter = action.Last();
            bool wantToCounter = true;
            if (cards.FirstOrDefault(x => toCounter.IsCounter(x)) != null && wantToCounter)
            {
                List<Card> counters = cards.Where(c => c.IsCounter(toCounter)).ToList();
                Card counter = counters[Random.Range(0, counters.Count)];
                Counter(counter);
                action.Add(counter);
                Game.context.PassAction(action);
                return;
            }
            else
            {
                AcceptAction(action);
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
    }
}
