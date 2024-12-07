using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorMainSystem : MonoBehaviour
{
    private float wheelDistance;
    private float Timer;
    public float wheelSpeed = 3.0f;
    public TextMeshProUGUI TimerText;

    public Scrollbar melodyScrollBar;
    void Start()
    {
        // フレームレートを60に設定
        Application.targetFrameRate = 60;
        wheelDistance = 0;
        melodyScrollBar.value = 0;
        GameMode.isEdit = true;
    }

    void Update()
    {   
        // マウスホイールの入力を取得
        if(!GameMode.isPlay){  
            float scrollInput = -Input.GetAxis("Mouse ScrollWheel");
            if(Mathf.Abs(scrollInput) > 0){
                // 新しい累積値を計算
                wheelDistance += scrollInput*wheelSpeed;

                // 累積値を制限範囲内にクランプ
                wheelDistance = Mathf.Clamp(wheelDistance, 0, EditorMelody.melodyLength);
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
