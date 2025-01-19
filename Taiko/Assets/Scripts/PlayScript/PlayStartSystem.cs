using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayStartSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text ComboText;
    void Start()
    {
        GameScore.Reset();
        GameMode.isPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = GameScore.score.ToString();
        ComboText.text = GameScore.combo.ToString();
    }
}
