using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SunModifier : MonoBehaviour
{
    public GameObject Sun;
    public Light SunLight;
    public Material SunMaterial;

    private Color BaseColor = new Color(255, 255, 255);

    private float CurrentLightIntens = 0f;
    public float CurrentMatIntens = 0.00001f;
    private float SunScale_1 = 0.1f;
    private float SunLight_1 = 15.0f;
    private float SunMatEmission_1 = 0.002f;

    private float SunScale_2 = 0.4f;
    private float SunLight_2 = 700.0f;
    private float SunMatEmission_2 = 0.01f;
    private float SecondGrowth = 5.0f;

    private float slideDelta = 0.001f;
    private float slideTime = 0.1f;
    private float LightScaleDif = 1000f;

    public Animator CloudAnimator;
    public Animator FadeAnim;
    public Animator PlaneAnim;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Movie_2")
        {
            Sun.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            SunLight.intensity = 0.00000001f;
            SunMaterial.SetColor("_EmissionColor", BaseColor * CurrentMatIntens);
        }
    }

    private void Update()
    {
        if (CloudAnimator.GetBool("HasCollapsed"))
        {
            Sun.transform.localScale = new Vector3(SunScale_2, SunScale_2, SunScale_2);
            CurrentMatIntens = SunMatEmission_2;
            CurrentLightIntens = SunLight_2;
        }
        SunMaterial.SetColor("_EmissionColor", BaseColor * CurrentMatIntens);
        SunLight.intensity = CurrentLightIntens;
    }

    public void GrowSun_1()
    {
        StartCoroutine(SunGrowing_1());
    }

    IEnumerator SunGrowing_1()
    {
        while (Sun.transform.localScale.x < SunScale_1 || CurrentMatIntens < SunMatEmission_1 || SunLight.intensity < SunLight_1)
        {
            if (Sun.transform.localScale.x < SunScale_1)
                Sun.transform.localScale = Vector3.MoveTowards(Sun.transform.localScale, new Vector3(SunScale_1, SunScale_1, SunScale_1), slideDelta);
            if (CurrentMatIntens < SunMatEmission_1)
                CurrentMatIntens = Mathf.MoveTowards(CurrentMatIntens, SunMatEmission_1, slideDelta / LightScaleDif);
            if (CurrentLightIntens < SunLight_1)
                CurrentLightIntens = Mathf.MoveTowards(SunLight.intensity, SunLight_1, slideDelta* LightScaleDif);
            yield return new WaitForSeconds(slideTime);
        }
    }

    public void GrowSun_2()
    {
        StartCoroutine(SunGrowing_2());
    }

    IEnumerator SunGrowing_2()
    {
        while (Sun.transform.localScale.x < SunScale_2 || CurrentMatIntens < SunMatEmission_2 || SunLight.intensity < SunLight_2)
        {
            if (Sun.transform.localScale.x < SunScale_2)
                Sun.transform.localScale = Vector3.MoveTowards(Sun.transform.localScale, new Vector3(SunScale_2, SunScale_2, SunScale_2), (slideDelta* SecondGrowth));
            if (CurrentMatIntens < SunMatEmission_2)
                CurrentMatIntens = Mathf.MoveTowards(CurrentMatIntens, SunMatEmission_2, (slideDelta * SecondGrowth * 5) / LightScaleDif);
            if (CurrentLightIntens < SunLight_2)
                CurrentLightIntens = Mathf.MoveTowards(SunLight.intensity, SunLight_2, (slideDelta * SecondGrowth) * LightScaleDif);
            yield return new WaitForSeconds(slideTime);
        }
    }

    public void RotateSun()
    {
        StartCoroutine(SunRotate());
    }

    IEnumerator SunRotate()
    {
        while (Sun.transform.localRotation.x != 0)
        {
            Sun.transform.rotation = Quaternion.Euler(Vector3.MoveTowards(Sun.transform.rotation.eulerAngles, new Vector3(0, Sun.transform.localRotation.y, Sun.transform.localRotation.z), 0.1f));
            this.transform.rotation = Quaternion.Euler(Vector3.MoveTowards(this.transform.rotation.eulerAngles, new Vector3(90, 0, 0), 1f));
            yield return new WaitForSeconds(slideTime);
        }
    }

    public void HasCollapsed()
    {
        CloudAnimator.SetBool("HasCollapsed", true);
    }

    public void HasBlown()
    {
        CloudAnimator.SetBool("HasBlown", true);
    }

    public void PlaneZoom()
    {
        PlaneAnim.SetTrigger("Zoom");
    }

    public void Fade(string scene)
    {
        StartCoroutine(FadeAndScene(scene));
    }

    IEnumerator FadeAndScene(string scene)
    { 
        yield return new WaitForSeconds(1.0f);
        FadeAnim.SetTrigger("Fade_Out");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
}
