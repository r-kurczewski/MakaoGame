using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Button _accept;
    [SerializeField] private Button _decline;
    [SerializeField] private GameObject listView;
#pragma warning restore 0649

    public Button Accept { get => _accept; private set => _accept = value; }
    public Button Decline { get => _decline; private set => _decline = value; }

    public static ActionWindow Create(List<Card> action)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/ActionWindow"), Game.context.transform);
       var target = obj.GetComponent<ActionWindow>();
        foreach (var card in action)
        {
            Instantiate(card.Obj, target.listView.transform);
        }
        return target;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
