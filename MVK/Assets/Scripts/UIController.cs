using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button[] CategoryButtons;

    private Button _selectedCategory;
    private Button _hamburgerButton;
    private VisualElement _itemList;

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
        foreach (VisualElement categoryButton in IconButtons.CategoryButtons)
        {
            categoryButtons.Add(categoryButton);
        }

        _itemList = root.Q<VisualElement>("ItemList");
        _hamburgerButton = root.Q<Button>("HamburgerIcon");
        Debug.Log(_hamburgerButton);
        _hamburgerButton.clicked += ToggleItemList;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
