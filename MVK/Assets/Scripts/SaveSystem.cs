using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public const string FileExt = ".txt";
    
    // Change to this one later
    // public static readonly string SaveFolder = Application.persistentDataPath + "/Saves/";
    private static readonly string SaveFolder = Application.dataPath + "/Saves/";

    /*
     * Saves room/scene as a json file in folder "Saves" 
     */
    public static void SaveRoom(SceneData sceneData)
    {
        // Test to see if save folder exists
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }

        // Translate scene data to json format and write file
        var json = JsonUtility.ToJson(sceneData);
        
        File.WriteAllText(SaveFolder + sceneData.fileName + FileExt, json);
    }
    
    public static string GetSaveFolderString()
    {
        return SaveFolder;
    }
    
    /*
     * Loads room/scene from file 
     */
    public static SceneData LoadRoom(string name)
    { 
        // If file exists: open and read file
        if (File.Exists(SaveFolder + name + FileExt)) {
            var saveString = File.ReadAllText(SaveFolder + name + FileExt);
            var sceneData = JsonUtility.FromJson<SceneData>(saveString);
            return sceneData;
        } 
        else
        {
            Debug.Log("Save file not found in " + SaveFolder + name+ FileExt );
            return null;
        }
    }
}
