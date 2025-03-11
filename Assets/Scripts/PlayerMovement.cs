using UnityEngine;
using UnityEngine.InputSystem;

namespace qiekn.core {
    public class PlayerMovement : MonoBehaviour {

        InputAction moveAction;
        [SerializeField] float speed = 5;

        void Start() {
            moveAction = InputSystem.actions.FindAction("Move");
        }

        void Update() {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();

            // move player
            transform.position += new Vector3(moveValue.x, moveValue.y, 0) * Time.deltaTime * speed;
        }
    }
}