using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void DrawCard()
    {
        var card = Game.context.Deck.DrawCard();
        GiveCard(card);
        Debug.Log($"{this.name} draws {card.name}");
    }

    public override void MakeAMove()
    {
        Card card;
        if (PlayableCards.Count > 0)
        {
            card = PlayableCards[Random.Range(0, PlayableCards.Count)];
            Play(card);
            Debug.Log($"{this.name} plays {card.name}");
        }
        else
        {
            DrawCard();
            Game.context.EndPlayerTurn();

        }
    }

    public override void ApplyAction(List<Card> action)
    {
        Card toCounter = action.Last();
        bool wantToCounter = true;
        if (cards.FirstOrDefault(x => toCounter.IsCounter(x)) != null && wantToCounter)
        {
            List<Card> counters = cards.Where(c => c.IsCounter(toCounter)).ToList();
            Card counter = counters[Random.Range(0, counters.Count)];
            Counter(counter);
            action.Add(counter);
            Debug.Log($"{this.name} counter card {toCounter.name} with {counter.name}");
            Game.context.PassAction(action);
            return;
        }
        else
        {
            foreach (var card in action)
            {
                card.Effect();
                Debug.Log($"{this.name} takes an action.");
            }
            Game.context.EndPlayerTurn();
        }
    }
}
