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

        public override void ApplyAction()
        {
            if (SkipTurn > 0)
            {
                Wait();
                Game.context.PassAction();
                return;
            }
            Card toCounter = Game.context.actionChain.LastOrDefault();
            if (cards.FirstOrDefault(c => c.IsCounterTo(toCounter)))
            {
                var actionWindow = ListWindow.Create(Game.context.actionChain, "Czy chcesz skontrować karty:");

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
                        pickWindow.Close();
                    });
                    actionWindow.Close();
                });

                // przyjmij akcję
                actionWindow.Decline.onClick.AddListener(delegate
                {
                    AcceptAction();
                    actionWindow.Close();
                });
            }
            else
            {
                AcceptAction();
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

        public override void ChooseAceColor(Ace card)
        {
            var window = AceEffectWindow.Create();
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

        public override void ChooseJackRequest(Jack jack)
        {
            var window = JackRequestWindow.Create();
            window.Accept.onClick.AddListener(delegate
            {
                jack.Request = window.Pick.original.GetType();
                Game.context.PassAction();
                window.Close();
            });
            window.Decline.onClick.AddListener(delegate
            {
                Game.context.actionChain.Clear();
                Game.context.EndPlayerTurn();
                window.Close();
            });
        }

        public override void JackEffect(Jack jack)
        {
            List<Card> toPlay = cards.Where(c => c.GetType() == jack.Request).ToList();

            if (toPlay.Count > 0)
            {
                var window = JackEffectWindow.Create(cards.Where(c => jack.Request == c.GetType()).ToList());
                window.Accept.onClick.AddListener(delegate
                {
                    Game.context.Pile.AddToPile(window.Pick.original);
                    cards.Remove(window.Pick.original);
                    PassJack(jack);
                    window.Close();
                });
                window.Decline.onClick.AddListener(delegate
                {
                    DrawACard();
                    PassJack(jack);
                    window.Close();
                });
            }
            else
            {
                DrawACard();
                PassJack(jack);
            }
        }

        private void PassJack(Jack jack)
        {
            if (jack.ThrowingPlayer == this)
            {
                Game.context.actionChain.Clear();
                Game.context.EndPlayerTurn();
            }
            else
            {
                Game.context.PassAction();
            }
        }
    }
}