using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditorMusicFileExplorer : MonoBehaviour
{
    private string musicFilePath;
    private string[] MusicFiles;

    void Start(){
        musicFilePath = Path.Combine(Application.persistentDataPath, "MusicList");
        if(Directory.Exists(musicFilePath)){
            MusicFiles = Directory.GetDirectories(musicFilePath);
        }else{
            Debug.Log("楽曲が存在しません");
        }
    }

    public string[] GetMps3Files(){
        return MusicFiles;
    }
}
