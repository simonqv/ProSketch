using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Class to store scene information.
 * Used for saving files. 
 */
[System.Serializable]
public class SceneData
{
    // Object data
    public List<ObjectInfo> objects;
    public string fileName;
    public SceneData()
    {
        objects = new List<ObjectInfo>();
        List<GameObject> rootObjects = new List<GameObject>();
        var scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // This stores every object in the scene, even light and camera etc. 
        foreach (var go in rootObjects)
        {
            objects.Add(new ObjectInfo(go));
        }
    }

    public void SetFileName(string name)
    {
        fileName = name;
    }
}
