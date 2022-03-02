
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class ListedItems
{

/*
    private static IEnumerable<Object> LoadPrefabsFromCategory(string category)
    {
        var gameObjects = Resources.LoadAll("Equipment/" + category, typeof(GameObject));
        return gameObjects;
    }

    public static List<Button> GetAllItemsInCategory(string category)
    {
        var prefabs = LoadPrefabsFromCategory(category);
        List<Button> allItems = new List<Button>();
        foreach (var prefab in prefabs)
        {
            var item = IconButtons.CreateButtonWithClass("list-item");
            var buttonImage = IconButtons.CreateButtonWithClass("list-item-bg");
            var icon = PrefabUtility.GetIconForGameObject(prefab);
            buttonImage.style.backgroundImage = new StyleBackground(icon);
            item.Add(buttonImage);
            allItems.Add(item);
        }

        return allItems;
    }
    */

}