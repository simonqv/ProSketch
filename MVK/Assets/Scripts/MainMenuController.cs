using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public Button CreateNewLessonButton;
    public Button LoadLessonButton;

    public Button ContinueToSceneButton;
    public Button CancelDimsInputButton;

    private GroupBox _setDimsBox;
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        CreateNewLessonButton = root.Q<Button>("Create-new-lesson-button");
        LoadLessonButton = root.Q<Button>("Load-lesson-button");
        ContinueToSceneButton = root.Q<Button>("Confirm-input-button");
        CancelDimsInputButton = root.Q<Button>("Cancel-input-button");
        
        _setDimsBox = root.Q<GroupBox>("Set-dims-box");
        _setDimsBox.style.display = DisplayStyle.None;


        CreateNewLessonButton.clicked += CreateNewLessonButtonPressed;
        LoadLessonButton.clicked += LoadLessonButtonPressed;
        ContinueToSceneButton.clicked += ContinueToSceneButtonPressed;
        CancelDimsInputButton.clicked += CancelDimsInputButtonPressed;
    }

    void CreateNewLessonButtonPressed()
    {
        _setDimsBox.style.display = DisplayStyle.Flex;

    }

    void ContinueToSceneButtonPressed()
    {
        var widthField = GetComponent<UIDocument>().rootVisualElement.Q<TextField>("Room-width-input").value;
        var lengthField = GetComponent<UIDocument>().rootVisualElement.Q<TextField>("Room-length-input").value;
        
        try
        {
            int width = Int32.Parse(widthField);
            int length = Int32.Parse(lengthField);
            
            if (length is < 151 and > 9 && width is < 151 and > 9)
            {
                // TODO: with Celina create new scene
                RoomClass.Setter(length, width);
                SceneManager.LoadScene("Scenes/UI");
                
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
                //cube.transform.position = new Vector3(0, 0, 0);
                
            }
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    void CancelDimsInputButtonPressed()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<TextField>("Room-width-input").value = "";
        GetComponent<UIDocument>().rootVisualElement.Q<TextField>("Room-length-input").value = "";

        _setDimsBox.style.display = DisplayStyle.None;
    }

    void LoadLessonButtonPressed()
    {
        var x = GameObject.Find("UIDocument").GetComponent<SceneHandler>();
        x.Load();
    }
}
