using UnityEngine;

public class Ball : MonoBehaviour, IPushable {
    LayerMask layerMask;

    void Start() {
        layerMask = LayerMask.GetMask("Interact");
    }

    public bool OnPushed(Vector2Int direction, int distance = 1) {
        var gridPos = GetComponent<GridPos>();

        // try move step by step
        int cnt = 99; // prevent infinite loops.
        for (bool flag = true; flag && cnt > 0; distance++, cnt--) {
            var newPos = gridPos.GetNextRawPosition(direction * distance);
            Collider2D hit = Physics2D.OverlapPoint(newPos, layerMask);
            if (hit != null) {
                flag = false;
                distance -= 2;
                Debug.Log("ball hit: " + (gridPos.GetPosition() + direction * distance));
            }
        }

        // apply move
        if (distance > 0) {
            gridPos.Translate(direction * distance);
            return true;
        }
        return false;
    }
}
