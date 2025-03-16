using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace qiekn.learn_editor {
    public class ManagerScript : MonoBehaviour {
        [HideInInspector]
        public bool saveLoadMenuOpen = false;

        [SerializeField] EditorMenu itemMenu;
        [SerializeField] EditorMenu cmdMenu;

        public Animator itemUIAnimation;
        public Animator cmdUIAnimation;
        public Animator saveUIAnimation;
        public Animator loadUIAnimation;

        public MouseScript mouse;

        private bool itemPositionIn = true;
        private bool cmdPositionIn = true;
        private LevelData levelData;

        void Start() {
            TileSprites.LoadSprites();
            CreateEditor(); // create new instance of level.
        }

        LevelData CreateEditor() {
            levelData = gameObject.AddComponent<LevelData>();
            return levelData;
        }

        /*────────────┐
        │ toggle menu │
        └─────────────*/

        public void SlideItemMenu() {
            if (itemPositionIn == false) {
                itemUIAnimation.SetTrigger("ItemMenuIn"); // slide menu into screen
                itemPositionIn = true; // indicate menu in screen view.
            } else {
                itemUIAnimation.SetTrigger("ItemMenuOut"); // slide menu out of screen
                itemPositionIn = false; // indicate menu off screen
            }
        }

        public void SlideOptionMenu() {
            if (cmdPositionIn == false) {
                cmdUIAnimation.SetTrigger("OptionMenuIn"); // slide menu into screen
                cmdPositionIn = true; // indicate menu in screen view.
            } else {
                cmdUIAnimation.SetTrigger("OptionMenuOut"); // slide menu out of screen
                cmdPositionIn = false; // indicate menu off screen
            }
        }


        /*──────────────┐
        │ choose object │
        └───────────────*/

        public void ChooseWall() {
            mouse.item = MouseScript.Item.Wall; // set object to place as cylinder
            mouse.spriteRenderer.sprite = TileSprites.wall;
        }

        public void ChooseBox() {
            mouse.item = MouseScript.Item.Box;
            mouse.spriteRenderer.sprite = TileSprites.box;
        }

        public void ChoosePlayer() {
            mouse.item = MouseScript.Item.Player;
            mouse.spriteRenderer.sprite = TileSprites.player;
        }

        public void ChoosePoint() {
            mouse.item = MouseScript.Item.Point;
            mouse.spriteRenderer.sprite = TileSprites.point;
        }

        /*───────────────┐
        │ choose command │
        └────────────────*/

        public void ChooseCreate() {
            mouse.cmd = MouseScript.Cmd.Create;
            mouse.spriteRenderer.enabled = true; // show mouse object mesh
        }

        public void ChooseDestroy() {
            mouse.cmd = MouseScript.Cmd.Destroy;
            mouse.spriteRenderer.enabled = false; // hide mouse mesh
        }

        public void ChooseSave() {
            Debug.Log("choose save");
        }

        public void ChooseLoad() {
            Debug.Log("choose load");
        }

        /* TODO: Savelevel <2025-03-16 16:15, @qiekn> */
        public void SaveLevel() { }

        // load a level
        /* TODO: LoadLevel <2025-03-16 16:15, @qiekn> */
        public void LoadLevel() { }

        // create objects based on data within level.
        void CreateFromFile() { }
    }
}

