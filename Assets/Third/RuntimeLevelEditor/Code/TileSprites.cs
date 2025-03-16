using UnityEngine;

public static class TileSprites {
    public static Sprite wall;
    public static Sprite box;
    public static Sprite player;
    public static Sprite point;

    public static void LoadSprites() {
        wall = Resources.Load<Sprite>("wall");
        box = Resources.Load<Sprite>("box");
        player = Resources.Load<Sprite>("player");
        point = Resources.Load<Sprite>("point");

        if (wall == null) Debug.LogError("Failed to load wall sprite");
        if (box == null) Debug.LogError("Failed to load box sprite");
        if (player == null) Debug.LogError("Failed to load player sprite");
        if (point == null) Debug.LogError("Failed to load point sprite");
        Debug.Log("load sprites finished");
    }
}
