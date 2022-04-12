using System;
using UnityEngine;

/*
 * Class too store information about objects.
 * Used for saving files. 
 */

[Serializable]
public class ObjectInfo
{
    public string objectTag;
    public string objectName;
    public Vector3 objectPosition;
    public Vector3 objectScale;
    public Quaternion objectRotation;

    public string GetTag()
    {
        return objectTag;
    }

    public string GetName()
    {
        return objectName;
    }
    
    // TODO: Store texture somehow
    public ObjectInfo(GameObject go)
    {
        objectTag = go.tag;
        objectName = go.name;
        objectPosition = go.transform.position;
        objectScale = go.transform.localScale;
        objectRotation = go.transform.rotation;
    }

}
