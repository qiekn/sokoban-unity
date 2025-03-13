using UnityEngine;

public class Point : MonoBehaviour, IPushable {
    public bool OnPushed(Vector2Int direction, int distance = 1) {
        // always allow other objects cross through
        return true;
    }
}
