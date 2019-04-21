using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : Card
{
    public override char Label
    {
        get
        {
            return 'J';
        }
    }

    public override void Effect(Game game)
    {
        throw new System.NotImplementedException();
    }
}
