using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    public GameObject levelButtonPrefab;
    public Transform gridParent; // Grid Layout Group

    void Start() {
        for (int i = 1; i <= 3; i++) {
            var button = Instantiate(levelButtonPrefab, gridParent);
            int levelIndex = i;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelIndex));
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i;
        }
    }

    void LoadLevel(int levelIndex) {
        string sceneName = Constants.LEVEL_NAME_PREFIX + levelIndex;
        Debug.Log("Loading scene: " + sceneName);

        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.LogError("Scene " + sceneName + " is not in Build Settings! Make sure it's added.");
        }
    }
}
