using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform cardFolder;

    [SerializeField] private bool _isComputer;
    [SerializeField] private int _skipTurn;
    [SerializeField] private List<Card> cards = new List<Card>();
    public List<Card> PlayableCards
    {
        get
        {
            var pileCard = Game.context.Pile.TopCard;
            if (pileCard == null) return cards;
            var type = pileCard.GetType();
            var color = pileCard.CardColor;
            return cards.Where(x => x.GetType() == type || x.CardColor == color).ToList();
        }
    }
    public bool IsComputer { get => _isComputer; private set => _isComputer = value; }
    public int SkipTurn { get => _skipTurn; private set => _skipTurn = value; }

    public void GiveCard(Card card)
    {
        card.transform.SetParent(cardFolder, false);
        card.transform.localScale = Vector3.one;
        //if (IsComputer) card.Hide();
        cards.Add(card);
    }

    private void SortCards()
    {

    }

    private void PlayACard(Card card)
    {
        card.Play();
        cards.Remove(card);
    }

    private void DrawCard()
    {
        Card card = Game.context.Deck.DrawCard();
        Debug.Log($"{this.name} drew a card.");
        GiveCard(card);
        Game.context.EndPlayerTurn();
    }

    public void MakeAMove()
    {
        if (IsComputer)
        {
            if (PlayableCards.Count > 0)
            {
                PlayACard(PlayableCards[Random.Range(0, PlayableCards.Count)]);
            }
            else
            {
                DrawCard();
            }
        }
        else
        {

        }
    }

    public void ApplyCardEffect(List<Card> cards)
    {
        Card toCounter = cards.LastOrDefault();
        if (Game.context.CurrentPlayer.cards.FirstOrDefault(x => toCounter.IsCounter(x)))
        {
            // ask if want to counter
            //Debug.Log("Can counter");
        }
        else
        {
            foreach (var card in cards)
            {
                card.Effect();
                //Debug.Log("Can't counter");
            }
        }
    }

}
