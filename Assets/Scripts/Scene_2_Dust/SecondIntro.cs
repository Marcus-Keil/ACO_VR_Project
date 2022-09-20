using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondIntro : MonoBehaviour
{
    public AudioSource Scene2IntroSpeach;
    public AudioSource Scene2EndSpeach;
    public GameObject MenuSphere;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
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

    public void PlayScene2Intro()
    {
        StartCoroutine(Wait());
        anim.Play("Base Layer.New Animation", 0, 0.5f);
        StartCoroutine(WaitForIntroSpeech(Scene2IntroSpeach));
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator WaitForIntroSpeech(AudioSource source)
    {
        source.Play();
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