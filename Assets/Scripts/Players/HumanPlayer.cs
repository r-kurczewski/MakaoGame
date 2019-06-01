using MakaoGame.GUI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public override void ApplyAction()
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.PassAction();
                return;
            }
            var action = Game.context.Pile.actionChain;
            Card toCounter = action.LastOrDefault();
            if (cards.FirstOrDefault(c => c.IsCounterTo(toCounter)))
            {
                var actionWindow = ListWindow.Create(action, "Czy chcesz skontrować karty:");

                // skontruj kartę
                actionWindow.Accept.onClick.AddListener(delegate
                {
                    var pickWindow = CardPickWindow.Create(cards.Where(c => c.IsCounterTo(toCounter)).ToList(), "Wybierz kartę którą chcesz rzucić:");
                    pickWindow.Accept.onClick.AddListener(delegate
                    {
                        Counter(pickWindow.Pick.original);
                        pickWindow.Close();
                    });
                    pickWindow.Decline.onClick.AddListener(delegate
                    {
                        AcceptAction();
                        Game.context.EndPlayerTurn();
                        pickWindow.Close();
                    });
                    actionWindow.Close();
                });

                // przyjmij akcję
                actionWindow.Decline.onClick.AddListener(delegate
                {
                    AcceptAction();
                    Game.context.EndPlayerTurn();
                    actionWindow.Close();
                });
            }
            else
            {
                AcceptAction();
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

        public override void Win()
        {
            SceneLoader.Load("Win");
        }

        public override void Lose()
        {
            SceneLoader.Load("Lose");
        }

        public override void AceEffect(Ace card)
        {
            var  window = AceEffectWindow.Create();
            window.Accept.onClick.AddListener(delegate
            {
                card.ChangeColor(window.Pick.color);
                Game.context.EndPlayerTurn();
                window.Close();
            });
            window.Decline.onClick.AddListener(delegate
            {
                window.Close();
            });
        }
    }
}