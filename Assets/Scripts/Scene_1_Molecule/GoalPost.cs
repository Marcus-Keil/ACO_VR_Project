using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    [SerializeField] Material GoalMat;
    public WaterScoreScript Score;
    public TimerScript TimerTxt;
    public int MaxScore = 5;

    private Animator animator;
    private String animatorEnter = "Enter";
    private String animatorGoal = "Finish";
    private bool Entered = false;

    public AudioSource StartAudio;
    public AudioSource GoalAudio;
    public AudioSource DoneAudio;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stationary"))
        {
            Entered = true;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (TimerTxt.TimerOn)
        {
            if (other.gameObject.CompareTag("Oxygen") && Entered)
            {
                Molecule otherMol = other.gameObject.GetComponent<Molecule>();
                if (otherMol.BondedMolecules.Count == 2)
                {
                    Score.GetComponent<WaterScoreScript>().ScoreUpdate();
                    GoalAudio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
                    GoalAudio.Play();
                    otherMol.InitiateDeath();
                    if (Score.GetComponent<WaterScoreScript>().GetScore() >= MaxScore)
                    {
                        GoalAudio.Play();
                    }
                }
            }

        }
    }

    public async void EnterIn()
    {
        animator.SetTrigger(animatorEnter);
        while (!Entered)
        {
            await Task.Delay(30);
        }
        StartAudio.Play();
        TimerTxt.StartTimer();
    }

    public void ExitOut()
    {
        animator.SetTrigger(animatorGoal);
    }
}
