using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoGame;

public class Game : MonoBehaviour
{
    [SerializeField] private bool clockwise = true;
    [SerializeField] private int currentPlayer = 0;
    [SerializeField] private PlayerHand[] Players = new PlayerHand[4];
    [SerializeField] private Deck deck;
    [SerializeField] private Pile pile;

    void Start()
    {
        Debug.Log("Game started...");
    }

    
}
