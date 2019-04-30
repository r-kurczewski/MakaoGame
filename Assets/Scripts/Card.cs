using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnoGame;

[ExecuteInEditMode]
[SelectionBase]
public abstract class Card : MonoBehaviour
{
    public GameObject Obj { get; private set; }

    [SerializeField] private CardColor _cardColor;
    public CardColor CardColor
    {
        get
        {
            return _cardColor;
        }
        set
        {
            _cardColor = value;
        }
    }
    public abstract char Label { get; }
    public bool hide;

    void Start()
    {
        foreach (Transform child in transform) DestroyImmediate(child.gameObject);
        Obj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
        name = Obj.name = GetType().ToString() + CardColor.ToString();
        Obj.name = "Card";
        Obj.transform.localPosition = Vector3.one;
        if (hide) Hide();
        foreach (var label in GetComponentsInChildren<TMP_Text>())
        {
            label.text = Label.ToString();
        }

        Color color;
        char symbolChar;
        switch (CardColor)
        {
            case CardColor.Heart:
                color = Color.red;
                symbolChar = '{';
                break;

            case CardColor.Tile:
                color = Color.red;
                symbolChar = '[';
                break;

            case CardColor.Clover:
                color = Color.black;
                symbolChar = ']';
                break;

            case CardColor.Pike:
                color = Color.black;
                symbolChar = '}';
                break;

            default:
                color = UnityEngine.Color.black;
                symbolChar = ' ';
                Debug.LogError("Wrong color of card.");
                break;
        }
        TMP_Text label1 = transform.Find("Card/Front/Label1").GetComponent<TMP_Text>();
        TMP_Text label2 = transform.Find("Card/Front/Label2").GetComponent<TMP_Text>();
        TMP_Text symbol = transform.Find("Card/Front/Symbol").GetComponent<TMP_Text>();
        label1.text = Label.ToString();
        label2.text = Label.ToString();
        symbol.text = symbolChar.ToString();
        foreach (var label in GetComponentsInChildren<TMP_Text>())
        {
            label.color = color;
        }
        if (!GetComponent<RectTransform>())
        {
            var transform = gameObject.AddComponent<RectTransform>();
            transform.sizeDelta = new Vector2(75, 100);
            transform.anchoredPosition = new Vector2(0, 0);
        };
    }

    public abstract void Effect(Game context);

    public void Play(Game context)
    {
        //Play a card
        Effect(context);
        //Finish playing the card
    }

    public static T Create<T>(CardColor color) where T : Card
    {
        T card = new GameObject("Card", typeof(RectTransform)).AddComponent<T>();
        card.CardColor = color;
        return card;
    }

    public void Hide()
    {
        transform.Find("Card/Back").GetComponent<Image>().enabled = true;
    }

    public void Show()
    {
        transform.Find("Card/Back").GetComponent<Image>().enabled = false;
    }

}

namespace UnoGame
{
    public enum CardColor { Heart, Tile, Clover, Pike }
}
