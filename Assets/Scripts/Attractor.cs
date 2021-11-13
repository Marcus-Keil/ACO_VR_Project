using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    private Rigidbody rb;
    private float minDistance = 0.075f;
    public bool Bondable;
    public GameObject Bonder;
    public CollisionManager CM;
    private float ScaleMult = 0.0005f;
    private float InitForce = 0.00001f;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        CM = GameObject.Find("CollisionManager").GetComponent<CollisionManager>();
        CM.ObjectsToAttract.Add(this);
        if (gameObject.CompareTag("Dust"))
        {
            gameObject.transform.localScale = new Vector3(Random.Range(1, 101) * ScaleMult, Random.Range(1, 101) * ScaleMult, Random.Range(1, 101) * ScaleMult);
            rb.mass = gameObject.transform.localScale.magnitude;
            rb.AddForce(new Vector3(Random.Range(-100, 100), Random.Range(-100, 101), Random.Range(-100, 101)) * (Random.Range(1, 101)* InitForce));
            rb.AddTorque(new Vector3(Random.Range(-100, 10), Random.Range(-100, 101), Random.Range(-100, 101)) * (Random.Range(1, 101) * InitForce));
        }
    }

    private void Update()
    {
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (Mathf.Abs(gameObject.transform.position.x) >= 20 || Mathf.Abs(gameObject.transform.position.y) >= 20 || Mathf.Abs(gameObject.transform.position.z) >= 20)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dust") || collision.gameObject.CompareTag("Planetoid"))
        {
            if ((collision.gameObject.GetComponent<Attractor>().Bonder == null || collision.gameObject.GetComponent<Attractor>().Bonder == gameObject) && (Bonder == null || Bonder == collision.gameObject))
            {
                if (this != null && !CM.ObjectsToCombine.Contains(this))
                {
                    CM.ObjectsToCombine.Add(this);
                    Bonder = collision.gameObject;
                    Bondable = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CM.ObjectsToCombine.Contains(this))
        {
            Bondable = false;
            Bonder = null;
            CM.ObjectsToCombine.Remove(this);
        }
    }
}
