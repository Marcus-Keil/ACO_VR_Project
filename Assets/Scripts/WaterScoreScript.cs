using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterScoreScript : MonoBehaviour
{
    public GameObject TimerTxt;
    public TextMeshProUGUI Score;
    private int CurrentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        TimerTxt = GameObject.Find("TimerText");
    }

    public void ScoreUpdate()
    {
        CurrentScore += 1;
        Score.text = string.Format("{0}", CurrentScore);
    }
}
