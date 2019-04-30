using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnoGame;

[SelectionBase]
public class Deck : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text cardCounter;

    [SerializeField] private List<Card> cards = new List<Card>();
    //public int DeckSize { get { return cards.Count; } }

    void Start()
    {
        cardCounter.text = cards.Count.ToString();
    }

    public void Shuffle()
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < cards.Count; i++)
        {
            int pos = i + rand.Next(cards.Count - i);

            var temp = cards[pos];
            cards[pos] = cards[i];
            cards[i] = temp;
        }
    }

    public void AddCardsFromPile(Pile pile)
    {
        Create();
        Shuffle();
    }

    public void Create()
    {
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            {
                cards.Add(Card.Create<Two>(color));
                cards.Add(Card.Create<Three>(color));
                cards.Add(Card.Create<Ace>(color));
                cards.Add(Card.Create<Jack>(color));
                cards.Add(Card.Create<Queen>(color));
                cards.Add(Card.Create<King>(color));
            }
        }
        foreach (var card in cards)
        {
            card.transform.SetParent(transform.Find("Cards"));
            card.name = "Card";
            card.gameObject.SetActive(false);
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0) AddCardsFromPile(Game.context.Pile);
        Card card = cards.Last();
        cards.Remove(card);
        cardCounter.text = cards.Count.ToString();
        card.gameObject.SetActive(true);
        return card;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Game.context.CurrentPlayer == Game.context.HumanPlayer)
            Game.context.HumanPlayer.GiveCard(DrawCard());
        else Debug.LogWarning("Now is not a player turn!");
    }
}
