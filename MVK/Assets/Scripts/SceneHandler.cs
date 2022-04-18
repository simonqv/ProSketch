using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    private static string _filename;
    
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Scene handler start");
    }

    private static void Save()
    {
        // TODO: Let user input name of file.
        SceneData sceneData = new SceneData();
        sceneData.SetFileName(_filename);
        SaveSystem.SaveRoom(sceneData);
    }

    // TODO: search for specific file, place objects in scene.
    private static void Load()
    {
        SaveSystem.LoadRoom();
    }
    
    // Update is called once per frame
    private void Update()
    {
        // TODO: make the pop-up for saving first
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Saving file");
            Save();
        } 
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Loading file");
            Load();
        }
    }

    public void ReadInputString(string s)
    {
        _filename = s;
        Debug.Log(_filename);
    }
}
