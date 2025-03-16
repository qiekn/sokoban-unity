using System;
using UnityEngine;

namespace qiekn.learn_editor {
    public class CameraMove : MonoBehaviour {

        [SerializeField] float speed; // camera speed slider
        [SerializeField] float size = 20f;

        float xAxis;
        float yAxis;
        // float zoom;
        Camera cam;

        void Start() {
            cam = GetComponent<Camera>();
        }

        void Update() {
            xAxis = Input.GetAxis("Horizontal");
            yAxis = Input.GetAxis("Vertical");
            // zoom = Input.GetAxis("Mouse ScrollWheel") * 10;

            transform.Translate(new Vector3(xAxis * speed, yAxis * -speed, 0.0f));
            transform.position = new Vector3(
                    Math.Clamp(transform.position.x, -size, size),
                    Math.Clamp(transform.position.y, -size, size));

            /*
            if (zoom < 0 && cam.orthographicSize >= -25) {
                cam.orthographicSize -= zoom * -speed.value;
            }

            if (zoom > 0 && cam.orthographicSize <= -5) {
                cam.orthographicSize += zoom * -speed.value;
            }
            */
        }

    }
}
