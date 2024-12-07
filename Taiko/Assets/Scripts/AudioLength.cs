using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLength : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            float audioLength = audioSource.clip.length; // 長さを取得
            Debug.Log("曲の長さ: " + audioLength + " 秒");
        }
        else
        {
            Debug.LogWarning("AudioSource または AudioClip が設定されていません。");
        }
    }
}
