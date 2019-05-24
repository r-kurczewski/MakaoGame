using MakaoGame.GUI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame
{
    public class HumanPlayer : Player
    {
        public override void GiveCard(Card card)
        {
            card.transform.SetParent(cardFolder, false);
            card.transform.localScale = Vector3.one;
            cards.Add(card);
        }

        public override void ApplyAction(List<Card> action)
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.PassAction(action);
                return;
            }
            Card toCounter = action.LastOrDefault();
            if (cards.FirstOrDefault(x => toCounter.IsCounter(x)))
            {
                var actionWindow = ActionWindow.Create(action);

                actionWindow.Accept.onClick.AddListener(delegate
                {
                    AcceptAction(action);
                    actionWindow.Close();
                });

                actionWindow.Decline.onClick.AddListener(delegate
                {
                    var pickWindow = CardPickWindow.Create(cards.Where(c => c.IsCounter(toCounter)).ToList());
                    pickWindow.Accept.onClick.AddListener(delegate
                    {
                        Counter(pickWindow.Pick.original);
                        Game.context.PassAction(action);
                        pickWindow.Close();
                    });
                    pickWindow.Decline.onClick.AddListener(delegate
                    {
                        AcceptAction(action);
                        Game.context.EndPlayerTurn();
                        pickWindow.Close();
                    });
                    actionWindow.Close();
                });
            }
            else
            {
                AcceptAction(action);
                Game.context.EndPlayerTurn();
            }
        }

        public override void MakeAMove()
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.EndPlayerTurn();
            }
        }
    }
}