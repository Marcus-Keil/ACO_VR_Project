using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor_Grav : MonoBehaviour
{
    private Rigidbody rb;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
}
