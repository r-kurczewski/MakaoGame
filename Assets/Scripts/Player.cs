using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected int _skipTurn;
    [SerializeField] protected List<Card> cards = new List<Card>();
    [SerializeField] protected Transform cardFolder;
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

    public int SkipTurn { get => _skipTurn; set => _skipTurn = value; }

    public abstract void GiveCard(Card card);

    public abstract void MakeAMove();

    private void SortCards()
    {

    }

    public abstract void ApplyAction(List<Card> cards);

    public void Play(Card card)
    {
        cards.Remove(card);
        Game.context.Pile.PutCard(card);
        Debug.Log($"{this.name} plays {card.name}");
    }

    public void Counter(Card card)
    {
        cards.Remove(card);
        Game.context.Pile.PutCounterCard(card);
        Debug.Log($"{this.name} plays {card.name}");
    }

}