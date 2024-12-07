using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorLoad : MonoBehaviour
{
    public GameObject LoadWindow;
    void Start()
    {
        LoadWindow.SetActive(false);
    }

    public void OnClicked(){
        if(LoadWindow.activeSelf){
            LoadWindow.SetActive(false);
            return;
        }else{
            LoadWindow.SetActive(true);
        }
    }
}
