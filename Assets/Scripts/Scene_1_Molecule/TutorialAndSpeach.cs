using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialAndSpeach : MonoBehaviour
{
    public AudioSource Introduction;
    public AudioSource PostTutorial;
    public AudioSource EndChallenge;
    public AudioSource FailChallenge;
    public AudioSource EndSpeach;

    public GameObject Dispensers;
    public GameObject GlowOrb_R;
    public GameObject GlowOrb_L;
    public GameObject MenuSphere;
    public GoalPost GP;

    public Material LHand;
    public Material RHand;
    public float slideDelta = 0.1f;
    public float slideTime = 1.0f;

    public Animator anim;
    public Animator FadeAnim;

    private bool OutroPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        Dispensers.gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        RHand.SetFloat("_Dissolve", 1);
        LHand.SetFloat("_Dissolve", 1);

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

    public void IntroPlay()
    {
        StartCoroutine(WaitForIntroFinish());
    }

    public void NoTutorialPlay()
    {
        StartCoroutine(WaitForTutorialAudioFinish());
    }

    public void Outro()
    {
        StartCoroutine(WaitForOutroAudioFinish());
    }

    IEnumerator WaitForIntroFinish()
    {
        yield return new WaitForSeconds(1.5f);
        Introduction.Play();
        yield return new WaitForSeconds(0.5f);
        anim.Play("Tutorial Scene");
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
        anim.Play("Post_Tutorial");
        while (Dispensers.gameObject.transform.localScale.x < 1)
        {
            Dispensers.gameObject.transform.localScale = Vector3.MoveTowards(Dispensers.gameObject.transform.localScale, new Vector3(1, 1, 1), slideDelta);
            yield return new WaitForSeconds(slideTime);
        }
        yield return new WaitWhile(() => PostTutorial.isPlaying);
        if (GlowOrb_R.activeSelf)
        {
            DeactivateOrbs();
        }
        GP.EnterIn();
        StoredKnowledge.DoneTutorial_1 = true;
    }

    IEnumerator WaitForGrowth()
    {
        PostTutorial.Play();
        while (Dispensers.gameObject.transform.localScale.x < 1)
        {
            Dispensers.gameObject.transform.localScale = Vector3.MoveTowards(new Vector3(1, 1, 1), Dispensers.gameObject.transform.localScale, slideDelta);
            yield return new WaitForSeconds(slideTime);
        }
        yield return new WaitWhile(() => PostTutorial.isPlaying);
    }

    IEnumerator WaitForOutroAudioFinish()
    {
        if (StoredKnowledge.Succeeded_1)
        {
            EndChallenge.Play();
            yield return new WaitWhile(() => EndChallenge.isPlaying);
        }
        else
        {
            FailChallenge.Play();
            yield return new WaitWhile(() => FailChallenge.isPlaying);
        }
        EndSpeach.Play();
        yield return new WaitWhile(() => EndSpeach.isPlaying);
        StoredKnowledge.Played_Scene_1 = true;
        yield return new WaitForSeconds(2.0f);
        FadeAnim.SetTrigger("Fade_Out");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Movie_2");
    }
}
