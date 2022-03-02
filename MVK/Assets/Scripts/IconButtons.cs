using System;
using UnityEngine;
using UnityEngine.UIElements;

public static class IconButtons
{
    public static Button CreateButtonWithClass(string className, string name = "")
    {
        var element = new Button();
        element.RemoveFromClassList("unity-button");
        element.AddToClassList(className);
        if (name == "") return element;
        element.name = name;
        return element;
    }

    public static VisualElement CreateDivWithClass(string className, string name = "")
    {
        var element = new VisualElement();
        element.AddToClassList(className);
        if (name == "") return element;
        element.name = name;
        return element;
    }

    private static Button CreateIcon(string path, string containerName="", string iconName="", bool disableSetButton = false)
    {
        Button element = CreateButtonWithClass("icon", containerName);
        Texture2D backgroundImage = Resources.Load(path) as Texture2D;
        VisualElement backgroundElement = CreateDivWithClass("icon-image", iconName);
        backgroundElement.style.backgroundImage = new StyleBackground(backgroundImage);
        if(!disableSetButton)
        {
            Debug.Log(containerName);
            element.clicked += () => UIController.SetButton(element);
        }
        element.Add(backgroundElement);
        return element;
    }
    public static readonly Button[] CategoryButtons = new Button[]
    {
        CreateIcon("category-icons/hamburger-menu", "Hamburger", iconName:"HamburgerIcon", true),
        CreateIcon("category-icons/plinth"),
        CreateIcon("category-icons/hula-hoop"),
        CreateIcon("category-icons/mat"),
        CreateIcon("category-icons/ball"),
        CreateIcon("category-icons/racket"),
    };
}