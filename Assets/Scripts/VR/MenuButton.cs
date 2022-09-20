using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string Scene;
    public Image img;
    public GameObject panel;
    private Color FreeCol = Color.white;
    private Color HoverCol = Color.green;
    private Color ClickedCol = Color.red;

    private void Start()
    {
        img = panel.GetComponent<Image>();
        img.color = FreeCol;
    }

    public void Hover()
    {
        img.color = HoverCol;
    }

    public void ReleaseHover()
    {
        img.color = FreeCol;
    }

    public void Click()
    {
        if (SceneManager.GetActiveScene().name != Scene)
        {
            img.color = ClickedCol;
            if (Scene != "Exit" && Scene != "Reset")
            {
                SceneManager.LoadScene(Scene);
            }
            else if (Scene == "Exit")
            {
                Application.Quit();
            }
            else if (Scene == "Reset")
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
                SceneManager.LoadScene("SplashScene");
            }
            img.color = Color.gray;
        }
    }
}