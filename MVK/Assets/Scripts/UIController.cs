using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button[] CategoryButtons;

    private static Button _selectedCategory;
    private static Button _savedSelection;
    private Button _hamburgerButton;
    private static VisualElement _itemList;
    public static GameObject spawnerContainer;

    public static Button _paintButton;                 //Tog bort static
    private static VisualElement _Colors;
    private static Button _Red;
    private static Button _Orange;
    private static Button _Green;
    private static Button _Blue;
    private static Button _Yellow;

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

    static void ClearItemList()

    {
        if (_itemList.hierarchy.childCount <= 0) return;
        _itemList.hierarchy.Clear();
    }

    static void PopulateItemList(string category)
    {
        if (category[0] != '_')
        {
            Debug.Log("too bad");
            return;
        };
        ClearItemList();
        foreach (var button in ListedItems.GetAllItemsInCategory(category.ToLower()[1..]))
        {
            _itemList.hierarchy.Add(button);
        }
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
        _paintButton = root.Q<Button>("Paint_Button");

        spawnerContainer = GameObject.Find("SpawnerContainer");
        _hamburgerButton.clicked += ToggleItemList;


        _Colors = root.Q<VisualElement>("Colors");
        _paintButton.clicked += HandleColors;
        /*_Red.clicked += ChoseColor(0);
        _Orange.clicked += ChoseColor(1);
        _Green.clicked += ChoseColor(2);
        _Blue.clicked += ChoseColor(3);
        _Yellow.clicked += ChoseColor(4);*/
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
            PopulateItemList(button.name);
            _selectedCategory.RemoveFromClassList("selected");
            _selectedCategory = button;
            _selectedCategory.AddToClassList("selected");
        }
        if (_itemList.ClassListContains("hidden"))
        {
            _itemList.RemoveFromClassList("hidden");
        }
    }

    public static void HandleColors()
    {
        if (_Colors.ClassListContains("hidden"))
        {
            _Colors.RemoveFromClassList("hidden");
        }
        else if (!_Colors.ClassListContains("hidden"))
        {
            _Colors.AddToClassList("hidden");
        }
    }

    public static void ChoseColor(int n)
    {
        if (_Colors.ClassListContains("hidden"))
        {
            return;
        }
        else
        {
            if (n == 0) {
                
            }
            if (n == 1) {

            }
            if (n == 2) {

            }
            if (n == 3) {

            }
            if (n == 4) {

            }
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
