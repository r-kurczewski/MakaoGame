using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Ten : Card
    {
        public override string Label => "10";

        public override bool IsCounterTo(Card card)
        {
            return false;
        }
    }
}
