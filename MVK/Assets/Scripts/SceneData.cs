using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    // Room data
    public int roomWidth;
    public int roomHeight;
    public int roomLength;
    
    // Object data
    public List<GameObject> objects;
    public List<Vector3> positions;
    public List<Quaternion> angles;
    public int color; // placeholder, should be something else... gotta ask textures
    
    public SceneData(RoomClass room)
    {
        // Room data
        roomWidth = room.GetWidth() ;
        roomHeight = room.GetHeight();
        roomLength = room.GetLength();

        // this should add all objects...??
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
            {
                objects.Add(go);
                
                // förmodligen onödigt... då detta finns i Game Object
                positions.Add(go.transform.position);
                angles.Add(go.transform.rotation);
            }
        }
    }
}
