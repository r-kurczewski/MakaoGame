using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Seven : Card
    {
        public override string Label => "7";

        public override bool IsCounterTo(Card card)
        {
            return false;
        }
    }
}
