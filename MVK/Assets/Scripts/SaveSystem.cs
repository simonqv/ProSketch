using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveRoom(RoomClass room)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        // Should probably have some sort of string for "room" in the path... so different rooms have different names
        // Probably let user input name or something. 
        // Because it is binary, the filetype can be called whatever we want. I just put .fun for now
        string path = Application.persistentDataPath + "/room.fun";
        
        // This is to create a new save file... Gotta look up updating already existing ones as well
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneData sceneData = new SceneData(room);
        
        formatter.Serialize(stream, sceneData);
        stream.Close();
    }

    public static SceneData LoadRoom()
    {
        // Same as above... this should be for a specific file we want to load, not just generic room
        string path = Application.persistentDataPath + "/room.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData sceneData =   formatter.Deserialize(stream) as SceneData;
            stream.Close();
            
            return sceneData;

        }
        else
        {
            Debug.LogError("Save file not found in " + path );
            return null;
        }

    }
}
