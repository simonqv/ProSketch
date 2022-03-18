using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private string selectableTag = "nonSelectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material previousMaterial;

    private Transform _selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        UI_script = paintButton.GetComponent<UIController>();
        if (Input.GetMouseButtonDown(0)) {
            
            if (_selection != null) {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                if (!_paintButton.clicked) {                                     //FIXA med flagga
                    selectionRenderer.material = previousMaterial;                      
                    _selection.tag= "Untagged";
                    _selection = null;
                }
            }
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
                    }
                    _selection = selection;
                }
            }
        }
    }
}
