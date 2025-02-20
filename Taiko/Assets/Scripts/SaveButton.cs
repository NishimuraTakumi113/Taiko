using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : GameButton
{
    public override void OnButtonClicked()
    {
        SaveManager sm = SaveManager.getInstance();
        sm.Save(EditorMelody.melodyName, EditorMelody.melodyBPM, TmpData.musicFilePath);
    }
}