using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Nine : Card
    {
        public override string Label => "9";

        public override bool IsCounterTo(Card card)
        {
            return false;
        }
    }
}
