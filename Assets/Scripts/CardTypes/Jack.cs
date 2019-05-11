using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : Card
{
    public override char Label => 'J';

    public override void Effect()
    {

    }

    public override bool IsCounter(Card card)
    {
        return card.GetType() == GetType();
    }
}
