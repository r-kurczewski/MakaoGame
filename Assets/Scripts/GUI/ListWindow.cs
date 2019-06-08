using MakaoGame;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.GUI
{
    /// <summary>
    /// Podstawowe okno wyświetlające listę kart i dwa przyci
    /// </summary>
    public class ListWindow : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] protected Button _accept;
        [SerializeField] protected Button _decline;
        [SerializeField] protected Transform listView;
        [SerializeField] protected TMP_Text title;
        #pragma warning restore 0649

        /// <summary>
        /// Przycisk zatwierdzający.
        /// </summary>
        public Button Accept { get => _accept; protected set => _accept = value; }

        /// <summary>
        /// Przycisk anulujący.
        /// </summary>
        public Button Decline { get => _decline; protected set => _decline = value; }

        /// <summary>
        /// Metoda tworząca okno
        /// </summary>
        /// <param name="action"> Lista kart do wyświetlenia w liście</param>
        /// <param name="title">Nagłówek okna</param>
        /// <returns></returns>
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

        /// <summary>
        /// Zamknięcie okna.
        /// </summary>
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
