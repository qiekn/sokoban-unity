using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace qiekn.learn_editor {
    public class MouseScript : MonoBehaviour {
        public enum Cmd { Create, Destroy };
        public enum Item { Wall, Box, Player, Point };

        [SerializeField] float limit = 30f;

        [HideInInspector]
        public Item item = Item.Wall; // default item

        [HideInInspector]
        public Cmd cmd = Cmd.Create; // default commnad

        [HideInInspector]
        public SpriteRenderer spriteRenderer;

        [SerializeField] ManagerScript ms;

        [SerializeField] bool colliding;
        LayerMask layerMask;
        Camera cam;

        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            layerMask = LayerMask.GetMask("Editor");
            cam = Camera.main;
        }

        void Update() {
            // Have the object follow the mouse cursor by getting
            // mouse coordinates and converting them to world point.
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // limit object movement
            transform.position = new Vector3(
                Mathf.Clamp(mousePos.x, -limit, limit),
                Mathf.Clamp(mousePos.y, -limit, limit));

            // send out raycast to detect objects
            var hit = Physics2D.OverlapPoint(mousePos, layerMask);
            if (hit != null) {
                colliding = true;
            } else {
                colliding = false;
            }

            // after pressing the left mouse button...
            if (Input.GetMouseButtonDown(0)) {
                // check if mouse over UI object.
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    if (!colliding && cmd == Cmd.Create) {
                        Create();
                    } else if (colliding && cmd == Cmd.Destroy) {
                        Destroy(hit.gameObject);
                    }
                }
            }
        }

        void Create() {
            var sprite = item switch {
                Item.Wall => TileSprites.wall,
                Item.Box => TileSprites.box,
                Item.Player => TileSprites.player,
                Item.Point => TileSprites.point,
                _ => null
            };

            if (sprite != null) {
                Debug.Log("create sprite: " + sprite.ToString());

                var obj = new GameObject(item.ToString()) { layer = 9 };
                obj.transform.position = transform.position;

                // collider
                var collider = obj.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(0.32f, 0.32f);

                // sprite
                var spriteRenderer = obj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;

                // meta data
                var tile = obj.AddComponent<Tile>();
                tile.data.pos = obj.transform.position;
                tile.data.type = (Tile.Type)Enum.Parse(typeof(Tile.Type), item.ToString());
            }
        }
    }
}
