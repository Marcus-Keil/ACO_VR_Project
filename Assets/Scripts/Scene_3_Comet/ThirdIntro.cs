using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdIntro : MonoBehaviour
{
    public AudioSource Scene3IntroSpeach;
    public AudioSource Scene3EndSpeach;

    public GameObject ButtonSphere;
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
        if (!StoredKnowledge.Played_Scene_3)
            PlayScene3Intro();
        else
        {
            StoredKnowledge.End_Game_3 = false;
            StoredKnowledge.StartTutorial_3 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StoredKnowledge.End_Game_3 && !StoredKnowledge.Played_Scene_3)
        {
            StoredKnowledge.Played_Scene_3 = true;
            StartCoroutine(WaitForEndSpeech(Scene3EndSpeach));
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

    public void PlayScene3Intro()
    {
        StartCoroutine(Wait());
        anim.Play("Dust Scene Intro");
        StartCoroutine(WaitForIntroSpeech(Scene3IntroSpeach));
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator WaitForIntroSpeech(AudioSource source)
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);
        StoredKnowledge.StartTutorial_3 = true;
        ButtonSphere.SetActive(true);
    }
    IEnumerator WaitForEndSpeech(AudioSource source)
    {
        ButtonSphere.SetActive(false);
        MenuSphere.SetActive(true);
        yield return new WaitWhile(() => source.isPlaying);
        StoredKnowledge.MenuUnlocked = true;
    }
}