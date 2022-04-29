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

    /*
     * Clears a scene of every GameObject, excluding things like the light source and such.
     * Populates the scene with saved GameObjects in the save file.
     * Argument: SceneData containing the GameObjects to spawn.
     */
    public void SpawnLoadedScene(SceneData sceneData)
    {
        // Destroys GameObjects in active scene.
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.CompareTag("GameObject") || go.CompareTag("room") || go.name == "Camera(Clone)")
            {
                Destroy(go);
            }
        }

        // Special case for room, needed to set its size before spawning it.
        foreach (var oi in sceneData.objects)
        {
            if (oi.GetTag() == "room")
            {
                RoomClass.Setter((int) oi.GetObjectScale().x, (int) oi.GetObjectScale().z);
            }
        }

        // Iterate over all objects and spawn them accordingly.
        foreach (var objectInfo in sceneData.objects)
        {
            string cat = objectInfo.GetObjectCategory();
            if (cat == "room")
            {
                FindObjectOfType<RoomManager>().Reset();
            }
            else
            {
                cat = char.ToUpper(cat[0]) + cat.Substring(1);
                var obName = objectInfo.GetName().Replace("(Clone)", "");
                var variableForInstance = Resources.Load("Equipment/" + cat + "/" + obName) as GameObject;
                if (variableForInstance != null)
                {
                    var instance = Instantiate(variableForInstance);
                    instance.transform.position = objectInfo.GetObjectPosition();
                    instance.transform.rotation = objectInfo.GetObjectRotation();
                    instance.transform.localScale = objectInfo.GetObjectScale();
                    instance.tag = objectInfo.GetTag();
                    instance.AddComponent<ObjectCategory>().category = cat;
                }
            }
        }
    }
}
