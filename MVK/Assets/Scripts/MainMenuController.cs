using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public Button CreateNewLessonButton;
    public Button LoadLessonButton;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        CreateNewLessonButton = root.Q<Button>("Create-new-lesson-button");
        LoadLessonButton = root.Q<Button>("Load-lesson-button");

        CreateNewLessonButton.clicked += CreateNewLessonButtonPressed;
        LoadLessonButton.clicked += LoadLessonButtonPressed;
    }

    void CreateNewLessonButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void LoadLessonButtonPressed()
    {
        throw new Exception("not implemented");
    }
}
