using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListButton : MonoBehaviour
{
    [SerializeField] private Text myText;
	[SerializeField] private ObjectListControl objectControl;

    public string myTextString;
    public void SetText(string textString)
    {
        myTextString = textString;
        myText.text = textString;
    }

    // When clicked.
    public void OnClick()
    {
        objectControl.ButtonClicked(myTextString);
    }
    
}
