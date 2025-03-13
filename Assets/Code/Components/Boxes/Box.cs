using System;
using UnityEngine;

public class Box : MonoBehaviour, IPushable {

    LayerMask layerMask;
    Color originalColor;
    SpriteRenderer spriteRenderer;

    void Start() {
        layerMask = LayerMask.GetMask("Interact");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
    }

    public void ChangeColor(Color color) {
        spriteRenderer.material.color = color;
    }

    public void ResetColor() {
        spriteRenderer.material.color = originalColor;
    }

    public bool OnPushed(Vector2Int direction, int distance = 1) {
        var gridPos = GetComponent<GridPos>();
        var newPos = gridPos.GetNextRawPosition(direction * distance);
        bool canMove;
        Collider2D hit = Physics2D.OverlapPoint(newPos, layerMask);
        // classical box logic
        if (hit == null) {
            canMove = true;
        } else if (hit.CompareTag("Point")) { // specific pass
            canMove = true;
        } else {
            canMove = false;
            Debug.Log("box hit: " + (gridPos.GetPosition() + direction * distance));
        }
        if (canMove) {
            gridPos.Translate(direction * distance);
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Point")) {
            ChangeColor(Color.green);
        }
        if (other.TryGetComponent<SpriteRenderer>(out var sr)) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Constants.TRANSPARENCY);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Point")) {
            ResetColor();
        }
        if (other.TryGetComponent<SpriteRenderer>(out var sr)) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }
}
