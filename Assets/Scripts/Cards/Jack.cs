using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MakaoGame
{
    public class Jack : Card
    {
        [SerializeField] private Player throwingPlayer;
        [SerializeField] private Type _request;
        [SerializeField] private string requestType = "";

        public override string Label => "J";

        public Type Request
        {
            get => _request;
            set
            {
                string cardNumber = "";
                switch (value.Name)
                {
                    case "Five":
                        cardNumber = "5";
                        break;
                    case "Six":
                        cardNumber = "6";
                        break;
                    case "Seven":
                        cardNumber = "7";
                        break;
                    case "Eight":
                        cardNumber = "8";
                        break;
                    case "Nine":
                        cardNumber = "9";
                        break;
                    case "Ten":
                        cardNumber = "10";
                        break;
                }

                transform.Find("Card/Front/Jack").GetComponentInChildren<TMP_Text>().text = cardNumber;
                requestType = value.Name;
                _request = value;
            }
        }
        public Player ThrowingPlayer { get => throwingPlayer; protected set => requestType = value.ToString(); }

        public override void CounterPlay()
        {
            throwingPlayer = Game.context.CurrentPlayer;
            Game.context.actionChain.Add(this);
            Game.context.Pile.AddToPile(this);
            Game.context.actionChain.RemoveAll(c => c != Game.context.actionChain.Last());
            Game.context.CurrentPlayer.ChooseJackRequest(this);
        }

        public override void Effect()
        {
            Game.context.actionChain.RemoveAll(c => c != Game.context.actionChain.Last());
            Game.context.CurrentPlayer.JackEffect(this);
        }

        public override bool IsCounterTo(Card card)
        {
            return card.GetType() == GetType();
        }

        public override void Play()
        {
            throwingPlayer = Game.context.CurrentPlayer;
            Game.context.Pile.AddToPile(this);
            Game.context.actionChain.Add(this);
            Game.context.CurrentPlayer.ChooseJackRequest(this);
        }

        public override void Reset()
        {
            Request = null;
            throwingPlayer = null;
            transform.Find("Card/Front/Jack").GetComponent<TMP_Text>().text = "";
        }
    }
}
