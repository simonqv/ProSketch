using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject prefab)
    {
        var instance = Instantiate(prefab);
    }

    public void SpawnLoadedScene(SceneData sceneData)
    {
        foreach (var objectInfo in sceneData.objects)
        {
            Debug.Log(objectInfo.objectName);
            var variableForInstance = Resources.Load("Equipment/Plinth/" + objectInfo.objectName) as GameObject;
            if (variableForInstance != null)
            {
                var instance = Instantiate(variableForInstance);
                instance.transform.position = objectInfo.objectPosition;
                instance.transform.rotation = objectInfo.objectRotation;
                instance.transform.localScale = objectInfo.objectScale;
                instance.tag = objectInfo.objectTag;
            }
        }
    }
}
