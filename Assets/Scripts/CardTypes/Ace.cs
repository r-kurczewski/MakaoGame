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

    public override void Effect(Game game)
    {
        throw new System.NotImplementedException();
    }
}
