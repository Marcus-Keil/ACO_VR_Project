using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondIntro : MonoBehaviour
{
    public AudioSource Scene2IntroSpeach;
    public AudioSource Scene2EndSpeach;
    public GameObject MenuSphere;

    public Material LHand;
    public Material RHand;
    public float slideDelta = 0.01f;
    public float slideTime = 0.01f;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        RHand.SetFloat("_Dissolve", 1);
        LHand.SetFloat("_Dissolve", 1);

        if (StoredKnowledge.MenuUnlocked)
        {
            MenuSphere.SetActive(true);
        }
        if (!StoredKnowledge.Played_Scene_2)
        {
            PlayScene2Intro();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StoredKnowledge.End_Game_2 && !StoredKnowledge.Played_Scene_2)
        {
            StartCoroutine(WaitForEndSpeech(Scene2EndSpeach));

        }
    }

    public void MaterialiseHands()
    {
        StartCoroutine(ResolveHands());
    }

    public void DematerialiseHands()
    {
        StartCoroutine(DissolveHands());
    }

    IEnumerator ResolveHands()
    {
        while (RHand.GetFloat("_Dissolve") > 0)
        {
            RHand.SetFloat("_Dissolve", Mathf.MoveTowards(RHand.GetFloat("_Dissolve"), 0, slideDelta));
            LHand.SetFloat("_Dissolve", Mathf.MoveTowards(LHand.GetFloat("_Dissolve"), 0, slideDelta));
            yield return new WaitForSeconds(slideTime);
        }
    }

    IEnumerator DissolveHands()
    {
        while (RHand.GetFloat("_Dissolve") < 1)
        {
            RHand.SetFloat("_Dissolve", Mathf.MoveTowards(RHand.GetFloat("_Dissolve"), 1, slideDelta));
            LHand.SetFloat("_Dissolve", Mathf.MoveTowards(LHand.GetFloat("_Dissolve"), 1, slideDelta));
            yield return new WaitForSeconds(slideTime);
        }
    }

    public void PlayScene2Intro()
    {
        StartCoroutine(WaitForIntroSpeech(Scene2IntroSpeach));
    }

    IEnumerator WaitForIntroSpeech(AudioSource source)
    {
        yield return new WaitForSeconds(1.5f);
        source.Play();
        anim.Play("Dust Scene Intro");
        yield return new WaitWhile(() => source.isPlaying);
        StoredKnowledge.Start_Game_2 = true;
    }
    IEnumerator WaitForEndSpeech(AudioSource source)
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);
        StoredKnowledge.Played_Scene_2 = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Scene_3_Comet");
    }
}