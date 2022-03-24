using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
   [SerializeField] private string selectableTag = "nonSelectable";
   [SerializeField] private Material highlightMaterial;   
   [SerializeField] private Material previousMaterial;

   private Transform _selection;
   //public GameObject pButton;
   //private UIController UI_script;

   // Start is called before the first frame update
   void Start()
   {
       //UI_script = pButton.GetComponent<UIController>();
   }

   // Update is called once per frame
   private void Update()
   {
       if (Input.GetMouseButtonDown(0)) {
         
           var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;
           if (Physics.Raycast(ray, out hit)) {
               var selection = hit.transform;
               if(!selection.CompareTag(selectableTag)) {
                   var selectionRenderer = selection.GetComponent<Renderer>();
                   if (selectionRenderer != null) {
                       selection.tag="Selected";
                       previousMaterial = selectionRenderer.material;
                       selectionRenderer.material = highlightMaterial;
                       Debug.Log("Select");
                   }
                   _selection = selection;
               }
               if (selection.CompareTag(selectableTag)) {
                   var selectionRenderer = _selection.GetComponent<Renderer>();
                   //bool pb = UI_script._paintButton.clicked;
                   Debug.Log("Deselect");
                   //selectionRenderer.material = previousMaterial;     
                   GameObject[] selectedObjects;
                   selectedObjects = GameObject.FindGameObjectsWithTag("Selected");
                   foreach(GameObject selected in selectedObjects) {
                       selectionRenderer = selected.GetComponent<Renderer>();
                       selected.tag= "Untagged";
                       ObjProperties selec = selected.transform.gameObject.GetComponent<ObjProperties>();
                       Debug.Log(selec.mainColor);
                      
                      

                      
                       selectionRenderer.material.SetColor("_Color",selec.mainColor);
                   }               
                   _selection = null;
               }
           }
       }
   }
}



