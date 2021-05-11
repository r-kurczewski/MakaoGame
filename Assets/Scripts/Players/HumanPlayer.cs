using MakaoGame.Cards;
using MakaoGame.GUI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame.Players
{
    /// <summary>
    /// Implementuje klasę <see cref="MakaoGame.Player"/>
    /// Klasa odpowiadająca za wykonywanie ruchów gracza.
    /// </summary>
    public class HumanPlayer : Player
    {
        public override void ApplyAction()
        {
            Card toCounter = Game.instance.actionChain.LastOrDefault();

            if (cards.Any(c => c.IsCounterTo(toCounter)))
            {
                var actionWindow = ListWindow.Create(Game.instance.actionChain, "Czy chcesz skontrować karty:");

                // potwierdź skontrowanie karty
                actionWindow.Accept.onClick.AddListener(delegate
                {
                    var pickWindow = CardPickWindow.Create(cards.Where(c => c.IsCounterTo(toCounter)).ToList(), "Wybierz kartę którą chcesz rzucić:");
                    pickWindow.Accept.onClick.AddListener(delegate
                         {
                             // kontrowanie
                             Counter(pickWindow.Pick.original);
                             pickWindow.Close();
                         });
                    pickWindow.Decline.onClick.AddListener(delegate
                         {
                             // rezygnacja z kontrowania i przyjęcie akcji
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

        public override IEnumerator Move()
        {
            if (SkipTurns > 0)
            {
                SkipAndEndTurn();
            }
            else if (Game.instance.actionChain.Count > 0)
            {
                ApplyAction();
                if (SkipTurns > 0)
                {
                    SkipAndEndTurn();
                }
            }
            while (!finishTurn)
            {
                yield return null;
            }
        }

        public override void Win()
        {
            FindObjectOfType<SceneLoader>().Win();
        }

        public override void Lose()
        {
            FindObjectOfType<SceneLoader>().Lose();
        }

        public override void ChooseAceColor(Ace card)
        {
            var window = AceEffectWindow.Create();
            window.Accept.onClick.AddListener(delegate
            {
                // zmień kolor
                card.ChangeColor(window.Pick.color);
                finishTurn = true;
                window.Close();
            });
            window.Decline.onClick.AddListener(delegate
            {
                // nie zmieniaj koloru
                finishTurn = true;
                window.Close();
            });
        }

        public override void ChooseJackRequest(Jack jack)
        {
            var window = JackRequestWindow.Create();
            window.Accept.onClick.AddListener(delegate
            {
                // żądaj
                jack.Request = window.Pick.original.GetType();
                finishTurn = true;
                window.Close();
            });
            window.Decline.onClick.AddListener(delegate
            {
                // nie żądaj
                Game.instance.actionChain.Clear();
                finishTurn = true;
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
                    // rzuć kartę danego typu
                    Card pick = window.Pick.original;
                    Play(pick);
                    window.Close();
                });
                window.Decline.onClick.AddListener(delegate
                {
                    // dobierz kartę
                    DrawCardAndEndTurn();
                    window.Close();
                });
            }
            else
            {
                DrawCardAndEndTurn();
            }
        }
    }
}