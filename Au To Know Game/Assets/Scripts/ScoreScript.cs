using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text textt;
    public int trueScore = 0;
    private int scoreNotified = 0;

    void Start()
    {
        textt.text = "Score: " + trueScore;
    }

    public void SetScore(int score)
    {
        trueScore += score;
        textt.text = "Score: " + trueScore;
    }

    public int ScoreNotification()
    {
        if (trueScore % 1000 == 0)
        {
            scoreNotified = trueScore / 1000;
            return scoreNotified;
        }
        else
        {
            return scoreNotified;
        }
    }
}
