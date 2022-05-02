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
            if (oi.objectTag == "room")
            {
                RoomClass.Setter((int) oi.objectScale.x, (int) oi.objectScale.z);
            }
        }

        // Iterate over all objects and spawn them accordingly.
        foreach (var objectInfo in sceneData.objects)
        {
            string cat = objectInfo.objectCategory.ToLower();
            if (cat == "room")
            {
                FindObjectOfType<RoomManager>().Reset();
            }
            else if (cat == "plinth")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/plinth", typeof(GameObject));
                Debug.Log(obName);
                foreach (var plinth in objectArray)
                {
                    if (plinth.name == obName)
                    {
                        var instance = Instantiate(plinth) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
            }
            else if (cat == "ball")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/ball", typeof(GameObject));
                Debug.Log(obName);
                foreach (var ball in objectArray)
                {
                    if (ball.name == obName)
                    {
                        var instance = Instantiate(ball) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
                
            } else if (cat == "bench")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/bench", typeof(GameObject));
                Debug.Log(obName);
                foreach (var bench in objectArray)
                {
                    if (bench.name == obName)
                    {
                        var instance = Instantiate(bench) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
            } else if (cat == "hulahoop")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/hulahoop", typeof(GameObject));
                Debug.Log(obName);
                foreach (var hula in objectArray)
                {
                    if (hula.name == obName)
                    {
                        var instance = Instantiate(hula) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
            } else if (cat == "mattor")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/mattor", typeof(GameObject));
                Debug.Log(obName);
                foreach (var matta in objectArray)
                {
                    if (matta.name == obName)
                    {
                        var instance = Instantiate(matta) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
            } else if (cat == "stallningar")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/stallningar", typeof(GameObject));
                Debug.Log(obName);
                foreach (var stallning in objectArray)
                {
                    if (stallning.name == obName)
                    {
                        var instance = Instantiate(stallning) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
            } else if (cat == "trampett")
            {
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                var objectArray = Resources.LoadAll("equipment/trampett", typeof(GameObject));
                Debug.Log(obName);
                foreach (var trampett in objectArray)
                {
                    if (trampett.name == obName)
                    {
                        var instance = Instantiate(trampett) as GameObject;
                        instance.transform.position = objectInfo.objectPosition;
                        instance.transform.rotation = objectInfo.objectRotation;
                        instance.transform.localScale = objectInfo.objectScale;
                        instance.tag = objectInfo.objectTag;
                        instance.AddComponent<ObjectCategory>().category = cat;
                    }
                }
            }
            /*else
            {
                cat = char.ToLower(cat[0]) + cat.Substring(1);
                Debug.Log(cat + "          " + objectInfo.objectName);
                var obName = objectInfo.objectName.Replace("(Clone)", "");
                
                var x = Resources.LoadAll("equipment/" + cat, typeof(GameObject));
                
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
            }*/
        }
    }
}
