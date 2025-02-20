using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class EditorFileListUI : MonoBehaviour
{
    public GameObject AudioPlayer;
    public EditorMusicFileExplorer fileExplorer;
    public Transform buttonParent;
    public GameObject buttonPrefab;
    public GameObject LoadWindow;
    public EditorMainSystem mainSystem;
    public Canvas AddScreen;

    void Start()
    {
        //MP3ファイルリストを取得
        string[] map3Files = fileExplorer.GetMps3Files();
        GameObject button;
        Button buttonComponent;
        TextMeshProUGUI buttonText;

        if(map3Files != null){
            foreach (var file in map3Files)
            {
                button = Instantiate(buttonPrefab, buttonParent);
                buttonComponent = button.GetComponent<Button>();
                buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                
                string fileName = Path.GetFileName(file);
                buttonText.text = fileName;

                buttonComponent.onClick.AddListener(() => {MusicLoad(file,fileName);});
            }
        }
        button = Instantiate(buttonPrefab, buttonParent);
        buttonComponent = button.GetComponent<Button>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "新規作成+";

        buttonComponent.onClick.AddListener(() => {AddScreen.gameObject.SetActive(true);});
    }

    void MusicLoad(string filePath, string fileName){
        StartCoroutine(LoadMp3FromFile(filePath, fileName));
    }

    private IEnumerator LoadMp3FromFile(string path, string fileName)
    {
        string url = "file://" + Path.Combine(path,fileName + ".mp3"); // ローカルファイルの場合、"file://" を追加

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading MP3: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                clip.name = fileName;
                AudioSource audioSource = AudioPlayer.gameObject.GetComponent<AudioSource>();
                audioSource.clip = clip;
                TmpData.musicFilePath = path;
                if(GameMode.isPlay){
                }else if(GameMode.isEdit){
                    mainSystem.EditStart(fileName);
                }
            }
        }
    }
}
