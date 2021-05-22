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
    /// Klasa odpowiadająca za wykonywanie losowych ruchów komputerów.
    /// </summary>
    [SelectionBase]
    public class RandomAIPlayer : Player
    {
        private const int actionDelay = 2;

        public override void GiveCard(Card card)
        {
            base.GiveCard(card);
            card.Hide(); // Debug
        }

        public override IEnumerator Move()
        {
            yield return new WaitForSeconds(actionDelay);
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
            else if (PlayableCards.Count > 0)
            {
                Card card = PlayableCards[Random.Range(0, PlayableCards.Count)];
                Play(card);
            }
            else
            {
                DrawCardAndEndTurn();
            }
        }

        public override void ApplyAction()
        {
            Card toCounter = Game.instance.actionChain.Last();
            var counters = cards.Where(c => c.IsCounterTo(toCounter)).ToList();
            bool wantToCounter = true;

            if (counters.Count > 0 && wantToCounter)
            {
                Card counter = counters[Random.Range(0, counters.Count)];
                Counter(counter);
            }
            else
            {
                AcceptAction();
            }
            
        }

        public override void Win()
        {
            ActiveTurnIndicator.color = Color.grey;
            nameLabel.color = Color.gray;
        }

        public override void Lose()
        {

        }

        public override void ChooseAceColor(Ace ace)
        {
            ace.ChangeColor((CardSuit)Random.Range(0, System.Enum.GetValues(typeof(CardSuit)).Length));
            finishTurn = true;
        }

        public override void ChooseJackRequest(Jack jack)
        {
            int cardNumber = Random.Range(5, 10 + 1);
            System.Type cardType = null;
            switch (cardNumber)
            {
                case 5:
                    cardType = typeof(Five);
                    break;
                case 6:
                    cardType = typeof(Six);
                    break;
                case 7:
                    cardType = typeof(Seven);
                    break;
                case 8:
                    cardType = typeof(Eight);
                    break;
                case 9:
                    cardType = typeof(Nine);
                    break;
                case 10:
                    cardType = typeof(Ten);
                    break;
            }
            jack.Request = cardType;
            finishTurn = true;
            Debug.Log($"{this.name} requests {cardType.Name}");
        }

        public override void JackEffect(Jack jack)
        {
            bool playCard = true;
            List<Card> toPlay = cards.Where(c => c.GetType() == jack.Request).ToList();

            if (playCard && toPlay.Count > 0)
            {
                Card pick = toPlay[Random.Range(0, toPlay.Count)];
                Play(pick);
                Debug.Log($"{this.name} fulfill request with {pick.name}");
            }
            else
            {
                DrawCardAndEndTurn();
            }
        }
    }
}
