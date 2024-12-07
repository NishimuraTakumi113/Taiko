using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditorMusicFileExplorer : MonoBehaviour
{
    private string musicFilePath;
    private string[] mp3Files;

    void Start(){
        musicFilePath = Path.Combine(Application.persistentDataPath, "Music");
        if(Directory.Exists(musicFilePath)){
            mp3Files = Directory.GetFiles(musicFilePath,"*.mp3");
        }else{
            Debug.Log("フォルダが存在しません");
        }
    }

    public string[] GetMps3Files(){
        return mp3Files;
    }
}
