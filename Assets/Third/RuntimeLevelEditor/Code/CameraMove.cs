using System;
using UnityEngine;
using UnityEngine.UI;

namespace qiekn.learn_editor {
    public class CameraMove : MonoBehaviour {

        [SerializeField] Slider speed; // camera speed slider
        [SerializeField] ManagerScript ms;
        [SerializeField] float clampSize = 20f;

        float xAxis;
        float yAxis;
        // float zoom;
        // Camera cam;

        void Start() {
            // cam = GetComponent<Camera>();
        }

        void Update() {
            if (1 == 2) {
                //if (ms.saveLoadMenuOpen == false) {
                xAxis = Input.GetAxis("Horizontal");
                yAxis = Input.GetAxis("Vertical");
                // zoom = Input.GetAxis("Mouse ScrollWheel") * 10;

                transform.Translate(new Vector3(xAxis * -speed.value, yAxis * -speed.value, 0.0f));
                transform.position = new Vector3(
                        Math.Clamp(transform.position.x, -clampSize, clampSize),
                        Math.Clamp(transform.position.y, -clampSize, clampSize));

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
}
