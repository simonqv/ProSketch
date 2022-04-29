using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // Change to this one later
    // public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    private static readonly string SaveFolder = Application.dataPath + "/Saves/";
    
    
    // TODO: Let user input file name
    public static void SaveRoom(SceneData sceneData)
    {
        // Test to see if save folder exists
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
        Debug.Log("hellurr");
        // Translate scene data to json format and write file
        var json = JsonUtility.ToJson(sceneData);
        
        File.WriteAllText(SaveFolder + sceneData.fileName + ".txt", json);
        // File.WriteAllText(SaveFolder + s + ".txt", json);
    }

    public static string getSaveFolderString()
    {
        return SaveFolder;
    }
    
    // TODO: Let user see all files and choose one.
    // TODO: Place all objects in scene.
    public static SceneData LoadRoom(string name)
    { 
        // If file exists: open and read file
        if (File.Exists(SaveFolder + name + ".txt")) {
            var saveString = File.ReadAllText(SaveFolder + name + ".txt");
            var sceneData = JsonUtility.FromJson<SceneData>(saveString);
            // GameObject.FindObjectOfType<Spawner>().SpawnLoadedScene(sceneData);
            return sceneData;
        } 
        else
        {
            Debug.Log("Save file not found in " + SaveFolder + name+ ".txt" );
            return null;
        }
    }
}
