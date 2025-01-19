using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSplitter : MonoBehaviour
{
    public AudioSource audioSource; // 曲を再生するAudioSource
    public float bpm = 120f;        // 曲のBPM
    public int beatsPerSegment = 4; // 分割する拍数（例: 4拍ごと）
    public GameObject splitLinePrefab; //テンポ点のプレハブ
    // public GameObject fourSpritPrefab;//4分割のプレハブ
    // public GameObject eightSpritPrefab;//8分割のプレハブ
    // public GameObject sixteenSpritPrefab;//16分割のプレハブ
    public GameObject[] notesPrefab;//ノーツのプレハブ

    public float spritNum = 16.0f;
    public Vector3 offset = new Vector3(0, 0, 0); // 分割点のオフセット
    private List<float> splitPoints = new List<float>();// 分割点のリスト
    private List<float> notesLocateList = new List<float>();// ノーツの位置のリスト
    private List<int> notesList = new List<int>();// ノーツのリスト
    public float spanLength = 1.0f;

    public void StartEdit()
    {
        GameMode.isReset = false;
        EditorMelody.Reset();
        EditorMelody.tmpOffset = offset;
        // EditorMelody.melodyBPM = bpm;
        // EditorMelody.melodyName = audioSource.clip.name;
        if (audioSource != null && audioSource.clip != null)
        {
            // 曲の長さを取得(補正込み)
            float songLength = audioSource.clip.length;

            // 曲の長さを保持(バックへ)
            EditorMelody.melodySpeed = spanLength;
            EditorMelody.melodyLength = songLength;// * spanLength;


            // 1拍の長さを計算
            float beatDuration = 60f / EditorMelody.melodyBPM;

            // 1小節の長さを計算（例: 4拍分）
            float segmentDuration = beatDuration * beatsPerSegment;

            // // 分割点を計算
            // for (float time = 0; time < songLength + segmentDuration; time += segmentDuration)
            // {
            //     splitPoints.Add(time*spanLength);
            // }

            //細分分音符までの位置を計算
            float time = 0;
            for (float i = 0; i < EditorMelody.notesList.Count; i++)
            {
                EditorMelody.notesLocate.Add(time * spanLength);
                time += segmentDuration/spritNum;
            }
            // Debug.Log(EditorMelody.notesList.Count);
            // Debug.Log(EditorMelody.notesLocate.Count);
        }

        for(int i = 0; i < EditorMelody.notesList.Count; i++)
        {
            if(i % spritNum == 0){
                SetTmpPrefab(splitLinePrefab,i);
            }

            if(EditorMelody.notesList[i] != 0){
                SetNotesPrefab(notesPrefab[EditorMelody.notesList[i]-1],i);
            }
        }
    }
    void SetTmpPrefab(GameObject prefab,int index)
    {
        GameObject splitLine = Instantiate(prefab, new Vector3(EditorMelody.notesLocate[index], 0, 1.0f) + offset, Quaternion.identity);
        splitLine.GetComponent<TmpController>().speed = spanLength;
        splitLine.GetComponent<TmpController>().pastPosition = splitLine.transform.position;
    }

    void SetNotesPrefab(GameObject prefab,int index)
    {
        GameObject notes = Instantiate(prefab, new Vector3(EditorMelody.notesLocate[index], 0, 1.0f + (float)index/10) + offset, Quaternion.identity);
        notes.GetComponent<NotesController>().pastPosition = notes.transform.position;
        notes.GetComponent<NotesController>().speed = EditorMelody.melodySpeed;
        notes.GetComponent<NotesController>().pastIndex = index;
        notes.GetComponent<NotesController>().locateIndex = index;
    }
}
