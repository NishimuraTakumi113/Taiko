using SFB; // StandaloneFileBrowser のネームスペース
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class FileBrowser : MonoBehaviour
{
    public Button musicAddButton;
    public TextMeshProUGUI musicNameText;
    public TextMeshProUGUI musicBPMText;
    public Image Alert;
    private string musicPath;

    public void OpenFileDialog()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("MP3ファイルを選択", "", "mp3", false);
        if (paths.Length > 0)
        {
            musicPath = paths[0];
            Debug.Log("選択したファイル: " + musicPath);
            TmpData.musicFilePath = musicPath;
            string fileName = Path.GetFileName(musicPath);
            Debug.Log(fileName);
            musicAddButton.GetComponentInChildren<TextMeshProUGUI>().text = fileName;
        }
    }

    // private IEnumerator LoadMp3(string path)
    // {
    //     using (var www = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip("file://" + path, UnityEngine.AudioType.MPEG))
    //     {
    //         yield return www.SendWebRequest();
    //         if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
    //         {
    //             AudioClip clip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(www);
    //             AudioSource audioSource = GetComponent<AudioSource>();
    //             audioSource.clip = clip;
    //             audioSource.Play();
    //         }
    //         else
    //         {
    //             Debug.LogError("MP3の読み込みに失敗: " + www.error);
    //         }
    //     }
    // }
}