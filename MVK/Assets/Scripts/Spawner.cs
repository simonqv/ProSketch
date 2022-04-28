using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject prefab, string category)
    {
        var instance = Instantiate(prefab);
        GameObject
            .FindGameObjectWithTag("roomManager")
            .GetComponent<RoomManager>()
            .SetSelectedObject(instance.transform);
            
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
            string cat = objectInfo.objectCategory;
            if (cat == "room")
            {
                FindObjectOfType<RoomManager>().Reset();
            }
            else
            {
                cat = char.ToUpper(cat[0]) + cat.Substring(1);
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
