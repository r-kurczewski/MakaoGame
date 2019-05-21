﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MakaoGame;
using System;
using System.Collections.Generic;

[SelectionBase]
public abstract class Card : MonoBehaviour, IPointerDownHandler
{
    public GameObject Obj { get; private set; }

    [SerializeField] protected CardColor _cardColor;
    public CardColor CardColor { get => _cardColor; private set => _cardColor = value; }
    [SerializeField] protected bool _hasEffect;
    public abstract bool HasEffect { get; }

    public abstract char Label { get; }

    private bool hideOnStart;

    void Start()
    {
        foreach (Transform child in transform) DestroyImmediate(child.gameObject);
        Obj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
        Obj.name = "Card";
        Obj.transform.localPosition = Vector3.one;
        if (hideOnStart) Hide();
        foreach (var label in GetComponentsInChildren<TMP_Text>())
        {
            label.text = Label.ToString();
        }

        Color color;
        char symbolChar;
        switch (CardColor)
        {
            case CardColor.Heart:
                color = Color.red;
                symbolChar = '{';
                break;

            case CardColor.Tile:
                color = Color.red;
                symbolChar = '[';
                break;

            case CardColor.Clover:
                color = Color.black;
                symbolChar = ']';
                break;

            case CardColor.Pike:
                color = Color.black;
                symbolChar = '}';
                break;

            default:
                color = Color.black;
                symbolChar = ' ';
                Debug.LogError("Wrong color of card.");
                break;
        }
        RectTransform rect = gameObject.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(75, 100);
        TMP_Text label1 = transform.Find("Card/Front/Label1").GetComponent<TMP_Text>();
        TMP_Text label2 = transform.Find("Card/Front/Label2").GetComponent<TMP_Text>();
        TMP_Text symbol = transform.Find("Card/Front/Symbol").GetComponent<TMP_Text>();
        label1.text = Label.ToString();
        label2.text = Label.ToString();
        symbol.text = symbolChar.ToString();
        foreach (var label in GetComponentsInChildren<TMP_Text>())
        {
            label.color = color;
        }
    }

    public abstract void Effect();

    public abstract bool IsCounter(Card card);

    public static T Create<T>(CardColor color) where T : Card
    {
        T card = new GameObject().AddComponent<T>();
        card.CardColor = color;
        card.name = $"{card.GetType().ToString()} {card.CardColor.ToString()}";
        return card;
    }

    
    public void Hide()
    {
        try
        {
            transform.Find("Card/Back").GetComponent<Image>().enabled = true;
        }
        catch (NullReferenceException)
        {
            hideOnStart = true;
        }
    }

    public void Show()
    {
        transform.Find("Card/Back").GetComponent<Image>().enabled = false;
    }

    public abstract void Reset();

    private readonly float maxTime = 0.5f;
    private float lastTimeClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        Game context = Game.context;
        if (context.CurrentPlayer == context.HumanPlayer && context.HumanPlayer == GetComponentInParent<Player>())
        {
            float deltaTime = Time.time - lastTimeClicked;

            if (deltaTime < maxTime)
            {
                Game.context.HumanPlayer.Play(this);

            }
            lastTimeClicked = Time.time;
        }
    }
}

namespace MakaoGame
{
    public enum CardColor { Heart, Tile, Clover, Pike }
}
