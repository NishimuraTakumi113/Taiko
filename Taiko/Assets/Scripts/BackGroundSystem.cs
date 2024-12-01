using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
