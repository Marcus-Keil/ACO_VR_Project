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
        StoredKnowledge.StartTutorial_1 = false;
        StoredKnowledge.DoneTutorial_1 = false;
        StoredKnowledge.End_Game_1 = false;
        StoredKnowledge.Played_Scene_1 = false;
        StoredKnowledge.StartTutorial_2 = false;
        StoredKnowledge.Start_Game_2 = false;
        StoredKnowledge.End_Game_2 = false;
        StoredKnowledge.Played_Scene_2 = false;
        StoredKnowledge.StartTutorial_3 = false;
        StoredKnowledge.Start_Game_3 = false;
        StoredKnowledge.End_Game_3 = false;
        StoredKnowledge.Played_Scene_3 = false;
        StoredKnowledge.MenuUnlocked = false;
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("Scene_1_Molecule");
    }
}
