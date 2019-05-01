using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoGame;

public class Debugging : MonoBehaviour
{
    void Start()
    {
        GetComponent<Player>().GiveCard(Card.Create<Ace>(CardColor.Clover));
    }
}
