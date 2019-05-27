using MakaoGame.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIListWindow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] protected Button _accept;
    [SerializeField] protected Button _decline;
    [SerializeField] protected GameObject listView;
    [SerializeField] protected CardPicker pick;
#pragma warning restore 0649

    public Button Accept { get => _accept; protected set => _accept = value; }
    public Button Decline { get => _decline; protected set => _decline = value; }

    public void Close()
    {
        Destroy(gameObject);
    }
}
