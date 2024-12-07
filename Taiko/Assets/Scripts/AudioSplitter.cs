using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSplitter : MonoBehaviour
{
    public AudioSource audioSource; // 曲を再生するAudioSource
    public float bpm = 120f;        // 曲のBPM
    public int beatsPerSegment = 4; // 分割する拍数（例: 4拍ごと）
    public GameObject splitLinePrefab; //テンポ点のプレハブ
    public GameObject fourSpritPrefab;//4分割のプレハブ
    public GameObject eightSpritPrefab;//8分割のプレハブ
    public GameObject sixteenSpritPrefab;//16分割のプレハブ

    public float spritNum = 16.0f;
    public Vector3 offset = new Vector3(0, 0, 0); // 分割点のオフセット
    private List<float> splitPoints = new List<float>();// 分割点のリスト
    private List<float> notesLocateList = new List<float>();// ノーツの位置のリスト
    private List<int> notesList = new List<int>();// ノーツのリスト
    public float spanLength = 1.0f;

    void Start()
    {
        EditorMelody.Reset();
        EditorMelody.tmpOffset = offset;
        EditorMelody.melodyBPM = bpm;
        EditorMelody.melodyName = audioSource.clip.name;
        if (audioSource != null && audioSource.clip != null)
        {
            // 曲の長さを取得(補正込み)
            float songLength = audioSource.clip.length;

            // 曲の長さを保持(バックへ)
            EditorMelody.melodySpeed = spanLength;
            EditorMelody.melodyLength = songLength;// * spanLength;


            // 1拍の長さを計算
            float beatDuration = 60f / bpm;

            // 1小節の長さを計算（例: 4拍分）
            float segmentDuration = beatDuration * beatsPerSegment;

            // // 分割点を計算
            // for (float time = 0; time < songLength + segmentDuration; time += segmentDuration)
            // {
            //     splitPoints.Add(time*spanLength);
            // }

            //細分分音符までの位置を計算
            for (float time = 0; time < songLength + segmentDuration; time += segmentDuration/spritNum)
            {
                notesLocateList.Add(time*spanLength);
                notesList.Add(0);
            }

            EditorMelody.notesLocate = notesLocateList;
            EditorMelody.notesList = notesList;
        }

        for(int i = 0; i < notesLocateList.Count; i++)
        {
            if(i % spritNum == 0){
                SetTmpPrefab(splitLinePrefab,i);
            }else if((i*4) % spritNum == 0  && i % spritNum != 0){
                SetTmpPrefab(fourSpritPrefab,i);
            }else if((i*8) % spritNum == 0  && (i*4) % spritNum != 0  && i % spritNum != 0){
                SetTmpPrefab(eightSpritPrefab,i);
            }else{
                SetTmpPrefab(sixteenSpritPrefab,i);
            }

        // foreach (var point in splitPoints)
        // {
        //     GameObject splitLine = Instantiate(splitLinePrefab, new Vector3(point, 0, 1.0f) + offset, Quaternion.identity);
        //     splitLine.GetComponent<TmpController>().speed = spanLength;
        // }
        }
    }
    void SetTmpPrefab(GameObject prefab,int index)
    {
        GameObject splitLine = Instantiate(prefab, new Vector3(notesLocateList[index], 0, 1.0f) + offset, Quaternion.identity);
        splitLine.GetComponent<TmpController>().speed = spanLength;
    }
}
