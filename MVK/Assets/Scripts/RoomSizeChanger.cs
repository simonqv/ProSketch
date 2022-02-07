using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSizeChanger : MonoBehaviour
{
    Vector3 scaleTemp;
    Vector3 transformTemp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f){
          scaleTemp = transform.localScale;
          scaleTemp.z -= Time.deltaTime*Input.GetAxisRaw("Horizontal");
          transform.localScale = scaleTemp;
        }
        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f){
          scaleTemp = transform.localScale;
          //transform.position = new Vector3(0f,0f,Time.deltaTime*Input.GetAxisRaw("Vertical"));
          scaleTemp.x += Time.deltaTime*Input.GetAxisRaw("Vertical");
          transform.localScale = scaleTemp;

        }
    }
}
