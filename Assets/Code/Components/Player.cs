
using UnityEngine;

public class Player : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<SpriteRenderer>(out var sr)) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Constants.TRANSPARENCY);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent<SpriteRenderer>(out var sr)) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }

}
