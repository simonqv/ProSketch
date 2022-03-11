using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBar : MonoBehaviour
{
    public GameObject bar;
    public int upperLimit;
    public int lowerLimit;
    
    
    

    void Update()
    {
        if (bar.CompareTag("Selected"))
        {
            if (bar.transform.position.y + 50*Input.GetAxis("Mouse ScrollWheel") <= upperLimit && bar.transform.position.y + 50*Input.GetAxis("Mouse ScrollWheel") >= lowerLimit)
            {
                bar.transform.position += new Vector3(0,Input.GetAxis("Mouse ScrollWheel")*50,0);
            }
        }
    }
}
