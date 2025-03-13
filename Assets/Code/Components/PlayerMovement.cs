using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    GridPos gridPos;
    LayerMask layerMask;

    void Start() {
        gridPos = GetComponent<GridPos>();
        layerMask = LayerMask.GetMask("Interact");
    }

    void Update() {
        // player move input
        int dx = 0, dy = 0;
        if (Input.GetKeyDown(KeyCode.W))
            dy = 1;
        else if (Input.GetKeyDown(KeyCode.S))
            dy = -1;
        else if (Input.GetKeyDown(KeyCode.A))
            dx = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            dx = 1;

        // player move handler
        if (dx != 0 || dy != 0) {
            var newPos = gridPos.GetNextRawPosition(dx, dy);
            bool canMove;

            // collision detection
            Collider2D hit = Physics2D.OverlapPoint(newPos, layerMask);
            if (hit != null) {
                Debug.Log("player try push: " + (gridPos.GetPosition() + new Vector2Int(dx, dy)));
                if (hit.TryGetComponent<IPushable>(out var obstacle))
                    canMove = obstacle.OnPushed(new Vector2Int(dx, dy));
                else {
                    canMove = false;
                    Debug.Log("player cant push");
                }
            } else {
                canMove = true; // no obstacle
            }

            if (canMove) {
                gridPos.Translate(dx, dy);
            }
        }
    }
}
