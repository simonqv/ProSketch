using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneHandler : MonoBehaviour
{
    private static string _filename;
    public static string chosenFile;

    private Button SaveFileWithNameButton;
    private Button CancelSaveFileWithNameButton;
    
    private ScrollView _fileList;
    VisualElement root;

    private static GroupBox _fileNameInputWindow;

    // Start is called before the first frame update
    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        _fileNameInputWindow = root.Q<GroupBox>("Set-file-name-window");
        _fileNameInputWindow.style.display = DisplayStyle.None;
        
        SaveFileWithNameButton = root.Q<Button>("Confirm-file-input-button");
        CancelSaveFileWithNameButton = root.Q<Button>("Cancel-file-input-button");

        SaveFileWithNameButton.clicked += SaveFileWithNameButtonClicked;
        CancelSaveFileWithNameButton.clicked += CancelSaveFileWithNameButtonClicked;
        
        Debug.Log("Scene handler started");
    }

    private static void Save()
    {
        // TODO: Let user input name of file.
        // SceneData sceneData = new SceneData();
        // sceneData.SetFileName("lol");
        // sceneData.SetFileName("room_name");
        // SaveSystem.SaveRoom(sceneData);
        
        _fileNameInputWindow.style.display = DisplayStyle.Flex;
    }

    private void SaveFileWithNameButtonClicked()
    {
        var fileNameField = root.Q<TextField>("File-name-input").value;
        SceneData sceneData = new SceneData();
        sceneData.SetFileName(fileNameField);
        SaveSystem.SaveRoom(sceneData);
        _fileNameInputWindow.style.display = DisplayStyle.None;
    }

    private void CancelSaveFileWithNameButtonClicked()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<TextField>("File-name-input").value = "";
        _fileNameInputWindow.style.display = DisplayStyle.None;
    }
    
    // TODO: search for specific file, place objects in scene.
    private void Load()
    {
        Debug.Log("load :3");
        ChooseFile();
        // May have to wait until "chosenFile" is initialized in ChooseFile()
        /*
        var sceneData = SaveSystem.LoadRoom("room_name");
        if (sceneData != null)
        {
            var spawner = GameObject.Find("SpawnerContainer");
            spawner.GetComponent<Spawner>().SpawnLoadedScene(sceneData);
        }*/
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

    // User can choose which scene/file to open from a dropdown  
    public void ChooseFile()
    {
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
        button.AddToClassList("button-style");
        button.name = mes;
        button.text = mes;
        button.clickable = new Clickable(() => Loader(button.name) /*chosenFile = button.name*/);
        //button.clicked += () => { Debug.Log(mes); };
        return button;
    }

    private void Loader(string fileName)
    {
        var sceneData = SaveSystem.LoadRoom(fileName);
        if (sceneData != null)
        {
            foreach (var x in sceneData.objects)
            {
                Debug.Log("Loader " + x.objectName);
            }
            var spawner = GameObject.Find("SpawnerContainer");
            spawner.GetComponent<Spawner>().SpawnLoadedScene(sceneData);
        }
    }
}