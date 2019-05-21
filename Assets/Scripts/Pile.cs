using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class Pile : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();
    public List<Card> actionChain;

    public Card TopCard { get { return cards.LastOrDefault(); } }

    private void AddToPile(Card card)
    {
        cards.Add(card);
        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;
        card.Obj.transform.localPosition = Vector3.zero;
        card.Show();
    }

    public bool PutCard(Card card)
    {
        if (TopCard == null || TopCard.GetType() == card.GetType() || TopCard.CardColor == card.CardColor)
        {
            AddToPile(card);
            if (card.HasEffect)
            {
                Game.context.PassAction(new List<Card> { card });
            }
            else
                Game.context.EndPlayerTurn();
            return true;
        }
        else
        {
            Debug.LogWarning($"Wrong card: {TopCard.name} on pile, {card.name} to put");
            return false;
        }
    }

    public void PutCounterCard(Card card)
    {
        AddToPile(card);
    }

    public List<Card> ClearPile()
    {
        var pileCards = cards.Take(cards.Count - 1).ToList();
        cards.RemoveAll(card => pileCards.Contains(card));
        return pileCards;
    }
}
