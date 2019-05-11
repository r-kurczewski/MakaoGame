using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ace : Card
{

    public override char Label
    {
        get
        {
            return 'A';
        }
    }

    public override void Effect()
    {

    }

    public override bool IsCounter(Card card)
    {
        return false;
    }
}
