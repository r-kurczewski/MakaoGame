using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four : Card
{
    public override char Label => '4';

    public override void Effect()
    {

    }

    public override bool IsCounter(Card card)
    {
        return card.GetType() == GetType();
    }
}
