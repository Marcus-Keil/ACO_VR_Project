using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SecondTimerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float TimeLeft;
    public bool TimerOn = false;

    public TextMeshProUGUI TimerTxt;
    public GameObject CollisionManager;
    public float WaitBeforeTimer = 10.0f;

    void Start()
    {
        float minutes = Mathf.FloorToInt(TimeLeft / 60);
        float seconds = Mathf.FloorToInt(TimeLeft % 60);
        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                StoredKnowledge.End_Game_2 = true;
                TimerOn = false;
            }
        }
        else if (!TimerOn && StoredKnowledge.Start_Game_2 && !StoredKnowledge.End_Game_2)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        TimerOn = true;
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
