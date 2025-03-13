using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject targetPrefab;

    public static LevelManager instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }

        DontDestroyOnLoad(this); // 切换场景时不会被销毁
    }

    void Start() {
        LoadLevel(levelData);
    }


    /*
        P = Player
        . = Background
        # = Wall
        * = Crate
        O = Target
        @ = Crate and Target
    */
    private readonly string[] levelData = new string[] {
        "#########",
        "#.......#",
        "#.P.....#",
        "#..*.0..#",
        "#.......#",
        "#########",
    };

    void LoadLevel(string[] levelData) {
        for (int y = 0; y < levelData.Length; y++) {
            for (int x = 0; x < levelData[y].Length; x++) {
                char tile = levelData[y][x];
                GameObject obj = null;

                switch (tile) {
                    case '#':
                        obj = Instantiate(wallPrefab);
                        break;
                    case 'P':
                        obj = Instantiate(playerPrefab);
                        break;
                    case '*':
                        obj = Instantiate(boxPrefab);
                        break;
                    case '0':
                        obj = Instantiate(targetPrefab);
                        break;
                }
                if (obj != null) {
                    int height = levelData.Length;
                    if (!obj.TryGetComponent<GridPos>(out var gridPos)) {
                        gridPos = obj.AddComponent<GridPos>();
                    }
                    gridPos.Translate(new Vector2Int(x, height - y - 1));
                    obj.transform.SetParent(transform);
                    obj.name = $"tile_{tile}_{x}_{height - y - 1}";
                }
            }
        }
    }

}
