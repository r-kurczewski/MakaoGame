using MakaoGame;
using MakaoGame.Cards;
using MakaoGame.GUI;
using UnityEngine;
using UnityEngine.UI;
namespace MakaoGame.GUI
{
    /// <summary>
    /// Klasa okna wyboru koloru asa.
    /// </summary>
    public class AceEffectWindow : ListWindow
    {
        [SerializeField] protected ColorPicker pick;
        /// <summary>
        /// Wybrany kolor
        /// </summary>
        public ColorPicker Pick { get => pick; private set => pick = value; }

        /// <summary>
        /// Tworzenie okna.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Wybranie koloru karty
        /// </summary>
        /// <param name="selection"></param>
        public void Select(ColorPicker selection)
        {
            if (Pick) Pick.transform.Find("Card/Select").GetComponent<Image>().enabled = false;
            Pick = selection;
            Pick.transform.Find("Card/Select").GetComponent<Image>().enabled = true;
            Accept.interactable = true;
        }
    }
}
