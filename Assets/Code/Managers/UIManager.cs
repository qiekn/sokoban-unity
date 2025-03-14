using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] GameObject backButtonPrefab;
    Button backButton;

    void Start() {
        GameObject buttonObj = Instantiate(backButtonPrefab, transform); // transform of Canvas
        backButton = buttonObj.GetComponent<Button>();
        backButton.onClick.AddListener(ReturnToLevelSelector);

        RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ReturnToLevelSelector();
        }
    }

    void ReturnToLevelSelector() {
        Debug.Log("Return to level selector");
        SceneManager.LoadScene(Constants.LEVELSELECTOR);
    }
}
