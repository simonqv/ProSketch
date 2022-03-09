using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBar : MonoBehaviour
{
    public GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        //barTransform = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (bar.CompareTag("Selected"))
        {
            if (bar.transform.position.y + 50*Input.GetAxis("Mouse ScrollWheel") <= 80 && bar.transform.position.y + 50*Input.GetAxis("Mouse ScrollWheel") >= -160 && Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                bar.transform.position += new Vector3(0,Input.GetAxis("Mouse ScrollWheel")*50,0);
            }
        }
    }
}
