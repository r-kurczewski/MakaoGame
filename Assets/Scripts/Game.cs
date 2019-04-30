using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnoGame;

public class Game : MonoBehaviour
{
    public static Game context;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private int currentPlayer = 0;
    [SerializeField] private Player[] _players;
    [SerializeField] private Deck _deck;
    [SerializeField] private Pile _pile;
    public Player[] Players
    {
        get
        {
            return _players;
        }
        private set
        {
            _players = value;
        }
    }
    public Deck Deck
    {
        get
        {
            return _deck;
        }
        private set
        {
            _deck = value;
        }
    }
    public Pile Pile
    {
        get
        {
            return _pile;
        }
        private set
        {
            _pile = value;
        }
    }
    public Player HumanPlayer { get { return Players.FirstOrDefault(p => p.isComputer == false); } }
    public Player PreviousPlayer { get { return !clockwise ? Players[(currentPlayer++) % 4] : Players[(4 + currentPlayer--) % 4]; } }
    public Player CurrentPlayer { get { return Players[currentPlayer]; } }
    public Player NextPlayer{ get { return clockwise ? Players[(currentPlayer++) % 4] : Players[(4 + currentPlayer--) % 4];} }

    void Start()
    {
        context = this;
        Debug.Log("Game started...");

        // Tworzenie talii
        Deck.Create();
        Deck.Shuffle();

        // Rozdanie kart graczom
        foreach (var player in Players)
        {
            for (int i = 0; i < 5; i++)
            {
                Card card = Deck.DrawCard();
                player.GiveCard(card);
            }
        }
    }
}
