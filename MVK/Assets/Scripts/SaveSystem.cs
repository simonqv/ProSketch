using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveSystem
{
    
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";

    public static void SaveRoom(RoomClass room)
    {
        // Test to see if save folder exists
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        
        // Borde funka så här?
        // Behöver ha någon mata in namn på scenen grej, så att man kan spara olika
        SceneData sceneData = new SceneData(room);
        string json = JsonUtility.ToJson(sceneData);
        File.WriteAllText(SAVE_FOLDER + "/room_name.txt", json);
    }

    
    public static void LoadRoom()
    {
        // Problem... välja vilken som ska laddas... hmmmm
        // Kan väl göra nån lista som visar alla ens sceners namn så kan man välja från den...
        if (File.Exists(SAVE_FOLDER + "/room_name.txt")) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/room_name.txt");
            Debug.Log("Loaded " + saveString);
            SceneData sceneData = JsonUtility.FromJson<SceneData>(saveString);
            
            
            // Borde väl göra om så att denna placerar ut allt också... 
            // return sceneData;
        } 
        else
        {
            Debug.LogError("Save file not found in " + SAVE_FOLDER + "/room_name.txt" );
            // return null;
        }

    }
}
