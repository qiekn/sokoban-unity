using UnityEngine;
using UnityEngine.UI;

public class EditorMenu : MonoBehaviour {
    public Button selectedButton;

    Button[] buttons;

    void Start() {
        buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons) {
            button.onClick.AddListener(() => OnButtonSelected(button));
        }
    }
    void OnButtonSelected(Button clickedButton) {
        Debug.Log(clickedButton + "selected");
        if (selectedButton == null) {
            selectedButton = clickedButton;
            selectedButton.interactable = false;
        }

        if (clickedButton == selectedButton) return;
        if (clickedButton != selectedButton) {
            selectedButton.interactable = true;
            selectedButton = clickedButton;
            selectedButton.interactable = false;
        }
    }
}
