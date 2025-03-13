using UnityEngine;

public class GridPos : MonoBehaviour {
    int x_ = 0, y_ = 0;
    readonly float gridSize = Constants.TILE_WIDTH;

    public void Translate(Vector2Int offset) {
        x_ += offset.x;
        y_ += offset.y;
        UpdatePosition();
    }

    public void Translate(int dx, int dy) {
        x_ += dx;
        y_ += dy;
        UpdatePosition();
    }

    public void InitPosition(Vector2Int pos) {
        x_ = pos.x;
        y_ = pos.y;
        UpdatePosition();
    }

    public void InitPosition(int x, int y) {
        x_ = x;
        y_ = y;
        UpdatePosition();
    }

    public void SetPositon(Vector2Int pos) {
        InitPosition(pos);
    }

    public void SetPositon(int x, int y) {
        InitPosition(x, y);
    }

    public Vector2Int GetPosition() {
        return new Vector2Int(x_, y_);
    }
    public Vector3 GetRawPosition() {
        var res = new Vector3(x_, y_, 0) * gridSize + new Vector3(gridSize / 2, gridSize / 2, 0);
        return res;
    }

    // used for check nearby obstacle
    public Vector3 GetNextRawPosition(int dx, int dy) {
        var raw = GetRawPosition();
        var res = raw + new Vector3(dx, dy, 0) * gridSize;
        return res;
    }

    public Vector3 GetNextRawPosition(Vector2Int offset) {
        return GetNextRawPosition(offset.x, offset.y);
    }

    void UpdatePosition() {
        transform.position = GetRawPosition();
    }
}
