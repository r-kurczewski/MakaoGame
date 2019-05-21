using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MakaoGame;

[SelectionBase]
public class Deck : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text cardNumber = default;

    [SerializeField] private List<Card> cards = new List<Card>();

    void Start()
    {
        cardNumber.text = cards.Count.ToString();
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

    public void AddCard(Card card)
    {
        card.transform.SetParent(transform.Find("Cards"));
        card.transform.rotation = Quaternion.identity;
        card.gameObject.SetActive(false);
    }

    public void AddCardsFromPile()
    {
        foreach (var card in Game.context.Pile.ClearPile())
        {
            AddCard(card);
            cards.Add(card);
        }
    }

    public void Create()
    {
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            {
                cards.Add(Card.Create<Two>(color));
                cards.Add(Card.Create<Three>(color));
                cards.Add(Card.Create<Four>(color));
                cards.Add(Card.Create<Ace>(color));
                cards.Add(Card.Create<Jack>(color));
                cards.Add(Card.Create<Queen>(color));
                cards.Add(Card.Create<King>(color));

                cards.Add(Card.Create<Two>(color));
                cards.Add(Card.Create<Three>(color));
                cards.Add(Card.Create<Four>(color));
                cards.Add(Card.Create<Ace>(color));
                cards.Add(Card.Create<Jack>(color));
                cards.Add(Card.Create<Queen>(color));
                cards.Add(Card.Create<King>(color));
            }
        }
        foreach (var card in cards)
        {
            AddCard(card);
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0) AddCardsFromPile();
        Card card = cards.Last();
        cards.Remove(card);
        cardNumber.text = cards.Count.ToString();
        card.gameObject.SetActive(true);
        return card;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Game.context.CurrentPlayer == Game.context.HumanPlayer)
        {
            var card = DrawCard();
            Game.context.HumanPlayer.GiveCard(card);
            Debug.Log($"{Game.context.HumanPlayer.name} draws {card.name}");
            Game.context.EndPlayerTurn();
        }

        else Debug.LogWarning("Now is not a player turn!");
    }
}
