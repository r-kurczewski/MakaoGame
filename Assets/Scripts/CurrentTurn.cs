using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTurn : MonoBehaviour
{
    public TMP_Text text;
    void Update()
    {
        text.text = Game.context.CurrentPlayerId.ToString();
    }
}
