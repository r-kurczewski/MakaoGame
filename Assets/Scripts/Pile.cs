using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();

    public Card TopCard { get { return cards.LastOrDefault(); } }

    public bool PutCard(Card card)
    {
        if(TopCard == null || TopCard.GetType() == card.GetType() || TopCard.CardColor == card.CardColor)
        {
            cards.Add(card);
            card.transform.SetParent(transform, false);
            card.transform.localPosition = Vector3.zero;
            card.Obj.transform.localPosition = Vector3.zero;
            card.Show();
            Debug.Log($"{Game.context.CurrentPlayer.name} plays {card.GetType().ToString()} {card.CardColor}.");
            Game.context.EndPlayerTurn();
            Game.context.CurrentPlayer.ApplyCardEffect(new List<Card>() {card });
            return true;
        }
        else
        {
            Debug.LogWarning("Wrong card");
            return false;
        }
    }

    public List<Card> ClearPile()
    {
        var pileCards = cards.Take(cards.Count - 1).ToList();
        cards.RemoveAll(card => pileCards.Contains(card));
        return pileCards;
    }

    void Start()
    {

    }
}
