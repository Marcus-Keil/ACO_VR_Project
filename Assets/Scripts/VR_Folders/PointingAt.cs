using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingAt : MonoBehaviour
{
    private LineRenderer LR;
    public float Base_Cone_Length = 0.5f;
    private float Cone_Length;
    public Vector3 StartPoint;
    private Vector3 StartPointTransform;
    private Vector3 EndPointTransform;

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        Cone_Length = Base_Cone_Length;
    }

    // Update is called once per frame
    void Update()
    {
        StartPointTransform = transform.TransformPoint(StartPoint);
        EndPointTransform = transform.TransformPoint(StartPoint.x, StartPoint.y, StartPoint.z + Cone_Length);
        LR.SetPosition(0, StartPointTransform);
        LR.SetPosition(1, EndPointTransform);
    }

    private void FixedUpdate()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(StartPointTransform, transform.TransformDirection(Vector3.forward), out hit, Base_Cone_Length, layerMask))
        {
            Cone_Length = hit.distance;
            Debug.Log("Did Hit");
        }
        else
        {
            Cone_Length = Base_Cone_Length;
            Debug.Log("Did not Hit");
        }
    }
}
