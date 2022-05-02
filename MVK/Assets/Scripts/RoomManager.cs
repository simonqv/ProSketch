using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    public new Camera camera;
    public Transform selectedObject;
    public RoomClass Room { get; private set; }

    public Material highlightMaterial;   
    [SerializeField] private Material previousMaterial;

    private Ray _ray;
    public bool movebool;
    public bool pickUp;
    private UIController _uiController;

    void TrySelectObject()
    {
        if (selectedObject != null) return;
        
        _ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (!Physics.Raycast(_ray, out RaycastHit hit)) return;
        
        //Get object from hit
        Transform obj = hit.transform;
        if (hit.transform.CompareTag("room") ||hit.transform == null) return;
        
        SetSelectedObject(obj);
    }
    
    public void SetSelectedObject(Transform obj)
    {
        if (selectedObject != null)
        {
            var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
            selectionRenderer.material = selectedObject.GetComponent<ObjProperties>().mainColor;

            selectedObject.tag = "GameObject";
        }
        
        selectedObject = obj;
        var selectionRenderer2 = selectedObject.GetComponentInChildren<Renderer>();
        
        if (selectionRenderer2 == null) return;
        
        previousMaterial = selectionRenderer2.material;
        selectionRenderer2.material = highlightMaterial;
        
        selectedObject.tag = "Selected";
        pickUp = true;
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
        
        GameObject.Find("Sidebar").GetComponent<UIController>().UnselectTool();
        selectedObject = null;
    }

    void Start()
    {
        Room = gameObject.AddComponent<RoomClass>();
        Room.CreateRoom();
        camera = Room.cam;
        if (LoadSave.GetLoad())
        {
            var spawner = GameObject.Find("SpawnerContainer");
            spawner.GetComponent<Spawner>().SpawnLoadedScene(LoadSave.GetSceneData());
        }
    }
    
    /*
     * Works like Start, but is callable
     */
    public void Reset()
    { 
        Room = gameObject.AddComponent<RoomClass>();
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

        if (selectedObject != null)
        {
            var selectionRenderer2 = selectedObject.GetComponentInChildren<Renderer>();
            previousMaterial = selectionRenderer2.material;
            selectionRenderer2.material = highlightMaterial;
            selectedObject.tag = "Selected";
            pickUp = true;
        }
        Move();
    }

    private void Move()
    {
        if (selectedObject == null) return;
        
        _ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(_ray, out RaycastHit raycastHit) && movebool)
        {
            if (raycastHit.transform.CompareTag("Selected")) return;
            
            selectedObject.position = new Vector3(raycastHit.point.x,0.2f,raycastHit.point.z);
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
