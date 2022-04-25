using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    private static string _filename;
    public static string chosenFile;  

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
    private void Load()
    {
        ChooseFile();
        // May have to wait until "chosenFile" is initialized in ChooseFile()
        // anrop
        var sceneData = SaveSystem.LoadRoom("room_name");
        if (sceneData != null)
        {
            var spawner = GameObject.Find("SpawnerContainer");
            spawner.GetComponent<Spawner>().SpawnLoadedScene(sceneData);
        }
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
    

    public void ChooseFile()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear(); 
        var path = SaveSystem.getSaveFolderString();
        string[] folderFiles = System.IO.Directory.GetFiles(path);
        
        //fill dropdown with file names
        // exclude every second file (file.txt, file.txt.meta, file2.txt, file2.txt.meta)???
        for (int i = 0; i < folderFiles.Length; i++)
        {
            // if does not containt .meta??
            dropdown.options.Add(new Dropdown.OptionData()
            {
                text = folderFiles[i].Replace(path,"")
            });
        }

        dropdown.onValueChanged.AddListener(delegate
        {
            int index = dropdown.value;
            chosenFile = dropdown.options[index].text;
        });
    }

   
}
