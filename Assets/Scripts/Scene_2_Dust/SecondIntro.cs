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
            StoredKnowledge.Played_Scene_2 = false;
        }
        if (!StoredKnowledge.Played_Scene_2)
        {
            PlayScene2Intro();
            StoredKnowledge.Played_Scene_2 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StoredKnowledge.End_Game_2 && !StoredKnowledge.Played_Scene_2)
        {
            PlayEndScene2();
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
        StartCoroutine(WaitForIntroSpeech());
    }

    public void PlayEndScene2()
    {
        StoredKnowledge.Played_Scene_2 = true;
        StartCoroutine(WaitForEndSpeech());
    }

    IEnumerator WaitForIntroSpeech()
    {
        yield return new WaitForSeconds(1.5f);
        Scene2IntroSpeach.Play();
        anim.Play("Dust Scene Intro");
        yield return new WaitWhile(() => Scene2IntroSpeach.isPlaying);
        StoredKnowledge.Start_Game_2 = true;
    }
    IEnumerator WaitForEndSpeech()
    {
        Scene2EndSpeach.Play();
        yield return new WaitWhile(() => Scene2EndSpeach.isPlaying);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Scene_3_Comet");
    }
}