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
    public static GameObject SpawnerContainer;
    private static Object[] _materials; 

    private RoomManager _roomManager;
    private static Button _paintButton;
    private static Button _rotateButton;
    private static VisualElement _rotateOptions;             //Tog bort static
    private static VisualElement _colors;
    private static Button _red;
    private static Button _orange;
    private static Button _green;
    private static Button _blue;
    private static Button _yellow;
    private static Button _leftRotateBtn;
    private static Button _rightRotateBtn;
    private Button moveButton;
    private Button selectButton;
    private Button _cameraButton;
    private RoomClass _room;

    public bool rotateBool = false;


    private static void ToggleItemList()
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


    private static void ClearItemList()

    {
        if (_itemList.hierarchy.childCount <= 0) return;
        _itemList.hierarchy.Clear();
    }

    private static void PopulateItemList(string category)
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
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        var root = GetComponent<UIDocument>().rootVisualElement;
        var categoryButtons = root.Q<VisualElement>("CategoryButtons");
        foreach (Button categoryButton in IconButtons.CategoryButtons)
        {
            categoryButtons.Add(
                categoryButton);
        }
        

        _materials = Resources.LoadAll("material/");
        
        _selectedCategory = categoryButtons[1] as Button;

        _itemList = root.Q<VisualElement>("ItemList");
        _hamburgerButton = root.Q<Button>("Hamburger");
        _paintButton = root.Q<Button>("Paint_Button");
        _rotateButton = root.Q<Button>("Rotate_Button");
        _leftRotateBtn = root.Q<Button>("LeftRotate_Button");
        _rightRotateBtn = root.Q<Button>("RightRotate_Button");
        _cameraButton = root.Q<Button>("Camera_Button");
        moveButton = root.Q<Button>("Hand_Button");
        selectButton = root.Q<Button>("Select_Button");
        _roomManager = FindObjectOfType<RoomManager>(); 
            
        SpawnerContainer = GameObject.Find("SpawnerContainer");

        _rotateOptions = root.Q<VisualElement>("Rotate_Options");
        _colors = root.Q<VisualElement>("Colors");
        _red = root.Q<Button>("Red");
        _blue = root.Q<Button>("Blue");
        _orange = root.Q<Button>("Orange");
        _green = root.Q<Button>("Green");
        _yellow = root.Q<Button>("Yellow");
        
        _hamburgerButton.clicked += ToggleItemList;
        _cameraButton.clicked += InvertCamera;
        _paintButton.clicked += HandleColors;
        _rotateButton.clicked += HandleRotation;
        _rightRotateBtn.clicked += RotateRight;
        _leftRotateBtn.clicked += RotateLeft;
        moveButton.clicked += HandMove;
        selectButton.clicked += SelectTool;
        _red.clicked += () => ChoseColor(0);
        _orange.clicked += () => ChoseColor(1);
        _green.clicked += () => ChoseColor(2);
        _blue.clicked += () => ChoseColor(3);
        _yellow.clicked += () => ChoseColor(4);

    }

    private void SelectTool()
    {
        _roomManager.movebool = false;
    }

    private void HandMove()
    {
        _roomManager.movebool = true;
    }
    
    private static void InvertCamera()
    {
        FindObjectOfType<RoomClass>().InvertCamera();
    }

    public void RotateRight()
    {
        Debug.Log(_roomManager.selectedObject);
        if (rotateBool)
        {
            _roomManager.Rotate(10f);
        }
    }
    private void RotateLeft()
    {
        Debug.Log(_roomManager.selectedObject);
        if (rotateBool)
        {
            _roomManager.Rotate(-10f);
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

    private static void HandleColors()
    {
        Debug.Log(_materials[0]);
        if (_colors.ClassListContains("hidden"))
        {
            _colors.RemoveFromClassList("hidden");
            _rotateOptions.AddToClassList("hidden");
        }
        else if (!_colors.ClassListContains("hidden"))
        {
            _colors.AddToClassList("hidden");
        }
    }

    private void ChoseColor(int n)
    {
        Debug.Log("Color");
        if (_colors.ClassListContains("hidden"))
        {
            return;
        }
        else
        {
            Debug.Log("Color");
            switch (n)
            {
                case 0:
                    Debug.Log("RED");
                    _roomManager.selectedObject.GetComponent<ObjProperties>().mainColor = (Material) _materials[4];
                    break;
                case 1:
                    _roomManager.selectedObject.GetComponent<ObjProperties>().mainColor = (Material) _materials[3];
                    break;
                case 2:
                    _roomManager.selectedObject.GetComponent<ObjProperties>().mainColor = (Material) _materials[2];
                    break;
                case 3:
                    Debug.Log("Blue");
                    _roomManager.selectedObject.GetComponent<ObjProperties>().mainColor = (Material) _materials[0];
                    break;
                case 4:
                    _roomManager.selectedObject.GetComponent<ObjProperties>().mainColor = (Material) _materials[7];
                    break;
            }
        }
    }


    private void HandleRotation()
    {
       
        if(_roomManager.selectedObject != null){
            if (_rotateOptions.ClassListContains("hidden"))
            {
                _rotateOptions.RemoveFromClassList("hidden"); //Synlig'
                _colors.AddToClassList("hidden");
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

    private static void UnsetButton()
    {
        if (_selectedCategory != null)
        {
            _savedSelection = _selectedCategory;
            _selectedCategory.RemoveFromClassList("selected");
        }

        _selectedCategory = null;

    }

    private static void SetButtonToSaved()
    {
        if (_savedSelection == null) return;
        _selectedCategory = _savedSelection;
        _savedSelection = null;
        _selectedCategory.AddToClassList("selected");
    }

    // Update is called once per frame
    void Update()
    {
        if (_roomManager.selectedObject == null)
        {
            _colors.AddToClassList("hidden");
            _rotateOptions.AddToClassList("hidden");
            
        }
    }
}
