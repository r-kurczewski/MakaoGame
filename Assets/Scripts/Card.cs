using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MakaoGame;
using System;
using System.Collections.Generic;

namespace MakaoGame
{
    [SelectionBase]
    public abstract partial class Card : MonoBehaviour, IPointerDownHandler
    {
        public GameObject Obj { get; private set; }

        [SerializeField] protected CardSuit _cardColor;
        public virtual CardSuit CardColor { get => _cardColor; protected set => _cardColor = value; }

        public abstract string Label { get; }

        private bool hideOnStart;

        void Start()
        {
            foreach (Transform child in transform) DestroyImmediate(child.gameObject);
            Obj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
            Obj.name = "Card";
            Obj.transform.localPosition = Vector3.one;
            if (hideOnStart) Hide();


            RectTransform rect = gameObject.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(75, 100);
            TMP_Text label1 = transform.Find("Card/Front/Label1").GetComponent<TMP_Text>();
            TMP_Text label2 = transform.Find("Card/Front/Label2").GetComponent<TMP_Text>();
            TMP_Text symbol = transform.Find("Card/Front/Symbol").GetComponent<TMP_Text>();
            label1.text = Label.ToString();
            label2.text = Label.ToString();
            GetSymbolParameters(CardColor, out char symbolString, out Color color);
            symbol.text = symbolString.ToString();
            foreach (var label in GetComponentsInChildren<TMP_Text>())
            {
                label.color = color;
            }
        }

        public virtual void Play()
        {
            Game.context.Pile.AddToPile(this);
            Game.context.EndPlayerTurn();
        }

        public virtual void CounterPlay()
        {
            Game.context.Pile.AddToPile(this);
            Game.context.Pile.actionChain.Add(this);
            Game.context.PassAction();
        }

        public virtual void Effect()
        {

        }

        public abstract bool IsCounterTo(Card card);

        public static T Create<T>(CardSuit color) where T : Card
        {
            T card = new GameObject().AddComponent<T>();
            card.CardColor = color;
            card.name = $"{card.GetType().Name} {card.CardColor.ToString()}";
            return card;
        }

        public void Hide()
        {
            try
            {
                transform.Find("Card/Back").GetComponent<Image>().enabled = true;
            }
            catch (NullReferenceException)
            {
                hideOnStart = true;
            }
        }

        public void Show()
        {
            transform.Find("Card/Back").GetComponent<Image>().enabled = false;
        }

        public virtual void Reset()
        {

        }

        private readonly float maxTime = 0.5f;
        private float lastTimeClicked;

        public void OnPointerDown(PointerEventData eventData)
        {
            Game context = Game.context;
            var topCard = Game.context.Pile.TopCard;
            if (context.CurrentPlayer == context.HumanPlayer // tura gracza
                && context.HumanPlayer == GetComponentInParent<Player>() // karta w ręku gracza
                && context.HumanPlayer.SkipTurn == 0 // gracz nie musi czekać tury
                && (topCard == null || topCard.GetType() == GetType() || topCard.CardColor == CardColor)) //karta pasuje do koloru lub figury
            {
                float deltaTime = Time.time - lastTimeClicked;

                if (deltaTime < maxTime)
                {
                    Game.context.HumanPlayer.Play(this);

                }
                lastTimeClicked = Time.time;
            }
        }

        public static void GetSymbolParameters(CardSuit cardColor, out char symbolstring, out Color color)
        {
            switch (cardColor)
            {
                case CardSuit.Heart:
                    color = Color.red;
                    symbolstring = '{';
                    break;

                case CardSuit.Tile:
                    color = Color.red;
                    symbolstring = '[';
                    break;

                case CardSuit.Clover:
                    color = Color.black;
                    symbolstring = ']';
                    break;

                case CardSuit.Pike:
                    color = Color.black;
                    symbolstring = '}';
                    break;

                default:
                    color = Color.blue;
                    symbolstring = 'E';
                    Debug.LogError("Wrong color of card.");
                    break;
            }
        }
    }

    public enum CardSuit { Heart, Tile, Clover, Pike }
}
