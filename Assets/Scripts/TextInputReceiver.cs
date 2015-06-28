using UnityEngine;
using System.Collections;

public class TextInputReceiver : MonoBehaviour {

    public GameObject computerUIRoot;
    public GameObject questUI;
    public GameObject inputField;
    public GameObject outputField;
    public GameObject player;
    UIInput inputField_UIInput;
    UILabel outputField_UILabel;

    void Start()
    {
        if (inputField != null)
        {
            inputField_UIInput = inputField.GetComponent<UIInput>();
        }
        else
        {
            Debug.LogWarning("Input Field is null in TextInputReceiver script");
        }

        if (outputField != null)
        {
            outputField_UILabel = outputField.GetComponent<UILabel>();
        }
        else
        {
            Debug.LogWarning("Output Field is null in TextInputReceiver script");
        }
    }

    // Update the output field with the input field's value
    // Called when the input field's value is submitted
    public void ReceiveTextInput()
    {
        if (questUI != null)
        {
            questUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Quest UI is null in TextInputReceiver script");
        }

        if (outputField_UILabel != null)
        {
            outputField_UILabel.text = "- " + inputField_UIInput.value;
            inputField_UIInput.value = "Type here";
        }
        else
        {
            Debug.LogWarning("Output Field is null in TextInputReceiver script");
        }

        if (computerUIRoot != null)
        {
            computerUIRoot.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Computer UI Root is null in TextInputReceiver script");
        }

        player.GetComponent<PlayerBehavior>().triggerEnding();
        player.GetComponent<Movement>().triggerEnding();
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