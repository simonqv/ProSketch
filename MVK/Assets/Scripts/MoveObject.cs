using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
  MoveObject lets user left-click with mouse to "select" an object in the scene.
  Then the user can move and rotate object. 
*/
public class MoveObject : MonoBehaviour
{
      public Camera cam;
      public bool isSelected;
      public Transform selectedObject;  // Transform of object

      public bool moves;
      public bool rotates;

      // Constants
      // Moving Object
      const string move = "m";
      const int MOUSEBUTTON = 0;        // Listen to left mouse button (left = 0, right = 1)
      const float YDISTANCE = 0.2f;     // Distance on y-axis for lifting object
      // Rotating object
      const string rotate = "r";
      // Exit
      const string exit = "enter";


    // Initialize
    public void Starter(Camera came)
    {
      isSelected = false;
      moves = false;
      rotates = false;
      //var room = gameObject.AddComponent<RoomClass>();
      cam = came;
      
    }

    // Update is called once per frame
    void Update() {
      if (!isSelected) {
        if (Input.GetMouseButtonDown(MOUSEBUTTON)) {
          isSelected = true;
          Ray ray = cam.ScreenPointToRay(Input.mousePosition);
          if (Physics.Raycast(ray, out RaycastHit objectHit)) {
              if (objectHit.transform.gameObject.tag != "room") {
                selectedObject = objectHit.transform;
              }
          }
        }
      }
      if (isSelected) {
        SelectOperation(selectedObject);
        if (moves) {
          if (Input.GetKey("d"))
          {
            isSelected = false;
            Drop();
          }
            Move();
        }
        else if(rotates) {
          RotateObject();
        }
      }

    }

  void Highlight(Transform objectTransform){
    objectTransform.GetComponent<Renderer>().material.color = Color.yellow;
  }

  void SelectOperation(Transform objectTransform) {
    // Here the user has selected an operation to do.
    // Note: Add menu commands later to this code block.

    // Highlight
    // Highlight(objectTransform);

    if(Input.GetKey(exit)) {
    }
    else if(Input.GetKey(rotate)){
      rotates = true;
    }
    else if(Input.GetKey(move)) {
      moves = true;
    }

  }


  void Move() {
    // Move object with mouse
    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
      // Avoid selectedObject collider
      if (raycastHit.collider != selectedObject.gameObject.GetComponent<Collider>()) {
        selectedObject.position = new Vector3(raycastHit.point.x,YDISTANCE,raycastHit.point.z);
      }
      // Debug.Log(Input.mousePosition); // Debug print out mouseposition when moving mouse
    }
  }

  void Drop() {
    moves = false;
    selectedObject.position = new Vector3(selectedObject.position.x,0,selectedObject.position.z);
    isSelected = false;
  }

  void RotateObject() {
    if (Input.GetKey("space")) {
      selectedObject.RotateAround(selectedObject.position, selectedObject.up, Time.deltaTime * 20);
    }
  }


}
