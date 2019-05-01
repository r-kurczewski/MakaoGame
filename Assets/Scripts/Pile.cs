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
            Debug.Log("OK");
            return true;
        }
        else
        {
            Debug.Log("Wrong card");
            return false;
        }
    }

    void Start()
    {

    }
}
