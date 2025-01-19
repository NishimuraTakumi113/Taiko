using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class PlayFileListUI : MonoBehaviour
{
    public GameObject AudioPlayer;
    public EditorMusicFileExplorer fileExplorer;
    public Transform buttonParent;
    public GameObject buttonPrefab;
    public GameObject LoadWindow;
    public PlayMainSystem mainSystem;

    void Start()
    {
        //MP3ファイルリストを取得
        string[] map3Files = fileExplorer.GetMps3Files();

        if(map3Files != null){
            foreach (var file in map3Files)
            {
                GameObject button = Instantiate(buttonPrefab, buttonParent);
                Button buttonComponent = button.GetComponent<Button>();
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                
                string fileName = Path.GetFileNameWithoutExtension(file);
                buttonText.text = fileName;

                buttonComponent.onClick.AddListener(() => {MusicLoad(file,fileName);});
            }
        }
    }

    void MusicLoad(string filePath, string fileName){
        StartCoroutine(LoadMp3FromFile(filePath, fileName));
    }

    private IEnumerator LoadMp3FromFile(string path, string fileName)
    {
        string url = "file://" + path; // ローカルファイルの場合、"file://" を追加

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
                mainSystem.PlayStart();
                
            }
        }
    }
}
