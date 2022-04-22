using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public Transform selectedObject = null;
    private RoomClass _room;

    [SerializeField] private Material highlightMaterial;   
    [SerializeField] private Material previousMaterial;

    private Ray _ray;
    public bool movebool = false;

    private Vector3 movePos;
    //private MoveObject _moveCTRL;

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
            return;
        };
        Debug.Log("Not a room :)");
        var selectionRenderer = obj.GetComponentInChildren<Renderer>();
        Debug.Log(selectionRenderer);
        if (selectionRenderer == null) return;
        selectedObject = obj;
        Debug.Log("Renderer");
        previousMaterial = selectionRenderer.material;
        selectionRenderer.material = highlightMaterial;
        selectedObject.tag = "Selected";
        _room = selectedObject.GetComponent<RoomClass>();
        Debug.Log("Select");
    }
    
    public void SetSelectedObject(Transform obj)
    {
        if (selectedObject != null)
        {
            var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
            selectionRenderer.material = previousMaterial;
            selectedObject.tag = "GameObject";
        }
        selectedObject = obj;
        var selectionRenderer2 = selectedObject.GetComponentInChildren<Renderer>();
        previousMaterial = selectionRenderer2.material;
        selectionRenderer2.material = highlightMaterial;
        selectedObject.tag = "Selected";
        _room = selectedObject.GetComponent<RoomClass>();
    }

    void UnselectObject()
    {
        if(selectedObject == null) return;
        Debug.Log("Deselect init");
        selectedObject.tag = "GameObject";
        
        var selectionRenderer = selectedObject.GetComponentInChildren<Renderer>();
        if (selectionRenderer == null) return;
        selectionRenderer.material = previousMaterial;
        Debug.Log("Deselect");
        selectedObject = null;

    }

    void Start()
    {
        _room = gameObject.AddComponent<RoomClass>();
        _room.CreateRoom();
        camera = _room.cam;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
            Debug.Log("Mouse0");
            TrySelectObject();
        }
        else if (selectedObject != null && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Mouse1");
            UnselectObject();
        }
        Move();
        Rotate();
    }

    private void Move()
    {
        if (selectedObject == null) return;
        Debug.Log("Move");
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

    public void Rotate()
    {
        if (selectedObject != null)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                selectedObject.transform.RotateAround(selectedObject.position,selectedObject.up,Input.GetAxis("Horizontal") * 3);
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                // selectedObject.transform.RotateAround(selectedObject.position,selectedObject.right,Input.GetAxis("Vertical"));
            }
        }
    }
}
