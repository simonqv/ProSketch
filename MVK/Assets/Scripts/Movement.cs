using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  public float moveSpeed = 1f;
  public Transform movePoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
        movePoint.position += new Vector3(moveSpeed*Time.deltaTime*Input.GetAxisRaw("Horizontal"),0f,0f);
      }

      if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
        movePoint.position += new Vector3(0f,0f,moveSpeed*Time.deltaTime*Input.GetAxisRaw("Vertical"));
      }
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
    }
}
