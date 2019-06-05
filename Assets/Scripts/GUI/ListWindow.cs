using MakaoGame;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.GUI
{
    public class ListWindow : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] protected Button _accept;
        [SerializeField] protected Button _decline;
        [SerializeField] protected Transform listView;
        [SerializeField] protected TMP_Text title;
        #pragma warning restore 0649

        public Button Accept { get => _accept; protected set => _accept = value; }
        public Button Decline { get => _decline; protected set => _decline = value; }

        public static ListWindow Create(List<Card> action, string title = default)
        {
            var window = Instantiate(Resources.Load<ListWindow>("Prefabs/ListWindow"), Game.context.transform);
            window.title.text = title;
            foreach (var card in action)
            {
                Instantiate(card.Obj, window.listView);
            }
            return window;
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
