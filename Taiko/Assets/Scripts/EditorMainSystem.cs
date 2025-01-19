using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorMainSystem : MonoBehaviour
{
    private string directoryPath;//ディレクトリのパス
    private string filePath;//ファイル名のパス
    private float wheelDistance;
    private float Timer;
    public float wheelSpeed = 3.0f;
    public TextMeshProUGUI TimerText;

    public Scrollbar melodyScrollBar;
    //各システムの初期化のための各スクリプトの取得
    public AudioSplitter audioSplitter;
    public EditorAudioPlayer editorAudioPlayer;

    void Start()
    {
        // フレームレートを60に設定
        Application.targetFrameRate = 60;
        wheelDistance = 0;
        melodyScrollBar.value = 0;
        GameMode.isEdit = false;
    }

    public async void EditStart(){
        wheelDistance = 0;
        melodyScrollBar.value = 0;
        GameMode.isReset = true;
        // 楽曲の情報を取得
        directoryPath = Path.Combine(Application.persistentDataPath, "MusicData");
        filePath = Path.Combine(directoryPath, editorAudioPlayer.melodySource.clip.name + ".json");
        MusicSaveData saveData = LoadMusicData();
        if(saveData == null){
            return;
        }
        await Wait(1000);
        GameMode.isEdit = true;
        // 楽曲の情報をセット
        EditorMelody.melodyName = saveData.musicName;
        EditorMelody.melodyBPM = saveData.musicBPM;
        EditorMelody.notesList = saveData.notesList;
        audioSplitter.StartEdit();
        } 

    void Update()
    {   
        if(GameMode.isEdit){
            // マウスホイールの入力を取得
            if(!GameMode.isPlay){  
                float scrollInput = -Input.GetAxis("Mouse ScrollWheel");
                if(Mathf.Abs(scrollInput) > 0){
                    // 新しい累積値を計算
                    wheelDistance += scrollInput*wheelSpeed;

                    // 累積値を制限範囲内にクランプ
                    wheelDistance = Mathf.Clamp(wheelDistance, 0, EditorMelody.melodyLength * EditorMelody.melodySpeed);
                    melodyScrollBar.value = wheelDistance/(EditorMelody.melodyLength * EditorMelody.melodySpeed);
                    EditorMelody.scrollPoint = wheelDistance;
                }else{
                    // マウスホイールの入力がない場合は、スクロールバーの値を取得
                    wheelDistance = melodyScrollBar.value * EditorMelody.melodyLength * EditorMelody.melodySpeed;
                    EditorMelody.scrollPoint = wheelDistance;
                }
            }

            // タイマー表示
            if(GameMode.isPlay){
                // プレイ中は時間を進める
                Timer += Time.deltaTime*EditorMelody.melodySpeed;
            }else{
                // 編集中は時間をリセット
                Timer = wheelDistance;
            }
            TimerText.text =  Mathf.Floor(Timer/(60.0f*EditorMelody.melodySpeed)).ToString() + ":" + Mathf.Floor((Timer/EditorMelody.melodySpeed) % 60).ToString() 
            + "/" + Mathf.Floor((EditorMelody.melodyLength * EditorMelody.melodySpeed)/(60.0f*EditorMelody.melodySpeed)).ToString() + ":" + Mathf.Floor((EditorMelody.melodyLength * EditorMelody.melodySpeed) % 60).ToString();
        }
    }

    public MusicSaveData LoadMusicData()
    {
        // ファイルが存在するか確認
        if (File.Exists(filePath))
        {
            // ファイルからJSONデータを読み込む
            string jsonData = File.ReadAllText(filePath);

            // JSONをMelodySaveDataオブジェクトに変換
            MusicSaveData saveData = JsonUtility.FromJson<MusicSaveData>(jsonData);
            Debug.Log("データを読み込みました: " + filePath);
            return saveData;
        }
        else
        {
            Debug.LogWarning("保存されたデータが見つかりません: " + filePath);
            return null;
        }
    }

    private async Task Wait(float time)
    {
        await Task.Delay((int)(time));
    }
}
