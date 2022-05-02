using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjProperties : MonoBehaviour
{
    public GameObject prefab;
    public Material mainColor;
    public Renderer pre;
    
    void Start()
    {
        pre = prefab.GetComponentInChildren<Renderer>();
        pre.material = mainColor;
    }
}
