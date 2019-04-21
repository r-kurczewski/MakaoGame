using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnoGame;

[SelectionBase]
public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> cards = new List<Card>();

    void Start()
    {
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            {
                cards.Add(Card.Create<Ace>(color));
                cards.Add(Card.Create<Jack>(color));
                cards.Add(Card.Create<Queen>(color));
                cards.Add(Card.Create<King>(color));
            }
        }
        foreach (var card in cards)
        {
            card.transform.SetParent(transform);
            card.name = "Card";
            card.gameObject.SetActive(false);
        }
    }

    public void Shuffle()
    {

    }
}
