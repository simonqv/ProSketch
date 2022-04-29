using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            // If statement got a bit long so made a list and called .Contains() instead /Gustav
            if (go.CompareTag("GameObject") || go.CompareTag("room") || go.name == "Camera(Clone)")
            {
                Destroy(go);
            }
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
                Debug.Log(cat + "          " + objectInfo.objectName);
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var variableForInstance = Resources.Load("Equipment/" + cat + "/" + obName) as GameObject;
                if (variableForInstance != null)
                {
                    var instance = Instantiate(variableForInstance);
                    instance.transform.position = objectInfo.objectPosition;
                    instance.transform.rotation = objectInfo.objectRotation;
                    instance.transform.localScale = objectInfo.objectScale;
                    instance.tag = objectInfo.objectTag;
                    instance.AddComponent<ObjectCategory>().category = cat;
                }
            }
        }
    }
}
