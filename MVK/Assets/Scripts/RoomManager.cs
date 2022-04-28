using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public new Camera camera;
    public Transform selectedObject = null;
    public RoomClass Room { get; private set; }

    [SerializeField] private Material highlightMaterial;   
    [SerializeField] private Material previousMaterial;

    private Ray _ray;
    public bool movebool = false;
    public bool pickUp;
    private Vector3 movePos;

    void TrySelectObject()
    {
        if (selectedObject != null) return;
        _ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out RaycastHit hit)) return;
        //Get object from hit
        Transform obj = hit.transform;
        if (hit.transform.CompareTag("room") ||hit.transform == null)
        {
            //pickUp = false;
            return;
        };
        SetSelectedObject(obj);
    }
    
    public void SetSelectedObject(Transform obj)
    {
        if (selectedObject != null)
        {
            var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
            if (selectionRenderer == null) return;
            selectionRenderer.material = previousMaterial;
            selectedObject.tag = "GameObject";
            //pickUp = false;
        }
        selectedObject = obj;
        var selectionRenderer2 = selectedObject.GetComponentInChildren<Renderer>();
        if (selectionRenderer2 == null) return;
        previousMaterial = selectionRenderer2.material;
        selectionRenderer2.material = highlightMaterial;
        selectedObject.tag = "Selected";
        //pickUp = true;
        Room = selectedObject.GetComponent<RoomClass>();
    }

    void UnselectObject()
    {
        if(selectedObject == null) return;
        selectedObject.tag = "GameObject";
        
        
        var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
        if (selectionRenderer == null) return;
        var prevMat = selectedObject.GetComponent<ObjProperties>().mainColor;
        selectionRenderer.material = prevMat;
        
        selectedObject = null;

    }

    void Start()
    {
        Room = gameObject.AddComponent<RoomClass>();
        Room.CreateRoom();
        camera = Room.cam;
    }

    public void Reset()
    { 
        Room.CreateRoom();
        camera = Room.cam;    
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !pickUp)
        {   
            TrySelectObject();
            if (selectedObject != null)
            {
                pickUp = true;
            }
        }
        
        else if (selectedObject != null && Input.GetKeyDown(KeyCode.Mouse1) && pickUp)
        {
            pickUp = false;
            movebool = false;
            UnselectObject();
            
        }
        Move();
        //Rotate();
    }

    private void Move()
    {
        
        if (selectedObject == null) return;
        
        _ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit raycastHit) && movebool) {
                
            //Lägg till att kolla om current pos n+ next pos har intersecting colliders,
            //skapa trigger: Om det blir intersect ignore action
    
            // Avoid selectedObject collider
            //if (raycastHit.collider != selectedObject.gameObject.GetComponent<Collider>()) {
                    
            selectedObject.position = new Vector3(raycastHit.point.x,0.2f,raycastHit.point.z);
            //}
        }
        
    }

    public void Rotate(float dir)
    {
        if (selectedObject != null)
        {
            selectedObject.transform.RotateAround(selectedObject.position,selectedObject.up,dir);
        }
    }
    
    // Function that deletes a selected object in scene.
    public void Delete()
    {
        if (selectedObject != null)
        {
            Destroy(selectedObject.gameObject);
            pickUp = false;
            UnselectObject();
        }
    }
}
