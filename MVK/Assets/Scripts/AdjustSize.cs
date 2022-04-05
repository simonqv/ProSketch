using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSize : MonoBehaviour
{
    public float size1;
    public float size2;
    public float size3;

    public GameObject prefab;
    // Update is called once per frame
    void Update()
    {
        if (prefab.CompareTag("Selected"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var scale = prefab.transform.localScale;
                scale.x = size1;
                prefab.transform.localScale = scale;
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var scale = prefab.transform.localScale;
                scale.x = size2;
                prefab.transform.localScale = scale;
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var scale = prefab.transform.localScale;
                scale.x = size3;
                prefab.transform.localScale = scale;
            }
        }
    }
}
