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

    private RoomManager _roomManager;
    public static Button _paintButton;  
    public static Button _rotateButton;  
    public static VisualElement _rotateOptions;             //Tog bort static
    private static VisualElement _Colors;
    private static Button _Red;
    private static Button _Orange;
    private static Button _Green;
    private static Button _Blue;
    private static Button _Yellow;
    public static Button _leftRotateBtn;    
    public static Button _rightRotateBtn;
    public Button moveButton;
    public Button selectButton;

    public bool rotateBool = false;


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
        Application.targetFrameRate = 50;
        var root = GetComponent<UIDocument>().rootVisualElement;
        var categoryButtons = root.Q<VisualElement>("CategoryButtons");
        _roomManager = GameObject.FindObjectOfType<RoomManager>();
        foreach (Button categoryButton in IconButtons.CategoryButtons)
        {
            categoryButtons.Add(
                categoryButton);
        }

        _selectedCategory = categoryButtons[1] as Button;

        _itemList = root.Q<VisualElement>("ItemList");
        _hamburgerButton = root.Q<Button>("Hamburger");
        _paintButton = root.Q<Button>("Paint_Button");
        _rotateButton = root.Q<Button>("Rotate_Button");
        _leftRotateBtn = root.Q<Button>("LeftRotate_Button");
        _rightRotateBtn = root.Q<Button>("RightRotate_Button");
        moveButton = root.Q<Button>("Hand_Button");
        selectButton = root.Q<Button>("Select_Button");
            
        spawnerContainer = GameObject.Find("SpawnerContainer");
        _hamburgerButton.clicked += ToggleItemList;

        _rotateOptions = root.Q<VisualElement>("Rotate_Options");
        _Colors = root.Q<VisualElement>("Colors");
        _paintButton.clicked += HandleColors;
        _rotateButton.clicked += HandleRotation;

        _rightRotateBtn.clicked += RotateRight;
        _leftRotateBtn.clicked += RotateLeft;
        moveButton.clicked += handMove;
        selectButton.clicked += selectTool;

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

    public void selectTool()
    {
        _roomManager.movebool = false;
    }
    public void handMove()
    {
        _roomManager.movebool = true;
    }

    public void RotateRight()
    {
        Debug.Log("Right");
        if (rotateBool)
        {
            _roomManager.Rotate(1f);
        }
    }
    public void RotateLeft()
    {
        Debug.Log("Left");
        if (rotateBool)
        {
            _roomManager.Rotate(-1f);
        }
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


    public void HandleRotation()
    {
        if(_roomManager.selectedObject != null){
            if (_rotateOptions.ClassListContains("hidden"))
            {
                _rotateOptions.RemoveFromClassList("hidden"); //Synlig'
                rotateBool = true;
            }
            else if (!_rotateOptions.ClassListContains("hidden"))
            {
                _rotateOptions.AddToClassList("hidden"); // ej synlig
                rotateBool = false;
            }
        }
        else{
            Debug.Log("Inget objekt selected");
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
