using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjProperties : MonoBehaviour
{
    public GameObject prefab;
    public Material mainColor;
    public Color selectColor;

    public Renderer pre;
    // Start is called before the first frame update
    void Start()
    {
        pre = prefab.GetComponentInChildren<Renderer>();
        pre.material = mainColor;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pre.material.SetColor("_Color",selectColor);
            prefab.transform.gameObject.tag = "Selected";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pre.material.SetColor("_Color",mainColor);
            prefab.transform.gameObject.tag = "Untagged";
        }*/

    }
}
