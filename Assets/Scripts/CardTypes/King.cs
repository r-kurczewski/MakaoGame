using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Card
{
    public override char Label => 'K';

    public override bool HasEffect => false;

    public override void Effect()
    {

    }

    public override bool IsCounter(Card card)
    {
        return false;
    }

    public override void Reset()
    {

    }
}
