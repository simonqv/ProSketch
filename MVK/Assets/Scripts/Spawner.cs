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
            var obName = objectInfo.objectName.Replace("(Clone)", "");

            string cat = objectInfo.objectCategory.ToLower();
            if (cat == "room")
            {
                FindObjectOfType<RoomManager>().Reset();
            }
            else if (cat == "plinth")
            {
                var objectArray = Resources.LoadAll("equipment/plinth", typeof(GameObject));
                foreach (var plinth in objectArray)
                { 
                    if (plinth.name == obName) Inst(plinth, objectInfo, cat);
                }
            }
            else if (cat == "ball")
            {
                var objectArray = Resources.LoadAll("equipment/ball", typeof(GameObject));
                foreach (var ball in objectArray)
                {
                    if (ball.name == obName) Inst(ball, objectInfo, cat);
                }
            } else if (cat == "bench")
            {
                var objectArray = Resources.LoadAll("equipment/bench", typeof(GameObject));
                foreach (var bench in objectArray)
                {
                    if (bench.name == obName) Inst(bench, objectInfo, cat);
                }
            } else if (cat == "hulahoop")
            {
                var objectArray = Resources.LoadAll("equipment/hulahoop", typeof(GameObject));
                foreach (var hula in objectArray)
                {
                    if (hula.name == obName) Inst(hula, objectInfo, cat);
                }
            } else if (cat == "mattor")
            {
                var objectArray = Resources.LoadAll("equipment/mattor", typeof(GameObject));
                foreach (var matta in objectArray)
                {
                    if (matta.name == obName) Inst(matta, objectInfo, cat);
                }
            } else if (cat == "stallningar")
            {
                var objectArray = Resources.LoadAll("equipment/stallningar", typeof(GameObject));
                foreach (var stallning in objectArray)
                {
                    if (stallning.name == obName) Inst(stallning, objectInfo, cat);
                }
            } else if (cat == "trampett")
            {
                var objectArray = Resources.LoadAll("equipment/trampett", typeof(GameObject));
                foreach (var trampett in objectArray)
                {
                    if (trampett.name == obName) Inst(trampett, objectInfo, cat);
                }
            }
        }
    }

    private void Inst(Object obs, ObjectInfo objectInfo, string cat)
    {
        var instance = Instantiate(obs) as GameObject;
        instance.transform.position = objectInfo.objectPosition;
        instance.transform.rotation = objectInfo.objectRotation;
        instance.transform.localScale = objectInfo.objectScale;
        instance.tag = objectInfo.objectTag;
        instance.AddComponent<ObjectCategory>().category = cat;
    }
}
