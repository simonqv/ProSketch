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

        var scroll = Input.mouseScrollDelta;
        if (scroll.y != 0)
        {
            var scrollOffset_y = _fileList.scrollOffset.y;
            var scrollOffset_x = _fileList.scrollOffset.x;
            Debug.Log(scrollOffset_y);
            Debug.Log("scroll " + scroll.y);
            _fileList.scrollOffset = new Vector2(scrollOffset_x, scrollOffset_y + (3 * scroll.y));
        }
    }

    public void ReadInputString(string s)
    {
        _filename = s;
    }
    

    public void ChooseFile()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _fileList = root.Q<ScrollView>("file-names");
        // var dropdown = root.Q<DropdownField>("Files-dropdown");
        // dropdown.choices.Clear();
        // var dropdown = GetComponent<Dropdown>();
        // dropdown.options.Clear(); 
        
        var path = SaveSystem.getSaveFolderString();
        string[] folderFiles = System.IO.Directory.GetFiles(path);

        List<string> field = new List<string>();
        // fill dropdown with file names
        // exclude every second file (file.txt, file.txt.meta, file2.txt, file2.txt.meta)???
        for (int i = 0; i < folderFiles.Length; i++)
        {
            // if does not containt .meta??
            /*dropdown.options.Add(new Dropdown.OptionData()
            {
                text = folderFiles[i].Replace(path,"")
            });*/
            // Debug.Log(folderFiles[i].Replace(path,""));
            field.Add(folderFiles[i].Replace(path,""));
            var button = CreateButton(folderFiles[i].Replace(path,""));
            // Debug.Log("Button " + button.name);
            _fileList.contentContainer.hierarchy.Add(button);
            // file_list.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
        }

        // dropdown.choices.Add(field[1]);
/*
        dropdown.onValueChanged.AddListener(delegate
        {
            int index = dropdown.value;
            chosenFile = dropdown.options[index].text;
        });*/
    }

    public Button CreateButton(string mes)
    {
        var button = new Button();
        button.name = mes;
        button.text = mes;
        button.clickable = new Clickable(() => Debug.Log(mes));
        //button.clicked += () => { Debug.Log(mes); };
        return button;
    }
    
}
