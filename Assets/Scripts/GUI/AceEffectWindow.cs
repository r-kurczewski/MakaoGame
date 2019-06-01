using MakaoGame;
using MakaoGame.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AceEffectWindow : ListWindow
{
    [SerializeField] protected ColorPicker pick;
    public ColorPicker Pick { get => pick; private set => pick = value; }

    public static AceEffectWindow Create()
    {
        var window = Instantiate(Resources.Load<AceEffectWindow>("Prefabs/AceEffectWindow"), Game.context.transform);
        foreach (CardSuit color in System.Enum.GetValues(typeof(CardSuit)))
        {
            ColorPicker pick = Card.Create<Ace>(color).gameObject.AddComponent<ColorPicker>();
            pick.transform.SetParent(window.listView, false);
            pick.color = color;
        }
        return window;
    }

    public void Select(ColorPicker selection)
    {
        if (Pick) Pick.transform.Find("Card/Select").GetComponent<Image>().enabled = false;
        Pick = selection;
        Pick.transform.Find("Card/Select").GetComponent<Image>().enabled = true;
        Accept.interactable = true;
    }
}
