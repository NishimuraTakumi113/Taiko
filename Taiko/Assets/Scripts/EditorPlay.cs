using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorPlay : MonoBehaviour
{
    private TextMeshProUGUI ButtonText;
    void Start()
    {
        ButtonText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnButtonClicked()
    {
        GameMode.isPlay = !GameMode.isPlay;
        if(GameMode.isPlay){
            ButtonText.text = "演奏中止";
        }else{
            ButtonText.text = "演奏開始";
        }
    }

}
