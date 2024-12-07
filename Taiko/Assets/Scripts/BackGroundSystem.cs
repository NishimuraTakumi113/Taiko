using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore
{
    public static int score = 0;
    public static int combo = 0;
    public static int maxCombo = 0;
    public static int perfect = 0;
    public static int good = 0;
    public static int miss = 0;
    public static int maxScore = 0;
    public static int totalNotes = 0;
    public static int totalPerfect = 0;
    public static int totalGreat = 0;
    public static int totalGood = 0;
    public static int totalMiss = 0;
    public static int totalMaxCombo = 0;
    public static int totalScore = 0;
    public static int totalMaxScore = 0;

    public static void Reset()
    {
        score = 0;
        combo = 0;
        maxCombo = 0;
        perfect = 0;
        good = 0;
        miss = 0;
        maxScore = 0;
        totalNotes = 0;
        totalPerfect = 0;
        totalGreat = 0;
        totalGood = 0;
        totalMiss = 0;
        totalMaxCombo = 0;
        totalScore = 0;
        totalMaxScore = 0;
    }
}

public class GameMode{
    public static bool isPlay = false;
    public static bool isEdit = false;
    public static bool isReset = false;
}

public class EditorMelody{
    public static string melodyName;//
    public static float melodyBPM;//
    //編集中のメロディの長さ
    public static float melodyLength;//

    //編集中のメロディのスピード
    public static float melodySpeed;//

    //編集中の現在のスクロール位置
    public static float scrollPoint;

    //編集中の表示のためのオフセット
    public static Vector3 tmpOffset;//

    //編集中のノーツの位置のリスト
    public static List<float> notesLocate;//

    //編集中のノーツのリスト
    public static List<int> notesList;//

    //初期化
    public static void Reset()
    {
        melodyLength = 0;
        melodySpeed = 1.0f;
        scrollPoint = 0;
        notesLocate = new List<float>();
    }
}

[System.Serializable]
public class MusicSaveData{
    public string musicName;
    public float musicBPM;
    public List<int> notesList;
}
