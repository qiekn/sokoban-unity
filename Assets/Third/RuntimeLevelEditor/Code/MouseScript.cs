using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace qiekn.learn_editor {
    public class MouseScript : MonoBehaviour {
        public enum Cmd { Create, Destroy };
        public enum Item { Shape1, Shape2, Shape3, Shape4 };

        [SerializeField] float limit = 30f;

        [HideInInspector]
        public Item item = Item.Shape1; // default item

        [HideInInspector]
        public Cmd cmd = Cmd.Create; // default commnad

        [HideInInspector]
        public MeshRenderer meshRenderer;


        [SerializeField] Material goodPlace;
        [SerializeField] Material badPlace;
        [SerializeField] ManagerScript ms;

        Vector3 mousePos;
        bool colliding;
        Ray ray;
        RaycastHit hit;
        LayerMask layerMask;

        void Start() {
            meshRenderer = GetComponent<MeshRenderer>();
            layerMask = LayerMask.GetMask("Editor");
        }

        void Update() {
            // Have the object follow the mouse cursor by getting
            // mouse coordinates and converting them to world point.
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // limit object movement
            transform.position = new Vector3(
                Mathf.Clamp(mousePos.x, -limit, limit),
                Mathf.Clamp(mousePos.y, -limit, limit));

            // send out raycast to detect objects
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                colliding = true;
                /* TODO: indicator <2025-03-15 12:19, @qiekn> */
            } else {
                colliding = false;
            }

            // after pressing the left mouse button...
            if (Input.GetMouseButtonDown(0)) {
                // check if mouse over UI object.
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    if (!colliding && cmd == Cmd.Create) {
                        Create();
                    } else if (!colliding && cmd == Cmd.Destroy) {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }

        void Create() {
            Debug.Log("mouse create");
            var sprite = item switch {
                Item.Shape1 => TileSprites.shape1,
                Item.Shape2 => TileSprites.shape2,
                Item.Shape3 => TileSprites.shape3,
                Item.Shape4 => TileSprites.shape4,
                _ => null
            };

            if (sprite != null) {
                Debug.Log("create sprite: " + sprite.ToString());
                var obj = new GameObject(item.ToString());
                var spriteRenderer = obj.AddComponent<SpriteRenderer>();
                var tile = obj.AddComponent<Tile>();
                Debug.Log("mouse ceate: enum parse: item.ToString->" + item.ToString());
                var type = (Tile.Type)Enum.Parse(typeof(Tile.Type), item.ToString());

                Debug.Log("mouse create: layer index" + layerMask);
                obj.layer = 9;
                obj.transform.position = transform.position;
                spriteRenderer.sprite = sprite;
                tile.data.pos = obj.transform.position;
                tile.data.type = type;
            }
        }
    }
}
