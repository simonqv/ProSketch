using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Buttons for main menu
    private Button _createNewLessonButton;
    private Button _loadLessonButton;

    private Button _continueToSceneButton;
    private Button _cancelDimsInputButton;

    // GroupBox for popup when creating new scene
    private GroupBox _setDimsBox;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Gets buttons
        _createNewLessonButton = root.Q<Button>("Create-new-lesson-button");
        _loadLessonButton = root.Q<Button>("Load-lesson-button");
        _continueToSceneButton = root.Q<Button>("Confirm-input-button");
        _cancelDimsInputButton = root.Q<Button>("Cancel-input-button");
        
        // Initializes input popup window as invisible
        _setDimsBox = root.Q<GroupBox>("Set-dims-box");
        _setDimsBox.style.display = DisplayStyle.None;
        
        // Assigns functions for buttons
        _createNewLessonButton.clicked += CreateNewLessonButtonPressed;
        _loadLessonButton.clicked += LoadLessonButtonPressed;
        _continueToSceneButton.clicked += ContinueToSceneButtonPressed;
        _cancelDimsInputButton.clicked += CancelDimsInputButtonPressed;
    }

    /*
     * When pressed, make room dimensions input window visible
     */
    void CreateNewLessonButtonPressed()
    {
        _setDimsBox.style.display = DisplayStyle.Flex;
    }

    /*
     * Read input fields and creates a new room with dimensions from user input.
     * Dimensions must be integers and cannot be too small or too big.
     */
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
                RoomClass.Setter(length, width);
                SceneManager.LoadScene("Scenes/UI");
            }
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    /*
     * When pressed, empty the textfield and close the popup window
     */
    void CancelDimsInputButtonPressed()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<TextField>("Room-width-input").value = "";
        GetComponent<UIDocument>().rootVisualElement.Q<TextField>("Room-length-input").value = "";

        _setDimsBox.style.display = DisplayStyle.None;
    }

    /*
     * Display the window with the list of saved files
     */
    void LoadLessonButtonPressed()
    {
        GameObject.Find("UIDocument").GetComponent<SceneHandler>().Load();
    }
}
