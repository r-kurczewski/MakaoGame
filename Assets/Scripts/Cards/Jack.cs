using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Jack : Card
    {
        [SerializeField] private Card _request;

        public override string Label => "J";

        public Card Request { get => _request; private set => _request = value; }

        

        public override void Effect()
        {

        }

        public override bool IsCounterTo(Card card)
        {
            return card.GetType() == GetType() || (Request && Request.GetType() == card.GetType());
        }

        public override void Reset()
        {
            Request = null;
        }
    }
}
