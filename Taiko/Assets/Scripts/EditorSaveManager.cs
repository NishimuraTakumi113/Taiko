using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditorSaveManager : MonoBehaviour
{
    private string directoryPath;//ディレクトリのパス
    private string filePath;//ファイル名

    public void Save(){
        //ディレクトリのパス
        directoryPath = Path.Combine(Application.persistentDataPath, "MusicData");
        //ディレクトリが存在しない場合
        CreateDirectoryIfNeeded();

        //ファイルの保存先
        filePath = Path.Combine(directoryPath, EditorMelody.melodyName + ".json");
        if(!File.Exists(filePath)){
            Debug.Log("ファイルが存在しません" + filePath);
            return;
        }

        //データの作成
        MusicSaveData saveData = new MusicSaveData{
            musicName = EditorMelody.melodyName,
            musicBPM = EditorMelody.melodyBPM,
            notesList = EditorMelody.notesList
        };

        //データの保存
        SaveMusicData(saveData);
    }

    // public static void Save(MusicSaveData saveData){
    //     //ディレクトリのパス
    //     private string directoryPath = Path.Combine(Application.persistentDataPath, "MusicData");
    //     //ディレクトリが存在しない場合
    //     CreateDirectoryIfNeeded();

    //     //ファイルの保存先
    //     filePath = Path.Combine(directoryPath, saveData.musicName + ".json");
    //     if(!File.Exists(filePath)){
    //         Debug.Log("ファイルが存在しません" + filePath);
    //         return;
    //     }

    //     //データの保存
    //     SaveMusicData(saveData);
    // }

    void CreateDirectoryIfNeeded()
    {
        if(!Directory.Exists(directoryPath)){
            Directory.CreateDirectory(directoryPath);
            Debug.Log("ディレクトリを作成しました"+directoryPath);
        }else{
            Debug.Log("ディレクトリは既に存在します" + directoryPath);
        }
    }

    public void SaveMusicData(MusicSaveData saveData){
        //データをJSONに変換
        string jsonData = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(filePath, jsonData);
        Debug.Log("データを保存しました" + filePath);
    }
}