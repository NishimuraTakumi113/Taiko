using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager
{
    private static SaveManager instance;
    private SaveManager(){}
    public static SaveManager getInstance(){
        if(instance == null){
            instance = new SaveManager();
        }
        return instance;
    }
    private string directoryPath;//ディレクトリのパス
    private bool isExist;
    private string targetMusicDirectoryPath = Path.Combine(Application.persistentDataPath, "MusicList");

    public void Save(string musicName,float bpm,string musicPath){
        //ディレクトリのパス
        if(Directory.Exists(musicPath)){
            isExist = true;
        }else{
            directoryPath = Path.Combine(targetMusicDirectoryPath, musicName);
            CreateDirectoryIfNeeded();
            isExist = false;
        }
        Debug.Log(isExist);

        MusicSaveData saveData;
        if(isExist){
            //データの作成
            string[] filePath = Directory.GetFiles(musicPath, "*.json");
            saveData = new MusicSaveData{
                musicName = EditorMelody.melodyName,
                musicBPM = EditorMelody.melodyBPM,
                notesList = EditorMelody.notesList
            };
            SaveMusicData(saveData,filePath[0]);
        }else{
            string targetFilePath = Path.Combine(directoryPath, musicName + ".mp3");
            File.Copy(TmpData.musicFilePath, targetFilePath);
            TmpData.musicFilePath = directoryPath;

            // JSON ファイルの作成
            string jsonFilePath = Path.Combine(directoryPath, musicName + ".json");
            File.Create(jsonFilePath).Close();

            saveData = new MusicSaveData{
                musicName = musicName,
                musicBPM = bpm,
                notesList = TmpData.defaultNotesList(bpm,TmpData.musicLength)
            };
            SaveMusicData(saveData,jsonFilePath);
        }
    }

    private void CreateDirectoryIfNeeded()
    {
        if(!Directory.Exists(directoryPath)){
            Directory.CreateDirectory(directoryPath);
            isExist = false;
            Debug.Log("ディレクトリを作成しました"+directoryPath);
        }
    }

    private void SaveMusicData(MusicSaveData saveData,string filePath){
        //データをJSONに変換
        string jsonData = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(filePath, jsonData);
        Debug.Log("データを保存しました" + filePath);
    }
}