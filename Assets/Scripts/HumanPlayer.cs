using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanPlayer : Player
{
    public override void GiveCard(Card card)
    {
        card.transform.SetParent(cardFolder, false);
        card.transform.localScale = Vector3.one;
        cards.Add(card);
    }

    public override void ApplyAction(List<Card> actionCards)
    {
        Card toCounter = actionCards.LastOrDefault();
        if (cards.FirstOrDefault(x => toCounter.IsCounter(x)))
        {
            var actionWindow = ActionWindow.Create(actionCards);

            actionWindow.Accept.onClick.AddListener(delegate
            {
                AcceptAction(actionCards);
                actionWindow.Close();
            });

            actionWindow.Decline.onClick.AddListener(delegate
            {
                var pickWindow = CardPickWindow.Create(cards.Where(c => c.IsCounter(toCounter)).ToList());
                actionWindow.Close();
            });
        }
        else
        {
            AcceptAction(actionCards);
            Debug.Log($"{this.name} takes an action");
        }
    }

    private void AcceptAction(List<Card> action)
    {
        foreach (var card in action)
        {
            card.Effect();
        }
        Game.context.EndPlayerTurn();
    }

    public override void MakeAMove()
    {
        return;
    }
}