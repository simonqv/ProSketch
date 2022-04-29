using System;
using UnityEngine;

/*
 * Class too store information about objects.
 * Used for saving files. 
 */

[Serializable]
public class ObjectInfo
{
    private string _objectTag;
    private string _objectName;
    private Vector3 _objectPosition;
    private Vector3 _objectScale;
    private Quaternion _objectRotation;
    private string _objectCategory;
    
    public string GetTag() {return _objectTag;}

    public string GetName() {return _objectName;}

    public Vector3 GetObjectPosition() {return _objectPosition;}

    public Vector3 GetObjectScale() {return _objectScale;}

    public Quaternion GetObjectRotation() {return _objectRotation;}

    public string GetObjectCategory() {return _objectCategory;}
    
    // TODO: Store texture somehow
    public ObjectInfo(GameObject go)
    {
        _objectTag = go.tag;
        _objectName = go.name;
        _objectPosition = go.transform.position;
        _objectScale = go.transform.lossyScale;
        _objectRotation = go.transform.rotation;
        var cat = go.GetComponent<ObjectCategory>();
        if (_objectName == "RoomManager")
        {
            _objectCategory = "room";
        }
        else if (cat != null)
        {
            _objectCategory = cat.category;
        }
        else
        {
            _objectCategory = "_";
        }
    }
}
