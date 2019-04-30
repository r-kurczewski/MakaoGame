using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();

    public Card TopCard { get { return cards.Last(); } }

    void Start()
    {

    }
}
