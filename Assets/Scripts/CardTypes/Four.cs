using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four : Card
{
    public override char Label => '4';

    public override bool HasEffect => false;

    public override void Effect()
    {
        Game.context.CurrentPlayer.SkipTurn++;
    }

    public override bool IsCounter(Card card)
    {
        return false;
        //return card.GetType() == GetType();
    }

    public override void Reset()
    {

    }
}
