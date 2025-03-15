using System;
using System.Security.Cryptography;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace qiekn.learn_editor {
    public class ManagerScript : MonoBehaviour {

        public enum Command { Create, Rotate, Destroy };
        public enum Item { Cat, Dog, Snake };

        [HideInInspector]
        public Item item = Item.Cat;

        [HideInInspector]
        public Command cmd = Command.Create;

        [HideInInspector]
        public MeshRenderer mr;

        public Material goodPlace;
        public Material badPlace;
        public GameObject player;
        public ManagerScript ms;

        Vector3 mousePos;
        bool colliding;
        Ray ray;
        RaycastHit hit;
        Camera cam;
        LayerMask layerMask;

        void Start() {
            mr = GetComponent<MeshRenderer>();
            cam = Camera.main;
            layerMask = LayerMask.GetMask("Editor");
        }

        void Update() {
            mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            transform.position = new Vector3(
                    Math.Clamp(mousePos.x, -20, 20),
                    Math.Clamp(mousePos.y, -20, 20),
                    0.75f);

            ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                colliding = true;
                mr.material = badPlace;
            } else {
                colliding = false;
                mr.material = goodPlace;
            }

            if (Input.GetMouseButton(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    if (!colliding && cmd == Command.Create) {
                        Create();
                    } else if (!colliding && cmd == Command.Rotate) {
                        Rotate();
                    } else if (!colliding && cmd == Command.Destroy) {
                        if (hit.collider.gameObject.name.Contains("PlayerModel")) {
                            ms.playerPlaced = false;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        } // end of update

        void Create() {
            GameObject newObj;
            switch (item) {
                case Item.Cat:
                    newObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    newObj.transform.position = transform.position;
                    newObj.layer = layerMask;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    eo.data.rot = newObj.transform.rotation;
                    eo.data.objectType = EditorObject.ObjectType.Cat;
                    break;
                case Item.Dog:
                    break;
                case Item.Snake:
                    break;
                default:
                    Debug.LogWarning($"ms: unknown item type {item}");
                    return;
            }
        }

        void Rotate() {
            var rotObject = hit.collider.gameObject;
            ms.rotSlider.value = rotObject.transform.rotation.y;
        }
    } // end of class
}
