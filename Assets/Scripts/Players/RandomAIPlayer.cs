using MakaoGame.GUI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakaoGame
{
    [SelectionBase]
    public class RandomAIPlayer : Player
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
            Card toCounter = Game.context.actionChain.Last();
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
            }
        }

        public override void Win()
        {
            ActiveTurnIndicator.color = Color.grey;
        }

        public override void Lose()
        {

        }

        public override void ChooseAceColor(Ace card)
        {
            card.ChangeColor((CardSuit)Random.Range(0, System.Enum.GetValues(typeof(CardSuit)).Length));
            Game.context.EndPlayerTurn();
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
            Game.context.Pile.AddToPile(jack);
            Game.context.actionChain.Add(jack);
            Game.context.PassAction();
            Debug.Log($"{this.name} requests {cardType.Name}");
        }

        public override void JackEffect(Jack jack)
        {
            bool putCard = true;
            List<Card> toPlay = cards.Where(c => c.GetType() == jack.Request).ToList();

            if (putCard && toPlay.Count > 0)
            {
                Card card = toPlay[Random.Range(0, toPlay.Count)];
                Game.context.Pile.AddToPile(card);
                Debug.Log($"{this.name} fulfill request with {card.name}");
            }
            else
                DrawACard();

            if (jack.ThrowingPlayer == this)
            {
                Game.context.actionChain.Clear();
                Game.context.EndPlayerTurn();
            }
            else Game.context.PassAction();
        }
    }
}
