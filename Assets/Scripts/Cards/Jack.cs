using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MakaoGame
{
    public class Jack : Card
    {
        [SerializeField] private Player throwingPlayer;
        [SerializeField] private Type _request;

        public override string Label => "J";

        public Type Request { get => _request; set => _request = value; }
        public Player ThrowingPlayer { get => throwingPlayer; protected set => throwingPlayer = value; }

        public override void CounterPlay()
        {
            
            throwingPlayer = Game.context.CurrentPlayer;
            Game.context.Pile.AddToPile(this);
            Game.context.actionChain.Clear();
            Game.context.actionChain.Add(this); 
            Game.context.CurrentPlayer.ChooseJackRequest(this);
        }

        public override void Effect()
        {
            Game.context.actionChain.Clear();
            Game.context.actionChain.Add(this);
            Game.context.CurrentPlayer.JackEffect(this);
        }

        public override bool IsCounterTo(Card card)
        {
            return card.GetType() == GetType();
        }

        public override void Play()
        {
            throwingPlayer = Game.context.CurrentPlayer;
            Game.context.CurrentPlayer.ChooseJackRequest(this);
        }

        public override void Reset()
        {
            Request = null;
            throwingPlayer = null;
        }
    }
}
