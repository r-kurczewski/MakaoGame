using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Jack : Card
    {
        [SerializeField] private Card _request;

        public override char Label => 'J';

        public override bool HasEffect => false;

        public Card Request { get => _request; private set => _request = value; }


        public override void Effect()
        {

        }

        public override bool IsCounter(Card card)
        {
            return card.GetType() == GetType() || (Request && Request.GetType() == card.GetType());
        }

        public override void Reset()
        {

        }
    }
}
