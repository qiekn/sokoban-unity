using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace qiekn.learn_editor {
    public class ManagerScript : MonoBehaviour {
        [HideInInspector]
        public bool saveLoadMenuOpen = false;

        public Animator itemUIAnimation;
        public Animator optionUIAnimation;
        public Animator saveUIAnimation;
        public Animator loadUIAnimation;
        public SpriteRenderer mouseObject;
        public MouseScript user;
        public InputField levelNameSave;
        public InputField levelNameLoad;
        public Text levelMessage;
        public Animator messageAnim;

        private bool itemPositionIn = true;
        private bool optionPositionIn = true;
        private bool saveLoadPositionIn = false;
        private LevelData levelData;
        LayerMask layerMask;

        void Awake() {
            TileSprites.LoadSprites();
        }


        void Start() {
            layerMask = LayerMask.GetMask("Editor");
            CreateEditor(); // create new instance of level.
        }

        LevelData CreateEditor() {
            levelData = gameObject.AddComponent<LevelData>();
            return levelData;
        }

        // selecting certain menus
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
            if (optionPositionIn == false) {
                optionUIAnimation.SetTrigger("OptionMenuIn"); // slide menu into screen
                optionPositionIn = true; // indicate menu in screen view.
            } else {
                optionUIAnimation.SetTrigger("OptionMenuOut"); // slide menu out of screen
                optionPositionIn = false; // indicate menu off screen
            }
        }

        public void ChooseSave() {
            Debug.Log("choose save");
            if (saveLoadPositionIn == false) {
                saveUIAnimation.SetTrigger("SaveLoadIn"); // slide menu into screen
                saveLoadPositionIn = true; // indicate menu on screen
                saveLoadMenuOpen = true; // indicate save menu open to prevent camera movement
            } else {
                saveUIAnimation.SetTrigger("SaveLoadOut"); // slide menu off screen
                saveLoadPositionIn = false; // indicate menu off screen
                saveLoadMenuOpen = false; // indicate save menu off screen, allow camera movement
            }
        }

        public void ChooseLoad() {
            Debug.Log("choose load");
            if (saveLoadPositionIn == false) {
                loadUIAnimation.SetTrigger("SaveLoadIn"); // slide menu into screen
                saveLoadPositionIn = true; // indicate menu on screen
                saveLoadMenuOpen = true; // indicate load menu open, prevent camera movement.
            } else {
                loadUIAnimation.SetTrigger("SaveLoadOut"); // slide menu off screen
                saveLoadPositionIn = false; // indicate menu off screen
                saveLoadMenuOpen = false; // indicate load menu off screen, allow camera movement.
            }
        }


        // choosing an object
        public void ChooseShape1() {
            user.item = MouseScript.Item.Shape1; // set object to place as cylinder
            mouseObject.sprite = TileSprites.shape1;
        }

        public void ChooseShape2() {
            user.item = MouseScript.Item.Shape2;
            mouseObject.sprite = TileSprites.shape2;
        }

        public void ChooseShape3() {
            user.item = MouseScript.Item.Shape3;
            mouseObject.sprite = TileSprites.shape3;
        }

        public void ChooseShape4() {
            user.item = MouseScript.Item.Shape4;
            mouseObject.sprite = TileSprites.shape4;
        }


        // choosing a command
        public void ChooseCreate() {
            user.cmd = MouseScript.Cmd.Create; // set mode to create
            user.meshRenderer.enabled = true; // show mouse object mesh
        }

        public void ChooseDestroy() {
            user.cmd = MouseScript.Cmd.Destroy; // set mode to destroy
            user.meshRenderer.enabled = false; // hide mouse mesh
        }

        // save a level
        public void SaveLevel() {
            // gather all objects with Tile component
            Tile[] tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
            foreach (var t in tiles)
                levelData.Tiles.Add(t.data);

            string json = JsonUtility.ToJson(levelData); // write the level data to json
            string folder = Application.dataPath + "/LevelData/"; // create a folder

            // file name
            string fileName;
            if (levelNameSave.text == "")
                fileName = "new_level.json";
            else
                fileName = levelNameSave.text + ".json";

            // create if not exist
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, fileName); // set filepath

            // overwrite file with same name, if applicable
            if (File.Exists(path))
                File.Delete(path);

            // create and save file
            File.WriteAllText(path, json);

            // remove save menu
            saveUIAnimation.SetTrigger("SaveLoadOut");
            saveLoadPositionIn = false;
            saveLoadMenuOpen = false;
            levelNameSave.text = ""; // clear input field
            levelNameSave.DeactivateInputField(); // remove focus from input field.

            // display message
            levelMessage.text = fileName + " saved to LevelData folder.";
            messageAnim.Play("MessageFade", 0, 0);
        }

        // load a level
        public void LoadLevel() {
            string folder = Application.dataPath + "/LevelData/";
            string levelFile;

            //set a default file name if no name given
            if (levelNameLoad.text == "")
                levelFile = "new_level.json";
            else
                levelFile = levelNameLoad.text + ".json";

            string path = Path.Combine(folder, levelFile); // set filepath

            if (File.Exists(path)) // if the file could be found in LevelData
            {
                // The objects currently in the level will be deleted
                Tile[] foundObjects = FindObjectsByType<Tile>(FindObjectsSortMode.None);
                foreach (Tile obj in foundObjects)
                    Destroy(obj.gameObject);

                string json = File.ReadAllText(path); // provide text from json file
                levelData = JsonUtility.FromJson<LevelData>(json); // level information filled from json file
                CreateFromFile(); // create objects from level data.
            } else // if file could not be found.
              {
                loadUIAnimation.SetTrigger("SaveLoadOut"); // remove menu
                saveLoadPositionIn = false; // indicate menu not on screen
                saveLoadMenuOpen = false; // indicate camera can move.
                levelMessage.text = levelFile + " could not be found!"; // send message
                messageAnim.Play("MessageFade", 0, 0);
                levelNameLoad.DeactivateInputField(); // remove focus from input field
            }
        }

        // create objects based on data within level.
        void CreateFromFile() {
            for (int i = 0; i < levelData.Tiles.Count; i++) {


                /*───────────────────────────┐
                │ create use for/switch loop │
                └────────────────────────────*/
                /*
                 * sprite
                 * obj -> tile -> data
                 */

                var tileData = levelData.Tiles[i];

                var sprite = tileData.type switch {
                    Tile.Type.Shape1 => TileSprites.shape1,
                    Tile.Type.Shape2 => TileSprites.shape2,
                    Tile.Type.Shape3 => TileSprites.shape3,
                    Tile.Type.Shape4 => TileSprites.shape4,
                    _ => null
                };

                /* TODO: use switch <2025-03-15 12:09, @qiekn> */
                // shape 1
                if (levelData.Tiles[i].type == Tile.Type.Shape1) {
                    var obj = new GameObject(tileData.type.ToString());
                    var spriteRenderer = obj.AddComponent<SpriteRenderer>();
                    var tile = obj.AddComponent<Tile>();

                    obj.transform.position = tile.data.pos;
                    obj.layer = layerMask;
                    spriteRenderer.sprite = sprite;
                    tile.data.pos = obj.transform.position;
                    tile.data.type = tileData.type;
                }
            }

            //Clear level box
            levelNameLoad.text = "";
            levelNameLoad.DeactivateInputField(); // remove focus from input field

            loadUIAnimation.SetTrigger("SaveLoadOut"); // slide load menu off screen
            saveLoadPositionIn = false; // indicate load menu off screen
            saveLoadMenuOpen = false; // allow camera movement.

            //Display message
            levelMessage.text = "Level loading...done.";
            messageAnim.Play("MessageFade", 0, 0);
        }
    }
}

