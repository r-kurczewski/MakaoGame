using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaoGame
{
    class Six : Card
    {
        public override string Label => "6";

        public override bool IsCounterTo(Card card)
        {
            return false;
        }
    }
}
