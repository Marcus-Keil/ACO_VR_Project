using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionSecondMovie : MonoBehaviour
{
    public Animator FadeAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade(string scene)
    {
        StartCoroutine(FadeAndScene(scene));
    }

    IEnumerator FadeAndScene(string scene)
    {
        FadeAnim.SetTrigger("Fade_Out");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
}
