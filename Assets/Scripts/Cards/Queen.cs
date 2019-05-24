using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Queen : Card
    {
        public override char Label => 'Q';

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
}
