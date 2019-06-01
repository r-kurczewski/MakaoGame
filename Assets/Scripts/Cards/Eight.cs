using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Eight : Card
    {
        public override string Label => "8";

        public override bool IsCounterTo(Card card)
        {
            return false;
        }
    }
}
