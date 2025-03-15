using UnityEngine;

public static class TileSprites {
    public static Sprite shape1;
    public static Sprite shape2;
    public static Sprite shape3;
    public static Sprite shape4;

    public static void LoadSprites() {
        shape1 = Resources.Load<Sprite>("wall");
        shape2 = Resources.Load<Sprite>("box");
        shape3 = Resources.Load<Sprite>("player");
        shape4 = Resources.Load<Sprite>("point");

        if (shape1 == null) Debug.LogError("Failed to load wall sprite");
        if (shape2 == null) Debug.LogError("Failed to load box sprite");
        if (shape3 == null) Debug.LogError("Failed to load player sprite");
        if (shape4 == null) Debug.LogError("Failed to load point sprite");
        Debug.Log("load sprites finished");
    }
}
