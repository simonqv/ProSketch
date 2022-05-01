using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneHandler : MonoBehaviour
{
    private static string _filename;

    private Button SaveFileWithNameButton;
    private Button CancelSaveFileWithNameButton;

    private Button CancelChooseFileButton;
    
    private ScrollView _fileList;
    VisualElement root;

    private static GroupBox _chooseFileWindow;
    private static GroupBox _fileNameInputWindow;

    // Start is called before the first frame update
    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        _fileNameInputWindow = root.Q<GroupBox>("Set-file-name-window");
        _fileNameInputWindow.style.display = DisplayStyle.None;
        
        _chooseFileWindow = root.Q<GroupBox>("Choose-file-window");
        _chooseFileWindow.style.display = DisplayStyle.None;
        
        SaveFileWithNameButton = root.Q<Button>("Confirm-file-input-button");
        CancelSaveFileWithNameButton = root.Q<Button>("Cancel-file-input-button");
        CancelChooseFileButton = root.Q<Button>("Cancel-choose-file");
        
        SaveFileWithNameButton.clicked += SaveFileWithNameButtonClicked;
        CancelSaveFileWithNameButton.clicked += CancelSaveFileWithNameButtonClicked;
        CancelChooseFileButton.clicked += CancelChooseFileButtonClicked;
        Debug.Log("Scene handler started");
    }

    public void Save()
    {
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

    private void CancelChooseFileButtonClicked()
    {
        _fileList.contentContainer.Clear();
        _chooseFileWindow.style.display = DisplayStyle.None;
    }
    
    // TODO: search for specific file, place objects in scene.
    public void Load()
    {
        Debug.Log("Choose file: " + _chooseFileWindow.name);
        _chooseFileWindow.style.display = DisplayStyle.Flex;
        ChooseFile();
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
        button.clickable = new Clickable(() => Loader(button.name));
        return button;
    }

    private void Loader(string fileName)
    {
        var sceneData = SaveSystem.LoadRoom(fileName);
        if (sceneData != null)
        {
            if (SceneManager.GetActiveScene().name == "StartMenu")
            {
                LoadSave.Setter(sceneData);
                SceneManager.LoadScene("UI");
            }
            else
            {
                var spawner = GameObject.Find("SpawnerContainer");
                spawner.GetComponent<Spawner>().SpawnLoadedScene(sceneData);
            }
        }
        _fileList.contentContainer.Clear();
        _chooseFileWindow.style.display = DisplayStyle.None;

    }
}