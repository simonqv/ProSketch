using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        List<string> unDestructibles = new List<string>()
        {
            "Directional Light",
            "EventSystem",
            "SpawnerContainer",
            "SceneHandler",
            "RoomManager",
            "UIDocument"
        };
        
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            // If statement got a bit long so made a list and called .Contains() instead /Gustav
            if (unDestructibles.Contains(go.name))
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
