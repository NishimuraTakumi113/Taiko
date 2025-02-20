using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class BuildButton : GameButton
{
    public TextMeshProUGUI musicNameText;
    public TextMeshProUGUI musicBPMText;
    public Image Alert;
    private float duration;
    private AudioSource audioSource;
    private string targetMusicDirectoryPath;
    private int bpm;
    public EditorMainSystem mainSystem;
    void Start()
    {
        // AudioSource をアタッチ
        audioSource = FindObjectOfType<AudioSource>();
    }
    
    public override async void OnButtonClicked()
    {
        targetMusicDirectoryPath = Path.Combine(Application.persistentDataPath, "MusicList");
        if (!Directory.Exists(targetMusicDirectoryPath))
        {
            Directory.CreateDirectory(targetMusicDirectoryPath);
            Debug.Log("ディレクトリを作成しました" + targetMusicDirectoryPath);
        }
        else
        {
            Debug.Log("ディレクトリは既に存在します" + targetMusicDirectoryPath);
        }

        // Debug.Log(musicBPMText.text);
        string input = musicBPMText.text.Trim();
        input = Regex.Replace(input, @"[^\d-]", "");  // 数字とマイナス記号以外を削除
        if(TmpData.musicFilePath == null || !int.TryParse(input, out bpm)){
            Alert.GetComponentInChildren<TextMeshProUGUI>().text = "入力が正しくありません";
            StartCoroutine(AlertMessage());
            // Debug.Log(bpm);
            return;
        }
        string targetFilePath = Path.Combine(targetMusicDirectoryPath, musicNameText.text.Trim());
        if(Directory.Exists(targetFilePath)){
            // Debug.Log("ファイルは既に存在します" + targetFilePath);
            Alert.GetComponentInChildren<TextMeshProUGUI>().text = "同名のファイルは既に存在します";
            StartCoroutine(AlertMessage());
            return;
        }
        // MP3 の長さを取得
        TmpData.musicLength = await GetMp3DurationAsync(TmpData.musicFilePath);
        // Debug.Log("曲の長さ: " + duration + " 秒");
        SaveManager sm = SaveManager.getInstance();
        sm.Save(musicNameText.text, bpm, TmpData.musicFilePath);
        mainSystem.EditStart(musicNameText.text);
    }
    IEnumerator AlertMessage(){
        Alert.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Alert.gameObject.SetActive(false);
    }

    private async Task<float> GetMp3DurationAsync(string path)
    {
        using (var www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield(); // 非同期で待機
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                return clip.length;
            }
            else
            {
                Debug.LogError("MP3の読み込みに失敗: " + www.error);
                return 0f;
            }
        }
    }
}