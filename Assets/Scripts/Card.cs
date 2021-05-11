using MakaoGame.Players;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MakaoGame
{
    /// <summary>
    /// Główna klasa kart.
    /// </summary>
    [SelectionBase]
    public abstract class Card : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// Referencja do grafiki karty
        /// </summary>
        public GameObject Obj { get; private set; }

        [SerializeField] protected CardSuit _cardColor;
        /// <summary>
        /// Typ enum przechowujący informacje o kolorze karty.
        /// </summary>
        public virtual CardSuit CardColor { get => _cardColor; protected set => _cardColor = value; }

        /// <summary>
        /// Etykieta karty
        /// </summary>
        public abstract string Label { get; }

        public virtual bool TakingActionEndsTurn { get; } = true;

        private bool hideOnStart;

        /// <summary>
        /// Inicjalizacja karty
        /// </summary>
        protected void Start()
        {
            foreach (Transform child in transform) DestroyImmediate(child.gameObject);
            Obj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
            Obj.name = "Card";

            RectTransform rect = GetComponent<RectTransform>() ? GetComponent<RectTransform>() : gameObject.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(75, 100);
            Obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            if (hideOnStart) Hide();

            GetSymbolParameters(CardColor, out char symbolChar, out Color color);
            TMP_Text label1 = transform.Find("Card/Front/Label1").GetComponent<TMP_Text>();
            TMP_Text label2 = transform.Find("Card/Front/Label2").GetComponent<TMP_Text>();
            TMP_Text symbol = transform.Find("Card/Front/Symbol").GetComponent<TMP_Text>();
            TMP_Text ace = transform.Find("Card/Front/Ace").GetComponentInChildren<TMP_Text>();
            label1.text = Label.ToString();
            label1.color = color;
            label2.text = Label.ToString();
            label2.color = color;
            symbol.text = symbolChar.ToString();
            symbol.color = color;
            ace.color = color;
            ace.text = symbolChar.ToString();
        }

        /// <summary>
        /// Efekt po wyłożeniu karty na stos
        /// </summary>
        public virtual void Play()
        {
            Game.instance.Pile.AddToPile(this);
            Game.instance.CurrentPlayer.finishTurn = true;
        }

        /// <summary>
        /// Efekt po skontrowaniu kartą innej karty w łańcuchu akcji
        /// </summary>
        public virtual void CounterPlay()
        {
            Game.instance.Pile.AddToPile(this);
            Game.instance.actionChain.Add(this);
            Game.instance.CurrentPlayer.finishTurn = true;
        }

        /// <summary>
        /// Efekt karty wywoływany na graczu, który przyjął akcję.
        /// </summary>
        public virtual void Effect()
        {
            return;
        }

        /// <summary>
        ///  Zwraca informację czy karta może kontrować inną daną kartę.
        /// </summary>
        /// <param name="card">Karta do skontrowania</param>
        /// <returns></returns>
        public virtual bool IsCounterTo(Card card)
        {
            return false;
        }

        /// <summary>
        /// Metoda do tworzenia gotowej karty danego typu
        /// </summary>
        /// <typeparam name="T">Klasa (Typ) karty</typeparam>
        /// <param name="color">Kolor karty (enum)</param>
        /// <returns></returns>
        public static T Create<T>(CardSuit color) where T : Card
        {
            T card = new GameObject().AddComponent<T>();
            card.CardColor = color;
            card.name = $"{card.GetType().Name} {card.CardColor.ToString()}";
            return card;
        }

        /// <summary>
        /// Odpowiada za ukrycie awersu karty jej rewersem.
        /// </summary>
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
        /// <summary>
        /// Odpowiada za odkrycie awersu karty.
        /// </summary>
        public void Show()
        {
            transform.Find("Card/Back").GetComponent<Image>().enabled = false;
        }

        /// <summary>
        /// Metoda która wywołuje sie przy tasowaniu kart i odpowiada za przygotowanie karty do kolejnego użycia.
        /// </summary>
        public virtual void Reset()
        {
            return;
        }

        private readonly float maxTime = 0.5f;
        private float lastTimeClicked;

        /// <summary>
        /// Metoda odpowiadająca za rzucanie karty przez użytkownika.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            var topCard = Game.instance.Pile.TopCard;
            var player = GetComponentInParent<Player>();

            if (player == Game.instance.CurrentPlayer
                 && player is HumanPlayer
                 && player.SkipTurns == 0
                 && (topCard == null || topCard.GetType() == this.GetType() || topCard.CardColor == CardColor)) //karta pasuje do koloru lub figury
            {
                float deltaTime = Time.time - lastTimeClicked;

                if (deltaTime < maxTime)
                {
                    player.Play(this);
                }
                lastTimeClicked = Time.time;
            }
        }

        /// <summary>
        /// Zwraca parametry potrzebne do renderowania grafiki karty
        /// </summary>
        /// <param name="cardColor">Kolor karty (enum)</param>
        /// <param name="symbolstring">Znak odpowiadający danemu symbolowi karty w czcionka</param>
        /// <param name="color">Właściwy kolor karty (czarny, czerwony)</param>
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

        public override string ToString()
        {
            return $"{Label} {CardColor}";
        }
    }

    public enum CardSuit { Heart, Tile, Clover, Pike }
}
