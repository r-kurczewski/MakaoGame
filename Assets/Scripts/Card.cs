using UnityEngine;
using UnityEngine.UI;
using UnoGame;

[ExecuteInEditMode]
[SelectionBase]
public abstract class Card : MonoBehaviour
{
    public GameObject Obj { get; private set; }
    [SerializeField] private CardColor _color;
    public CardColor Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
        }
    }
    public abstract char Label { get; }

    void Start()
    {
        foreach (Transform child in transform) DestroyImmediate(child.gameObject);
        Obj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
        name = Obj.name = GetType().ToString() + Color.ToString();
        Obj.name = "Card";
        Obj.transform.localPosition = Vector3.one;
        Obj.GetComponentInChildren<Text>().text = Label.ToString();
        UnityEngine.Color color;
        switch (Color)
        {
            case CardColor.Heart:
                color = UnityEngine.Color.red;
                break;

            case CardColor.Tile:
                color = UnityEngine.Color.green;
                break;

            case CardColor.Clover:
                color = UnityEngine.Color.blue;
                break;

            case CardColor.Pike:
                color = UnityEngine.Color.yellow;
                break;

            default:
                color = UnityEngine.Color.black;
                Debug.LogError("Wrong color of card.");
                break;
        }
        Obj.GetComponentInChildren<Image>().color = color;
    }

    public abstract void Effect(Game game);

public static T Create<T>(CardColor color) where T : Card
{
    T card = new GameObject().AddComponent<T>();
    card.Color = color;
    return card;
}

    //public void Reload()
    //{
    //    Start();
    //}

}

namespace UnoGame
{
    public enum CardColor { Heart, Tile, Clover, Pike }
}
