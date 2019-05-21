using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPickWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Button _accept;
    [SerializeField] private Button _decline;
    [SerializeField] private GameObject listView;
#pragma warning restore 0649

    public Button Accept { get => _accept; private set => _accept = value; }
    public Button Decline { get => _decline; private set => _decline = value; }

    public static CardPickWindow Create(List<Card> cards)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/CardPickWindow"), Game.context.transform);
        var target = obj.GetComponent<CardPickWindow>();
        foreach (var card in cards)
        {
            CardPicker pick = Instantiate(card.Obj, target.listView.transform).AddComponent<CardPicker>();
            pick.original = card;
        }
        return target;
    }

    public void AddCardToPick(Card card)
    {

    }

    public void Close()
    {
        Destroy(gameObject);
    }
}

