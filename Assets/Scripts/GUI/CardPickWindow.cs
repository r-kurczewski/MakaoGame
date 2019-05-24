using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.GUI
{
    public class CardPickWindow : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private Button _accept;
        [SerializeField] private Button _decline;
        [SerializeField] private GameObject listView;
        [SerializeField] private CardPicker pick;
#pragma warning restore 0649

        public Button Accept { get => _accept; private set => _accept = value; }
        public Button Decline { get => _decline; private set => _decline = value; }
        public CardPicker Pick { get => pick; private set => pick = value; }

        public static CardPickWindow Create(List<Card> cards)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/CardPickWindow"), Game.context.transform);
            var target = obj.GetComponent<CardPickWindow>();
            foreach (var card in cards)
            {
                CardPicker pick = Instantiate(card.Obj, target.listView.transform).AddComponent<CardPicker>();
                pick.original = card;
                pick.gameObject.AddComponent<Button>();
            }
            return target;
        }

        public void Select(CardPicker selection)
        {
            if (Pick) Pick.transform.Find("Select").GetComponent<Image>().enabled = false;
            Pick = selection;
            Pick.transform.Find("Select").GetComponent<Image>().enabled = true;
            Accept.interactable = true;
            //Debug.Log($"Picked {selection.name}");
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}

