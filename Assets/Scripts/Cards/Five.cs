using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaoGame
{
    class Five : Card
    {
        public override string Label => "5";

        public override bool IsCounterTo(Card card)
        {
            return false;
        }
    }
}
