using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button[] CategoryButtons;

    private static Button _selectedCategory;
    private static Button _savedSelection;
    private Button _hamburgerButton;
    private static VisualElement _itemList;

    void ToggleItemList()
    {
        if (_itemList.ClassListContains("hidden"))
        {
            _itemList.RemoveFromClassList("hidden");
            SetButtonToSaved();
        }
        else if(!_itemList.ClassListContains("hidden"))
        {
            _itemList.AddToClassList("hidden");
            UnsetButton();
        }
        
    }

    void PopulateItemList()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var categoryButtons = root.Q<VisualElement>("CategoryButtons");
        foreach (Button categoryButton in IconButtons.CategoryButtons)
        {
            categoryButtons.Add(
                categoryButton);
        }

        _selectedCategory = categoryButtons[1] as Button;

        _itemList = root.Q<VisualElement>("ItemList");
        _hamburgerButton = root.Q<Button>("Hamburger");
        _hamburgerButton.clicked += ToggleItemList;
/*        
        foreach (var button in ListedItems.GetAllItemsInCategory("Ball"))
        {
            _itemList.hierarchy.Add(button);
        }
  */      
    }

    public static void SetButton(Button button)
    {
        if (_selectedCategory == null)
        {
            _selectedCategory = button;
            _selectedCategory.AddToClassList("selected");
        }
        else
        {
            _selectedCategory.RemoveFromClassList("selected");
            _selectedCategory = button;
            _selectedCategory.AddToClassList("selected");
        }
        if (_itemList.ClassListContains("hidden"))
        {
            _itemList.RemoveFromClassList("hidden");
        }
    }

    public static void UnsetButton()
    {
        if (_selectedCategory != null)
        {
            _savedSelection = _selectedCategory;
            _selectedCategory.RemoveFromClassList("selected");
        }

        _selectedCategory = null;

    }

    public static void SetButtonToSaved()
    {
        if (_savedSelection != null)
        {
            _selectedCategory = _savedSelection;
            _savedSelection = null;
            _selectedCategory.AddToClassList("selected");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
