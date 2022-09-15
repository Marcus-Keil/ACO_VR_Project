using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{

    public float timer = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        StoredKnowledge.StartTutorial = false;
        StoredKnowledge.DoneTutorial = false;
        StoredKnowledge.Played_Scene_1 = false;
        StoredKnowledge.Played_Scene_2 = false;
        StoredKnowledge.Played_Scene_3 = false;
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(1);
    }
}
