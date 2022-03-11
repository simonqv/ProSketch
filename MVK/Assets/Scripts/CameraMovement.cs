using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            camera.transform.position += new Vector3(Input.GetAxis("Horizontal") *10 ,0,0);
        }
        else if(Input.GetAxis("Vertical") != 0)
        {
            camera.transform.Rotate(-Input.GetAxis("Vertical"),0,0,Space.Self);
        }
    }
}
