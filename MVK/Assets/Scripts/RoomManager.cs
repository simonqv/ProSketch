using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public Transform selectedObject;
    private RoomClass _room;

    private Ray _ray;

    private Vector3 movePos;
    //private MoveObject _moveCTRL;
    void Start()
    {
        _room = gameObject.AddComponent<RoomClass>();
        _room.CreateRoom();
        camera = _room.cam;
    }

    public void Reset()
    { 
        _room.CreateRoom();
        camera = _room.cam;    
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Call the select function from selection group
            
            
            _ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out RaycastHit objectHit))
            {
                if (objectHit.transform.gameObject.tag == "GameObject")
                {
                    objectHit.transform.gameObject.tag = "Selected";
                }
                
                if (objectHit.transform.gameObject.CompareTag("Selected")) {
                    selectedObject = objectHit.transform;
                }
                
            }
            
        }
        if (selectedObject != null && Input.GetKeyDown(KeyCode.Mouse1))
        {
            selectedObject = null;
        }
        Move();
        Rotate();
    }

    public void Move()
    {
        if (selectedObject != null)
        {
            _ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out RaycastHit raycastHit)) {
                
                //Lägg till att kolla om current pos n+ next pos har intersecting colliders,
                //skapa trigger: Om det blir intersect ignore action
                
                // Avoid selectedObject collider
                //if (raycastHit.collider != selectedObject.gameObject.GetComponent<Collider>()) {
                    
                    selectedObject.position = new Vector3(raycastHit.point.x,0.2f,raycastHit.point.z);
                //}
                // Debug.Log(Input.mousePosition); // Debug print out mouseposition when moving mouse
            }
        }
    }

    public void Rotate()
    {
        if (selectedObject != null)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                selectedObject.transform.RotateAround(selectedObject.position,selectedObject.up,Input.GetAxis("Horizontal"));
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                // selectedObject.transform.RotateAround(selectedObject.position,selectedObject.right,Input.GetAxis("Vertical"));
            }
        }
    }
}
