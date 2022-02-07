using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectListControl : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;

    [SerializeField] private int[] intArray;

    private List<GameObject> buttons;

    // Starts the list.
    void Start()
    {
        buttons = new List<GameObject>();
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }

            buttons.Clear();
        }
        foreach (int i in intArray)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<ObjectListButton>().SetText("Button #" + i);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    // Simply prints the name of the button when clicked.
    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }
}
