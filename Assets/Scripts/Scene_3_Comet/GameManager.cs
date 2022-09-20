using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRPlugin;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public CometManager CM;

    public float ScreenGrowth = 0.2f;
    public float EarthGrowth = 0.1f;
    public float CometShrink = 0.00f;
    public float WaitBeforeDestroy = 1.0f;
    public float WaitAfterDestroy = 5.0f;
    public float SlideTime = 0.01f;
    public float GrowthTime = 0.01f;
    public float PoofTime = 0.01f;
    public GameObject Earth;
    public GameObject Screen;
    public float TimeScale;
    private float defaultTimeScale;
    private float fixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        defaultTimeScale = Time.timeScale;
    }

    public void DestroyComet(int MyIndex)
    {
        CM.DestroyComet(MyIndex);
    }

    public void SunPoof(int MyIndex, GameObject comet)
    {
        comet.GetComponent<Attractor_Grav>().CreatePoof();
        CM.DestroyComet(MyIndex);
    }

    public void AnimateEarth(int MyIndex, GameObject comet)
    {
        if (!StoredKnowledge.End_Game_3)
        {
            StartCoroutine(EarthAnimateCoroutine(MyIndex, comet));
        }
        else
        {
            comet.GetComponent<Attractor_Grav>().CreatePoof();
            CM.DestroyComet(MyIndex);
        }
    }

    IEnumerator EarthAnimateCoroutine(int MyIndex, GameObject comet)
    {
        Screen.gameObject.SetActive(true);
        Time.timeScale = TimeScale;
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        while (Screen.gameObject.transform.localScale.y < Screen.gameObject.transform.localScale.x)
        {
            Screen.gameObject.transform.localScale = Vector3.MoveTowards(Screen.gameObject.transform.localScale, new Vector3(Screen.gameObject.transform.localScale.x, Screen.gameObject.transform.localScale.x, Screen.gameObject.transform.localScale.x), ScreenGrowth);
            yield return new WaitForSeconds(SlideTime);
        }
        yield return new WaitForSeconds(WaitBeforeDestroy);
        comet.GetComponent<Attractor_Grav>().CreatePoof();
        while (comet.transform.localScale.y > 0.001f)
        {
            comet.transform.localScale = Vector3.MoveTowards(comet.transform.localScale, new Vector3(0.001f, 0.001f, 0.001f), CometShrink);
            yield return new WaitForSeconds(PoofTime);
        }
        CM.DestroyComet(MyIndex);
        while (Earth.gameObject.transform.localScale.y < 1)
        {
            Earth.gameObject.transform.localScale = Vector3.MoveTowards(Earth.gameObject.transform.localScale, new Vector3(1, 1, 1), EarthGrowth);
            yield return new WaitForSeconds(GrowthTime);
        }
        yield return new WaitForSeconds(WaitAfterDestroy);
        Time.timeScale = defaultTimeScale;
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        StoredKnowledge.End_Game_3 = true;
    }
}
