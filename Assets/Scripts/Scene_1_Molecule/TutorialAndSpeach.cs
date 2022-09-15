using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAndSpeach : MonoBehaviour
{
    public AudioSource Introduction;
    public AudioSource PostTutorial;
    public AudioSource StartChallenge;
    public AudioSource EndChallenge;

    public GameObject GlowOrb_R;
    public GameObject GlowOrb_L;
    public GoalPost GP;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (!StoredKnowledge.DoneTutorial)
        {
            IntroPlay();
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
        if (StoredKnowledge.End_Game_1 && !StoredKnowledge.Played_Scene_1)
        {
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
        StartCoroutine(WaitForIntroFinish(Introduction));
    }

    public void NoTutorialPlay()
    {
        StartCoroutine(Wait());
        StartCoroutine(WaitForIntroFinish(PostTutorial));
    }

    public void Outro()
    {
        StartCoroutine(Wait());
        StartCoroutine(WaitForOutroAudioFinish(EndChallenge));
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator WaitForIntroFinish(AudioSource source)
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);
        ActivateOrbs();
        StoredKnowledge.StartTutorial = true;
        StartCoroutine(WaitForTutorialAudioFinish(PostTutorial));
    }
    IEnumerator WaitForTutorialAudioFinish(AudioSource source)
    {
        Debug.Log(StoredKnowledge.StartTutorial);
        while (!StoredKnowledge.DoneTutorial)
        {
            yield return null;
        }
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);
        if (GlowOrb_R.activeSelf)
        {
            DeactivateOrbs();
        }
        GP.EnterIn();
        StoredKnowledge.StartTutorial = true;
    }
    IEnumerator WaitForOutroAudioFinish(AudioSource source)
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);
        StoredKnowledge.Played_Scene_1 = true;
    }
}
