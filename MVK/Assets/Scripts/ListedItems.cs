
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class ListedItems
{


    private static Object[] LoadPrefabsFromCategory(string category)
    {
        
        
        Object[] gameObjects = Resources.LoadAll("equipment/" + category, typeof(GameObject));
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
            var icon = AssetPreview.GetAssetPreview(prefab as GameObject);
            buttonImage.style.backgroundImage = new StyleBackground(icon);
            buttonImage.AddToClassList("equipment-button-icon");
            item.Add(buttonImage);
            item.AddToClassList("equipment-button");
            allItems.Add(item);
        }

        return allItems;
    }
    

}