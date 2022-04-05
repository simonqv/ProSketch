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
                Vector3 pos = go.transform.position;
                positions.Add(pos);
                angles.Add(go.transform.rotation);
            }
        }
        
        
        // int num_objects_in_scene = 10;  // Placeholder 
        // objects = new GameObject[num_objects_in_scene];
        // positions = new Vector3[num_objects_in_scene];
        // angles = new Vector3[num_objects_in_scene];
        
    }
    
    
}
