using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Card
{
    public override char Label => 'Q';

    public override void Effect()
    {

    }

    public override bool IsCounter(Card card)
    {
        return false;
    }
}
