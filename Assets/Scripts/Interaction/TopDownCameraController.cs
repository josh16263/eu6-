using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    /// <summary>
    /// Replaces the airplane controls with a simple top-down camera.
    /// The camera can pan with WASD/arrow keys and zoom with the mouse wheel.
    /// Attach this script to the main camera in the scene.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class TopDownCameraController : MonoBehaviour
    {
        [Tooltip("Movement speed when panning the camera across the map.")]
        [SerializeField] private float panSpeed = 10f;

        [Tooltip("Scaling applied to mouse wheel zoom input.")]
        [SerializeField] private float zoomSpeed = 200f;

        [Tooltip("Minimum camera height above the map.")]
        [SerializeField] private float minHeight = 10f;

        [Tooltip("Maximum camera height above the map.")]
        [SerializeField] private float maxHeight = 200f;

        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            // Force the camera to look straight down like a grand-strategy map.
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        private void Update()
        {
            Pan();
            Zoom();
        }

        // Pan horizontally using keyboard input.
        private void Pan()
        {
            Vector2 move = Vector2.zero;
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) move += Vector2.up;
                if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) move += Vector2.down;
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) move += Vector2.left;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move += Vector2.right;
            }

            Vector3 translation = new Vector3(move.x, 0f, move.y) * (panSpeed * Time.deltaTime);
            transform.Translate(translation, Space.World);
        }

        // Zoom in and out using the mouse scroll wheel.
        private void Zoom()
        {
            if (Mouse.current == null) return;
            float scroll = Mouse.current.scroll.ReadValue().y;
            if (Mathf.Approximately(scroll, 0f)) return;

            float height = Mathf.Clamp(transform.position.y - scroll * zoomSpeed * Time.deltaTime, minHeight, maxHeight);
            Vector3 pos = transform.position;
            pos.y = height;
            transform.position = pos;
        }
    }
}

