using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMovieSpeach : MonoBehaviour
{
    public AudioSource SecondMovieSpeachAudio;

    public Material LHand;
    public Material RHand;
    public float slideDelta = 0.01f;
    public float slideTime = 0.01f;

    public Animator anim;
    public Animator CloudAnim;

    // Start is called before the first frame update
    void Start()
    {
        PlaySpeech();
    }

    public void PlaySpeech()
    {
        StartCoroutine(WaitForSpeech());
    }

    IEnumerator WaitForSpeech()
    {
        yield return new WaitForSeconds(1.5f);
        SecondMovieSpeachAudio.Play();
        CloudAnim.SetTrigger("Collapse");
        yield return new WaitWhile(() => SecondMovieSpeachAudio.isPlaying);
    }
}
