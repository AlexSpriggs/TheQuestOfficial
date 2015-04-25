using UnityEngine;
using System.Collections;

public class TextInputReceiver : MonoBehaviour {

    public GameObject inputField;
    UIInput inputField_UIInput;
    UILabel outputField_UILabel;

    void Start()
    {
        inputField_UIInput = inputField.GetComponent<UIInput>();
        outputField_UILabel = gameObject.GetComponent<UILabel>();
    }

    // Update the output field with the input field's value
    // Called when the input field's value is submitted
    public void ReceiveTextInput()
    {
        outputField_UILabel.text = inputField_UIInput.value;
    }
}

/*
 * ABOUT:
 * - Requires an NGUI object for input, and another for output
 * - The Input Field
 *      - Must have the UILabel and UIInput script, and a Box Collider
 *      - UILabel
 *          - "Overflow"        : Resize Height
 *          - "Alignment"       : Left
 *          - "Widget"
 *              - "Size"        : 250
 *              - "Collider"    : re-enable "auto-adjust to match" to auto fit the collider
 *      - UIInput
 *          - "On Return Key"   : Submit
 *          - "Character Limit" : 60
 *          - "On Submit"       : Drag-and-drop the object for output into "Notify"
 *                              : Select "TextInputReceiver" -> "ReceiveTextInput" under "Method"
 * - The Output Field
 *      - Must have the UILabel and TextInputReceiver script
 *      - UILabel
 *          - Same values as The Input Field
 *      - TextInputReceiver
 *          - "Input Field"     : Drag-and-drop the object for input 
 */