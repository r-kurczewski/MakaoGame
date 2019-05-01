using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform cardFolder;

    [SerializeField] private bool _isComputer;
    [SerializeField] private List<Card> cards = new List<Card>();
    public bool isComputer
    {
        get
        {
            return _isComputer;
        }
        private set
        {
            _isComputer = value;
        }
    }

    void Start()
    {

    }

    public void GiveCard(Card card)
    {
        card.transform.SetParent(cardFolder, false);
        card.transform.localScale = Vector3.one;
        if (isComputer) card.hideOnStart = true;
        Debug.Log("Dodanie karty");
        cards.Add(card);
    }

    void SortCards()
    {

    }

    public void MakeAMove()
    {
        if (isComputer)
        {

        }
        else
        {

        }
    }

    void Update()
    {
        
    }
}
