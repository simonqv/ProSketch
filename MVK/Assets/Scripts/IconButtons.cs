using System;
using UnityEngine;
using UnityEngine.UIElements;

public static class IconButtons
{
    private static Button CreateButtonWithClass(string className, string name = "")
    {
        var element = new Button();
        element.RemoveFromClassList("unity-button");
        element.AddToClassList(className);
        if (name == "") return element;
        element.name = name;
        return element;
    }

    private static Button CreateIcon(string path, string containerName="", string iconName="")
    {
        Button element = CreateButtonWithClass("icon", containerName);
        Texture2D backgroundImage = Resources.Load(path) as Texture2D;
        Button backgroundElement = CreateButtonWithClass("icon-image", iconName);
        backgroundElement.style.backgroundImage = new StyleBackground(backgroundImage);
        backgroundElement.clicked += () => UIController.SetButton(element);
        element.Add(backgroundElement);
        return element;
    }
    public static readonly Button[] CategoryButtons = new Button[]
    {
        CreateIcon("category-icons/hamburger-menu", "Hamburger", iconName:"HamburgerIcon"),
        CreateIcon("category-icons/plinth"),
        CreateIcon("category-icons/hula-hoop"),
        CreateIcon("category-icons/mat"),
        CreateIcon("category-icons/ball"),
        CreateIcon("category-icons/racket"),
    };
}