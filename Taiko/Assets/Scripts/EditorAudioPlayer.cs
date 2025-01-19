using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorAudioPlayer : MonoBehaviour
{
    public AudioSource melodySource;
    public Button playButton;
    private float startTime;
    private bool hasStarted = false;
    void Start()
    {
        startTime = 0;
        if(playButton != null){
            playButton.onClick.AddListener(PlayMelody);
        }
    }
    void Update()
    {
        startTime = EditorMelody.scrollPoint/EditorMelody.melodySpeed;
        if(!hasStarted && melodySource.isPlaying){
            hasStarted = false;
            playButton.GetComponent<EditorPlay>().OnButtonClicked();
        }
    }

    public void PlayMelody()
    {
        if(!GameMode.isEdit){
            return;
        }
        
        if(melodySource.isPlaying){
            melodySource.Pause();
            hasStarted = false;
        }else{
            melodySource.time = startTime;
            hasStarted = true;
            melodySource.Play();
        }
    }
}
