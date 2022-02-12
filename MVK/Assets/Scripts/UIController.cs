using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button[] CategoryButtons;

    private static Button _selectedCategory;
    private Button _hamburgerButton;
    private static VisualElement _itemList;

    void ToggleItemList()
    {
        if (_itemList.ClassListContains("hidden"))
        {
            _itemList.RemoveFromClassList("hidden");
        }
        else
        {
            _itemList.AddToClassList("hidden");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var categoryButtons = root.Q<VisualElement>("CategoryButtons");
        foreach (Button categoryButton in IconButtons.CategoryButtons)
        {
            categoryButton.clicked += () => SetButton(categoryButton);
            Debug.Log(categoryButton);
            Debug.Log(CategoryButtons);
            categoryButtons.Add(
                categoryButton);
        }

        _selectedCategory = categoryButtons[1] as Button;

        _itemList = root.Q<VisualElement>("ItemList");
        _hamburgerButton = root.Q<Button>("HamburgerIcon");
        Debug.Log(_hamburgerButton);
        _hamburgerButton.clicked += ToggleItemList;

    }

    public static void SetButton(Button button)
    {
        _selectedCategory.RemoveFromClassList("selected");
        _selectedCategory = button;
        _selectedCategory.AddToClassList("selected");
        if (_itemList.ClassListContains("hidden"))
        {
            _itemList.RemoveFromClassList("hidden");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
