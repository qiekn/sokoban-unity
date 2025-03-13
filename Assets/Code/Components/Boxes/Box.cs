using UnityEngine;

public class Box : MonoBehaviour, IPushable {
    public bool OnPushed(Vector2Int direction, int distance = 1) {
        var gridPos = GetComponent<GridPos>();
        var newPos = gridPos.GetNextRawPosition(direction * distance);
        bool canMove;
        Collider2D hit = Physics2D.OverlapPoint(newPos);
        // classical box logic
        if (hit == null) {
            canMove = true;
        } else if (hit.CompareTag("Point")) { // specific pass
            canMove = true;
        } else {
            canMove = false;
        }
        Debug.Log("box hit: " + (gridPos.GetPosition() + direction * distance));
        if (canMove) {
            gridPos.Translate(direction * distance);
            return true;
        }
        return false;
    }
}
