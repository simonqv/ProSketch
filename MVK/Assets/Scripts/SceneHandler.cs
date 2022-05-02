using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;

public class SceneHandler : MonoBehaviour
{
    // Name of file when saving
    private static string _filename;

    // Buttons for Save window
    private Button _saveFileWithNameButton;
    private Button _cancelSaveFileWithNameButton;

    // Button for Load window
    private Button _cancelChooseFileButton;
    
    // Scrollable list of all files 
    private ScrollView _fileList;
    
    private VisualElement _root;

    // Save and Load windows 
    private static GroupBox _fileNameInputWindow;
    private static GroupBox _chooseFileWindow;

    // Start is called before the first frame update
    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        
        _fileNameInputWindow = _root.Q<GroupBox>("Set-file-name-window");
        _fileNameInputWindow.style.display = DisplayStyle.None;
        
        _chooseFileWindow = _root.Q<GroupBox>("Choose-file-window");
        _chooseFileWindow.style.display = DisplayStyle.None;
        
        _saveFileWithNameButton = _root.Q<Button>("Confirm-file-input-button");
        _cancelSaveFileWithNameButton = _root.Q<Button>("Cancel-file-input-button");
        _cancelChooseFileButton = _root.Q<Button>("Cancel-choose-file");
        
        _saveFileWithNameButton.clicked += SaveFileWithNameButtonClicked;
        _cancelSaveFileWithNameButton.clicked += CancelSaveFileWithNameButtonClicked;
        _cancelChooseFileButton.clicked += CancelChooseFileButtonClicked;
    }

    /*
     * Displays Save window.
     */
    public void Save()
    {
        _fileNameInputWindow.style.display = DisplayStyle.Flex;
    }

    /*
     * Called when Save button clicked after user input file name.
     * Reads user input and creates a new Save file containing GameObject information.
     */
    private void SaveFileWithNameButtonClicked()
    {
        var fileNameField = _root.Q<TextField>("File-name-input").value;
        
        // fileName can only contain numbers, letters a-z, underscore(_), Hyphen-minus (-)
        if (Regex.IsMatch(fileNameField, @"^(([a-z]|[A-Z]|\d|-|_)+)$"))
        {
            SceneData sceneData = new SceneData();
            sceneData.SetFileName(fileNameField);
            SaveSystem.SaveRoom(sceneData);
            _fileNameInputWindow.style.display = DisplayStyle.None;
        }
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
    
    /*
     * Displays window with scrollable file list.
     */
    public void Load()
    {
        Debug.Log("Choose file: " + _chooseFileWindow.name);
        _chooseFileWindow.style.display = DisplayStyle.Flex;
        ChooseFile();
    }
    
    // User can choose which scene/file to open from a Scroll list  
    private void ChooseFile()
    {
        _fileList = _root.Q<ScrollView>("file-names");
        var path = SaveSystem.GetSaveFolderString();
        string[] folderFiles = System.IO.Directory.GetFiles(path);
        foreach (var fileName in folderFiles)
        {
            if (!fileName.EndsWith(".meta"))
            {
                var button = CreateButton(fileName.Replace(path,"").Replace(".txt",""));
                _fileList.contentContainer.hierarchy.Add(button);
            }
        }
    }

    // Creates a button for the load file/scene Scroll list 
    private Button CreateButton(string mes)
    {
        var button = new Button();
        button.AddToClassList("button-style");
        button.name = mes;
        button.text = mes;
        button.clickable = new Clickable(() => Loader(button.name));
        return button;
    }

    /*
     * When file button is pressed, load that file and spawn the scene.
     */
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