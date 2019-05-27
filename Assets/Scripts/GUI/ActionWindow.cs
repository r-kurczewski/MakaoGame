using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MakaoGame.GUI
{
    public class ActionWindow : GUIListWindow
    {
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
    }
}
