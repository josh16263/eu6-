using UnityEngine;

namespace Map
{
    /// <summary>
    /// Represents a settlement or province on the map. This component
    /// stores a reference to the renderer so that the settlement can be
    /// highlighted when selected.
    /// </summary>
    [DisallowMultipleComponent]
    public class Settlement : MonoBehaviour
    {
        [Tooltip("Renderer used to tint the settlement when selected.")]
        [SerializeField] private Renderer targetRenderer;

        private Color originalColor;

        private void Awake()
        {
            if (targetRenderer == null)
            {
                targetRenderer = GetComponentInChildren<Renderer>();
            }

            if (targetRenderer != null)
            {
                originalColor = targetRenderer.material.color;
            }
        }

        /// <summary>
        /// Highlights or clears the highlight on the settlement.
        /// </summary>
        /// <param name="highlight">True to enable highlight, false to disable.</param>
        public void SetHighlight(bool highlight)
        {
            if (targetRenderer == null) return;

            targetRenderer.material.color = highlight ? Color.yellow : originalColor;
        }

        /// <summary>
        /// Display name used by UI elements.
        /// </summary>
        public string DisplayName => gameObject.name;
    }
}
