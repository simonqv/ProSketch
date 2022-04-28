using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave
{
    private static bool _load;
    private static SceneData _sceneData;

    public static void Setter(SceneData x)
    {
        _load = true;
        _sceneData = x;
    }

    public static SceneData GetSceneData()
    {
        return _sceneData;
    }

    public static bool GetLoad()
    {
        return _load;
    }
}
