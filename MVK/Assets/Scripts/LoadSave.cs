
/*
 * Used to be able to switch scene from main menu to a loaded scene
 */
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
