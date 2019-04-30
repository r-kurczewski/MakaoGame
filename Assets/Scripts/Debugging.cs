using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoGame;

public class Debugging : MonoBehaviour
{
    void Start()
    {
        Debug.Log("OK");
        for (int i = 0; i < 5; i++)
        {
            var card = Card.Create<Ace>(CardColor.Clover);
            card.transform.SetParent(transform, false);
        }
    }

    void Update()
    {

    }
}
