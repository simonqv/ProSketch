using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTrapett : MonoBehaviour
{
    public GameObject prefab;
    public int upperLimit;
    public int lowerLimit;
    void Update()
    {
        if (prefab.CompareTag("Selected"))
        {
            if (prefab.transform.localEulerAngles.z + Input.GetAxis("Mouse ScrollWheel") <= lowerLimit || prefab.transform.localEulerAngles.z + Input.GetAxis("Mouse ScrollWheel") >= upperLimit ) 
            {
                prefab.transform.Rotate(0,0,50 * Input.GetAxis("Mouse ScrollWheel"),Space.Self);
            }
        }
    }
}
