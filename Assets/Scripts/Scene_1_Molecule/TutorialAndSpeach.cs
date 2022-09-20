using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialAndSpeach : MonoBehaviour
{
    public AudioSource Introduction;
    public AudioSource PostTutorial;
    public AudioSource EndChallenge;
    public AudioSource EndSpeach;

    public GameObject GlowOrb_R;
    public GameObject GlowOrb_L;
    public GameObject MenuSphere;
    public GoalPost GP;

    public Animator anim;

    private bool OutroPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (StoredKnowledge.MenuUnlocked)
        {
            MenuSphere.SetActive(true);
        }
        if (!StoredKnowledge.DoneTutorial_1)
        {
            IntroPlay();
            StoredKnowledge.Played_Scene_1 = false;
        }
        else
        {
            NoTutorialPlay();
            StoredKnowledge.Played_Scene_1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StoredKnowledge.End_Game_1 && !OutroPlayed)
        {
            OutroPlayed = true;
            Outro();
        }
    }


    private void ActivateOrbs()
    {
        GlowOrb_R.SetActive(true);
        GlowOrb_L.SetActive(true);
    }
    private void DeactivateOrbs()
    {
        GlowOrb_R.SetActive(false);
        GlowOrb_L.SetActive(false);
    }

    public void IntroPlay()
    {
        StartCoroutine(Wait());
        anim.Play("Base Layer.New Animation", 0, 0.5f);
        StartCoroutine(WaitForIntroFinish());
    }

    public void NoTutorialPlay()
    {
        StartCoroutine(Wait());
        StartCoroutine(WaitForTutorialAudioFinish());
    }

    public void Outro()
    {
        StartCoroutine(WaitForOutroAudioFinish());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator WaitForIntroFinish()
    {
        Introduction.Play();
        yield return new WaitWhile(() => Introduction.isPlaying);
        ActivateOrbs();
        StoredKnowledge.StartTutorial_1 = true;
        StartCoroutine(WaitForTutorialAudioFinish());
    }

    IEnumerator WaitForTutorialAudioFinish()
    {
        while (!StoredKnowledge.DoneTutorial_1)
        {
            yield return null;
        }
        PostTutorial.Play();
        yield return new WaitWhile(() => PostTutorial.isPlaying);
        if (GlowOrb_R.activeSelf)
        {
            DeactivateOrbs();
        }
        GP.EnterIn();
        StoredKnowledge.DoneTutorial_1 = true;
    }

    IEnumerator WaitForOutroAudioFinish()
    {
        Debug.Log("I was at outro");
        EndChallenge.Play();
        yield return new WaitWhile(() => EndChallenge.isPlaying);
        EndSpeach.Play();
        yield return new WaitWhile(() => EndSpeach.isPlaying);
        StoredKnowledge.Played_Scene_1 = true;
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Scene_2_Dust");
    }
}
