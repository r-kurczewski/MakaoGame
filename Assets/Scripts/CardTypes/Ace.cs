using MakaoGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ace : Card
{

    public override char Label => 'A';

    public override bool HasEffect => false;

    [SerializeField] private CardColor _virtualColor;

    public new CardColor CardColor { get => _virtualColor; set => _virtualColor = value; }

    public override void Effect()
    {

    }

    public override bool IsCounter(Card card)
    {
        return false;
    }

    public override void Reset()
    {
        _virtualColor = _cardColor;
    }
}
