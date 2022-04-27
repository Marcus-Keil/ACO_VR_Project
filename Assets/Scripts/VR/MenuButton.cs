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
            if (Scene != "Exit")
            {
                SceneManager.LoadScene(Scene);
            }
            else if (Scene == "Exit")
            {
                Application.Quit();
            }
            img.color = Color.gray;
        }
    }
}