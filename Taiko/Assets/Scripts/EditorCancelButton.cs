using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCancelButton : GameButton
{
    public Canvas AddScreen;
    public override void OnButtonClicked()
    {
        AddScreen.gameObject.SetActive(false);
    }
}