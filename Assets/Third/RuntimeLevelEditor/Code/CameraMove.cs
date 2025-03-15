using System;
using UnityEngine;

namespace qiekn.lenrn_editor {
    public class CameraMove : MonoBehaviour {

        [SerializeField] Slider cameraSpeedSlide;
        [SerializeField] ManagerScript ms;

        float xAxis;
        float yAxis;
        float zoom;
        Camera cam;

        void Start() {
            cam = GetComponent<Camera>();
        }

        void Update() {
            if (ms.saveLoadMenuOpen() == false) {
                xAxis = Input.GetAxis("Horizontal");
                yAxis = Input.GetAxis("Vertical");
                zoom = Input.GetAxis("Mouse ScrollWheel") * 10;

                transform.Translate(new Vector3(xAxis * -cameraSpeedSlide.value, yAxis * -cameraSpeedSlide.value, 0.0f));
                transform.position = new Vector3(
                        Math.Clamp(transform.position.x, -20, 20),
                        Math.Clamp(transform.position.y, -20, 20),
                        Math.Clamp(transform.position.z, 20, 20));
                if (zoom < 0 && cam.orthographicSize >= -25) {
                    cam.orthographicSize -= zoom * -cameraSpeedSlide.value;
                }
                if (zoom > 0 && cam.orthographicSize <= -5) {
                    cam.orthographicSize += zoom * -cameraSpeedSlide.value;
                }
            }
        }

    }
}
