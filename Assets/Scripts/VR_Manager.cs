using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Manager : MonoBehaviour
{
    public List<GameObject> GrabbableObjects;
    private GameObject DynamicGO;
    public GameObject L_Controller;
    public GameObject R_Controller;

    // Start is called before the first frame update
    void Start()
    {
        DynamicGO = GameObject.Find("Dynamic");
        checkChild(DynamicGO);
    }

    // Update is called once per frame

    void checkChild(GameObject objectChecked)
    {
        if (objectChecked.transform.childCount > 0)
        {
            foreach (Transform child in objectChecked.transform)
            {
                checkChild(child.gameObject);
            }
        }
        else if (objectChecked.GetComponent<Grabbable>() != null)
        {
            GrabbableObjects.Add(objectChecked);
        }
    }
}
