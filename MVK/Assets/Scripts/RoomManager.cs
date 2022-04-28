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

    public Material highlightMaterial;   
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
        Debug.Log("Hit");
        //Get object from hit
        Transform obj = hit.transform;
        Debug.Log(hit.transform.name);
        if (hit.transform.CompareTag("room") ||hit.transform == null)
        {
            Debug.Log("Hit the room!");
            //pickUp = false;
            return;
        };
        Debug.Log("Not a room :)");
        Debug.Log(hit.transform.tag);
        var selectionRenderer = obj.GetComponentInChildren<Renderer>();
        Debug.Log(selectionRenderer);
        if (selectionRenderer == null) return;
        selectedObject = obj;
        Debug.Log("Renderer");
        previousMaterial = selectionRenderer.material;
        selectionRenderer.material = highlightMaterial;
        selectedObject.tag = "Selected";
        //pickUp = true;
        Room = selectedObject.GetComponent<RoomClass>();
        Debug.Log("Select");
    }
    
    public void SetSelectedObject(Transform obj)
    {
        if (selectedObject != null)
        {
            var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
            selectionRenderer.material = selectedObject.GetComponent<ObjProperties>().mainColor;
            selectedObject.tag = "GameObject";
            //pickUp = false;
        }
        selectedObject = obj;
        var selectionRenderer2 = selectedObject.GetComponentInChildren<Renderer>();
        previousMaterial = selectionRenderer2.material;
        selectionRenderer2.material = highlightMaterial;
        selectedObject.tag = "Selected";
        pickUp = true;
        Room = selectedObject.GetComponent<RoomClass>();
    }

    void UnselectObject()
    {
        if(selectedObject == null) return;
        Debug.Log("Deselect init");
        selectedObject.tag = "GameObject";
        
        
        var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
        if (selectionRenderer == null) return;
        var prevMat = selectedObject.GetComponent<ObjProperties>().mainColor;
        selectionRenderer.material = prevMat;
        Debug.Log("Deselect");
        
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
            Debug.Log("Mouse0");
            Debug.Log(selectedObject);
            if (selectedObject != null)
            {
                Debug.Log(selectedObject);
                pickUp = true;
            }
        }
        
        else if (selectedObject != null && Input.GetKeyDown(KeyCode.Mouse1) && pickUp)
        {
            Debug.Log("Mouse1");
            pickUp = false;
            movebool = false;
            UnselectObject();
            
        }

        if (selectedObject != null)
        {
            var selectionRenderer2 = selectedObject.GetComponentInChildren<Renderer>();
            previousMaterial = selectionRenderer2.material;
            selectionRenderer2.material = highlightMaterial;
            selectedObject.tag = "Selected";
            pickUp = true;
        }
        Move();
        //Rotate();
    }

    private void Move()
    {
        //Debug.Log(selectedObject);
        
        if (selectedObject == null) return;
        
        _ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit raycastHit) && movebool) {
                
            //Lägg till att kolla om current pos n+ next pos har intersecting colliders,
            //skapa trigger: Om det blir intersect ignore action
    
            // Avoid selectedObject collider
            //if (raycastHit.collider != selectedObject.gameObject.GetComponent<Collider>()) {
                    
            selectedObject.position = new Vector3(raycastHit.point.x,0.2f,raycastHit.point.z);
            //}
            // Debug.Log(Input.mousePosition); // Debug print out mouseposition when moving mouse
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
        }
    }
}
