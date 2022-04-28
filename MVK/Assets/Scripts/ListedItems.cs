using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class ListedItems
{

    private static Sprite[] LoadPicsFromCategory(string category)
    {
        Sprite[] gameObjects = Resources.LoadAll("equipment/" + category, typeof(Sprite)) as Sprite[];
        return gameObjects;
    }

    private static Object[] LoadPrefabsFromCategory(string category)
    {
        
        
        Object[] gameObjects = Resources.LoadAll("equipment/" + category, typeof(GameObject));
        return gameObjects; 
    }

    public static List<Button> GetAllItemsInCategory(string category)
    {
        var prefabs = LoadPrefabsFromCategory(category);
        List<Button> allItems = new List<Button>();
        
        Texture2D tex = null;
        byte[] fileData;
        //string filePath = "Assets/Resources/Equipment/Plinth/plint7deljpg.jpg";

        foreach (var prefab in prefabs)
        {
            fileData = System.IO.File.ReadAllBytes("Assets/Resources/Equipment/"+ category + "/"+ (prefab as GameObject).transform.name.Replace(" (UnityEngine.GameObject)","")+".jpg");
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            var item = IconButtons.CreateButtonWithClass("list-item");
            var buttonImage = IconButtons.CreateDivWithClass("list-item-bg");
            var catName = char.ToUpper(category[0]) + category.Substring(1);
            //string path = "plint7deljpg.jpg";
            //var icon = Resources.Load<Sprite>(path);
            
           
            
            buttonImage.style.backgroundImage = new StyleBackground(tex);
            buttonImage.AddToClassList("equipment-button-icon");
            Spawner s = UIController.SpawnerContainer.AddComponent<Spawner>();
            item.clicked += () =>
            {
                s.Spawn(prefab as GameObject, category);
            };
            item.Add(buttonImage);
            item.AddToClassList("equipment-button");
            allItems.Add(item);
        }

        return allItems;
    }
    

}