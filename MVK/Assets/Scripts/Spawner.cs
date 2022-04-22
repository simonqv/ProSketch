using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject prefab, string category)
    {
        var instance = Instantiate(prefab);
        instance.AddComponent<ObjectCategory>().category = category;

    }

    public void SpawnLoadedScene(SceneData sceneData)
    {

        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.name == "Directional Light" || go.name == "EventSystem")
            {
                continue;
            }
            Destroy(go);
        }
        
        foreach (var oi in sceneData.objects)
        {
            if (oi.objectTag == "room")
            {
                RoomClass.Setter((int) oi.objectScale.x, (int) oi.objectScale.z);
            }
        }
        foreach (var objectInfo in sceneData.objects)
        {
            Debug.Log(objectInfo.objectName);
            string cat = objectInfo.objectCategory;
            if (cat == "room")
            {
                Debug.Log("Room????");
                FindObjectOfType<RoomManager>().Reset();
            }
            else 
            {
                var variableForInstance = Resources.Load("Equipment/" + cat + "/" + objectInfo.objectName) as GameObject;
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
}
