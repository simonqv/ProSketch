using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneHandler : MonoBehaviour
{
    private static string _filename;
    public static string chosenFile;

    private ScrollView _fileList;
    // public static VisualElement root;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Scene handler start");
        // root = GetComponent<UIDocument>().rootVisualElement;

    }

    private static void Save()
    {
        // TODO: Let user input name of file.
        SceneData sceneData = new SceneData();
        sceneData.SetFileName(_filename);
        // sceneData.SetFileName("room_name");
        SaveSystem.SaveRoom(sceneData);
    }

    // TODO: search for specific file, place objects in scene.
    private void Load()
    {
        Debug.Log("load :3");
        ChooseFile();
        // May have to wait until "chosenFile" is initialized in ChooseFile()
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
/*
        var scroll = Input.mouseScrollDelta;
        if (scroll.y != 0)
        {
            var scrollOffset_y = _fileList.scrollOffset.y;
            var scrollOffset_x = _fileList.scrollOffset.x;
            Debug.Log(scrollOffset_y);
            Debug.Log("scroll " + scroll.y);
            _fileList.scrollOffset = new Vector2(scrollOffset_x, scrollOffset_y + (3 * scroll.y));
        }*/
    }

    public void ReadInputString(string s)
    {
        _filename = s;
        Debug.Log(_filename);
    }
    
    // User can choose which scene/file to open from a dropdown  
    public void ChooseFile()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _fileList = root.Q<ScrollView>("file-names");
        var path = SaveSystem.getSaveFolderString();
        string[] folderFiles = System.IO.Directory.GetFiles(path);
        var fileName = "";
        for (int i = 0; i < folderFiles.Length; i++)
        {
            fileName = folderFiles[i];
            if (!fileName.EndsWith(".meta"))
            {
                var button = CreateButton(fileName.Replace(path,"").Replace(".txt",""));
                _fileList.contentContainer.hierarchy.Add(button);
                // file_list.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
            }
        }
    }

    // Creates a button for the load file/scene dropdown 
    public Button CreateButton(string mes)
    {
        var button = new Button();
        button.name = mes;
        button.text = mes;
        button.clickable = new Clickable(() => chosenFile = button.name);
        //button.clicked += () => { Debug.Log(mes); };
        return button;
    }
}