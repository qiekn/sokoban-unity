using UnityEngine;

public interface IPushable {
    bool OnPushed(Vector2Int direction, int distance = 1);
}

